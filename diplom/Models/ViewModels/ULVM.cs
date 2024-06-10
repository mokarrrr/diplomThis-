namespace diplom.Models.ViewModels
{
    public class ULVM
    {
        public List<UserLikes> User_Likes { get; set; }
        public Product Product { get; set; }
        public List<User> Users { get; set; }
        public List<_order> _orders { get; set; }

        public bool HasUsers { get; set; }
        public bool HasOrders { get; set; }

        // Новые свойства для пагинации пользователей
        public int UserPageNumber { get; set; }
        public int UserTotalPages { get; set; }
        public int UserPageSize { get; set; }
        public int OrderPageNumber { get; set; }
        public int OrderTotalPages { get; set; }
        public int OrderPageSize { get; set; }
    }
    
}
