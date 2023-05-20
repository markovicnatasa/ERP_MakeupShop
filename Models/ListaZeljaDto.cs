using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeupShop.Models
{
    public class ListaZeljaDto
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int listaZeljaID { get; set; }
        public DateTime datumNastanka { get; set; }
        public int kolicinaProizvoda { get; set; }

        [ForeignKey("Korisnik")]
        [System.ComponentModel.DataAnnotations.Required]
        public int korisnikID { get; set; }
    }
}
