using Microsoft.EntityFrameworkCore;

namespace diplom.Models
{
    [PrimaryKey(nameof(client_id), nameof(product_id))]
    public class user_basket
    {
        public int client_id { get; set; }
        public int product_id { get; set; }
    }
}
