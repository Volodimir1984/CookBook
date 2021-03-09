using Microsoft.EntityFrameworkCore;

namespace CookBookModel
{
    public class CookBookContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }

        public CookBookContext(){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CookBook;" +
                                        "Trusted_Connection=True;", 
                i => i.UseHierarchyId());
        }
    }
}