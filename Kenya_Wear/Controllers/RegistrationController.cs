using Kenya_Wear.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kenya_Wear.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private DBHandler dbhandler;

        public RegistrationController(IWebHostEnvironment hostingEnvironment, DBHandler mydbhandler)
        {
            _hostingEnvironment = hostingEnvironment;
            dbhandler = mydbhandler;
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
