using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    [Authorize(Roles = "Admin")]
    public class AdministradorController : Controller
    {
        private readonly IProfessorRepositorio _professorRepositorio;
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly PF_GerenciaEscolarDbContext _contexto;

        public AdministradorController(PF_GerenciaEscolarDbContext contexto, 
            IProfessorRepositorio professorRepositorio,
            IAlunoRepositorio alunoRepositorio, 
            UserManager<IdentityUser> userManager)
        {
            _contexto = contexto;
            _professorRepositorio = professorRepositorio;
            _alunoRepositorio = alunoRepositorio;
            _userManager = userManager;
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
        public async Task<IActionResult> CadastrarProfessor(CreateProfessorViewModel ProfessorVM)
        {
            if (!ModelState.IsValid)
            {
                return View(ProfessorVM);
            }

            var user = new ApplicationUser
            {
                Nome = ProfessorVM.User.Nome,
                Sobrenome = ProfessorVM.User.Sobrenome,
                CPF = ProfessorVM.User.CPF,
                Email = ProfessorVM.User.Email,
                PasswordHash = ProfessorVM.User.PasswordHash
            };

            var professor = new Professor
            {
                User = user,
                Disciplina = ProfessorVM.Disciplina
            };
 
            var result = await _userManager.CreateAsync(user, user.PasswordHash);

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            _professorRepositorio.Adicionar(professor);
            return RedirectToAction("Disciplinas");
        }

        public async Task<IActionResult> EditarProfessor(int id)
        {
            var professor = await _professorRepositorio.GetByIdAsync(id);

            if (professor == null) return View("Error");

            var user = new ApplicationUser
            {
                Nome = professor.User.Nome,
                Sobrenome = professor.User.Sobrenome,
                CPF = professor.User.CPF,
                Email = professor.User.Email,
                PasswordHash = professor.User.PasswordHash
            };

            var professorVM = new EditProfessorViewModel
            {
                Id = professor.Id,
                User = user,
                Disciplina = professor.Disciplina
            };

            return View(professorVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditarProfessor(int id, EditProfessorViewModel professorVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Falha ao editar Professor");
                return View(professorVM);
            }

            var user = await _contexto.Users.FirstOrDefaultAsync(u => u.Id == id.ToString());

            if (user == null)
            {
                return NotFound();
            }

            user.Nome = professorVM.User.Nome;
            user.Sobrenome = professorVM.User.Sobrenome;
            user.CPF = professorVM.User.CPF;
            user.Email = professorVM.User.Email;

            if (!string.IsNullOrEmpty(professorVM.User.PasswordHash))
            {
                user.PasswordHash = professorVM.User.PasswordHash;
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) return View("Error");

            var professor = new Professor
            {
                Id = professorVM.Id,
                UserId = user.Id,
                User = user,
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
        public async Task<IActionResult> CadastrarAluno(CreateAlunoViewModel AlunoVM)
        {
            if (!ModelState.IsValid)
            {
                return View(AlunoVM);
            }

            var user = new ApplicationUser
            {
                Nome = AlunoVM.User.Nome,
                Sobrenome = AlunoVM.User.Sobrenome,
                Email = AlunoVM.User.Email,
                CPF = AlunoVM.User.CPF,
                PasswordHash = AlunoVM.User.PasswordHash,
            };

            var aluno = new Aluno
            {
                User = user,
                Turma = AlunoVM.Turma
            };

            var result = await _userManager.CreateAsync(user, user.PasswordHash);

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

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
                User = new ApplicationUser
                {
                    Nome = aluno.User.Nome,
                    Sobrenome = aluno.User.Sobrenome,
                    Email = aluno.User.Email,
                    CPF = aluno.User.CPF,
                    PasswordHash = aluno.User.PasswordHash,
                },
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

            var user = new ApplicationUser
            {
                Nome = alunoVM.User.Nome,
                Sobrenome = alunoVM.User.Sobrenome,
                Email = alunoVM.User.Email,
                CPF = alunoVM.User.CPF,
                PasswordHash = alunoVM.User.PasswordHash,
            };

            var aluno = new Aluno
            {
                Id = alunoVM.Id,
                User = user,
                Turma = alunoVM.Turma
            };

            var result = _userManager.UpdateAsync(user);

            if (result.IsCanceled) return View("Error");

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
