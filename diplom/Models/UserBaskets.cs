
namespace diplom.Models
{

    public class UserBaskets
    {
        public int Id { get; set; }

        public User _User { get; set; }
        public int client_id { get; set; }

        public Product Product { get; set; }
        public int product_id { get; set; }
    }
}
