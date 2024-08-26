using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }

/*        [ForeignKey("UserId")]
        public User User { get; set; }*/

        [Required]
        public string OrderDate { get; set; }

        [Required]
        public string Status { get; set; }

/*        public ICollection<OrderDetail> OrderDetails { get; set; }
*/    }

}
