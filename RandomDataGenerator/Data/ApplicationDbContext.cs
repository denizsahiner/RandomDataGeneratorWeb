using Microsoft.EntityFrameworkCore;
using RandomDataGenerator.Models;

namespace RandomDataGenerator.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Field> Fields { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source-fields.db");
            }
                
        }
    }
}
