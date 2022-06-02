using MJ_CAIS.Common.Constants;
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
    public interface ICertificateGenerationService : IBaseAsyncService<CertificateDTO, CertificateDTO, CertificateGridDTO, ACertificate, string>
    {
        Task<byte[]> CreateCertificate(string certificateID);
        Task<byte[]> CreateCertificate(ACertificate certificate, string mailSubjectPattern,
             string mailBodyPattern, string signingCertificateName, AApplicationStatus statusCertificateServerSign, AApplicationStatus statusCertificateForDelivery, AApplicationStatus statusCertificateDelivered, AApplicationStatus statusCertificatePaperPrint, string? webportalUrl = null);
               //todo: да се измести някъде на по-общо място
        Task<string?> GetWebPortalAddress();
        Task DeliverCertificateAsync(ACertificate certificate, string mailBodyPattern, string mailSubjectPattern, string webportalUrl);


    }
}
