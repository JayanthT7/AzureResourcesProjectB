using Microsoft.EntityFrameworkCore;

namespace contactapi.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Contact> Contacts { get; set; }
    }
}
