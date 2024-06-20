using System.ComponentModel.DataAnnotations;

namespace bestbrigh.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        [Required]
        [StringLength(50)]
        public string SKU { get; set; }

        [Required]
        public int StockLevel { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public int Threshold { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
    
}
