using Microsoft.EntityFrameworkCore;

namespace Api_React_CRUD.Models.cfgOperations
{
    public class cfgOperationsContext : DbContext
    {
        public cfgOperationsContext(DbContextOptions<cfgOperationsContext> options) : base(options)
        {

        }
        public DbSet<cfgOperations> cfgOperationss { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cfgOperations>().ToTable("cfgOperations");
        }
    }
}
