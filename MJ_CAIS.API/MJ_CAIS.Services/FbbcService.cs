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

namespace MJ_CAIS.Services
{
    public class FbbcService : BaseAsyncService<FbbcDTO, FbbcDTO, FbbcGridDTO, Fbbc, string, CaisDbContext>, IFbbcService
    {
        private readonly IFbbcRepository _fbbcRepository;

        public FbbcService(IMapper mapper, IFbbcRepository fbbcRepository)
            : base(mapper, fbbcRepository)
        {
            _fbbcRepository = fbbcRepository;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }

        public virtual async Task<IgPageResult<FbbcGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<FbbcGridDTO> aQueryOptions, string statusId)
        {
            var entityQuery = this.GetSelectAllQueriable().Where(x => x.StatusCode == statusId);
            var baseQuery = entityQuery.ProjectTo<FbbcGridDTO>(mapperConfiguration);
            var resultQuery = await this.ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<FbbcGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
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
    }
}
