using Microsoft.Build.Framework;

namespace MakeupShop.Models
{
    public class KorisnikCreateDto
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string imePrezime { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string adresa { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string username { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string password { get; set; }
        //[System.ComponentModel.DataAnnotations.Required]
        //public string uloga { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string kontakt { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string grad { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string email { get; set; }
    }
}
