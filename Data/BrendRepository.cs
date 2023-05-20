using AutoMapper;
using MakeupShop.Entities;

namespace MakeupShop.Data
{
    public class BrendRepository : IBrendRepository
    {
            private readonly MakeupShopContext context;
            private readonly IMapper mapper;
            public BrendRepository(MakeupShopContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }
            public bool SaveChanges()
            {
                return context.SaveChanges() > 0;
            }
            public Brend CreateBrend(Brend brend)
            {
                var createdEntity = context.Add(brend);
                context.SaveChanges();
                return mapper.Map<Brend>(createdEntity.Entity);
            }

            public void DeleteBrend(int brendID)
            {
                var brend = GetBrendById(brendID);
                context.Remove(brend);
                context.SaveChanges();
            }
            public Brend GetBrendById(int brendID)
            {
                return context.Brend.FirstOrDefault(b => b.brendID == brendID);
            }

            public List<Brend> GetBrendList()
            {
                return context.Brend.ToList();
            }
            public void UpdateBrend(Brend brend)
            {
                //nije potrebno posebno implementirati update
            }
    }
}

