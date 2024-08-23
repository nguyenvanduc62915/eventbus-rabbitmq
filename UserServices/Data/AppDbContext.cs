using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserServices.Models;

namespace UserServices.Data
{
    public class AppDbContext : IdentityDbContext<Onwer>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Onwer> Onwers { get; set; } 
    }
}
