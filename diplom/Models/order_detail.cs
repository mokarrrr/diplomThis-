using Microsoft.EntityFrameworkCore;

namespace diplom.Models
{
    [PrimaryKey(nameof(id_product), nameof(id_order))]
    public class order_detail
    {
        public int id_product { get; set; }
        public int id_order { get; set; }
    }
}
