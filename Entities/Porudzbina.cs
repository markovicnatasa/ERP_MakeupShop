using MakeupShop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeupShop.Entities
{
    public class Porudzbina
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int porudzbinaID { get; set; }
        public decimal ukupnaCena { get; set; }
        public DateTime datumPorudzbine { get; set; }
        public DateTime datumIsporuke { get; set; }

        [ForeignKey("Korisnik")]
        public int korisnikID { get; set; }

        [NotMapped]
        public KorisnikDto KorisnikDto { get; set; }
    }
}
