using Microsoft.AspNetCore.Mvc;
using PF_GerenciaEscolar.Data;
using PF_GerenciaEscolar.Interfaces;
using PF_GerenciaEscolar.Models;
using PF_GerenciaEscolar.Repositorio;
using PF_GerenciaEscolar.ViewModels;

namespace PF_GerenciaEscolar.Controllers
{
    public class AlunoController : Controller
    {
        private readonly PF_GerenciaEscolarDbContext _contexto;
        private readonly IAvaliacaoRepositorio _avaliacaoRepositorio;
        private readonly INotaRepositorio _notaRepositorio;

        public AlunoController(PF_GerenciaEscolarDbContext contexto, 
            IAvaliacaoRepositorio avaliacaoRepositorio,
            INotaRepositorio notaRepositorio)
        {
            _contexto = contexto;
            _avaliacaoRepositorio = avaliacaoRepositorio;
            _notaRepositorio = notaRepositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        // NOTA
        public IActionResult VisualizarNotas()
        {
            return View();
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
        public async Task<IActionResult> EnviarAvaliacao(CreateNotaViewModel avaliacaoVM) //ARRUMAR
        {
            if (!ModelState.IsValid)
            {
                return View(avaliacaoVM);
            }

            var Nota = new Nota
            {
                Valor = 0,
                AlunoId = 1,
                AvaliacaoId = 1
            };

            _notaRepositorio.Adicionar(Nota);
            return RedirectToAction("Avaliacoes");
        }

    }
}
