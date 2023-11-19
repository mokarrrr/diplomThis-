using System.ComponentModel.DataAnnotations;
namespace diplom.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Admin_login { get; set; }
        public string Admin_password { get; set; }
        public string Admin_name { get; set; }
    }
}
