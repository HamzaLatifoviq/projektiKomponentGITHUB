using System;

namespace projektiKomponentGITHUB.Models
{
    public class BookingViewModel
    {
        public int Id { get; set; }
        public string Emri { get; set; }
        public string Kategoria { get; set; }
        public string Qyteti { get; set; }
        public string Distanca { get; set; }
        public string Transmetimi { get; set; }
        public string LlojiKarburantit { get; set; }
        public string Pershkrimi { get; set; }
        public double Vleresimi { get; set; }
        public int NrRecensioneve { get; set; }
        public string FotoPath { get; set; }

        public int VeturaId { get; set; }
        public string DropOffLocation { get; set; }

        public DateTime? PickupDate { get; set; }
        public DateTime? DropoffDate { get; set; }

        public bool GPS { get; set; }
        public bool BabySeat { get; set; }
        public bool ExtraInsurance { get; set; }
        public bool AdditionalDriver { get; set; }

        public DateTime BookingDate { get; set; }

        // Constructor to set default booking date
        public BookingViewModel()
        {
            BookingDate = DateTime.Now;
        }
    }
}
