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



        //User Login
        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.Title = "Login";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Employees employees)
        {
            var loggedInEmployee = _ObjEmployees.UserLogin(employees);
            if (loggedInEmployee != null)
            {
                return RedirectToAction("GetEmployeeList");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View();
            }

        }


        //Fetching Employee Data
        [HttpGet]
        public IActionResult GetEmployeeList()
        {
            List<Employees> emps = _ObjEmployees.GetAllEmployeeData();
            if (emps == null || !emps.Any())
            {
                ViewBag.Message = "No employees found.";
            }
            return View(emps);
        }



        //Save Employee Data
        [HttpGet]
        public IActionResult SignUp()
        {
            ViewBag.Title = "SignUp";
            return View();
        }

        [HttpPost]
        public JsonResult EmployeeSave(string firstname, string lastname, string username, string gender, int age, string dateofbirth, string phone, string email, string password, string confirmpassword, string address, string city, string state, string country, int zipcode)
        {
            if (ModelState.IsValid)
            {
                int retVal = 0;
                var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
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
            return Json(new { success = false, error_message = "Invalid data submitted." });
        }



        //Update Employee Data
        [HttpGet]
        public IActionResult EmployeeUpdate(int Emp_Id)
        {
            Employees employee = _ObjEmployees.GetEmployeeById(Emp_Id);
            return View(employee);

        }

        [HttpPost]
        public JsonResult EmployeeUpdate(int employeeLog_Id, string firstname, string lastname, string username, string gender, int age, string dateofbirth, string phone, string email, string password, string confirmpassword, string address, string city, string state, string country, int zipcode)
        {
            var retVal = 0;
            Employees objEmployee = new Employees
            {
                EmployeeLog_Id = employeeLog_Id,
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



        //Delete Employee Data
        [HttpGet]
        public IActionResult EmployeeDelete(int Emp_Id)
        {
            int retVal = 0;
            retVal = _ObjEmployees.DeleteEmployee(Emp_Id);

            if (retVal > 0)
            {
                return RedirectToAction("GetEmployeeList");
            }
            else
            {
                return NotFound("Employee not found.");
            }
        }

    }
}
