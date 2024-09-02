using Microsoft.AspNetCore.Mvc;
using My_Portfolio.Models;
using System.Diagnostics;

namespace My_Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Home ";
            return View();
        }
        [HttpPost]
        public IActionResult DownloadCV()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", "YourCV.pdf"); // Adjust the path and filename as needed
            var fileName = "Sudarshan_Sharma_CV.pdf"; // The name the file will have when downloaded

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(); // Handle the case when the file doesn't exist
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/pdf", fileName);
        }

        public IActionResult Privacy()
        {
            ViewBag.Title = "Privacy";
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.Title = "Login";
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Username, string Password)
        {

            ViewBag.Title = "Login";
            return View();
        }

       


        [HttpGet]
        public IActionResult SignUp()
        {
            ViewBag.Title = "SignUp";
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(string Username, string Email, long Mobile, string Password, string ConfirmPassword)
        {
            ViewBag.Title = "SignUp";
            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Title = "Contact";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
