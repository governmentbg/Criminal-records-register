using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO;
using MJ_CAIS.DTO.Inquiry;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services
{
    public class InquiryService : BaseAsyncService<BaseDTO, BaseDTO, BaseGridDTO, VBulletin, string, CaisDbContext>, IInquiryService
    {
        private readonly IInquiryRepository _inquiryRepository;
        protected override bool IsChildRecord(string aId, List<string> aParentsList) => false;

        public InquiryService(IInquiryRepository inquiryRepository, IMapper mapper)
        : base(mapper, inquiryRepository)
        {
            _inquiryRepository = inquiryRepository;
        }

        public async Task<IgPageResult<InquiryBulletinGridDTO>> SearchBulletinsWithPaginationAsync(ODataQueryOptions<InquiryBulletinGridDTO> aQueryOptions, InquirySearchBulletinDTO searchParams)
        {
            var baseQuery = _inquiryRepository.FilterBulletins(searchParams);
            var resultQuery = await this.ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<InquiryBulletinGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }

        public async Task<List<ExportInquiryBulletinGridDTO>> ExportBulletinsAsync(InquirySearchBulletinDTO searchParams)
        {
            var baseQuery = _inquiryRepository.FilterBulletinsForExport(searchParams)
                .ProjectTo<ExportInquiryBulletinGridDTO>(mapperConfiguration);

            var result = await baseQuery.ToListAsync(); // todo: max
            return result;
        }

        public async Task<IgPageResult<InquiryBulletinByPersonGridDTO>> SearchBulletinsByPersonWithPaginationAsync(ODataQueryOptions<InquiryBulletinByPersonGridDTO> aQueryOptions, InquirySearchBulletinByPersonDTO searchParams)
        {
            var baseQuery = _inquiryRepository.FilterBulletinsByPerson(searchParams);
            var resultQuery = await this.ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<InquiryBulletinByPersonGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }
    }
}
