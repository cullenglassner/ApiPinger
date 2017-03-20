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
        public string Address_Line1 { get; set; }
        public string Address_Line2 { get; set; }
        public string Address_Line3 { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Owner { get; set; }
        public double Price { get; set; }
    }
}