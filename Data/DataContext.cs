using backend_portal_empleos.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend_portal_empleos.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Applications> Applications { get; set; }
    }
}
