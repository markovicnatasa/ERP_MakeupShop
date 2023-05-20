using MakeupShop.Entities;

namespace MakeupShop.Data
{
    public interface IProizvodRepository
    {
        List<Proizvod> GetProizvodList();
        Proizvod GetProizvodById(int proizvodID);
        Proizvod CreateProizvod(Proizvod proizvod);

        void UpdateProizvod(Proizvod proizvod);

        void DeleteProizvod(int proizvodID);
        bool SaveChanges();
    }
}
