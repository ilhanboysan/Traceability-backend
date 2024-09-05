using Microsoft.EntityFrameworkCore;
using Api_React_CRUD.Models.cfgMachines;
using Api_React_CRUD.Models.cfgOperations;
using Api_React_CRUD.Models.cfgMachineOperation; 

namespace Api_React_CRUD.Models.MultipleTable
{
    public class MultipleTableContext : DbContext
    {
        public MultipleTableContext(DbContextOptions<MultipleTableContext> options) : base(options)
        {

        }

        public DbSet<Api_React_CRUD.Models.cfgMachines.cfgMachines> cfgMachines { get; set; }
        public DbSet<Api_React_CRUD.Models.cfgOperations.cfgOperations> cfgOperations { get; set; }
        public DbSet<cfgMachineOperation.cfgMachineOperation> cfgMachineOperations { get; set; } 

        public DbSet<MultipleTable> MultipleTables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Api_React_CRUD.Models.cfgMachines.cfgMachines>().ToTable("cfgMachines");
            modelBuilder.Entity<Api_React_CRUD.Models.cfgOperations.cfgOperations>().ToTable("cfgOperations");
            modelBuilder.Entity<cfgMachineOperation.cfgMachineOperation>().ToTable("cfgMachineOperations"); 

            modelBuilder.Entity<MultipleTable>().ToTable("MultipleTable");

            modelBuilder.Entity<MultipleTable>()
                .HasKey(mt => mt.Id); 

            modelBuilder.Entity<MultipleTable>()
                .HasOne(mt => mt.cfgMachines)
                .WithMany()
                .HasForeignKey(mt => mt.MachineID);

            modelBuilder.Entity<MultipleTable>()
                .HasOne(mt => mt.cfgOperations)
                .WithMany()
                .HasForeignKey(mt => mt.OperationNo);
        }
    }
}
