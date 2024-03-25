using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace diplom.Models
{
    public class Rate
    {
        [Key]
        public int Rate_id { get; set; }
        public double _Rate { get; set; }
        public string? Rate_comment { get; set; }
        public int client_id { get; set;}
        [ForeignKey("client_id")]
        public User User  { get; set; }
        public int Productid { get; set; }
        [ForeignKey("Productid")]
        [JsonIgnore]
        public Product Product { get; set; }


    }
}
