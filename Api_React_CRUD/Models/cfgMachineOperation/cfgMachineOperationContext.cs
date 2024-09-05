using Microsoft.EntityFrameworkCore;

namespace Api_React_CRUD.Models.cfgMachineOperation
{
    public class cfgMachineOperationContext : DbContext
    {
        public cfgMachineOperationContext(DbContextOptions<cfgMachineOperationContext> options) : base(options)
        {

        }
        public DbSet<cfgMachineOperation> cfgMachineOperation { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cfgMachineOperation>().ToTable("cfgMachineOperations");
        }
    }
}
