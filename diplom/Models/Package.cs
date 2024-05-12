using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace diplom.Models
{
    [Table("Package")]
    public class Package
    {

        [Key]
        public int Package_id { get; set; }
        public string Package_name { get; set;}
    }
}
