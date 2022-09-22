using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using MJ_CAIS.AutoMapperContainer;
using MJ_CAIS.Common.Constants;
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
    public class ExtAdministrationService : BaseAsyncService<ExtAdministrationInDTO, ExtAdministrationDTO, ExtAdministrationGridDTO, GExtAdministration, string, CaisDbContext>, IExtAdministrationService
    {
        private readonly IExtAdministrationRepository _extAdministrationRepository;

        public ExtAdministrationService(IMapper mapper, IExtAdministrationRepository extAdministrationRepository) : base(mapper, extAdministrationRepository)
        {
            this._extAdministrationRepository = extAdministrationRepository;
        }
        public override async Task<string> InsertAsync(ExtAdministrationInDTO aInDto)
        {
            this.ValidateData(aInDto);
            GExtAdministration entity = mapper.MapToEntity<ExtAdministrationInDTO, GExtAdministration>(aInDto, isAdded: true);
            if (aInDto.ExtAdministrationUics != null)
            {
                await UpdateTransactionsAsync(aInDto, entity);
            }
            this.TransformDataOnInsertAsync(entity);
            await this.SaveEntityAsync(entity);
            return entity.Id;
        }

        private async Task UpdateTransactionsAsync(ExtAdministrationInDTO aInDto, GExtAdministration entity)
        { 
            var deletedUICs = aInDto.ExtAdministrationUics
                    .Where(x => x.Type == TransactionTypesEnum.DELETE)
                    .Select(x => x.Id).ToList();

            List<GExtAdministrationUic> uicEntities = await _extAdministrationRepository.GetDeletedUICsAsync(deletedUICs);

            // added or updated entities
            foreach (var currentTransaction in aInDto.ExtAdministrationUics.Where(x => x.Type != TransactionTypesEnum.DELETE && x.Type != null))
            {
                var sanction = mapper.MapTransaction<ExtAdministrationUicDTO, GExtAdministrationUic>(currentTransaction);

                uicEntities.Add(sanction);
            }

            entity.GExtAdministrationUics = uicEntities;
        }

        public override async Task UpdateAsync(string aId, ExtAdministrationInDTO aInDto)
        {
            // TODO: should not select from db, but it must check if the saveChanges has returned true (or 1)
            GExtAdministration repoObj = await this.baseAsyncRepository.SelectAsync(aId);
            if (repoObj == null)
            {
                throw new Exception("Object with id [" + aId + "] was not found!");
            }

            this.ValidateData(aInDto);

            GExtAdministration entity = mapper.MapToEntity<ExtAdministrationInDTO, GExtAdministration>(aInDto, isAdded: false);
            await UpdateTransactionsAsync(aInDto, entity);
            await this.SaveEntityAsync(entity);
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
        public async Task<List<ExtAdministrationGridDTO>> SelectAllAsync()
        {
            var query = this.GetSelectAllQueryable();
            var baseQuery = query.ProjectTo<ExtAdministrationGridDTO>(mapperConfiguration);
            var repoList = baseQuery.ToList();
            return repoList;
        }
    }
}
