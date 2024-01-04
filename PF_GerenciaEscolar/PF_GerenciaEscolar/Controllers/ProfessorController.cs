using Microsoft.AspNetCore.Mvc;
using PF_GerenciaEscolar.Data;
using PF_GerenciaEscolar.Data.Enum;
using PF_GerenciaEscolar.Interfaces;
using PF_GerenciaEscolar.Models;
using PF_GerenciaEscolar.Repositorio;
using PF_GerenciaEscolar.ViewModels;

namespace PF_GerenciaEscolar.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly PF_GerenciaEscolarDbContext _contexto;
        private readonly IProfessorRepositorio _professorRepositorio;
        private readonly IAvaliacaoRepositorio _avaliacaoRepositorio;
        private readonly IAlunoRepositorio _alunoRepositorio;

        public ProfessorController(PF_GerenciaEscolarDbContext contexto,
            IProfessorRepositorio professorRepositorio,
            IAvaliacaoRepositorio avaliacaoRepositorio,
            IAlunoRepositorio alunoRepositorio)
        {
            _contexto = contexto;
            _professorRepositorio = professorRepositorio;
            _avaliacaoRepositorio = avaliacaoRepositorio;
            _alunoRepositorio = alunoRepositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        // TURMA
        public async Task<IActionResult> Turmas()
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
        public async Task<IActionResult> Avaliacoes()
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
        public async Task<IActionResult> CadastrarAvaliacao(CreateAvaliacaoViewModel AvaliacaoVM)
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
    }
}
