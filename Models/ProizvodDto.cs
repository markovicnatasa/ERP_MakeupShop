using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeupShop.Models
{
    public class ProizvodDto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int proizvodID { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string imeProizvoda { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string tipProizvoda { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string serijskiBroj { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public float cena { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public bool dostupnost { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public int kolicinaNaStanju { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string slika { get; set; }

        [ForeignKey("Brend")]
        [System.ComponentModel.DataAnnotations.Required]
        public int brendID { get; set; }
    }
}
