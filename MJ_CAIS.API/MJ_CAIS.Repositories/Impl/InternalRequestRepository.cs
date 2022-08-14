using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.InternalRequest;
using MJ_CAIS.Repositories.Contracts;
using static MJ_CAIS.Common.Constants.InternalRequestConstants;

namespace MJ_CAIS.Repositories.Impl
{
    public class InternalRequestRepository : BaseAsyncRepository<NInternalRequest, CaisDbContext>, IInternalRequestRepository
    {
        private readonly IUserContext _userContext;

        public InternalRequestRepository(CaisDbContext dbContext, IUserContext userContext)
            : base(dbContext)
        {
            this._userContext = userContext;
        }

        public IQueryable<NInternalRequest> SelectAllByIdsAsync(List<string> ids)
            => this._dbContext.NInternalRequests.AsNoTracking()
                       .Where(x => ids.Contains(x.Id));
        public override IQueryable<NInternalRequest> SelectAll()
        {
            var query = this._dbContext.NInternalRequests.AsNoTracking()
                .Include(x => x.ReqStatusCodeNavigation)
                .Include(x => x.FromAuthority)
                .Include(x => x.ToAuthority);

            return query;
        }

        public IQueryable<NInternalRequest> SelectAllForJudge()
        {
            var query = this._dbContext.NInternalRequests.AsNoTracking()
                 .Include(x => x.ReqStatusCodeNavigation)
                 .Include(x => x.FromAuthority);

            return query;
        }

