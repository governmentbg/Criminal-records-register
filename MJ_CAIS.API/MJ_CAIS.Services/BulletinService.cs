using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.AutoMapperContainer;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;

namespace MJ_CAIS.Services
{
    public class BulletinService : BaseAsyncService<BulletinDTO, BulletinDTO, BulletinGridDTO, BBulletin, string, CaisDbContext>, IBulletinService
    {
        private readonly IBulletinRepository _bulletinRepository;

        public BulletinService(IMapper mapper, IBulletinRepository bulletinRepository)
            : base(mapper, bulletinRepository)
        {
            _bulletinRepository = bulletinRepository;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }

        public override async Task<string> InsertAsync(BulletinDTO aInDto)
        {
            var isAdded = true;
            var entity = mapper.MapToEntity<BulletinDTO, BBulletin>(aInDto, isAdded);

            entity.BOffences = mapper.MapTransactions<BOffenceDTO, BOffence>(aInDto.OffancesTransactions);

            await SaveEntityAsync(entity);
            return entity.Id;
        }

        public override async Task UpdateAsync(string aId, BulletinDTO aInDto)
        {
            var isAdded = false;
            var entity = mapper.MapToEntity<BulletinDTO, BBulletin>(aInDto, isAdded);

            entity.BOffences = mapper.MapTransactions<BOffenceDTO, BOffence>(aInDto.OffancesTransactions);

            await SaveEntityAsync(entity);
        }

        public async Task<IQueryable<BOffenceDTO>> GetOffencesByBulletinIdAsync(string aId)
        {
            var dbContext = _bulletinRepository.GetDbContext();

            var result = dbContext.BOffences
                .AsNoTracking()
                .Where(x => x.BulletinId == aId)
                .ProjectTo<BOffenceDTO>(mapper.ConfigurationProvider);

            return await Task.FromResult(result);
        }
    }
}
