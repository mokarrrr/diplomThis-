using System.ComponentModel.DataAnnotations;

namespace diplom.Models
{
    public class catalog_product
    {
        [Key]
        public int Id_catalog { get; set; }
        public int id_product_catalog { get; set; }
    }
}
