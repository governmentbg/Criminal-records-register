using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DTO.OffenceCategory;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using System.Collections.Generic;

namespace MJ_CAIS.Services
{
    public class OffenceCategoryService : BaseAsyncService<OffenceCategoryDTO, OffenceCategoryDTO, OffenceCategoryGridDTO, BOffenceCategory, string, CaisDbContext>, IOffenceCategoryService
    {
        private readonly IOffenceCategoryRepository _offenceCategoryRepository;

        public OffenceCategoryService(IMapper mapper, IOffenceCategoryRepository offenceCategoryRepository)
            : base(mapper, offenceCategoryRepository)
        {
            _offenceCategoryRepository = offenceCategoryRepository;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
    }
}
