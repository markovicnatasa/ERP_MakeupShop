using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeupShop.Entities
{
    public class Korisnik
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int korisnikID { get; set; }

        public string imePrezime { get; set; }

        public string adresa { get; set; }

        public string username { get; set; }

        public string password { get; set; }
        public string uloga { get; set; }
        public string kontakt { get; set; }
        public string grad { get; set; }
        public string email { get; set; }
    }
}
