using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_и_AJAX._Guest_Book.Models
{
    public class Guest_BookContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public Guest_BookContext(DbContextOptions<Guest_BookContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}