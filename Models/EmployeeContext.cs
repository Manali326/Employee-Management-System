using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace EmployeeManagementSystem.Models
{
    public class EmployeeContext : IdentityDbContext<IdentityUser>
    {
        public EmployeeContext(DbContextOptions options) : base(options) 
        {
            
        }
        public DbSet<Employees> Employees { get; set; } 

    }

}
