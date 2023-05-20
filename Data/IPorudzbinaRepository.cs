using MakeupShop.Entities;

namespace MakeupShop.Data
{
    public interface IPorudzbinaRepository
    {
        List<Porudzbina> GetPorudzbinaList();
        List<Porudzbina> GetAllPorudzbinaList(int korisnikID);
        Porudzbina GetPorudzbinaById(int porudzbinaID);
        Porudzbina CreatePorudzbina(Porudzbina porudzbina);

        void UpdatePorudzbina(Porudzbina porudzbina);

        void DeletePorudzbina(int porudzbinaID);
        bool SaveChanges();
    }
}
