using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PF_GerenciaEscolar.Data;
using PF_GerenciaEscolar.Data.Enum;
using PF_GerenciaEscolar.Interfaces;
using PF_GerenciaEscolar.Models;
using PF_GerenciaEscolar.Repositorio;
using PF_GerenciaEscolar.ViewModels;

namespace PF_GerenciaEscolar.Controllers
{
    public class AdministradorController : Controller
    {
        private readonly IAdministradorRepositorio _administradorRepositorio;
        private readonly IProfessorRepositorio _professorRepositorio;
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly PF_GerenciaEscolarDbContext _contexto;

        public AdministradorController(PF_GerenciaEscolarDbContext contexto, 
            IAdministradorRepositorio administradorRepositorio, 
            IProfessorRepositorio professorRepositorio,
            IAlunoRepositorio alunoRepositorio)
        {
            _contexto = contexto;
            _administradorRepositorio = administradorRepositorio;
            _professorRepositorio = professorRepositorio;
            _alunoRepositorio = alunoRepositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        // TURMA
        public ActionResult Turmas()
        {
            return View();
        }

        public IActionResult DetalhesTurma(Turma turma)
        {
            var detalhes = _contexto.Alunos.Where(aluno => aluno.Turma == turma).ToList();
            ViewBag.CodigoTurma = turma;
            return View(detalhes);
        }

        // DISCIPLINA
        public IActionResult Disciplinas()
        {
            return View();
        }

        public IActionResult DetalhesDisciplina(Disciplina disciplina)
        {
            var detalhes = _contexto.Professores.Where(professor => professor.Disciplina == disciplina).ToList();
            ViewBag.Disciplina = disciplina;
            return View(detalhes);
        }


        // PROFESSOR
        public async Task<IActionResult> DetalhesProfessor(int id)
        {
            Professor professor = await _professorRepositorio.GetByIdAsync(id);
            return View(professor);
        }

        public IActionResult CadastrarProfessor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarProfessor(CreateProfessorViewModel ProfessorVM)
        {
            if (!ModelState.IsValid)
            {
                return View(ProfessorVM);
            }

            var professor = new Professor
            {
                Nome = ProfessorVM.Nome,
                Email = ProfessorVM.Email,
                Autenticacao = new Autenticacao
                {
                    Cpf = ProfessorVM.Autenticacao.Cpf,
                    Senha = ProfessorVM.Autenticacao.Senha,
                    Cargo = Cargo.Professor
                },
                Disciplina = ProfessorVM.Disciplina
            };

            _professorRepositorio.Adicionar(professor);
            return RedirectToAction("Disciplinas");
        }

        public async Task<IActionResult> EditarProfessor(int id)
        {
            var professor = await _professorRepositorio.GetByIdAsync(id);
            if(professor == null) return View("Error");
            var professorVM = new EditProfessorViewModel
            {
                Id = professor.Id,
                Nome = professor.Nome,
                Email = professor.Email,
                AutenticacaoId = professor.AutenticacaoId,
                Autenticacao = professor.Autenticacao,
                Disciplina = professor.Disciplina
            };
            return View(professorVM);

        }

        [HttpPost]
        public IActionResult EditarProfessor(int id, EditProfessorViewModel professorVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Falha ao editar Professor");
                return View(professorVM);
            }

            var professor = new Professor
            {
                Id = professorVM.Id,
                Nome = professorVM.Nome,
                Email = professorVM.Email,
                AutenticacaoId = professorVM.AutenticacaoId,
                Autenticacao = professorVM.Autenticacao,
                Disciplina = professorVM.Disciplina
            };

            _contexto.Entry(professor).State = EntityState.Modified;

            _professorRepositorio.Atualizar(professor);
            return RedirectToAction("Disciplinas");
        }

        public async Task<IActionResult> RemoverProfessor(int id)
        {
            var DetalhesProfessor = await _professorRepositorio.GetByIdAsync(id);
            if (DetalhesProfessor == null) return View("Error");
            return View(DetalhesProfessor);
        }

        [HttpPost]
        public async Task<IActionResult> RemoverProf(int id)
        {
            var DetalhesProfessor = await _professorRepositorio.GetByIdAsync(id);
            if (DetalhesProfessor == null) return View("Error");

            _professorRepositorio.Remover(DetalhesProfessor);
            return RedirectToAction("Disciplinas");
        }

        // ALUNO

        public async Task<IActionResult> DetalhesAluno(int id)
        {
            Aluno aluno = await _alunoRepositorio.GetByIdAsync(id);
            return View(aluno);
        }

        public IActionResult CadastrarAluno()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarAluno(CreateAlunoViewModel AlunoVM)
        {
            if (!ModelState.IsValid)
            {
                return View(AlunoVM);
            }

            var aluno = new Aluno
            {
                Nome = AlunoVM.Nome,
                Email = AlunoVM.Email,
                Autenticacao = new Autenticacao
                {
                    Cpf = AlunoVM.Autenticacao.Cpf,
                    Senha = AlunoVM.Autenticacao.Senha,
                    Cargo = Cargo.Aluno
                },
                Turma = AlunoVM.Turma
            };

            _alunoRepositorio.Adicionar(aluno);
            return RedirectToAction("Turmas");
        }

        public async Task<IActionResult> EditarAluno(int id)
        {
            var aluno = await _alunoRepositorio.GetByIdAsync(id);
            if (aluno == null) return View("Error");
            var alunoVM = new EditAlunoViewModel
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Email = aluno.Email,
                AutenticacaoId = aluno.AutenticacaoId,
                Autenticacao = aluno.Autenticacao,
                Turma = aluno.Turma
            };
            return View(alunoVM);
        }

        [HttpPost]
        public IActionResult EditarAluno(int id, EditAlunoViewModel alunoVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Falha ao editar Aluno");
                return View(alunoVM);
            }

            var aluno = new Aluno
            {
                Id = alunoVM.Id,
                Nome = alunoVM.Nome,
                Email = alunoVM.Email,
                AutenticacaoId = alunoVM.AutenticacaoId,
                Autenticacao = alunoVM.Autenticacao,
                Turma = alunoVM.Turma
            };

            _contexto.Entry(aluno).State = EntityState.Modified;

            _alunoRepositorio.Atualizar(aluno);
            return RedirectToAction("Turmas");
        }

        public async Task<IActionResult> RemoverAluno(int id)
        {
            var DetalhesAluno = await _alunoRepositorio.GetByIdAsync(id);
            if (DetalhesAluno == null) return View("Error");
            return View(DetalhesAluno);
        }

        [HttpPost]
        public async Task<IActionResult> RemoverAlu(int id)
        {
            var DetalhesAluno = await _alunoRepositorio.GetByIdAsync(id);
            if (DetalhesAluno == null) return View("Error");

            _alunoRepositorio.Remover(DetalhesAluno);
            return RedirectToAction("Turmas");
        }
    }
}
