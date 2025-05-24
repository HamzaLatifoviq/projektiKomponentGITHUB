using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projektiKomponentGITHUB.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        [StringLength(500)]
        public string Comment { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}