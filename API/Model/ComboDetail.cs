using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{

    public class ComboDetail
    {
        [Key]
        public int ComboDetailId { get; set; }

        [Required]
        public int ComboId { get; set; }


        [Required]
        public int FoodItemId { get; set; }
/*
        [ForeignKey("ComboId")]
        public Combo Combo { get; set; }*/
/*        [ForeignKey("FoodItemId")]
        public FoodItem FoodItem { get; set; }*/
    }
}
