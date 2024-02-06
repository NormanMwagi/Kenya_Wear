using Microsoft.AspNetCore.Mvc;

namespace Kenya_Wear.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult SingleItem()
        {
            return View();
        }

        public IActionResult Electronics()
        {
            return View(); 
        }
		public IActionResult Codes()
		{
			return View();
		}
		public IActionResult Contact()
		{
			return View();
		}
		public IActionResult Cart()
		{
			return View();
		}

	}
}
