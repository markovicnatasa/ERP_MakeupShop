using AutoMapper;
using MakeupShop.Entities;

namespace MakeupShop.Data
{
    public class ProizvodRepository : IProizvodRepository
    {
        private readonly MakeupShopContext context;
        private readonly IMapper mapper;
        public ProizvodRepository(MakeupShopContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public Proizvod CreateProizvod(Proizvod proizvod)
        {
            var createdEntity = context.Add(proizvod);
            context.SaveChanges();
            return mapper.Map<Proizvod>(createdEntity.Entity);
        }

        public void DeleteProizvod(int proizvodID)
        {
            var proizvod = GetProizvodById(proizvodID);
            context.Remove(proizvod);
            context.SaveChanges();
        }
        public Proizvod GetProizvodById(int proizvodID)
        {
            return context.Proizvod.FirstOrDefault(p => p.proizvodID == proizvodID);
        }

        public List<Proizvod> GetProizvodList()
        {
            return context.Proizvod.ToList();
        }
        public void UpdateProizvod(Proizvod proizvod)
        {
            //nije potrebno posebno implementirati update
        }
    }
}
