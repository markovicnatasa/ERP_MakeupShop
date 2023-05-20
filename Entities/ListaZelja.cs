using MakeupShop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeupShop.Entities
{
    public class ListaZelja
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int listaZeljaID { get; set; }
        public DateTime datumNastanka { get; set; }

        public int kolicinaProizvoda { get; set; }

        [ForeignKey("Korisnik")]
        public int korisnikID { get; set; }

        [NotMapped]
        public KorisnikDto KorisnikDto { get; set; }
    }
}
