using EF_Practise.Modals;
using Microsoft.EntityFrameworkCore;

namespace EF_Practise.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        public DbSet<VillaDto> VillaDtos {  get; set; } 
    }
}
