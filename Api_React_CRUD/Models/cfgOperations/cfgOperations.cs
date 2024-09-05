using System.ComponentModel.DataAnnotations;

namespace Api_React_CRUD.Models.cfgOperations
{
    public class cfgOperations
    {
        [Key]
        public int OperationNo { get; set; }
        public string? OperationDescription { get; set; }
        public string? OperationNotes { get; set; }
        public bool AssemblyOperation { get; set; }

  
    }
}
