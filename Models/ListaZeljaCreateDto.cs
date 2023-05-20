using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeupShop.Models
{
    public class ListaZeljaCreateDto
    {
        public DateTime datumNastanka { get; set; }

        public int kolicinaProizvoda { get; set; }

        [ForeignKey("Korisnik")]
        [System.ComponentModel.DataAnnotations.Required]
        public int korisnikID { get; set; }
    }
}
