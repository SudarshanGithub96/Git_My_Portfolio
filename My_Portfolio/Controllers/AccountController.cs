using Microsoft.AspNetCore.Mvc;

namespace My_Portfolio.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login_Page()
        {
            return View();
        }
    }
}
