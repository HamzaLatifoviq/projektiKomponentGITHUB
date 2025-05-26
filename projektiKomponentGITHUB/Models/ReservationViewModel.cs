using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projektiKomponentGITHUB.Models
{
    public class ReservationViewModel
    {
        [Key]
        public int ReservationID { get; set; }

        [ForeignKey("Hotel")]
        public int HotelID { get; set; }

        [ForeignKey("Room")]
        public int RoomID { get; set; }
        public string Emri { get; set; }
        public string Mbiemri { get; set; }
        public string Emaili { get; set; }

        public string HotelEmri { get; set; }
        public string RoomLlojiDhomes { get; set; }

        public DateTime DataCheckIn { get; set; }
        public DateTime DataCheckOut { get; set; }

        public int NumriTeRriturve { get; set; }
        public int? NumriFemijeve { get; set; }
        public int NumriDhomave { get; set; }
        public decimal CmimiTotal { get; set; }
    }
}