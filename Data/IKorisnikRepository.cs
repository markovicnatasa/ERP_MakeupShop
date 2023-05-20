using MakeupShop.Entities;

namespace MakeupShop.Data
{
    public interface IKorisnikRepository
    {
        List<Korisnik> GetKorisnikList();
        Korisnik GetKorisnikById(int korisnikID);
        Korisnik CreateKorisnik(Korisnik korisnik);

        void UpdateKorisnik(Korisnik korisnik);

        void DeleteKorisnik(int korisnikID);
        bool SaveChanges();
    }
}
