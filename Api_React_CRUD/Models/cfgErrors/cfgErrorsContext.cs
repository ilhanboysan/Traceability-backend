using Microsoft.EntityFrameworkCore;

namespace Api_React_CRUD.Models.cfgErrors
{
    public class cfgErrorsContext : DbContext
    {
        public cfgErrorsContext(DbContextOptions<cfgErrorsContext> options) : base(options)
        {

        }
        public DbSet<cfgErrors> cfgErrorss { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cfgErrors>().ToTable("cfgErrors");
        }
    }
}
