using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace diplom.Models
{
    [Table ("_order")]
    public class _order
    {
        [Key]
        public int Idorder { get; set; }
        public int status_id { get; set; }
        [ForeignKey("status_id")]
        public _status Status { get; set; }
        public string order_city { get; set;}
        public DateTime order_date { get; set;}
        public string order_address { get; set;}
        public string? order_commentary { get; set;}
        public int _user_id { get; set;}
        [ForeignKey("_user_id")]
        public User User { get; set; }
        public int OrderSum { get; set;}
        public List<Product> Products { get; set; }
        public List<order_detail> order_Details { get; set; }

    }
}
