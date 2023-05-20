using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeupShop.Models
{
    public class PorudzbinaCreateDto
    {
        public decimal ukupnaCena { get; set; }
        public DateTime datumPorudzbine { get; set; }
        public DateTime datumIsporuke { get; set; }

        [ForeignKey("Korisnik")]
        [System.ComponentModel.DataAnnotations.Required]
        public int korisnikID { get; set; }
    }
}
