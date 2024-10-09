using My_Portfolio.Models;
using System.Security.Principal;

namespace My_Portfolio.Interface
{
    public interface IEmployees
    {
        int SaveEmployeeData(Employees employees);
        List<Employees> GetAllEmployeeData();
        int UpdateEmployeeData(Employees employees);
        Employees GetEmployeeById(int Emp_Id);
        int DeleteEmployee(int Emp_Id);
        Employees UserLogin(Employees employees);
    }
}
