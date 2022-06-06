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
     
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            throw new NotImplementedException();
        }

        public async Task<byte[]> CreateReport(string reportID)
        {
            var report = await dbContext.AReports
            .FirstOrDefaultAsync(x => x.Id == reportID);
            if (report == null)
            {
                //todo: resources and EH
                throw new Exception($"Certificate with ID {reportID} does not exist.");
            }
            var signingCertificateName = (await dbContext.GSystemParameters
            .FirstOrDefaultAsync(x => x.Code == SystemParametersConstants.SystemParametersNames.SYSTEM_SIGNING_CERTIFICATE_NAME))?.ValueString;
            if (string.IsNullOrEmpty(signingCertificateName))
            {//todo: EH & resources
                throw new Exception($"Системният параметър {SystemParametersConstants.SystemParametersNames.SYSTEM_SIGNING_CERTIFICATE_NAME} не е настроен.");
            }

            var result = await CreateReport(report, signingCertificateName);



            dbContext.SaveChanges();

            return result;
        }


        public async Task<byte[]> CreateReport(AReport report, string signingCertificateName)
        {

            byte[] contentReport;
            contentReport = await CreatePdf(report.Id,  JasperReportsNames.Conviction_Report, signingCertificateName);
            bool isExistingDoc = false;
            bool isExistingContent = false;
            DDocument doc;
            if (!string.IsNullOrEmpty(report.DocId))
            {
                var currentDocument = await dbContext.DDocuments.FirstOrDefaultAsync(d => d.Id == report.DocId);
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
                var currentContent = await dbContext.DDocContents.FirstOrDefaultAsync(d => d.Id == doc.DocContentId);
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
         

            if (isExistingContent)
            {
                dbContext.DDocContents.Update(content);
            }
            else
            {
                dbContext.DDocContents.Add(content);
            }
            if (isExistingDoc)
            {
                dbContext.DDocuments.Update(doc);
            }
            else
            {
                dbContext.DDocuments.Add(doc);
            }
            dbContext.AReports.Update(report);

            return contentReport;

        }

        private async Task<byte[]> CreatePdf(string reportid, JasperReportsNames conviction_Report, string signingCertificateName)
        {
            byte[] fileArray = await _printerService.PrintReport(reportid, conviction_Report);
            //todo: кои полета да се добавят за валидиране?!
            //fileArray = _pdfSignerService.SignPdf(fileArray, signingCertificateName,
            //    new Dictionary<string, string>() { { "report_id", reportid } });

            return fileArray;
        }
    }
}
