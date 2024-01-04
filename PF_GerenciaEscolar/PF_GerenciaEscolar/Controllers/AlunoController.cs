using Microsoft.AspNetCore.Mvc;

namespace PF_GerenciaEscolar.Controllers
{
    public class AlunoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
