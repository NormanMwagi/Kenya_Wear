using Microsoft.AspNetCore.Mvc;

namespace Kenya_Wear.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Product()
        {
            return View();
        }
        public IActionResult Shop()
        {
            return View();
        }
    }
}
