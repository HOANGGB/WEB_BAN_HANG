using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class Combo
    {
        [Key]
        public int ComboId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

/*        public ICollection<ComboDetail> ComboDetails { get; set; }*/
    }
}
