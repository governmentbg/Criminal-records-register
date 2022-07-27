using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.Common.Exceptions;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.DTO.Documents;
using MJ_CAIS.DTO.Fbbc;
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
        private readonly IDDocumentRepository _documentsRepository;
        public DocumentService(IMapper mapper, IDDocumentRepository  documentsRepository)
                                             : base(mapper, documentsRepository)
        {
            _documentsRepository = documentsRepository;
        }

        public async Task<IQueryable<DocumentDTO>> GetDocumentsByApplicationIdAsync(string aId)
        {
            
            IQueryable<DocumentDTO> result = _documentsRepository.GetDocumentDataByApplicationID(aId)
                .ProjectTo<DocumentDTO>(mapper.ConfigurationProvider);

            return await Task.FromResult(result);
        }
        public async Task<IQueryable<DocumentDTO>> GetDocumentsByFbbcIdAsync(string aId)
        {

            IQueryable<DocumentDTO> result = _documentsRepository.GetDocumentDataByFbbcID(aId)
                .ProjectTo<DocumentDTO>(mapper.ConfigurationProvider);

            return await Task.FromResult(result);
        }



        public async Task InsertDocumentAsync(string? applicationId, string? bulletinId, string? fbbcId, DocumentDTO aInDto)
        {
            if (aInDto == null)
            {
                throw new ArgumentNullException(nameof(aInDto));
            }
            if(string.IsNullOrEmpty(applicationId + bulletinId + fbbcId))
            {
                throw new ArgumentNullException("Трябва да е въведен бюлетин, заявление или FBBC запис.");
            }

            if (aInDto.DocumentContent?.Length == 0)
            {
                throw new ArgumentNullException("Document is empty");
            }

            var docContentId = string.IsNullOrEmpty(aInDto.DocumentContentId)
                ? Guid.NewGuid().ToString()
                : aInDto.DocumentContentId;

            var document = mapper.Map<DocumentDTO, DDocument>(aInDto);
            document.ApplicationId = applicationId;
            document.FbbcId = fbbcId;
            document.BulletinId = bulletinId;
            document.DocContentId = docContentId;
            document.EntityState = EntityStateEnum.Added;

            var documentContent = new DDocContent
            {
                Id = docContentId,
                Content = aInDto.DocumentContent,
                MimeType = aInDto.MimeType
            };
            documentContent.EntityState = EntityStateEnum.Added;

            _documentsRepository.ApplyChanges(documentContent, new List<IBaseIdEntity>());
            _documentsRepository.ApplyChanges(document, new List<IBaseIdEntity>());
            await _documentsRepository.SaveChangesAsync();
            //dbContext.Add(document);
            //dbContext.Add(documentContent);
            //await dbContext.SaveChangesAsync();
        }

        public async Task DeleteDocumentAsync(string documentId)
        {
            //todo:  има същия метод в бюлетините, виж по-долу
            DDocument document = await _documentsRepository.GetDocumentWithContentByID(documentId);

            if (document == null)
            {
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.documentDoesNotExist, documentId));

                // throw new ArgumentException($"Document with id: {documentId} is missing");
            }

            document.EntityState = EntityStateEnum.Deleted;
            if (document.DocContent != null)
            {
                document.DocContent.EntityState = EntityStateEnum.Deleted;
            }

            await _documentsRepository.SaveEntityAsync(document, true);
        }

    

        public async Task<DocumentDTO> GetDocumentContentAsync(string documentId)
        {
           

            var document = await _documentsRepository.GetDocumentWithContentByID(documentId);

            if (document == null || document.DocContent == null)
            {
                return null;
            }

            return new DocumentDTO
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
        //метод от бюлетините:
        //public async Task DeleteDocumentAsync(string documentId)
        //{
        //    var document = await _bulletinRepository.SelectDocumentAsync(documentId);
        //    if (document == null)
        //        throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.documentDoesNotExist, documentId));

        //    document.EntityState = EntityStateEnum.Deleted;
        //    if (document.DocContent != null)
        //    {
        //        document.DocContent.EntityState = EntityStateEnum.Deleted;
        //    }

        //    await dbContext.SaveEntityAsync(document, true);
        //}
        //public async Task<DocumentDTO> GetDocumentContentAsync(string documentId)
        //{
        //    var document = await _bulletinRepository.SelectDocumentAsync(documentId);
        //    if (document == null) return null;

        //    var documentDTO = mapper.Map<DocumentDTO>(document);
        //    documentDTO.DocumentContent = document.DocContent.Content;
        //    return documentDTO;
        //}
        //от фббц
        //public async Task DeleteDocumentAsync(string documentId)
        //{
        //    var document = await dbContext.Set<DDocument>().AsNoTracking()
        //        .Include(x => x.DocContent)
        //        .FirstOrDefaultAsync(x => x.Id == documentId);

        //    if (document == null)
        //    {
        //        throw new ArgumentException($"Document with id: {documentId} is missing");
        //    }

        //    document.EntityState = EntityStateEnum.Deleted;
        //    if (document.DocContent != null)
        //    {
        //        document.DocContent.EntityState = EntityStateEnum.Deleted;
        //    }

        //    await dbContext.SaveEntityAsync(document, true);
        //}
        //public async Task<FbbcDocumentDTO> GetDocumentContentAsync(string documentId)
        //{
        //    var document = await dbContext.Set<DDocument>().AsNoTracking()
        //        .Include(x => x.DocContent)
        //        .FirstOrDefaultAsync(x => x.Id == documentId);

        //    if (document == null || document.DocContent == null) return null;

        //    return new FbbcDocumentDTO
        //    {
        //        Name = document.Name,
        //        DocumentContent = document.DocContent.Content,
        //        MimeType = document.DocContent.MimeType
        //    };
        //}
        //public async Task InsertFbbcDocumentAsync(string fbbcId, DocumentDTO aInDto)
        //{
        //    if (aInDto == null)
        //    {
        //        throw new ArgumentNullException(nameof(aInDto));
        //    }

        //    if (aInDto.DocumentContent?.Length == 0)
        //    {
        //        throw new ArgumentNullException("Documetn is empty");
        //    }

        //    var docContentId = string.IsNullOrEmpty(aInDto.DocumentContentId) ?
        //        Guid.NewGuid().ToString() : aInDto.DocumentContentId;

        //    var document = mapper.Map<DocumentDTO, DDocument>(aInDto);
           
        //    document.DocContentId = docContentId;

        //    var documentContent = new DDocContent()
        //    {
        //        Id = docContentId,
        //        Content = aInDto.DocumentContent,
        //        MimeType = aInDto.MimeType
        //    };
            
        //    document.FbbcId = fbbcId;

        //    dbContext.Add(document);
        //    dbContext.Add(documentContent);
        //    await dbContext.SaveChangesAsync();
        //}
    }
}
