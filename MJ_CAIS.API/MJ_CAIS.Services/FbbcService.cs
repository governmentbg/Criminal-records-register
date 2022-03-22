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

        public async Task<IQueryable<FbbcDocumentDTO>> GetDocumentsByFbbcIdAsync(string aId)
        {
            var dbContext = _fbbcRepository.GetDbContext();

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
                
            var context = _fbbcRepository.GetDbContext();
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

            context.Add(document);
            context.Add(documentContent);
            await context.SaveChangesAsync();
        }

        public async Task DeleteDocumentAsync(string documentId)
        {
            var context = _fbbcRepository.GetDbContext();

            var document = await context.Set<DDocument>().AsNoTracking()
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

            await context.SaveEntityAsync(document, true);
        }

        public async Task<FbbcDocumentDTO> GetDocumentContentAsync(string documentId)
        {
            var context = _fbbcRepository.GetDbContext();

            var document = await context.Set<DDocument>().AsNoTracking()
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
    }
}
