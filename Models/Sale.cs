using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bestbrigh.Models
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal SalePrice { get; set; }

        public DateTime SaleDate { get; set; } = DateTime.Now;
    }
}
