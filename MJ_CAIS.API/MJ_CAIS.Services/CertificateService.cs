using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DTO.Certificate;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using System.Collections.Generic;

namespace MJ_CAIS.Services
{
    public class CertificateService : BaseAsyncService<CertificateDTO, CertificateDTO, CertificateGridDTO, ACertificate, string, CaisDbContext>, ICertificateService
    {
        private readonly ICertificateRepository _certificateRepository;

        public CertificateService(IMapper mapper, ICertificateRepository certificateRepository)
            : base(mapper, certificateRepository)
        {
            _certificateRepository = certificateRepository;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
    }
}
