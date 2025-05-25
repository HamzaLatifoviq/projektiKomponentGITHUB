using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projektiKomponentGITHUB.Models
{
    [Table("Reservations")]
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int HotelId { get; set; }

        [Required]
        [StringLength(50)]
        public string ReservationRange { get; set; }

        [Required]
        public int AdultsCount { get; set; }

        public int? ChildrenCount { get; set; }

        [Required]
        public int RoomsCount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
