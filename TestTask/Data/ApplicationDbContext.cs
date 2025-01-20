using Microsoft.EntityFrameworkCore;
using TestTask.Models;

namespace TestTask.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public DbSet<Order> Orders { get; set; }

        public string DbPath;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source=database.db");
    }
}
