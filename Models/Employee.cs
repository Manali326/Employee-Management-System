using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class Employees
    {
        [Key]
       public int Id { get; set; }
        public String Name { get; set; }
        public String City { get; set; }

    }
}
