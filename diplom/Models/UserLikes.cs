namespace diplom.Models
{
    public class UserLikes
    {
        public int Id { get; set; }

        public User _User { get; set; }
        public int UserID { get; set; }

        public Product Product { get; set; }
        public int ProductID { get; set; }
    }
}
