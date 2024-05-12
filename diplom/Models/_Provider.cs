using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace diplom.Models
{
    [Table("_Provider")]
    public class _Provider
    {
        [Key]
        public int IdProvider { get; set; }
        public string provider_name { get; set; }

        public string? provider_phone { get; set; }
    }
}
