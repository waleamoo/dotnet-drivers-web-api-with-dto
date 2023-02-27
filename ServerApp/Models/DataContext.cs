using Microsoft.EntityFrameworkCore;

namespace ServerApp.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opts) : base(opts) { }
        // tables 
        public DbSet<Driver> Drivers { get; set; }
    }
}
