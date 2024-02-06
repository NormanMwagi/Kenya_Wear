using Microsoft.AspNetCore.Mvc;

namespace Kenya_Wear.Controllers
{
	public class GenderController : Controller
	{
		public IActionResult Men()
		{
			return View();
		}
		public IActionResult Women()
		{
			return View();
		}
	}
}
