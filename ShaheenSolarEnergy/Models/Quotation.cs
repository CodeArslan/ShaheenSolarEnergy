using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShaheenSolarEnergy.Models
{
    public class Quotation
    {
        [Key]
        public int Id { get; set; } 

        public Product Product { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [Required]
        public int UnitPrice { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; } 

        [Required]
        [DataType(DataType.Currency)]
        public int TotalCost { get; set; } 
        public Order Order { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
    }
}