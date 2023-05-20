using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeupShop.Models
{
    public class KorisnikUpdateDto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int korisnikID { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string imePrezime { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string adresa { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string username { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string password { get; set; }
       // [System.ComponentModel.DataAnnotations.Required]
        //public string uloga { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string kontakt { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string grad { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string email { get; set; }
    }
}
