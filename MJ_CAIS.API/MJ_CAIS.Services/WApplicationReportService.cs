using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.WApplicationReport;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;

namespace MJ_CAIS.Services
{
    public class WApplicationReportService : BaseAsyncService<WApplicationReportDTO, WApplicationReportDTO, WApplicationReportGridDTO, WReport, string, CaisDbContext>, IWApplicationReportService
    {
        private readonly IWApplicationReportRepository _wApplicationReportRepository;

        public WApplicationReportService(IMapper mapper,
            IWApplicationReportRepository wApplicationReportRepository)
            : base(mapper, wApplicationReportRepository)
        {
            _wApplicationReportRepository = wApplicationReportRepository;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList) => false;
    }
}

