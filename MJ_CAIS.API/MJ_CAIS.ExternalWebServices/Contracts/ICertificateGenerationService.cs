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
    internal interface ICertificateGenerationService : IBaseAsyncService<CertificateDTO, CertificateDTO, CertificateGridDTO, ACertificate, string>
    {
    }
}
