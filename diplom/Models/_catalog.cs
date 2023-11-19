using System.ComponentModel.DataAnnotations;

namespace diplom.Models
{
    public class _catalog
    {
        [Key]
        public int Idcatalog { get; set; }
        public string catalog_name { get; set; }
        public string catalog_description { get; set;}
    }
}
