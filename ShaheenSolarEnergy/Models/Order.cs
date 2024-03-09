using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShaheenSolarEnergy.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide Order Number.")]
        public int OrderNumber { get; set; }

        [Required(ErrorMessage = "Please provide Order Status.")]
        public string OrderStatus { get; set; }

        [Required(ErrorMessage = "Please provide Order Date.")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Please provide Customer Name.")]
        public string CustomerName { get; set; }
       
        public string BillTo { get; set;}
        public string CustomerAddress { get; set; }
    }
}