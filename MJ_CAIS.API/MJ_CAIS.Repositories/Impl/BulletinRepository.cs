using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace MJ_CAIS.Repositories.Impl
{
    public class BulletinRepository : BaseAsyncRepository<BBulletin, CaisDbContext>, IBulletinRepository
    {
        public BulletinRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public async Task ChangeStatusAsync(string aInDto, string statusId)
        {
            var bulletin = await _dbContext.BBulletins
                .FirstOrDefaultAsync(x => x.Id == aInDto);

            if (bulletin == null)
                throw new ArgumentException($"Bulletin with id: {aInDto} is missing");

            bulletin.StatusId = statusId;
            await _dbContext.SaveChangesAsync();
        }
    }
}
