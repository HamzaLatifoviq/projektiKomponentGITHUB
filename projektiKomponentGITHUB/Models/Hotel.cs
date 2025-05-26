using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projektiKomponentGITHUB.Models
{
    public class Hotel
    {
        [Key]
        public int HotelID { get; set; }

        [Required]
        public string Emri { get; set; }

        [Required]
        public string Lokacioni { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}