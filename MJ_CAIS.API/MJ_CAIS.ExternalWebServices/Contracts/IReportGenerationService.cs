using MJ_CAIS.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.ExternalWebServices.Contracts
{
    public interface IReportGenerationService
    {
        Task<byte[]> CreateReport(string reportID);
        Task<byte[]> CreateReport(AReport report, string signingCertificateName);
    }
}
