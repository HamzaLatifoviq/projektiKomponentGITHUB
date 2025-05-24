using System;
using System.ComponentModel.DataAnnotations;

namespace projektiKomponentGITHUB.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Emri { get; set; }
        public string Mbiemri { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "Client";  // default role when registering
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? LastLogin { get; set; }
    }
}
