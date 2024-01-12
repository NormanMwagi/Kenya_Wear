using Microsoft.AspNetCore.Mvc;

namespace Kenya_Wear.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
