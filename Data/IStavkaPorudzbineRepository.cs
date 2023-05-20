using MakeupShop.Entities;

namespace MakeupShop.Data
{
    public interface IStavkaPorudzbineRepository
    {
        List<StavkaPorudzbine> GetStavkaPorudzbineList();
        List<StavkaPorudzbine> GetAllStavkaPorudzbine(int korisnikID);
        StavkaPorudzbine GetStavkaPorudzbineById(int stavkaPorudzbineID);
        StavkaPorudzbine CreateStavkaPorudzbine(StavkaPorudzbine stavkaPorudzbine);

        void UpdateStavkaPorudzbine(StavkaPorudzbine stavkaPorudzbine);

        void DeleteStavkaPorudzbine(int stavkaPorudzbineID);
        bool SaveChanges();
    }
}
