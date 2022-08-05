using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Report;
using MJ_CAIS.ExternalWebServices.Contracts;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL.Signer;

namespace MJ_CAIS.ExternalWebServices
{
    public class ReportGenerationService: BaseAsyncService<ReportDTO, ReportDTO, ReportGridDTO, AReport, string, CaisDbContext>, IReportGenerationService
    {

        private readonly IPdfSigner _pdfSignerService;
        private readonly IPrintDocumentService _printerService;
        private readonly IReportRepository _reportRepository;
    

        public ReportGenerationService(IMapper mapper, 
            IPdfSigner pdfSignerService, IPrintDocumentService printerService, IReportRepository reportRepository)
            : base(mapper, reportRepository)
        {       

            _pdfSignerService = pdfSignerService;
            _printerService = printerService;
            _reportRepository = reportRepository;


        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            throw new NotImplementedException();
        }

        public async Task<byte[]> CreateReport(string reportID)
        {
            var report = await _reportRepository.GetReport(reportID);//.SingleOrDefaultAsync<AReport>(x => x.Id == reportID); 
           
            if (report == null)
            {
                //todo: resources and EH
                throw new Exception($"Certificate with ID {reportID} does not exist.");
            }
            var signingCertificateName = (await _reportRepository.SingleOrDefaultAsync<GSystemParameter>(x => 
            x.Code == SystemParametersConstants.SystemParametersNames.SYSTEM_SIGNING_CERTIFICATE_NAME))?.ValueString;

            //(await dbContext.GSystemParameters
            //.FirstOrDefaultAsync(x => x.Code == SystemParametersConstants.SystemParametersNames.SYSTEM_SIGNING_CERTIFICATE_NAME))?.ValueString;
            if (string.IsNullOrEmpty(signingCertificateName))
            {//todo: EH & resources
                throw new Exception($"Системният параметър {SystemParametersConstants.SystemParametersNames.SYSTEM_SIGNING_CERTIFICATE_NAME} не е настроен.");
            }

            var result = await CreateReport(report, signingCertificateName);



            await _reportRepository.SaveChangesAsync();
          
            
            return result;
        }


        private async Task<byte[]> CreateReport(AReport report, string signingCertificateName)
        {

            byte[] contentReport;
            contentReport = await CreatePdf(report.Id,   signingCertificateName);
            bool isExistingDoc = false;
            bool isExistingContent = false;
            DDocument doc;
            if (!string.IsNullOrEmpty(report.DocId))
            {
                var currentDocument = await _reportRepository.SingleOrDefaultAsync<DDocument>(d => d.Id == report.DocId);

                //await dbContext.DDocuments.FirstOrDefaultAsync(d => d.Id == report.DocId);
                if (currentDocument != null)
                {
                    doc = currentDocument;
                    isExistingDoc = true;
                }
                else
                {
                    doc = new DDocument();
                    doc.Id = BaseEntity.GenerateNewId();
                    doc.Name = "Справка за съдимост";
                }

            }
            else
            {
                doc = new DDocument();
                doc.Id = BaseEntity.GenerateNewId();
                doc.Name = "Справка за съдимост";
            }

            DDocContent content;
            if (!isExistingDoc || doc.DocContentId == null)
            {
                content = new DDocContent();
                content.Id = BaseEntity.GenerateNewId();

            }
            else
            {
                var currentContent = await _reportRepository.SingleOrDefaultAsync<DDocContent>(d => d.Id == doc.DocContentId);
                //await dbContext.DDocContents.FirstOrDefaultAsync(d => d.Id == doc.DocContentId);
                if (currentContent != null)
                {
                    content = currentContent;
                    isExistingContent = true;
                }
                else
                {
                    content = new DDocContent();
                    content.Id = BaseEntity.GenerateNewId();
                }

            }
            content.MimeType = "application/pdf";
            content.Content = contentReport;
            content.Bytes = content.Content.Length;


            doc.DocContentId = content.Id;
            doc.DocContent = content;

            report.DocId = doc.Id;
            report.Doc = doc;
         

    
            if (isExistingContent)
            {
                content.EntityState = EntityStateEnum.Modified;
                if (content.ModifiedProperties == null)
                {
                    content.ModifiedProperties = new List<string>();
                }
                content.ModifiedProperties.Add(nameof(content.MimeType));
                content.ModifiedProperties.Add(nameof(content.Content));
                content.ModifiedProperties.Add(nameof(content.Bytes));

                //dbContext.DDocContents.Update(content);
            }
            else
            {
                content.EntityState = EntityStateEnum.Added;
                //dbContext.DDocContents.Add(content);
            }
            if (isExistingDoc)
            {
                doc.EntityState = EntityStateEnum.Modified;
                if (doc.ModifiedProperties == null)
                {
                    doc.ModifiedProperties = new List<string>();
                }
                doc.ModifiedProperties.Add(nameof(doc.DocContentId));
                // dbContext.DDocuments.Update(doc);
            }
            else
            {
                doc.EntityState = EntityStateEnum.Added;
                //dbContext.DDocuments.Add(doc);
            }
            //dbContext.ACertificates.Update(certificate);
            report.EntityState = EntityStateEnum.Modified;


            if (report.ModifiedProperties == null)
            {
                report.ModifiedProperties = new List<string>();
            }
            report.ModifiedProperties.Add(nameof(report.DocId));

            report.StatusCode = ReportApplicationConstants.Status.ReadyReport;

            if(report.AReportStatusHes == null)
            {
                report.AReportStatusHes = new List<AReportStatusH>();
            }

            report.AReportStatusHes.Add(new AReportStatusH
            {
                Id= BaseEntity.GenerateNewId(),
                EntityState = EntityStateEnum.Added,
                AReportId = report.Id,
                StatusCode = ReportApplicationConstants.Status.ReadyReport,
                Descr = "Създаден PDF документ",
                ReportOrder = report.AReportStatusHes.Count(x=>x.StatusCode== ReportApplicationConstants.Status.ReadyReport) + 1
            });


            //_reportRepository.ApplyChanges(content, new List<IBaseIdEntity>());
            //_reportRepository.ApplyChanges(doc, new List<IBaseIdEntity>());
            _reportRepository.ApplyChanges(report, new List<IBaseIdEntity>(),true);

            //await _reportRepository.SaveChangesAsync();
            return contentReport;

        }

        private async Task<byte[]> CreatePdf(string reportid, string signingCertificateName)
        {
            byte[] fileArray = await _printerService.PrintReport(reportid);
            //todo: кои полета да се добавят за валидиране?!
            //fileArray = _pdfSignerService.SignPdf(fileArray, signingCertificateName,
            //    new Dictionary<string, string>() { { "report_id", reportid } });

            return fileArray;
        }
    }
}
