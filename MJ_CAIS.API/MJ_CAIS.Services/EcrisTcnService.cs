using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DTO.EcrisTcn;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using MJ_CAIS.Services.Contracts.Utils;
using Microsoft.AspNet.OData.Query;

namespace MJ_CAIS.Services
{
    public class EcrisTcnService : BaseAsyncService<EcrisTcnDTO, EcrisTcnDTO, EcrisTcnGridDTO, EEcrisTcn, string, CaisDbContext>, IEcrisTcnService
    {
        private readonly IEcrisTcnRepository _ecrisTcnRepository;

        public EcrisTcnService(IMapper mapper, IEcrisTcnRepository ecrisTcnRepository)
            : base(mapper, ecrisTcnRepository)
        {
            _ecrisTcnRepository = ecrisTcnRepository;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }

        public virtual async Task<IgPageResult<EcrisTcnGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<EcrisTcnGridDTO> aQueryOptions, string? statusId)
        {
            var entityQuery = this.GetSelectAllQueriable();
            if (!string.IsNullOrEmpty(statusId))
            {
                entityQuery = entityQuery.Where(x => x.Status == statusId);
            }

            var baseQuery = entityQuery.ProjectTo<EcrisTcnGridDTO>(mapperConfiguration);
            var resultQuery = await this.ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<EcrisTcnGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }

        public async Task ChangeStatusAsync(string aInDto, string statusId)
        {
            var ecrisTcn = await dbContext.EEcrisTcns
               .FirstOrDefaultAsync(x => x.Id == aInDto);

            if (ecrisTcn == null)
            {
                throw new ArgumentException($"EcrisTcn with id: {aInDto} is missing");
            }

            ecrisTcn.Status = statusId;
            await dbContext.SaveChangesAsync();
        }
    }
}
