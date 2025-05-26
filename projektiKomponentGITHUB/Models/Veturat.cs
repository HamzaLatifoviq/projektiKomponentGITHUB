using System.ComponentModel.DataAnnotations.Schema;

namespace projektiKomponentGITHUB.Models
{
    [Table("Veturat")]
    public class Veturat
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
        public decimal Price { get; set; }
    }

}