        public override async Task<NInternalRequest> SelectAsync(string id)
        {
            return await this._dbContext.NInternalRequests.AsNoTracking()
                        .Include(x => x.FromAuthority)
                        .Include(x => x.ToAuthority)
                        .Include(x => x.PPersId)
                        .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<NInternalRequest> SelectForDeleteAsync(string id)
        {
            return await this._dbContext.NInternalRequests.AsNoTracking()
                        .Include(x => x.NInternalReqBulletins)
                        .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<RequestCountDTO> GetInternalRequestsCountAsync()
        {
            var inboxCount = await _dbContext.NInternalRequests.AsNoTracking()
                .CountAsync(x => x.ReqStatusCode == Status.Sent && x.ToAuthorityId == _userContext.CsAuthorityId);

            var outboxCount = await _dbContext.NInternalRequests.AsNoTracking()
                .CountAsync(x => (x.ReqStatusCode == Status.Cancelled ||
                    x.ReqStatusCode == Status.Ready) &&
                 x.FromAuthorityId == _userContext.CsAuthorityId);


            return new RequestCountDTO
            {
                InboxCount = inboxCount,
                OutboxCount = outboxCount
            };
        }

        public IQueryable<SelectPidGridDTO> SelectAllPidsForSelection()
        {
            var result = this._dbContext.PPersonIds.AsNoTracking()
                  .Include(x => x.Person)
                  .Include(x => x.PidType)
                  .Select(x => new SelectPidGridDTO
                  {
                      Id = x.Id,
                      CreatedOn = x.CreatedOn,
                      PersonBirthDate = x.Person.BirthDate,
                      Firstname = x.Person.Firstname,
                      Surname = x.Person.Surname,
                      Familyname = x.Person.Familyname,
                      Pid = x.Pid,
                      PidType = x.PidType.Name
                  });

            return result;
        }

        public IQueryable<SelectedPersonBulletinGridDTO> GetPersonBulletins(string pidId)
        {
            var bulletinsByEgn = from bulletin in _dbContext.BBulletins.AsNoTracking()
                                 where bulletin.EgnId == pidId
                                 select new SelectedPersonBulletinGridDTO
                                 {
                                     BulletinId = bulletin.Id,
                                     BulletinType = bulletin.BulletinType,
                                     RegistrationNumber = bulletin.RegistrationNumber,
                                     StatusId = bulletin.StatusId,
                                     CreatedOn = bulletin.CreatedOn,
                                     BulletinAuthorityId = bulletin.BulletinAuthorityId,
                                 };


            var bulletinsByLnch = from bulletin in _dbContext.BBulletins.AsNoTracking()
                                  where bulletin.LnchId == pidId
                                  select new SelectedPersonBulletinGridDTO
                                  {
                                      BulletinId = bulletin.Id,
                                      BulletinType = bulletin.BulletinType,
                                      RegistrationNumber = bulletin.RegistrationNumber,
                                      StatusId = bulletin.StatusId,
                                      CreatedOn = bulletin.CreatedOn,
                                      BulletinAuthorityId = bulletin.BulletinAuthorityId,
                                  };

            var bulletinsByLn = from bulletin in _dbContext.BBulletins.AsNoTracking()
                                where bulletin.LnId == pidId
                                select new SelectedPersonBulletinGridDTO
                                {
                                    BulletinId = bulletin.Id,
                                    BulletinType = bulletin.BulletinType,
                                    RegistrationNumber = bulletin.RegistrationNumber,
                                    StatusId = bulletin.StatusId,
                                    CreatedOn = bulletin.CreatedOn,
                                    BulletinAuthorityId = bulletin.BulletinAuthorityId,
                                };


            var bulletinsByIdDoc = from bulletin in _dbContext.BBulletins.AsNoTracking()
                                   where bulletin.IdDocNumberId == pidId
                                   select new SelectedPersonBulletinGridDTO
                                   {
                                       BulletinId = bulletin.Id,
                                       BulletinType = bulletin.BulletinType,
                                       RegistrationNumber = bulletin.RegistrationNumber,
                                       StatusId = bulletin.StatusId,
                                       CreatedOn = bulletin.CreatedOn,
                                       BulletinAuthorityId = bulletin.BulletinAuthorityId,
                                   };


            var bulletinsBySuid = from bulletin in _dbContext.BBulletins.AsNoTracking()
                                  where bulletin.SuidId == pidId
                                  select new SelectedPersonBulletinGridDTO
                                  {
                                      BulletinId = bulletin.Id,
                                      BulletinType = bulletin.BulletinType,
                                      RegistrationNumber = bulletin.RegistrationNumber,
                                      StatusId = bulletin.StatusId,
                                      CreatedOn = bulletin.CreatedOn,
                                      BulletinAuthorityId = bulletin.BulletinAuthorityId,
                                  };

            var bulletins = bulletinsByEgn
                                .Union(bulletinsByLnch)
                                .Union(bulletinsByLn)
                                .Union(bulletinsByIdDoc)
                                .Union(bulletinsBySuid);

            var query = from bulletin in bulletins
                        join auth in _dbContext.GDecidingAuthorities.AsNoTracking() on bulletin.BulletinAuthorityId equals auth.Id
                            into authLeft
                        from auth in authLeft.DefaultIfEmpty()

                        join status in _dbContext.BBulletinStatuses.AsNoTracking() on bulletin.StatusId equals status.Code
                        select new SelectedPersonBulletinGridDTO
                        {
                            BulletinId = bulletin.BulletinId,
                            BulletinType = bulletin.BulletinType == BulletinConstants.Type.Bulletin78A ? BulletinResources.Bulletin78A :
                                                        bulletin.BulletinType == BulletinConstants.Type.ConvictionBulletin ? BulletinResources.ConvictionBulletin :
                                                             BulletinResources.Unspecified,
                            RegistrationNumber = bulletin.RegistrationNumber,
                            StatusName = status.Name,
                            BulletinAuthorityName = auth.Name,
                            CreatedOn = bulletin.CreatedOn,
                        };

            return query;
        }

        public IQueryable<SelectedPersonBulletinGridDTO> GetSelectedBulletins(string aId)
        {
            var query = from bInternalRequest in _dbContext.NInternalReqBulletins
                        join bulletin in _dbContext.BBulletins.AsNoTracking() on bInternalRequest.BulletinId equals bulletin.Id
                        join status in _dbContext.BBulletinStatuses.AsNoTracking() on bulletin.StatusId equals status.Code

                        join auth in _dbContext.GDecidingAuthorities.AsNoTracking() on bulletin.BulletinAuthorityId equals auth.Id
                        into authLeft
                        from auth in authLeft.DefaultIfEmpty()
                        where bInternalRequest.InternalReqId == aId
                        select new SelectedPersonBulletinGridDTO
                        {
                            Id = bInternalRequest.Id,
                            BulletinId = bulletin.Id,
                            BulletinType = bulletin.BulletinType == BulletinConstants.Type.Bulletin78A ? BulletinResources.Bulletin78A :
                                           bulletin.BulletinType == BulletinConstants.Type.ConvictionBulletin ? BulletinResources.ConvictionBulletin :
                                           BulletinResources.Unspecified,
                            RegistrationNumber = bulletin.RegistrationNumber,
                            StatusId = bulletin.StatusId,
                            CreatedOn = bInternalRequest.CreatedOn,
                            BulletinAuthorityId = bulletin.BulletinAuthorityId,
                            Version = bInternalRequest.Version,
                            BulletinAuthorityName = auth.Name,
                            Remarks = bInternalRequest.Remarks,
                            StatusName = status.Name
                        };

            return query;
        }

        public async Task<SelectedPersonBulletinGridDTO> GetBulletinWithPidDataAsync(string aId)
        {
            var bulletin = await (from bulletins in _dbContext.BBulletins.AsNoTracking()
                                  join status in _dbContext.BBulletinStatuses.AsNoTracking() on bulletins.StatusId equals status.Code

                                  join auth in _dbContext.GDecidingAuthorities.AsNoTracking() on bulletins.BulletinAuthorityId equals auth.Id
                                  into authLeft
                                  from auth in authLeft.DefaultIfEmpty()
                                  select new
                                  {
                                      BulletinId = bulletins.Id,
                                      BulletinType = bulletins.BulletinType == BulletinConstants.Type.Bulletin78A ? BulletinResources.Bulletin78A :
                                                     bulletins.BulletinType == BulletinConstants.Type.ConvictionBulletin ? BulletinResources.ConvictionBulletin :
                                                     BulletinResources.Unspecified,
                                      RegistrationNumber = bulletins.RegistrationNumber,
                                      StatusId = bulletins.StatusId,
                                      CreatedOn = bulletins.CreatedOn,
                                      BulletinAuthorityId = bulletins.BulletinAuthorityId,
                                      Version = bulletins.Version,
                                      BulletinAuthorityName = auth.Name,
                                      StatusName = status.Name,
                                      EgnId = bulletins.EgnId,
                                      Egn = bulletins.Egn,
                                      LnchId = bulletins.LnchId,
                                      Lnch = bulletins.Lnch,
                                      LnId = bulletins.LnId,
                                      Ln = bulletins.Ln,
                                      IdDocNumberId = bulletins.IdDocNumberId,
                                      IdDocNumber = bulletins.IdDocNumber,
                                      SuidId = bulletins.SuidId,
                                      Suid = bulletins.Suid,
                                  }).FirstOrDefaultAsync(x => x.BulletinId == aId);

            var result = new SelectedPersonBulletinGridDTO
            {
                BulletinId = bulletin.BulletinId,
                BulletinType = bulletin.BulletinType,
                RegistrationNumber = bulletin.RegistrationNumber,
                StatusId = bulletin.StatusId,
                CreatedOn = bulletin.CreatedOn,
                BulletinAuthorityId = bulletin.BulletinAuthorityId,
                Version = bulletin.Version,
                BulletinAuthorityName = bulletin.BulletinAuthorityName,
                StatusName = bulletin.StatusName,
            };

            var pidId = string.Empty;
            var pid = string.Empty;
            if (!string.IsNullOrEmpty(bulletin.EgnId))
            {
                pidId = bulletin.EgnId;
                pid = bulletin.Egn;
            }
            else if (!string.IsNullOrEmpty(bulletin.LnchId))
            {
                pidId = bulletin.LnchId;
                pid = bulletin.Lnch;
            }
            else if (!string.IsNullOrEmpty(bulletin.LnId))
            {
                pidId = bulletin.LnId;
                pid = bulletin.Ln;
            }
            else if (!string.IsNullOrEmpty(bulletin.IdDocNumberId))
            {
                pidId = bulletin.IdDocNumberId;
                pid = bulletin.IdDocNumber;
            }
            else
            {
                pidId = bulletin.SuidId;
                pid = bulletin.Suid;
            }

            result.PidId = pidId;
            result.Pid = pid;
            return result;
        }
    }
}
