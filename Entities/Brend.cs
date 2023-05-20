using MakeupShop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeupShop.Entities
{
    public class Brend
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int brendID { get; set; }
        public string imeBrenda { get; set; }

    }
}
