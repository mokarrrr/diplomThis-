using System.ComponentModel.DataAnnotations;

namespace diplom.Models
{
    public class _Provider
    {
        [Key]
        public int IdProvider { get; set; }
        public string provider_name { get; set; }
    }
}
