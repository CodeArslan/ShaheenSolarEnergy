using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShaheenSolarEnergy.Models
{
    public class Product
    {
        public int Id { get; set; }

        [DisplayName("Product Name")]
        [Required(ErrorMessage = "Please provide Product Name.")]
        public string Name { get; set; }

        [DisplayName("Retail Price")]
        [Required(ErrorMessage = "Please provide a Retail Price.")]
        public int RetailPrice { get; set; }

        [DisplayName("WholeSale Price")]
        [Required(ErrorMessage = "Please provide a WholeSale Price.")]
        public int WholeSalePrice { get; set; }



    }
}