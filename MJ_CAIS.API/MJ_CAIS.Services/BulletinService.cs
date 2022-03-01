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
            => await UpdateBulletinAsync(aInDto, true);

        public override async Task UpdateAsync(string aId, BulletinDTO aInDto)
            => await UpdateBulletinAsync(aInDto, false);

        public async Task<IQueryable<OffenceDTO>> GetOffencesByBulletinIdAsync(string aId)
        {
            var dbContext = _bulletinRepository.GetDbContext();

            var result = dbContext.BOffences
                .AsNoTracking()
                .Include(x => x.OffenceCat)
                .Include(x => x.EcrisOffCat)
                .Include(x => x.OffPlaceCountry)
                .Include(x => x.OffPlaceSubdiv)
                .Include(x => x.OffPlaceCity)
                .Include(x => x.OffLvlCompl)
                .Include(x => x.OffLvlPart)
                .Where(x => x.BulletinId == aId)
                .ProjectTo<OffenceDTO>(mapper.ConfigurationProvider);

            return await Task.FromResult(result);
        }

        public async Task<IQueryable<SanctionDTO>> GetSanctionsByBulletinIdAsync(string aId)
        {
            var dbContext = _bulletinRepository.GetDbContext();

            var result = dbContext.BSanctions
                .AsNoTracking()
                .Include(x => x.EcrisSanctCateg)
                .Include(x => x.SanctActivity)
                .Include(x => x.SanctCategory)
                .Include(x => x.SanctProbCateg)
                .Include(x => x.SanctProbMeasure)
                .Where(x => x.BulletinId == aId)
                .ProjectTo<SanctionDTO>(mapper.ConfigurationProvider);

            return await Task.FromResult(result);
        }

        private async Task<string> UpdateBulletinAsync(BulletinDTO aInDto, bool isAdded)
        {
            var entity = mapper.MapToEntity<BulletinDTO, BBulletin>(aInDto, isAdded);

            entity.BOffences = mapper.MapTransactions<OffenceDTO, BOffence>(aInDto.OffancesTransactions);
            //entity.BSanctions = mapper.MapTransactions<SanctionDTO, BSanction>(aInDto.SanctionsTransactions);

            await SaveEntityAsync(entity);
            return entity.Id;
        }
    }
}
