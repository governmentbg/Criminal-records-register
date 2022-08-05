using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.WApplicationReport;
using MJ_CAIS.DTO.WApplicationReportSearchPer;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;

namespace MJ_CAIS.Services
{
    public class WApplicationReportSearchPerService : BaseAsyncService<WApplicationReportSearchPerDTO, WApplicationReportSearchPerDTO, WApplicationReportSearchPerGridDTO, WReportSearchPer, string, CaisDbContext>, IWApplicationReportSearchPerService
    {
        private readonly IWApplicationReportSearchPerRepository _wApplicationReportSearchPerRepository;

        public WApplicationReportSearchPerService(IMapper mapper,
            IWApplicationReportSearchPerRepository wApplicationReportSearchPerRepository)
            : base(mapper, wApplicationReportSearchPerRepository)
        {
            _wApplicationReportSearchPerRepository = wApplicationReportSearchPerRepository;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList) => false;
    }
}

