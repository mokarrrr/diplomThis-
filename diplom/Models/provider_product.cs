using Microsoft.EntityFrameworkCore;

namespace diplom.Models
{
    [PrimaryKey(nameof(Id_provider), nameof(ID_product))]
    public class provider_product
    {
        public int Id_provider { get; set; }
        public int ID_product { get; set; }
    }
}
