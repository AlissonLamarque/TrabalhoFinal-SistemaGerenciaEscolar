using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PF_GerenciaEscolar.Data;
using PF_GerenciaEscolar.Data.Enum;
using PF_GerenciaEscolar.Interfaces;
using PF_GerenciaEscolar.Models;
using PF_GerenciaEscolar.Repositorio;
using PF_GerenciaEscolar.ViewModels;
using static PF_GerenciaEscolar.ViewModels.NotaAvaliacaoViewModel;

namespace PF_GerenciaEscolar.Controllers
{
    [Authorize(Roles = "Professor")]
    public class ProfessorController : Controller
    {
        private readonly PF_GerenciaEscolarDbContext _contexto;
        private readonly IProfessorRepositorio _professorRepositorio;
        private readonly IAvaliacaoRepositorio _avaliacaoRepositorio;
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly INotaRepositorio _notaRepositorio;

        public ProfessorController(PF_GerenciaEscolarDbContext contexto,
            IProfessorRepositorio professorRepositorio,
            IAvaliacaoRepositorio avaliacaoRepositorio,
            IAlunoRepositorio alunoRepositorio,
            INotaRepositorio notaRepositorio)
        {
            _contexto = contexto;
            _professorRepositorio = professorRepositorio;
            _avaliacaoRepositorio = avaliacaoRepositorio;
            _alunoRepositorio = alunoRepositorio;
            _notaRepositorio = notaRepositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        // TURMA
        public IActionResult Turmas()
        {
            return View();
        }

        public IActionResult DetalhesTurma(Turma turma)
        {
            var detalhes = _contexto.Alunos.Where(aluno => aluno.Turma == turma).ToList();
            ViewBag.CodigoTurma = turma;
            return View(detalhes);
        }

        public async Task<IActionResult> DetalhesAluno(int id)
        {
            Aluno aluno = await _alunoRepositorio.GetByIdAsync(id);
            return View(aluno);
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

        public IActionResult CadastrarAvaliacao()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarAvaliacao(CreateAvaliacaoViewModel AvaliacaoVM)
        {
            if (!ModelState.IsValid)
            {
                return View(AvaliacaoVM);
            }

            DateTime Inicio = DateTime.Today;

            var avaliacao = new Avaliacao
            {
                Titulo = AvaliacaoVM.Titulo,
                Valor = AvaliacaoVM.Valor,
                Inicio = Inicio,
                Prazo = AvaliacaoVM.Prazo,
                Disciplina = AvaliacaoVM.Disciplina,
                Turma = AvaliacaoVM.Turma
            };

            _avaliacaoRepositorio.Adicionar(avaliacao);
            return RedirectToAction("Avaliacoes");
        }

        public async Task<IActionResult> RemoverAvaliacao(int id)
        {
            var DetalhesAvaliacao = await _avaliacaoRepositorio.GetByIdAsync(id);
            if (DetalhesAvaliacao == null) return View("Error");
            return View(DetalhesAvaliacao);
        }

        [HttpPost]
        public async Task<IActionResult> RemoverAv(int id)
        {
            var DetalhesAvaliacao = await _avaliacaoRepositorio.GetByIdAsync(id);
            if (DetalhesAvaliacao == null) return View("Error");

            _avaliacaoRepositorio.Remover(DetalhesAvaliacao);
            return RedirectToAction("Avaliacoes");
        }

        // NOTAS
        public async Task<IActionResult> NotasAvaliacao(int id)
        {
            var avaliacao = await _avaliacaoRepositorio.GetByIdWithNotasAsync(id);

            if (avaliacao == null) return View("Error");

            var notasAlunosViewModel = new List<NotaAlunoViewModel>();

            foreach (var nota in avaliacao.Notas)
            {
                var aluno = await _alunoRepositorio.GetByIdAsync(nota.AlunoId.Value);

                notasAlunosViewModel.Add(new NotaAlunoViewModel
                {
                    Nota = nota,
                    Aluno = aluno
                });
            }

            var viewModel = new AvaliacaoViewModel
            {
                Avaliacao = avaliacao,
                NotasAlunos = notasAlunosViewModel
            };

            return View(viewModel);
        }


        public async Task<IActionResult> LancarNota(int id)
        {
            var NotasAvaliacao = await _notaRepositorio.GetByIdAsync(id);
            if (NotasAvaliacao == null) return View("Error");
            var LancarNotaVM = new LancarNotaViewModel
            {
                Id = NotasAvaliacao.Id,
                Valor = NotasAvaliacao.Valor,
                AlunoId = NotasAvaliacao.AlunoId,
                AvaliacaoId = NotasAvaliacao.AvaliacaoId
            };
            return View(LancarNotaVM);
        }

        
        [HttpPost]
        public IActionResult LancarNota(int id, LancarNotaViewModel LancarNotaVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Falha ao lançar nota");
                return View(LancarNotaVM);
            }

            var Nota = new Nota
            {
                Id = LancarNotaVM.Id,
                Valor = LancarNotaVM.Valor,
                AlunoId = LancarNotaVM.AlunoId,
                AvaliacaoId = LancarNotaVM.AvaliacaoId
            };

            _contexto.Entry(Nota).State = EntityState.Modified;

            _notaRepositorio.Atualizar(Nota);
            return RedirectToAction("Avaliacoes");
        }
    }
}
