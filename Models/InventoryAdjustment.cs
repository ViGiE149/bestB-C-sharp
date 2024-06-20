using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bestbrigh.Models
{
    public class InventoryAdjustment
    {
        [Key]
        public int AdjustmentId { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        [StringLength(20)]
        public string AdjustmentType { get; set; }  // e.g., 'Restock', 'Correction'

        [Required]
        public int Quantity { get; set; }

        public DateTime AdjustmentDate { get; set; } = DateTime.Now;

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
