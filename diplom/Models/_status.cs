using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace diplom.Models
{
    [Table ("_status")]
    public class _status
    {
        [Key]
        public int Idstatus { get; set; }
        public string status_name { get; set; }
        
    }
}
