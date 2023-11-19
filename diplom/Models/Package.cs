using System.ComponentModel.DataAnnotations;

namespace diplom.Models
{
    public class Package
    {
        [Key]
        public int Package_id { get; set; }
        public string Package_name { get; set;}
    }
}
