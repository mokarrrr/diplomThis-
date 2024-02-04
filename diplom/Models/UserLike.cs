namespace diplom.Models
{
    public class UserLike
    {
        public int Id { get; set; }

        public _User User { get; set; }
        public int UserID { get; set; }

        public Product product { get; set; }
        public int ProductID { get; set; }
    }
}
