using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projektiKomponentGITHUB.Models
{
    [Table("Bookings")]
    public class VeturBooking
    {
        [Key]
        public int BookingID { get; set; }

        [ForeignKey("Vetura")]
        public int VeturaID { get; set; }

        public int? UserID { get; set; }  // optional

        [StringLength(100)]
        public string DropOffLocation { get; set; }

        public string AddOns { get; set; } // comma-separated add-ons like "GPS,BabySeat"

        public DateTime BookingDate { get; set; } = DateTime.Now;

        public DateTime? PickupDate { get; set; }

        public DateTime? DropoffDate { get; set; }

        public bool GPS { get; set; } = false;

        public bool BabySeat { get; set; } = false;

        public bool ExtraInsurance { get; set; } = false;

        public bool AdditionalDriver { get; set; } = false;

        public decimal? PriceAtBooking { get; set; }

        // Navigation property to the related Veturat entity (car)
        public virtual Veturat Vetura { get; set; }
    }
}
