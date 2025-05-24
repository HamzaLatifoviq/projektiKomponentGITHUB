using System.Data.Entity;

namespace projektiKomponentGITHUB.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("name=DefaultConnection")
        {
        }

        //public DbSet<LoginView> Users { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}
