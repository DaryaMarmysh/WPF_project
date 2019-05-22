using System.Data.Entity;
using kurs.Models;

namespace kurs
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("DefaultConnection")
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<TestTitle> TestTitles { get; set; }
    }
}
