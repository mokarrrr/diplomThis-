using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace diplom.Models
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string User_name { get; set; }
        public string role { get; set; }

        public string Surname { get; set; }
        [JsonIgnore]
        public string user_password { get; set; }

    }
}
