using System.ComponentModel.DataAnnotations;
using System;
namespace projektiKomponentGITHUB.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Emri është i nevojshëm")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Emri duhet të përmbajë vetëm shkronja.")]
        public string Emri { get; set; }

        [Required(ErrorMessage = "Mbiemri është i nevojshëm")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Mbiemri duhet të përmbajë vetëm shkronja.")]
        public string Mbiemri { get; set; }

        [Required, EmailAddress(ErrorMessage = "Emaili është i pavlefshëm")]
        public string Emaili { get; set; }

        [Required]
        public string Adresa { get; set; }

        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "Numri i telefonit duhet të përmbajë vetëm numra.")]
        public string NumriTelefonit { get; set; }

        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "Numri i kartës duhet të përmbajë vetëm numra.")]
        public string NumriKartes { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataSkadimit { get; set; }

        [Required]
        [RegularExpression(@"^\d{3,4}$", ErrorMessage = "CVV duhet të jetë 3 ose 4 numra.")]
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
