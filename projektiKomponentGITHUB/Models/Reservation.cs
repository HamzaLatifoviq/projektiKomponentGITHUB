using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace projektiKomponentGITHUB.Models
{

    public class Reservations
    {
        [Key]
        public int Id { get; set; }

        public int HotelId { get; set; }

        [Required]
        [StringLength(50)]
        public string ReservationRange { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int AdultsCount { get; set; }

        [Range(0, int.MaxValue)]
        public int? ChildrenCount { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int RoomsCount { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")] // përshtat formatin decimal sipas db
        public decimal Price { get; set; }


    }
}