using MakeupShop.Entities;

namespace MakeupShop.Data
{
    public interface IPlacanjeRepository
    {
        List<Placanje> GetPlacanjeList();
        Placanje GetPlacanjeById(int placanjeID);
        Placanje CreatePlacanje(Placanje placanje);

        void UpdatePlacanje(Placanje placanje);

        void DeletePlacanje(int placanjeID);
        bool SaveChanges();
    }
}
