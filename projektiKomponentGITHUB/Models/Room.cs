using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projektiKomponentGITHUB.Models
{
    public class Room
    {
        [Key]
        public int RoomID { get; set; }

        [ForeignKey("Hotel")]
        public int HotelID { get; set; }

        [Required]
        public string LlojiDhomes { get; set; }

        [Required]
        public decimal CmimiPerNate { get; set; }

        [Required]
        public int Kapaciteti { get; set; }

        [Required]
        public int Sasia { get; set; }

        public virtual Hotel Hotel { get; set; }
    }
}