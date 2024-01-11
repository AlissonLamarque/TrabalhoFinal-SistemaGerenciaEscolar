using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PF_GerenciaEscolar.Data;
using PF_GerenciaEscolar.Interfaces;
using PF_GerenciaEscolar.Models;
using PF_GerenciaEscolar.Repositorio;
using PF_GerenciaEscolar.ViewModels;
using static PF_GerenciaEscolar.ViewModels.NotaDisciplinaViewModel;

namespace PF_GerenciaEscolar.Controllers
{
    [Authorize(Roles = "Aluno")]
    public class AlunoController : Controller
    {
        private readonly PF_GerenciaEscolarDbContext _contexto;
        private readonly IAvaliacaoRepositorio _avaliacaoRepositorio;
        private readonly INotaRepositorio _notaRepositorio;
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly UserManager<IdentityUser> _userManager;

        public AlunoController(PF_GerenciaEscolarDbContext contexto, 
            IAvaliacaoRepositorio avaliacaoRepositorio,
            INotaRepositorio notaRepositorio,
            IAlunoRepositorio alunoRepositorio,
            UserManager<IdentityUser> userManager)
        {
            _contexto = contexto;
            _avaliacaoRepositorio = avaliacaoRepositorio;
            _notaRepositorio = notaRepositorio;
            _alunoRepositorio = alunoRepositorio;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        // NOTA
        public async Task<IActionResult> VisualizarNotas(int id)
        {
            var aluno = await _alunoRepositorio.GetByIdAsync(id);

            if (aluno == null) return View("Error");

            var NotasAluno = new List<NotaPorDisciplinaViewModel>();

            foreach(var nota in aluno.Notas)
            {
                var avaliacao = await _avaliacaoRepositorio.GetByIdAsync(nota.AvaliacaoId.Value);

                NotasAluno.Add(new NotaPorDisciplinaViewModel
                {
                    Nota = nota,
                    Disciplina = avaliacao.Disciplina,
                });
            }

            var viewModel = new AlunoViewModel
            {
                Aluno = aluno,
                Notas = NotasAluno
            };

            return View(viewModel);
        }

        // AVALIAÇÃO
        public IActionResult Avaliacoes()
        {
            var avaliacoes = _contexto.Avaliacoes.ToList();
            return View(avaliacoes);
        }

        public IActionResult DetalhesAvaliacao(int id)
        {
            var detalhes = _contexto.Avaliacoes.Where(avaliacao => avaliacao.Id == id).ToList();
            return View(detalhes);
        }

        public async Task<IActionResult> EnviarAvaliacao(int id)
        {
            var DetalhesAvaliacao = await _avaliacaoRepositorio.GetByIdAsync(id);
            if (DetalhesAvaliacao == null) return View("Error");
            return View(DetalhesAvaliacao);
        }

        [HttpPost]
        public async Task<IActionResult> EnviarAvaliacao(CreateNotaViewModel avaliacaoVM)
        {
            if (!ModelState.IsValid)
            {
                return View(avaliacaoVM);
            }

            var usuarioLogado = await _userManager.GetUserAsync(User);

            var Nota = new Nota
            {
                Valor = avaliacaoVM.Valor,
                AlunoId = int.Parse(usuarioLogado.Id),
                AvaliacaoId = avaliacaoVM.AvaliacaoId
            };

            _notaRepositorio.Adicionar(Nota);
            return RedirectToAction("Avaliacoes");
        }

    }
}
