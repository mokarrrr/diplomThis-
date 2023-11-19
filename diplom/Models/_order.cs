using System;
using System.ComponentModel.DataAnnotations;

namespace diplom.Models
{
    public class _order
    {
        [Key]
        public int Idorder { get; set; }
        public int status_id { get; set; }
        public string order_city { get; set;}
        public DateTime order_date { get; set;}
        public string order_address { get; set;}
        public string order_commentary { get; set;}
        public int _user_id { get; set;}
    }
}
