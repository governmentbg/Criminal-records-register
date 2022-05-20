using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Certificate;
using MJ_CAIS.ExternalWebServices.Contracts;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL.EDelivery;

namespace MJ_CAIS.ExternalWebServices
{
    public class CAISEDeliveryService : BaseAsyncService<CertificateDTO, CertificateDTO, CertificateGridDTO, ACertificate, string, CaisDbContext>, ICAISEDeliveryService
    {
        private readonly ICertificateRepository _certificateRepository;
        private readonly IEDeliveryService _eDeliveryService;


        public CAISEDeliveryService(IMapper mapper, ICertificateRepository certificateRepository, IEDeliveryService eDeliveryService)
            : base(mapper, certificateRepository)
        {
            _certificateRepository = certificateRepository;
            _eDeliveryService = eDeliveryService;
      
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            throw new NotImplementedException();
        }
    }
}
