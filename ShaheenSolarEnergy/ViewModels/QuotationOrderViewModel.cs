using ShaheenSolarEnergy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShaheenSolarEnergy.ViewModels
{
    public class QuotationOrderViewModel
    {
        public List<Quotation> Quotations { get; set; }
        public List<Product> Products { get; set; }
        public Order Orders { get; set; }
        
    }
}