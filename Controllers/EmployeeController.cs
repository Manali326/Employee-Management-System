using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeContext employeeContext;

        public EmployeeController(EmployeeContext employeeContext)
        {
            this.employeeContext = employeeContext;
        }
        [HttpGet]
        [Route("GetEmployees")]
        public List<Employees> GetEmployee()
        {

            return employeeContext.Employees.ToList();
        }

        [HttpGet]
        [Route("GetEmployee")]
        public Employees GetEmployee(int id)
        {
            return employeeContext.Employees.Where(x => x.Id == id).FirstOrDefault();
        }

        [HttpPost]
        [Route("AddEmployee")]

        public string AddEmployee(Employees employees)
        {
            string response = string.Empty;
            employeeContext.Employees.Add(employees);
            employeeContext.SaveChanges();
            return "Employee added";

        }

        [HttpPut]
        [Route("UpdateEmployee")]
        public string UpdateEmployee(Employees employees)
        {
            employeeContext.Entry(employees).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
             employeeContext.SaveChanges();

            return "Employee Updated";
        }

        [HttpDelete]
        [Route("DeleteEmployee")]
        public string DeleteEmployee(int id)
        {
            Employees employee=employeeContext.Employees.Where(x => x.Id == id).FirstOrDefault();
            if (employee != null)
            {
                employeeContext.Employees.Remove(employee);
                employeeContext.SaveChanges();
                return "Employee Deleted";
            }
            else
            {
                return "No Employee Found ";
            }
        }
    }
}
