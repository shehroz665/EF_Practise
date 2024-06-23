using EF_Practise.Modals;
using Microsoft.EntityFrameworkCore;

namespace EF_Practise.Data
{
    // Define the ApplicationDbContext class inheriting from DbContext
    public class ApplicationDbContext: DbContext
    {
        // Constructor that accepts DbContextOptions for configuration and passes it to the base class constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        // Define a DbSet property for Villa entities to map to the Villas table in the database
        public DbSet<Villa> Villas {  get; set; } 

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
