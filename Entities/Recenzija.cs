using MakeupShop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeupShop.Entities
{
    public class Recenzija
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int recenzijaID { get; set; }
        public int ocena { get; set; }
        public string komentar { get; set; }

        [ForeignKey("Korisnik")]
        public int korisnikID { get; set; }

        [ForeignKey("Proizvod")]
        public int proizvodID { get; set; }

        [NotMapped]
        public KorisnikDto KorisnikDto { get; set; }

        [NotMapped]
        public ProizvodDto ProizvodDto { get; set; }
    }
}
