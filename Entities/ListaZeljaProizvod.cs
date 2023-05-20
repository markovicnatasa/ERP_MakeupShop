using MakeupShop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeupShop.Entities
{
    public class ListaZeljaProizvod
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int listaProID { get; set; }

        [ForeignKey("ListaZelja")]
        public int listaZeljaID { get; set; }

        [ForeignKey("Proizvod")]
        public int proizvodID { get; set; }

        [NotMapped]
        public ListaZeljaDto ListaZeljaDto { get; set; }

        [NotMapped]
        public ProizvodDto ProizvodDto { get; set; }

    }
}
