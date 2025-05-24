using System.ComponentModel.DataAnnotations;
using System;
namespace projektiKomponentGITHUB.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Emri është i nevojshëm")]
        public string Emri { get; set; }

        [Required(ErrorMessage = "Mbiemri është i nevojshëm")]
        public string Mbiemri { get; set; }

        [Required, EmailAddress(ErrorMessage = "Emaili është i pavlefshëm")]
        public string Emaili { get; set; }

        [Required]
        public string Adresa { get; set; }

        [Required]
        public string NumriTelefonit { get; set; }

        [Required]
        public string NumriKartes { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataSkadimit { get; set; }

        [Required]
        public string CVV { get; set; }

        [Required]
        public string MenyraPageses { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Shuma duhet të jetë më e madhe se zero")]
        public decimal Shuma { get; set; }

        public DateTime DataPageses { get; set; }

        public string PaymentStatus { get; set; }
    }
}
