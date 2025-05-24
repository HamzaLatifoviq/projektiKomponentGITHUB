using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projektiKomponentGITHUB.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Emri { get; set; }
        public string Mbiemri { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
    }

}