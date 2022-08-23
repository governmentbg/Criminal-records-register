using MJ_CAIS.Common.Enums;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public class RehabilitationRepository : BaseAsyncRepository<BBulletin, CaisDbContext>, IRehabilitationRepository
    {
        public RehabilitationRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Add or delete a rehabilitation date.
        /// Change the status of the bulletin if necessary.
        /// </summary>
        /// <param name="bulletinId">Bulletin identifier</param>
        /// <param name="rehabilitationDate">The date must be null 
        /// if a crime was committed during the probation period</param>
        /// <param name="status">The status may be missing if you do not need to change the status of bulletin</param>
        /// <returns></returns>
        public void UpdateRehabilitationData(string bulletinId, decimal? bulletinVersion, DateTime? rehabilitationDate, string? status)
        {
            var bulletin = new BBulletin
            {
                Id = bulletinId,
                RehabilitationDate = rehabilitationDate,
                Version = bulletinVersion,
                EntityState = EntityStateEnum.Modified
            };

            bulletin.ModifiedProperties = new List<string>
            {
                nameof(bulletin.RehabilitationDate),
                nameof(bulletin.Version),
            };

            if (!string.IsNullOrEmpty(status))
            {
                bulletin.StatusId = status;
                bulletin.ModifiedProperties.Add(nameof(bulletin.StatusId));
            }

            _dbContext.ApplyChanges(bulletin, new List<IBaseIdEntity>());
        }
    }
}

