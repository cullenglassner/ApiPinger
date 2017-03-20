using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPinger.ViewModels
{
    public class ListingViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string Address_Line1 { get; set; }
        [Required]
        public string Address_Line2 { get; set; }
        [Required]
        public string Address_Line3 { get; set; }
        public double Price { get; set; }
    }
}
