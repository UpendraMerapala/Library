using Microsoft.EntityFrameworkCore;

namespace LibearayManagementSystem.Models
{
    public class LibDBContext : DbContext
    {
        public LibDBContext(DbContextOptions options):base(options)
        {
        }
        public DbSet<Book> Books {  get; set; }
        public DbSet<Publication> publications { get; set; }
        public  DbSet<Address> addresses { get; set; }
        public  DbSet<Author> authors { get; set; }
    }
}
