using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Services
{
    //todo: този клас не е от генерираните!!! Кои да са обектите?!
    public class DocumentService :
         BaseAsyncService<ApplicationInDTO, ApplicationOutDTO, ApplicationGridDTO, DDocument, string, CaisDbContext>,
        IDocumentService
    {
        public DocumentService(IMapper mapper, IDDocumentRepository  documentsRepository)
                                             : base(mapper, documentsRepository)
        {
        }

        public async Task<IQueryable<ApplicationDocumentDTO>> GetDocumentsByApplicationIdAsync(string aId)
        {
            //todo:  това за тук ли е?! Някакъв DocumentsService?!
            var result = dbContext.DDocuments
                .AsNoTracking()
                .Include(x => x.DocType)
                .Include(x => x.DocContent)
                .Where(x => x.ApplicationId == aId)
                .ProjectTo<ApplicationDocumentDTO>(mapper.ConfigurationProvider);

            return await Task.FromResult(result);
        }


        public async Task InsertApplicationDocumentAsync(string applicationId, ApplicationDocumentDTO aInDto)
        {
            //todo:  това за тук ли е?! Някакъв DocumentsService?!
            if (aInDto == null)
            {
                throw new ArgumentNullException(nameof(aInDto));
            }

            if (aInDto.DocumentContent?.Length == 0)
            {
                throw new ArgumentNullException("Document is empty");
            }

            var docContentId = string.IsNullOrEmpty(aInDto.DocumentContentId)
                ? Guid.NewGuid().ToString()
                : aInDto.DocumentContentId;

            var document = mapper.Map<ApplicationDocumentDTO, DDocument>(aInDto);
            document.ApplicationId = applicationId;
            document.DocContentId = docContentId;

            var documentContent = new DDocContent
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
            //todo:  това за тук ли е?! Някакъв DocumentsService?!
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

        public async Task<ApplicationDocumentDTO> GetDocumentContentAsync(string documentId)
        {
            //todo:  това за тук ли е?! Някакъв DocumentsService?!
            var document = await dbContext.Set<DDocument>().AsNoTracking()
                .Include(x => x.DocContent)
                .FirstOrDefaultAsync(x => x.Id == documentId);

            if (document == null || document.DocContent == null)
            {
                return null;
            }

            return new ApplicationDocumentDTO
            {
                Name = document.Name,
                DocumentContent = document.DocContent.Content,
                MimeType = document.DocContent.MimeType
            };
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            throw new NotImplementedException();
        }
    }
}
