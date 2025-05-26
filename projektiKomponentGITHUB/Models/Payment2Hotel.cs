using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projektiKomponentGITHUB.Models
{
    public class Payments2Hotel
    {
        public int Id { get; set; }
        public string Emri { get; set; }
        public string Mbiemri { get; set; }
        public string Emaili { get; set; }
        public string Adresa { get; set; }
        public string NumriTelefonit { get; set; }
        public string NumriKartes { get; set; }
        public DateTime DataSkadimit { get; set; }
        public string CVV { get; set; }
        public string MenyraPageses { get; set; }
        public decimal Shuma { get; set; }
        public DateTime DataPageses { get; set; }
        public string PaymentStatus { get; set; }
        public int? ReservationID { get; set; }

        // Optional navigation property if you want to access Reservation data
        public virtual Reservation Reservation { get; set; }
    }
}