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
        Task<byte[]> PrintCertificate(string certificateID, string checkUrl, JasperReportsNames reportName);
        Task<byte[]> PrintReport(string reportId, JasperReportsNames reportName);
    }
}
