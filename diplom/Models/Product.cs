using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace diplom.Models
{
    
    public class Product
    {
        [Key]
        public int IdProduct { get; set; }
        public string Name_product { get; set; }
        public string product_description_ { get; set; }
        public int? product_price { get; set; }
        public int? product_weight { get; set; }
        public string product_mass_per_fat { get; set; }
        public int? product_fat { get; set; }
        public int? product_protein { get; set; }
        public int? product_fatty { get; set; }
        public double product_carb { get; set; }
        public int? product_energy_value { get; set; }
        public int? product_storage_life { get; set; }
        public int? product_package_id { get; set; }
        public string product_storage_conditions { get; set; }
        public double? product_sale { get; set; }
        public int? product_remain { get; set; }
        public string product_article { get; set; }
        public string product_sostav { get; set; }
        
        public string product_img { get; set; }
        public int Provider_id { get; set; }
        public bool Ishidden { get; set; }

        [JsonIgnore]
        public List<_order> _Orders { get; set; }
        public ICollection<Rate> Rates { get; set; }
        [JsonIgnore]
        public List<order_detail> order_Details { get; set; }
        
    }
}
