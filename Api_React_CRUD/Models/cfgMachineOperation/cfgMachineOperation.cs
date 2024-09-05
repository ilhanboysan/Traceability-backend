using System.ComponentModel.DataAnnotations;

namespace Api_React_CRUD.Models.cfgMachineOperation
{
    public class cfgMachineOperation
    {
        [Key]
        public int MachineID { get; set; }
        public int OperationNo { get; set; }
    }
}
