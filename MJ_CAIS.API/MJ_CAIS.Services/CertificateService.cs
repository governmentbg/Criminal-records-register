using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Services
{
    public class CertificateService : BaseAsyncService<ApplicationDTO, ApplicationDTO, DTO.Application.ApplicationDTO, ACertificate, string, CaisDbContext>, ICertificateService
    {

        public CertificateService(IMapper mapper, IBaseAsyncRepository<ACertificate, string, CaisDbContext> baseAsyncRepository) : base(mapper, baseAsyncRepository)
        {
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
    }
}
