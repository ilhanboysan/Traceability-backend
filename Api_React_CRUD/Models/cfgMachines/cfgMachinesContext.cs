using Microsoft.EntityFrameworkCore;

namespace Api_React_CRUD.Models.cfgMachines
{
    public class cfgMachinesContext : DbContext
    {
        public cfgMachinesContext(DbContextOptions<cfgMachinesContext> options) : base(options)
        {

        }
        public DbSet<cfgMachines> cfgMachines { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cfgMachines>().ToTable("cfgMachines");
        }
    }
}
