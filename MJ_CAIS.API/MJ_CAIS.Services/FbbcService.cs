using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DTO.Fbbc;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.Services.Contracts.Utils;
using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DTO.EcrisMessage;
using MJ_CAIS.AutoMapperContainer;
using MJ_CAIS.DTO.Person;
using static MJ_CAIS.Common.Constants.PersonConstants;

namespace MJ_CAIS.Services
{
    public class FbbcService : BaseAsyncService<FbbcDTO, FbbcDTO, FbbcGridDTO, Fbbc, string, CaisDbContext>, IFbbcService
    {
        private readonly IFbbcRepository _fbbcRepository;
        private readonly IEcrisMessageRepository _ecrisMessageRepository;
        private readonly IPersonService _personService;

        public FbbcService(IMapper mapper, IFbbcRepository fbbcRepository, IEcrisMessageRepository ecrisMessageRepository, IPersonService personService)
            : base(mapper, fbbcRepository)
        {
            _fbbcRepository = fbbcRepository;
            _ecrisMessageRepository = ecrisMessageRepository;
            _personService = personService;
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
            await this.SaveEntityAsync(entity);

            dbContext.Entry(entity).State = EntityState.Detached;
            entity.Version = 1;
            entity.EntityState = EntityStateEnum.Modified;
            entity.ModifiedProperties = new List<string> { nameof(entity.Version) };

            await UpdatePersonAsync(aInDto, entity);
            return entity.Id;
        }

        public override async Task UpdateAsync(string aId, FbbcDTO aInDto)
        {
            var entity = mapper.MapToEntity<FbbcDTO, Fbbc>(aInDto, false);
            await dbContext.SaveEntityAsync(entity);

            await UpdatePersonAsync(aInDto, entity);
        }

        public async Task<FbbcDTO> SelectWithPersonDataAsync(string personId)
        {
            var result = new FbbcDTO();
            var person = await _personService.SelectWithBirthInfoAsync(personId);
            result.Person = person ?? new PersonDTO();
            return result;
        }

        public async Task<IQueryable<EcrisMessageGridDTO>> GetEcrisMessagesByFbbcIdAsync(string aId)
        {
            var result = dbContext.EEcrisMessages.AsNoTracking()
                .Where(x => x.FbbcId == aId)
                .ProjectTo<EcrisMessageGridDTO>(mapper.ConfigurationProvider);

            return await Task.FromResult(result);
        }

        public async Task<IQueryable<FbbcDocumentDTO>> GetDocumentsByFbbcIdAsync(string aId)
        {
            var result = dbContext.DDocuments
                .AsNoTracking()
                .Include(x => x.DocType)
                .Include(x => x.DocContent)
                .Where(x => x.FbbcId == aId)
                .ProjectTo<FbbcDocumentDTO>(mapper.ConfigurationProvider);

            return await Task.FromResult(result);
        }

        public async Task InsertFbbcDocumentAsync(string fbbcId, FbbcDocumentDTO aInDto)
        {
            if (aInDto == null)
            {
                throw new ArgumentNullException(nameof(aInDto));
            }

            if (aInDto.DocumentContent?.Length == 0)
            {
                throw new ArgumentNullException("Documetn is empty");
            }

            var docContentId = string.IsNullOrEmpty(aInDto.DocumentContentId) ?
                Guid.NewGuid().ToString() : aInDto.DocumentContentId;

            var document = mapper.Map<FbbcDocumentDTO, DDocument>(aInDto);
            document.FbbcId = fbbcId;
            document.DocContentId = docContentId;

            var documentContent = new DDocContent()
            {
                Id = docContentId,
                Content = aInDto.DocumentContent,
                MimeType = aInDto.MimeType
            };

            dbContext.Add(document);
            dbContext.Add(documentContent);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteDocumentAsync(string documentId)
        {
            var document = await dbContext.Set<DDocument>().AsNoTracking()
                .Include(x => x.DocContent)
                .FirstOrDefaultAsync(x => x.Id == documentId);

            if (document == null)
            {
                throw new ArgumentException($"Document with id: {documentId} is missing");
            }

            document.EntityState = EntityStateEnum.Deleted;
            if (document.DocContent != null)
            {
                document.DocContent.EntityState = EntityStateEnum.Deleted;
            }

            await dbContext.SaveEntityAsync(document, true);
        }

        public async Task<FbbcDocumentDTO> GetDocumentContentAsync(string documentId)
        {
            var document = await dbContext.Set<DDocument>().AsNoTracking()
                .Include(x => x.DocContent)
                .FirstOrDefaultAsync(x => x.Id == documentId);

            if (document == null || document.DocContent == null) return null;

            return new FbbcDocumentDTO
            {
                Name = document.Name,
                DocumentContent = document.DocContent.Content,
                MimeType = document.DocContent.MimeType
            };
        }

        public async Task ChangeStatusAsync(string aInDto, string statusId)
        {
            var fbbc = await dbContext.Fbbcs
               .FirstOrDefaultAsync(x => x.Id == aInDto);

            if (fbbc == null)
            {
                throw new ArgumentException($"Fbbc with id: {aInDto} is missing");
            }

            fbbc.StatusCode = statusId;

            if (statusId == EntityStateEnum.Deleted.ToString())
            {
                fbbc.DestroyedDate = DateTime.Now;
            }

            await dbContext.SaveChangesAsync();
        }

        private async Task UpdatePersonAsync(FbbcDTO aInDto, Fbbc entity)
        {

            var personDto = aInDto.Person;
            // preate person object, apply changes
            var person = await _personService.CreatePersonAsync(personDto);

            foreach (var personIdObj in person.PPersonIds)
            {
                //personIdObj.Id = BaseEntity.GenerateNewId();
                //personIdObj.EntityState = EntityStateEnum.Added;

                if (personIdObj.PidTypeId == PidType.Egn)
                {
                    entity.ModifiedProperties.Add(nameof(entity.PersonId));
                    entity.PersonId = personIdObj.Id;
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

                dbContext.ApplyChanges(personIdObj, new List<IBaseIdEntity>());
            }

            dbContext.ApplyChanges(entity, new List<IBaseIdEntity>());
            await dbContext.SaveChangesAsync();
        }
    }
}
