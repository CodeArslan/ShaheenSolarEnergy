using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ShaheenSolarEnergy.Models
{
    public class Supplier
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Please provide a name.")]
        public string Name { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "Please provide an address.")]
        public string Address { get; set; }

        [DisplayName("Phone")]
        [Required(ErrorMessage = "Please provide a phone number.")]
        public string Phone { get; set; }

        [DisplayName("Telephone")]
        public string Telephone { get; set; }
    }
}