using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeupShop.Models
{
    public class BrendDto
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int brendID { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string imeBrenda { get; set; }

    }
}
