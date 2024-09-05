using System.ComponentModel.DataAnnotations;

namespace Api_React_CRUD.Models.MultipleTable
{
    public class MultipleTable
    {
        [Key]
        public int Id { get; set; }

        public int MachineID { get; set; }
        public cfgMachines.cfgMachines cfgMachines { get; set; }
        public string? MachineName { get; set; }

        public int OperationNo { get; set; }
        public cfgOperations.cfgOperations cfgOperations { get; set; }
        public string? OperationDescription { get; set; }
        public string? OperationNotes { get; set; }
        public bool AssemblyOperation { get; set; }
    }
}
