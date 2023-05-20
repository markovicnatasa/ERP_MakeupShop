using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeupShop.Models
{
    public class PlacanjeDto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int placanjeID { get; set; }
        public DateTime datumPlacanja { get; set; }

        [ForeignKey("Porudzbina")]
        [System.ComponentModel.DataAnnotations.Required]
        public int porudzbinaID { get; set; }
    }
}
