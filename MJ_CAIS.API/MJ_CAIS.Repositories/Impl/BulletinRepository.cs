using MJ_CAIS.DataAccess;
using MJ_CAIS.Entities;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public class BulletinRepository : BaseAsyncRepository<Bulletin, CaisDbContext>, IBulletinRepository
    {
        public BulletinRepository(CaisDbContext context) : base(context)
        {
        }

        public override IQueryable<Bulletin> SelectAllAsync()
        {
            // TODO: 
            var data = new Bulletin[]
            {
                new Bulletin() { Id = "1", FirstName = "Ivan", FamilyName = "Ivanov" },
                new Bulletin() { Id = "2", FirstName = "Petar", FamilyName = "Petrov" },
            };

            return data.AsQueryable();
        }
    }
}
