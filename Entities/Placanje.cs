using MakeupShop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeupShop.Entities
{
    public class Placanje
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int placanjeID { get; set; }
        public DateTime datumPlacanja { get; set; }

        [ForeignKey("Porudzbina")]
        public int porudzbinaID { get; set; }

        [NotMapped]
        public PorudzbinaDto PorudzbinaDto { get; set; }
    }
}
