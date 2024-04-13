using Microsoft.AspNetCore.Mvc;

namespace MetReciclaWebFinalOK.Controllers
{
    public class PaginaPrincipalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
