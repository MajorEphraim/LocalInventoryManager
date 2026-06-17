using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LocalInventoryManager.Models
{
    public class Product
    {
        [Key]
        //Tells the SQLite database that this property is the primary key for the Product table
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Sku { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public int LowStockThreshold { get; set; }

        //A read-only property that returns LOW if the product's quantity is less than or equal to the low stock threshold, indicating that the product is low in stock
        public string Status => Quantity <= LowStockThreshold ? "LOW" : "OK";
    }
}
