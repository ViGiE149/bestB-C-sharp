using bestbrigh.Models;
using  Microsoft.EntityFrameworkCore;

namespace bestbrigh.data
{
    public class ApplicationDbContext : DbContext
    {
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }

    
        public DbSet<InventoryAdjustment> InventoryAdjustments { get; set; }

    

    }
}
