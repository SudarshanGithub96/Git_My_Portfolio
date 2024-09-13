using Microsoft.AspNetCore.Mvc;
using My_Portfolio.Models;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;

namespace My_Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EmployeeDataAccessLayer dal;

        public HomeController(ILogger<HomeController> logger)
        {
            dal = new EmployeeDataAccessLayer();
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Home";
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = "About";
            return View();
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


        [HttpPost]
        public IActionResult MailSetup(string name, string email, string phone, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var fromAddress = new MailAddress("sudarshan.sharma@kreatetechnologies.com", "Name");
                    var password = Environment.GetEnvironmentVariable("SMTP_PASSWORD") ?? "Kreate@9870"; // Use a secure method to store passwords
                    var toAddress = new MailAddress("surajrwt0056@gmail.com", "Contact Us - Kreate Energy FZE");

                    var smtpClient = new SmtpClient
                    {
                        Host = "smtp.office365.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, password)
                    };


                    string emailBody = $@"
                Dear Team,<br><br> I hope this email finds you well.<br>
                <br><strong>Client or enquiry Details:</strong>
                <br><strong>Name:</strong> {name}
                <br><strong>Contact:</strong> {phone}
                <br><strong>Email:</strong> {email}
                <br><br><strong>Message:</strong> {message}
                <br><br>This is a website contact form reference email.";

                    using (var mailMessage = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = "Website Query: " + name,
                        Body = emailBody,
                        IsBodyHtml = true,
                    })
                    {
                        smtpClient.Send(mailMessage);
                    }
                    return Json(new { success = true });
                }
                return Json(new { success = false, error = "Invalid model state." });
            }
            catch (Exception ex)
            {
                // Log the exception
                return Json(new { success = false, error = ex.Message });
            }
        }



        public IActionResult Digital_Clock()
        {
            ViewBag.Title = "Digital_Clock";
            return View();
        }

        public EmployeeDataAccessLayer GetDal()
        {
            return dal;
        }

        public IActionResult GetEmployee()
        {
            ViewBag.Title = "GetEmployee";
            List<Employees> em = dal.GetEmployees();
            return View(em);
        }

























        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
