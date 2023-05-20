using MakeupShop.Entities;

namespace MakeupShop.Data
{
    public interface IListaZeljaProizvodRepository
    {
        List<ListaZeljaProizvod> GetListaZeljaProizvodList();
        List<ListaZeljaProizvod> GetAllListaZeljaProizvod(int korisnikID);
        ListaZeljaProizvod GetListaZeljaProizvodById(int listaProID);
        ListaZeljaProizvod CreateListaZeljaProizvod(ListaZeljaProizvod listaZeljaProizvod);

        void UpdateListaZeljaProizvod(ListaZeljaProizvod listaZeljaProizvod);

        void DeleteListaZeljaProizvod(int listaProID);
        bool SaveChanges();
    }
}
