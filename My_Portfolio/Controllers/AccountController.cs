using Microsoft.AspNetCore.Mvc;
using My_Portfolio.Interface;
using My_Portfolio.Models;

namespace My_Portfolio.Controllers
{
    public class AccountController : Controller
    {
        private IEmployees _ObjEmployees;
        public AccountController(IEmployees objEmployees)
        {
            this._ObjEmployees = objEmployees;
        }



        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.Title = "Login";
            return View();
        }

        [HttpPost]
        public IActionResult Login(Employees employees)
        {
            var employee = _ObjEmployees.UserLogin(employees);
            return View();
        }

        [HttpGet]        
        public IActionResult GetEmployeeList()
        {
            List<Employees> emps = _ObjEmployees.GetAllEmployeeData();
            return View(emps);
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            ViewBag.Title = "SignUp";
            return View();
        }

        [HttpPost]
        public JsonResult EmployeeSave(string firstname, string lastname, string username, string gender, int age, string dateofbirth, string phone, string email, string password, string confirmpassword, string address, string city, string state, string country, int zipcode)
        {
            var retVal = 0;
            Employees objEmployee = new Employees
            {
                Firstname = firstname,
                Lastname = lastname,
                Username = username,
                Gender = gender,
                Age = age,
                DateOfBirth = dateofbirth,
                Email = email,
                Phone = phone,
                Password = password,
                ConfirmPassword = confirmpassword,
                Address = address,
                City = city,
                State = state,
                Country = country,
                Zipcode = zipcode
            };

            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
            retVal = this._ObjEmployees.SaveEmployeeData(objEmployee);

            if (retVal > 0)
            {
                return Json(new { success = true, success_message = "Data Saved successfully." });
            }
            else
            {
                return Json(new { success = false, error_message = "failed to save data in the database." });
            }
        }






        [HttpGet]
        public IActionResult EmployeeUpdate(int Emp_Id)
        {
            Employees employee = _ObjEmployees.GetEmployeeById(Emp_Id);
            return View(employee);

        }

        [HttpPost]
        public JsonResult EmployeeUpdate(string firstname, string lastname, string username, string gender, int age, string dateofbirth, string phone, string email, string password, string confirmpassword, string address, string city, string state, string country, int zipcode)
        {
            var retVal = 0;
            Employees objEmployee = new Employees
            {
                Firstname = firstname,
                Lastname = lastname,
                Username = username,
                Gender = gender,
                Age = age,
                DateOfBirth = dateofbirth,
                Email = email,
                Phone = phone,
                Password = password,
                ConfirmPassword = confirmpassword,
                Address = address,
                City = city,
                State = state,
                Country = country,
                Zipcode = zipcode
            };

            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
            retVal = this._ObjEmployees.UpdateEmployeeData(objEmployee);

            if (retVal > 0)
            {
                return Json(new { success = true, success_message = "Data Update successfully." });
            }
            else
            {
                return Json(new { success = false, error_message = "failed to update data in the database." });
            }
        }



        //Delete Controller
        [HttpGet]
        public IActionResult EmployeeDelete(int Emp_Id)
        {
            int retval = _ObjEmployees.DeleteEmployee(Emp_Id);
            return RedirectToAction("GetEmployeeList");
        }

    }
}
