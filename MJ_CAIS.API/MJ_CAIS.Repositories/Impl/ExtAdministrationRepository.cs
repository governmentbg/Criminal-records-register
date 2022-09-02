using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Repositories.Impl
{
    public class ExtAdministrationRepository : BaseAsyncRepository<GExtAdministration, CaisDbContext>, IExtAdministrationRepository
    {
        public ExtAdministrationRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddUicAsync(string? administrationId, string uic, string? ou)
        {
            //var existingAdministrationId = await _dbContext.GExtAdministrationUics.Where(exUic => exUic.Value == uic).Select(exUic => exUic.AdministrationId).AsNoTracking().FirstOrDefaultAsync();
            var existingAdministrationId = string.Empty;
            if (!string.IsNullOrEmpty(existingAdministrationId)) {
                if (existingAdministrationId != administrationId)
                {
                    throw new ApplicationException("UIC already added to another administration");
                }
                else
                {
                    return;
                }
            }
            else
            {
                var gExtAdminUic = new GExtAdministrationUic()
                {
                    //AdministrationId = administrationId,
                    Name = ou,
                    Value = uic
                };
                gExtAdminUic.Id = BaseEntity.GenerateNewId();
                gExtAdminUic.EntityState = EntityStateEnum.Added;
                _dbContext.ApplyChanges(gExtAdminUic);
            }
        }

        public async Task<List<GExtAdministrationUic>> GetDeletedUICsAsync(List<string> deletedUICs)
        {

            if (deletedUICs.Count == 0)
            {
                return new List<GExtAdministrationUic>();
            }

            var deletedSanctionAndItsProbations = await _dbContext.GExtAdministrationUics.AsNoTracking()
                      .Where(x => deletedUICs.Contains(x.Id))
                      .Select(x => new GExtAdministrationUic
                      {
                          Id = x.Id,
                          EntityState = EntityStateEnum.Deleted,
                          Version = x.Version
                      }).ToListAsync();

            return deletedSanctionAndItsProbations;
        }

        public async Task<GExtAdministration> SelectAsync(string id)
        {
            var result = await this._dbContext.Set<GExtAdministration>().Include( a => a.GExtAdministrationUics).AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
    }
}
