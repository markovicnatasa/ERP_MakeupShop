using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeupShop.Models
{
    public class StavkaPorudzbineUpdateDto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int stavkaPorudzbineID { get; set; }

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
