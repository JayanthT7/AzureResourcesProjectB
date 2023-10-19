using Microsoft.EntityFrameworkCore;

namespace KeyValutJayanthApp.Models
{
    public class DataContext:  DbContext
    {
        public DataContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            Database.EnsureCreated();
        }

        public DbSet<Customer> customers { get; set; }
    }
}
