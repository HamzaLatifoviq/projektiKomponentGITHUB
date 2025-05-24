using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projektiKomponentGITHUB.Models
{
    public class VeturBooking
    {
        [Key]
        public int BookingID { get; set; }
        public int VeturaID { get; set; }
        public string DropOffLocation { get; set; }
        public string AddOns { get; set; } // e.g., "GPS,BabySeat"
        public DateTime BookingDate { get; set; } = DateTime.Now;

        public virtual VeturBooking Vetura { get; set; }
    }

}