using Microsoft.AspNetCore.Mvc;
using My_Portfolio.Models;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using My_Portfolio.Interface;
using System.Text.RegularExpressions;

namespace My_Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        private IEmail _ObjEmail;
        private IEmployees _ObjEmployees;


        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration, IEmail objEmail, IEmployees ObjEmployees)
        {
            this._logger = logger;
            this._ObjEmail = objEmail;
            this._ObjEmployees = ObjEmployees;
            this._httpClientFactory = httpClientFactory;
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
                    EmailModel emailmodel = new EmailModel
                    {
                        E_Name = name,
                        E_Email = email,
                        E_Phone = phone,
                        E_Message = message
                    };

                    var fromAddress = new MailAddress("noreply@kreatecloud.com", "contactus@kreatenergy.ae");
                    dynamic password = "Foq97150";
                    var toAddress = new MailAddress("ritika.sharma@kreatetechnologies.com", "Contact Us - Kreate Energy FZE");

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
                        this._ObjEmail.SaveEmailData(emailmodel);
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




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
