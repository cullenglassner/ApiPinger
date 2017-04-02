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
        public string Address { get; set; }
        [Required]
        public string CityState { get; set; }
        [Required]
        [StringLength(5, MinimumLength = 5)]
        public string Zipcode { get; set; }
    }
}
