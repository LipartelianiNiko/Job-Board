using Microsoft.EntityFrameworkCore;
using JobBoard.Models;

//this is dbcontext class, DbContext is ASP.NET's built in object, that acts as bridge between C# code and database
namespace JobBoard.Data
{
    public class AppDbContext : DbContext//inherits from built-in db context class
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }  
        public DbSet<SeekerProfile> SeekersProfiles { get; set; }
        public DbSet<EmployerProfile> EmployerProfiles { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<SavedJob> SavedJobs { get; set; }


    }
}
