using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DTO.Certificate;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using System.Collections.Generic;

using MJ_CAIS.Common.Constants;
using System.Text;
using MJ_CAIS.Common.Enums;

namespace MJ_CAIS.Services
{
    public class CertificateService : BaseAsyncService<CertificateDTO, CertificateDTO, CertificateGridDTO, ACertificate, string, CaisDbContext>, ICertificateService
    {
        private readonly ICertificateRepository _certificateRepository;
      //  private readonly IJasperReportsClient _jasperReportsClient;

        public CertificateService(IMapper mapper, ICertificateRepository certificateRepository)//, IJasperReportsClient jasperClient)
            : base(mapper, certificateRepository)
        {
            _certificateRepository = certificateRepository;
            //_jasperReportsClient = jasperClient;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }

     
    }
}
