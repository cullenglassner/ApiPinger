using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPinger.Models
{
    public class Listing
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string CityState { get; set; }
        public string Zipcode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Owner { get; set; }
        public double Price { get; set; }
    }
}