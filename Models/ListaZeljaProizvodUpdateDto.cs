using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeupShop.Models
{
    public class ListaZeljaProizvodUpdateDto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int listaProID { get; set; }

        [ForeignKey("ListaZelja")]
        [System.ComponentModel.DataAnnotations.Required]
        public int listaZeljaID { get; set; }

        [ForeignKey("Proizvod")]
        [System.ComponentModel.DataAnnotations.Required]
        public int proizvodID { get; set; }
    }
}
