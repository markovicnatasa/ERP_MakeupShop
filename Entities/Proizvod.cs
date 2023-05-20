using MakeupShop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeupShop.Entities
{
    public class Proizvod
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int proizvodID { get; set; }
        public string imeProizvoda { get; set; }
        public string tipProizvoda { get; set; }
        public string serijskiBroj { get; set; }
        public float cena { get; set; }
        public bool dostupnost { get; set; }
        public int kolicinaNaStanju { get; set; }
        public string slika { get; set; }

        [ForeignKey("Brend")]
        public int brendID { get; set; }

        [NotMapped]
        public BrendDto BrendDto { get; set; }
    }
}
