using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace diplom.Models
{
    [PrimaryKey(nameof(Product_id),nameof(Raiting_id))]
    public class Product_Raiting
    {
        public int Product_id { get; set; }
        public int Raiting_id { get; set;}

    }
}
