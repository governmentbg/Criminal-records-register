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
    }
}
