using System.ComponentModel.DataAnnotations;

namespace Api_React_CRUD.Models.cfgErrors
{
    public class cfgErrors
    {
        [Key]
        public int ErrorNo { get; set; }
        public string? ErrorText { get; set; }
    }
}
