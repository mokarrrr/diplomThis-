using System.ComponentModel.DataAnnotations;

namespace diplom.Models
{
    public class _status
    {
        [Key]
        public int Id { get; set; }
        public string status_name { get; set; }
    }
}
