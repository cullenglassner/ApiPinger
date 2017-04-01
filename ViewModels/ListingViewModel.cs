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
        public string Street_Address { get; set; }
        [Required]
        public string City_State { get; set; }
        [Required]
        public string Zipcode { get; set; }
    }
}
