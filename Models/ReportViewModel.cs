using System;
using System.Collections.Generic;


namespace bestbrigh.Models
{
    public class ReportViewModel
    {
        public DateTime ReportDate { get; set; }
        public List<SaleReportItem> Sales { get; set; }
        public List<InventoryReportItem> Inventory { get; set; }

        public class SaleReportItem
        {
            public string ProductName { get; set; }
            public int QuantitySold { get; set; }
            public decimal TotalPrice { get; set; }
            public string SalespersonName { get; set; }
            public DateTime SaleDate { get; set; }
        }

        public class InventoryReportItem
        {
            public string ProductName { get; set; }
            public int StockLevel { get; set; }
            public decimal Price { get; set; }
        }
    }
}
