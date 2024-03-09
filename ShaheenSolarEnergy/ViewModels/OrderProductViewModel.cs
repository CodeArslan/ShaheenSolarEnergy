using ShaheenSolarEnergy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShaheenSolarEnergy.ViewModels
{
    public class OrderProductViewModel
    {
        public Order Order { get; set; }
        public List<Product> Products { get; set; }
        public Quotation Quotations { get; set; }
    }
}