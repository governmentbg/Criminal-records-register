using MJ_CAIS.Common.Enums;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Certificate;
using MJ_CAIS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.ExternalWebServices.Contracts
{
    public interface IPrintDocumentService 
    {
        Task<byte[]> PrintApplication(string applicationID);
        Task<byte[]> PrintCertificate(string certificateID, string checkUrl);
        Task<byte[]> PrintElectronicCertificate(string certificateID, string checkUrl);
        Task<byte[]> PrintExternalElectronicCertificate(string certificateID, string checkUrl);
        Task<byte[]> PrintReport(string reportId);
        Task<byte[]> PrintBulletin(string bulletinID);

        Task<byte[]> PrintDailyReports(DateTime fromDate, DateTime toDate);
        Task<byte[]> PrintDailyCertificates(DateTime fromDate, DateTime toDate);
        Task<byte[]> PrintDailyBulletins(DateTime fromDate, DateTime toDate, string status);
        Task<byte[]> PrintDailyApplications(DateTime fromDate, DateTime toDate);
        Task<byte[]> PrintDailyReportApplications(DateTime fromDate, DateTime toDate);

    }
}
