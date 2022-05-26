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


        public CertificateService(IMapper mapper, ICertificateRepository certificateRepository)
            : base(mapper, certificateRepository)
        {
            _certificateRepository = certificateRepository;
         
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }

        public  void SetCertificateStatus(ACertificate certificate,  AApplicationStatus newStatus, string description)
        {
            certificate.StatusCode = newStatus.Code;
            certificate.StatusCodeNavigation = newStatus;
            AStatusH aStatusH = new AStatusH();
            aStatusH.Descr = description;
            aStatusH.StatusCode = newStatus.Code;
            aStatusH.StatusCodeNavigation = newStatus;
            if (certificate.AStatusHes == null)
            {
                certificate.AStatusHes = new List<AStatusH>();
            }
            aStatusH.ReportOrder = certificate.AStatusHes.Count(x => x.StatusCode == newStatus.Code) + 1;
            aStatusH.Id = BaseEntity.GenerateNewId();
            aStatusH.CertificateId = certificate.Id;

            certificate.AStatusHes.Add(aStatusH);
            dbContext.AStatusHes.Add(aStatusH);
            dbContext.ACertificates.Update(certificate);


        }


    }
}
