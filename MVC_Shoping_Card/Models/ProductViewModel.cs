using System.ComponentModel.DataAnnotations;

namespace MVC_Shoping_Card.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Amount { get; set; }
        public string Info { get; set; }
        public decimal Price { get; set; }
        [Required]
        public CategoryViewModel Category { get; set; }
    }
}
