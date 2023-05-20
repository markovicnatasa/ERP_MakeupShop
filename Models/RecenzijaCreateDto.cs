using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeupShop.Models
{
    public class RecenzijaCreateDto
    {
        public int ocena { get; set; }
        public string komentar { get; set; }

        [ForeignKey("Korisnik")]
        [System.ComponentModel.DataAnnotations.Required]
        public int korisnikID { get; set; }

        [ForeignKey("Proizvod")]
        [System.ComponentModel.DataAnnotations.Required]
        public int proizvodID { get; set; }
    }
}
