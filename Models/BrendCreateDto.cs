using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeupShop.Models
{
    public class BrendCreateDto
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string imeBrenda { get; set; }
    }
}
