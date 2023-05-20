using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeupShop.Models
{
    public class PlacanjeCreateDto
    {
        public DateTime datumPlacanja { get; set; }

        [ForeignKey("Porudzbina")]
        [System.ComponentModel.DataAnnotations.Required]
        public int porudzbinaID { get; set; }
    }
}
