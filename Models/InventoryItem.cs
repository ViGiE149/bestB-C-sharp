using System.ComponentModel.DataAnnotations;

namespace bestbrigh.Models
{
    public class InventoryItem
    {
        public int InventoryItemId { get; set; }

        [Required(ErrorMessage = "Please select a product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required(ErrorMessage = "Stock level is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock level must be a positive number")]
        public int StockLevel { get; set; }

        [Required(ErrorMessage = "Reorder level is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Reorder level must be a positive number")]
        public int ReorderLevel { get; set; }


    }
}
