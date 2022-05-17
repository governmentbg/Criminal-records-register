using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.ExtAdministration;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Services
{
    public class ExtAdministrationService : BaseAsyncService<ExtAdministrationDTO, ExtAdministrationDTO, ExtAdministrationGridDTO, GExtAdministration, string, CaisDbContext>, IExtAdministrationService
    {
        private readonly IExtAdministrationRepository _extAdministrationRepository;

        public ExtAdministrationService(IMapper mapper, IExtAdministrationRepository extAdministrationRepository) : base(mapper, extAdministrationRepository)
        {
            this._extAdministrationRepository = extAdministrationRepository;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
        public async Task<List<ExtAdministrationGridDTO>> SelectAllAsync()
        {
            var query = this.GetSelectAllQueriable();
            var baseQuery = query.ProjectTo<ExtAdministrationGridDTO>(mapperConfiguration);
            var repoList = baseQuery.ToList();
            return repoList;
        }
    }
}
