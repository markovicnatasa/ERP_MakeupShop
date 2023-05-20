using MakeupShop.Entities;

namespace MakeupShop.Data
{
    public interface IRecenzijaRepository
    {
        List<Recenzija> GetRecenzijaList();
        Recenzija GetRecenzijaById(int recenzijaID);
        Recenzija CreateRecenzija(Recenzija recenzija);

        void UpdateRecenzija(Recenzija recenzija);

        void DeleteRecenzija(int recenzijaID);
        bool SaveChanges();
    }
}
