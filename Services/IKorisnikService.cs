using MakeupShop.Entities;
using MakeupShop.Models;

namespace MakeupShop.Services
{
    public interface IKorisnikService
    {
       public LoginOdgovorDto Authentication(Korisnik korisnik);
       //public LoginOdgovorDto GenerateJwtToken(Korisnik korisnik);
    }
}
