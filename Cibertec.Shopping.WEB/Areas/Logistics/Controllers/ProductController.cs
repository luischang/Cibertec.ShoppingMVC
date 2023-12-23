using Microsoft.AspNetCore.Mvc;

namespace Cibertec.Shopping.WEB.Areas.Logistics.Controllers
{
    [Area(nameof(Logistics))]
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List() {
            return PartialView();
        }
    }
}
