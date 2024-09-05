using System.ComponentModel.DataAnnotations;

namespace Api_React_CRUD.Models.cfgMachines
{
    public class cfgMachines 
    {
        [Key]
        public int MachineID { get; set; }
        public string? MachineName { get; set; }


    }
}
