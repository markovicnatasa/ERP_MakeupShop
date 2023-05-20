using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeupShop.Models
{
    public class StavkaPorudzbineCreateDto
    {
        [ForeignKey("Porudzbina")]
        [System.ComponentModel.DataAnnotations.Required]
        public int porudzbinaID { get; set; }

        [ForeignKey("Proizvod")]
        [System.ComponentModel.DataAnnotations.Required]
        public int proizvodID { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public int kolicinaProizvoda { get; set; }
    }
}
