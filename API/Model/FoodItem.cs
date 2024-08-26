using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class FoodItem
    {
        [Key]
        public int FoodItemId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(200)]
        public string ImageUrl { get; set; }

        [Required]
        public string Category { get; set; }

/*        public ICollection<ComboDetail> ComboDetails { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }*/
    }

}
