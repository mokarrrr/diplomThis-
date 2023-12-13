using System.ComponentModel.DataAnnotations;

namespace diplom.Models
{
    public class Rate
    {
        [Key]
        public int Rate_id { get; set; }
        public double _Rate { get; set;}
        public string? Rate_comment { get; set;}
        public int client_id { get; set;}
        public int Productid { get; set; }


    }
}
