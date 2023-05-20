using MakeupShop.Entities;

namespace MakeupShop.Data
{
    public interface IListaZeljaRepository
    {
        List<ListaZelja> GetListaZeljaList();
        ListaZelja GetListaZeljaById(int listaZeljaID);
        ListaZelja CreateListaZelja(ListaZelja listaZelja);

        void UpdateListaZelja(ListaZelja listaZelja);

        void DeleteListaZelja(int listaZeljaID);
        bool SaveChanges();
    }
}
