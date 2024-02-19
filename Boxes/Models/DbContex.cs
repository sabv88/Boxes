using Microsoft.EntityFrameworkCore;

namespace Boxes.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Bottle> Bottles { get; set; }
        public DbSet<Box> Boxes { get; set; }
        public DbSet<Pallet> Pallets { get; set; }
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "Data Source=boxes6.db");

        }
    }
}
