using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TST.DataAccess.Context
{
    public class TstContext : IdentityDbContext<ApplicationUser>
    {
        public TstContext()
        {
        }

        public virtual DbSet<Slide> Slide { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Performance> Performance { get; set; }
        public virtual DbSet<PerformanceImage> PerformanceImage { get; set; }
        public virtual DbSet<PerformanceType> PerformanceType { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Welcome> Welcome { get; set; }
        


        public TstContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
