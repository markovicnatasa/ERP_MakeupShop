using MakeupShop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeupShop.Entities
{
    public class StavkaPorudzbine
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int stavkaPorudzbineID { get; set; }

        [ForeignKey("Porudzbina")]
        public int porudzbinaID { get; set; }

        [ForeignKey("Proizvod")]
        public int proizvodID { get; set; }
        public int kolicinaProizvoda { get; set; }

        [NotMapped]
        public PorudzbinaDto PorudzbinaDto { get; set; }

        [NotMapped]
        public ProizvodDto ProizvodDto { get; set; }
    }
}
