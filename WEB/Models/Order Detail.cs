using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{

    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }

        [Required]
        public int OrderId { get; set; }
/*
        [ForeignKey("OrderId")]
        public Order Order { get; set; }*/

        [Required]
        public int FoodItemId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }
/*
        [ForeignKey("FoodItemId")]
        public FoodItem FoodItem { get; set; }*/
    }
}
