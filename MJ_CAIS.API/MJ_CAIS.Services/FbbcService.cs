using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.AutoMapperContainer;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EcrisMessage;
using MJ_CAIS.DTO.Fbbc;
using MJ_CAIS.DTO.Person;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;
using static MJ_CAIS.Common.Constants.PersonConstants;

namespace MJ_CAIS.Services
{
    public class FbbcService : BaseAsyncService<FbbcDTO, FbbcDTO, FbbcGridDTO, Fbbc, string, CaisDbContext>, IFbbcService
    {
        private readonly IFbbcRepository _fbbcRepository;
        private readonly IEcrisMessageRepository _ecrisMessageRepository;
        private readonly IManagePersonService _managePersonService;

        public FbbcService(IMapper mapper, IFbbcRepository fbbcRepository,
            IEcrisMessageRepository ecrisMessageRepository,
            IManagePersonService managePersonService)
            : base(mapper, fbbcRepository)
        {
            _fbbcRepository = fbbcRepository;
            _ecrisMessageRepository = ecrisMessageRepository;
            _managePersonService = managePersonService;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }

        public virtual async Task<IgPageResult<FbbcGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<FbbcGridDTO> aQueryOptions, string statusId)
        {
            var entityQuery = await _fbbcRepository.SelectByStatusCodeAsync(statusId);
            var resultQuery = await this.ApplyOData(entityQuery, aQueryOptions);
            var pageResult = new IgPageResult<FbbcGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, entityQuery, resultQuery);
            return pageResult;
        }

        public override async Task<string> InsertAsync(FbbcDTO aInDto)
        {
            var entity = mapper.MapToEntity<FbbcDTO, Fbbc>(aInDto, true);
            await _fbbcRepository.SaveEntityAsync(entity,false);
            //todo: защо се прави това тук?!
            //dbContext.Entry(entity).State = EntityState.Detached;
            entity.EntityState = EntityStateEnum.Detached;
            entity.Version = 1;
            entity.EntityState = EntityStateEnum.Modified;
            entity.ModifiedProperties = new List<string> { nameof(entity.Version) };

            await UpdatePersonAsync(aInDto, entity);
            return entity.Id;
        }

        public override async Task UpdateAsync(string aId, FbbcDTO aInDto)
        {
            var entity = mapper.MapToEntity<FbbcDTO, Fbbc>(aInDto, false);
            await _fbbcRepository.SaveEntityAsync(entity,false);

            await UpdatePersonAsync(aInDto, entity);
        }

        public async Task<FbbcDTO> SelectWithPersonDataAsync(string personId)
        {
            var result = new FbbcDTO();
            var person = await _managePersonService.SelectWithBirthInfoAsync(personId);
            result.Person = person ?? new PersonDTO();
            return result;
        }

        public async Task<IQueryable<EcrisMessageGridDTO>> GetEcrisMessagesByFbbcIdAsync(string aId)
        {
            var result =(await _fbbcRepository.FindAsync<EEcrisMessage>(x => x.FbbcId == aId))
                //dbContext.EEcrisMessages.AsNoTracking()
                //.Where(x => x.FbbcId == aId)
                .ProjectTo<EcrisMessageGridDTO>(mapper.ConfigurationProvider);

            return await Task.FromResult(result);
        }

        //public async Task<IQueryable<FbbcDocumentDTO>> GetDocumentsByFbbcIdAsync(string aId)
        //{
        //    var result = dbContext.DDocuments
        //        .AsNoTracking()
        //        .Include(x => x.DocType)
        //        .Include(x => x.DocContent)
        //        .Where(x => x.FbbcId == aId)
        //        .ProjectTo<FbbcDocumentDTO>(mapper.ConfigurationProvider);

        //    return await Task.FromResult(result);
        //}

     

     
     

        public async Task ChangeStatusAsync(string aInDto, string statusId)
        {//todo: може би да се вика селект метода?!
            var fbbc = await _fbbcRepository.SingleOrDefaultAsync<Fbbc>(x => x.Id == aInDto);
            //await dbContext.Fbbcs
               //.FirstOrDefaultAsync(x => x.Id == aInDto);

            if (fbbc == null)
            {
                throw new ArgumentException($"Fbbc with id: {aInDto} is missing");
            }

            fbbc.StatusCode = statusId;
            fbbc.EntityState = EntityStateEnum.Modified;
            if (fbbc.ModifiedProperties == null)
            {
                fbbc.ModifiedProperties = new List<string>();
            }
            fbbc.ModifiedProperties.Add(nameof(fbbc.StatusCode));

            if (statusId == EntityStateEnum.Deleted.ToString())
            {
                fbbc.DestroyedDate = DateTime.Now;
                fbbc.ModifiedProperties.Add(nameof(fbbc.DestroyedDate));
            }

            await _fbbcRepository.SaveEntityAsync(fbbc, false);
            //await _fbbcRepository.SaveChangesAsync();
        }

        private async Task UpdatePersonAsync(FbbcDTO aInDto, Fbbc entity)
        {

            var personDto = aInDto.Person;
            // create person object, apply changes
            var person = await _managePersonService.CreatePersonAsync(personDto);

            foreach (var personIdObj in person.PPersonIds)
            {
                //personIdObj.Id = BaseEntity.GenerateNewId();
                //personIdObj.EntityState = EntityStateEnum.Added;
                if (entity.ModifiedProperties == null)
                {
                    entity.ModifiedProperties = new List<string>();
                }
                if (entity.ModifiedProperties == null)
                {
                    entity.ModifiedProperties = new List<string>();
                }
                if (personIdObj.PidTypeId == PidType.Egn)
                {
                    entity.ModifiedProperties.Add(nameof(entity.PersonId));
                    entity.Person = personIdObj;

                }
                else if (personIdObj.PidTypeId == PidType.Suid)
                {
                    entity.ModifiedProperties.Add(nameof(entity.SuidId));
                    entity.ModifiedProperties.Add(nameof(entity.Suid));
                    entity.Suid = personIdObj.Pid;
                    entity.SuidId = personIdObj.Id;
                    entity.SuidNavigation = personIdObj;
                }

                _fbbcRepository.ApplyChanges(personIdObj, new List<IBaseIdEntity>());
            }

            _fbbcRepository.ApplyChanges(entity, new List<IBaseIdEntity>());
            await _fbbcRepository.SaveChangesAsync();
        }
    }
}
