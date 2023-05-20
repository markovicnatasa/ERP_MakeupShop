using MakeupShop.Entities;

namespace MakeupShop.Data
{
    public interface IBrendRepository
    {
        List<Brend> GetBrendList();
        Brend GetBrendById(int brendID);
        Brend CreateBrend(Brend brend);

        void UpdateBrend(Brend brend);

        void DeleteBrend(int brendID);
        bool SaveChanges();
    }
}
