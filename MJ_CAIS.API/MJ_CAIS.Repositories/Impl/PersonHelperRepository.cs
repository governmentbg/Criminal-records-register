using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.DTO.Fbbc;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public class PersonHelperRepository : BaseAsyncRepository<PPerson, CaisDbContext>, IPersonHelperRepository
    {
        public PersonHelperRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<BulletinByPersonIdDTO> GetAllBulletinsByPersonId(string personId)
        {
            var bulletinsByEgn = from bulletin in _dbContext.BBulletins.AsNoTracking()
                                 join egn in _dbContext.PPersonIds.AsNoTracking() on bulletin.EgnId equals egn.Id
                                 where egn.PersonId == personId
                                 select new BulletinByPersonIdDTO
                                 {
                                     Id = bulletin.Id,
                                     BulletinType = bulletin.BulletinType,
                                     RegistrationNumber = bulletin.RegistrationNumber,
                                     StatusId = bulletin.StatusId,
                                     BulletinAuthorityId = bulletin.BulletinAuthorityId,
                                     CaseNumber = bulletin.CaseNumber,
                                     CaseYear = bulletin.CaseYear,
                                     Egn = bulletin.Egn,
                                     Lnch = bulletin.Lnch,
                                     FullName = bulletin.Fullname,
                                     FirstName = bulletin.Firstname,
                                     SurName = bulletin.Surname,
                                     FamilyName = bulletin.Familyname,
                                     BirthDate = bulletin.BirthDate,
                                     CreatedOn = bulletin.CreatedOn,
                                 };


            var bulletinsByLnch = from bulletin in _dbContext.BBulletins.AsNoTracking()
                                  join lnch in _dbContext.PPersonIds.AsNoTracking() on bulletin.LnchId equals lnch.Id
                                  where lnch.PersonId == personId
                                  select new BulletinByPersonIdDTO
                                  {
                                      Id = bulletin.Id,
                                      BulletinType = bulletin.BulletinType,
                                      RegistrationNumber = bulletin.RegistrationNumber,
                                      StatusId = bulletin.StatusId,
                                      BulletinAuthorityId = bulletin.BulletinAuthorityId,
                                      CaseNumber = bulletin.CaseNumber,
                                      CaseYear = bulletin.CaseYear,
                                      Egn = bulletin.Egn,
                                      Lnch = bulletin.Lnch,
                                      FullName = bulletin.Fullname,
                                      FirstName = bulletin.Firstname,
                                      SurName = bulletin.Surname,
                                      FamilyName = bulletin.Familyname,
                                      BirthDate = bulletin.BirthDate,
                                      CreatedOn = bulletin.CreatedOn,
                                  };


            var bulletinsByLn = from bulletin in _dbContext.BBulletins.AsNoTracking()
                                join ln in _dbContext.PPersonIds.AsNoTracking() on bulletin.LnId equals ln.Id
                                where ln.PersonId == personId
                                select new BulletinByPersonIdDTO
                                {
                                    Id = bulletin.Id,
                                    BulletinType = bulletin.BulletinType,
                                    RegistrationNumber = bulletin.RegistrationNumber,
                                    StatusId = bulletin.StatusId,
                                    BulletinAuthorityId = bulletin.BulletinAuthorityId,
                                    CaseNumber = bulletin.CaseNumber,
                                    CaseYear = bulletin.CaseYear,
                                    Egn = bulletin.Egn,
                                    Lnch = bulletin.Lnch,
                                    FullName = bulletin.Fullname,
                                    FirstName = bulletin.Firstname,
                                    SurName = bulletin.Surname,
                                    FamilyName = bulletin.Familyname,
                                    BirthDate = bulletin.BirthDate,
                                    CreatedOn = bulletin.CreatedOn,
                                };


            var bulletinsByIdDoc = from bulletin in _dbContext.BBulletins.AsNoTracking()
                                   join idDoc in _dbContext.PPersonIds.AsNoTracking() on bulletin.IdDocNumberId equals idDoc.Id
                                   where idDoc.PersonId == personId
                                   select new BulletinByPersonIdDTO
                                   {
                                       Id = bulletin.Id,
                                       BulletinType = bulletin.BulletinType,
                                       RegistrationNumber = bulletin.RegistrationNumber,
                                       StatusId = bulletin.StatusId,
                                       BulletinAuthorityId = bulletin.BulletinAuthorityId,
                                       CaseNumber = bulletin.CaseNumber,
                                       CaseYear = bulletin.CaseYear,
                                       Egn = bulletin.Egn,
                                       Lnch = bulletin.Lnch,
                                       FullName = bulletin.Fullname,
                                       FirstName = bulletin.Firstname,
                                       SurName = bulletin.Surname,
                                       FamilyName = bulletin.Familyname,
                                       BirthDate = bulletin.BirthDate,
                                       CreatedOn = bulletin.CreatedOn,
                                   };


            var bulletinsBySuid = from bulletin in _dbContext.BBulletins.AsNoTracking()
                                  join suid in _dbContext.PPersonIds.AsNoTracking() on bulletin.SuidId equals suid.Id
                                  where suid.PersonId == personId
                                  select new BulletinByPersonIdDTO
                                  {
                                      Id = bulletin.Id,
                                      BulletinType = bulletin.BulletinType,
                                      RegistrationNumber = bulletin.RegistrationNumber,
                                      StatusId = bulletin.StatusId,
                                      BulletinAuthorityId = bulletin.BulletinAuthorityId,
                                      CaseNumber = bulletin.CaseNumber,
                                      CaseYear = bulletin.CaseYear,
                                      Egn = bulletin.Egn,
                                      Lnch = bulletin.Lnch,
                                      FullName = bulletin.Fullname,
                                      FirstName = bulletin.Firstname,
                                      SurName = bulletin.Surname,
                                      FamilyName = bulletin.Familyname,
                                      BirthDate = bulletin.BirthDate,
                                      CreatedOn = bulletin.CreatedOn,
                                  };

            var bulletins = bulletinsByEgn
                                .Union(bulletinsByLnch)
                                .Union(bulletinsByLn)
                                .Union(bulletinsByIdDoc)
                                .Union(bulletinsBySuid);
            return bulletins;
        }

        public IQueryable<BulletinByPersonIdForEventsDTO> GetAllBulletinsForEventsByPersonId(string personId)
        {
            var bulletinsByEgn = from bulletin in _dbContext.BBulletins.AsNoTracking()
                                 join egn in _dbContext.PPersonIds.AsNoTracking() on bulletin.EgnId equals egn.Id
                                 where egn.PersonId == personId
                                 select new BulletinByPersonIdForEventsDTO
                                 {
                                     Id = bulletin.Id,
                                     BulletinType = bulletin.BulletinType,
                                     RegistrationNumber = bulletin.RegistrationNumber,
                                     StatusId = bulletin.StatusId,
                                     BulletinAuthorityId = bulletin.BulletinAuthorityId,
                                     CaseNumber = bulletin.CaseNumber,
                                     CaseYear = bulletin.CaseYear,
                                     Egn = bulletin.Egn,
                                     Lnch = bulletin.Lnch,
                                     FullName = bulletin.Fullname,
                                     FirstName = bulletin.Firstname,
                                     SurName = bulletin.Surname,
                                     FamilyName = bulletin.Familyname,
                                     BirthDate = bulletin.BirthDate,
                                     CreatedOn = bulletin.CreatedOn,
                                     CaseTypeId = bulletin.CaseTypeId,
                                     DecisionDate = bulletin.DecisionDate,
                                     PrevSuspSent = bulletin.PrevSuspSent,
                                     Version = bulletin.Version
                                 };


            var bulletinsByLnch = from bulletin in _dbContext.BBulletins.AsNoTracking()
                                  join lnch in _dbContext.PPersonIds.AsNoTracking() on bulletin.LnchId equals lnch.Id
                                  where lnch.PersonId == personId
                                  select new BulletinByPersonIdForEventsDTO
                                  {
                                      Id = bulletin.Id,
                                      BulletinType = bulletin.BulletinType,
                                      RegistrationNumber = bulletin.RegistrationNumber,
                                      StatusId = bulletin.StatusId,
                                      BulletinAuthorityId = bulletin.BulletinAuthorityId,
                                      CaseNumber = bulletin.CaseNumber,
                                      CaseYear = bulletin.CaseYear,
                                      Egn = bulletin.Egn,
                                      Lnch = bulletin.Lnch,
                                      FullName = bulletin.Fullname,
                                      FirstName = bulletin.Firstname,
                                      SurName = bulletin.Surname,
                                      FamilyName = bulletin.Familyname,
                                      BirthDate = bulletin.BirthDate,
                                      CreatedOn = bulletin.CreatedOn,
                                      CaseTypeId = bulletin.CaseTypeId,
                                      DecisionDate = bulletin.DecisionDate,
                                      PrevSuspSent = bulletin.PrevSuspSent,
                                      Version = bulletin.Version
                                  };


            var bulletinsByLn = from bulletin in _dbContext.BBulletins.AsNoTracking()
                                join ln in _dbContext.PPersonIds.AsNoTracking() on bulletin.LnId equals ln.Id
                                where ln.PersonId == personId
                                select new BulletinByPersonIdForEventsDTO
                                {
                                    Id = bulletin.Id,
                                    BulletinType = bulletin.BulletinType,
                                    RegistrationNumber = bulletin.RegistrationNumber,
                                    StatusId = bulletin.StatusId,
                                    BulletinAuthorityId = bulletin.BulletinAuthorityId,
                                    CaseNumber = bulletin.CaseNumber,
                                    CaseYear = bulletin.CaseYear,
                                    Egn = bulletin.Egn,
                                    Lnch = bulletin.Lnch,
                                    FullName = bulletin.Fullname,
                                    FirstName = bulletin.Firstname,
                                    SurName = bulletin.Surname,
                                    FamilyName = bulletin.Familyname,
                                    BirthDate = bulletin.BirthDate,
                                    CreatedOn = bulletin.CreatedOn,
                                    CaseTypeId = bulletin.CaseTypeId,
                                    DecisionDate = bulletin.DecisionDate,
                                    PrevSuspSent = bulletin.PrevSuspSent,
                                    Version = bulletin.Version
                                };


            var bulletinsByIdDoc = from bulletin in _dbContext.BBulletins.AsNoTracking()
                                   join idDoc in _dbContext.PPersonIds.AsNoTracking() on bulletin.IdDocNumberId equals idDoc.Id
                                   where idDoc.PersonId == personId
                                   select new BulletinByPersonIdForEventsDTO
                                   {
                                       Id = bulletin.Id,
                                       BulletinType = bulletin.BulletinType,
                                       RegistrationNumber = bulletin.RegistrationNumber,
                                       StatusId = bulletin.StatusId,
                                       BulletinAuthorityId = bulletin.BulletinAuthorityId,
                                       CaseNumber = bulletin.CaseNumber,
                                       CaseYear = bulletin.CaseYear,
                                       Egn = bulletin.Egn,
                                       Lnch = bulletin.Lnch,
                                       FullName = bulletin.Fullname,
                                       FirstName = bulletin.Firstname,
                                       SurName = bulletin.Surname,
                                       FamilyName = bulletin.Familyname,
                                       BirthDate = bulletin.BirthDate,
                                       CreatedOn = bulletin.CreatedOn,
                                       CaseTypeId = bulletin.CaseTypeId,
                                       DecisionDate = bulletin.DecisionDate,
                                       PrevSuspSent = bulletin.PrevSuspSent,
                                       Version = bulletin.Version
                                   };


            var bulletinsBySuid = from bulletin in _dbContext.BBulletins.AsNoTracking()
                                  join suid in _dbContext.PPersonIds.AsNoTracking() on bulletin.SuidId equals suid.Id
                                  where suid.PersonId == personId
                                  select new BulletinByPersonIdForEventsDTO
                                  {
                                      Id = bulletin.Id,
                                      BulletinType = bulletin.BulletinType,
                                      RegistrationNumber = bulletin.RegistrationNumber,
                                      StatusId = bulletin.StatusId,
                                      BulletinAuthorityId = bulletin.BulletinAuthorityId,
                                      CaseNumber = bulletin.CaseNumber,
                                      CaseYear = bulletin.CaseYear,
                                      Egn = bulletin.Egn,
                                      Lnch = bulletin.Lnch,
                                      FullName = bulletin.Fullname,
                                      FirstName = bulletin.Firstname,
                                      SurName = bulletin.Surname,
                                      FamilyName = bulletin.Familyname,
                                      BirthDate = bulletin.BirthDate,
                                      CreatedOn = bulletin.CreatedOn,
                                      CaseTypeId = bulletin.CaseTypeId,
                                      DecisionDate = bulletin.DecisionDate,
                                      PrevSuspSent = bulletin.PrevSuspSent,
                                      Version = bulletin.Version
                                  };

            var bulletins = bulletinsByEgn
                                .Union(bulletinsByLnch)
                                .Union(bulletinsByLn)
                                .Union(bulletinsByIdDoc)
                                .Union(bulletinsBySuid);
            return bulletins;
        }

        public IQueryable<ApplicationsByPersonIdDTO> GetAllAplicationsByPersonId(string personId)
        {
            var applicationsByEgn = from application in _dbContext.AApplications.AsNoTracking()
                                    join egn in _dbContext.PPersonIds.AsNoTracking() on application.EgnId equals egn.Id
                                    where egn.PersonId == personId
                                    select new ApplicationsByPersonIdDTO
                                    {
                                        ApplicationId = application.Id,
                                        ApplicationTypeId = application.ApplicationTypeId,
                                        CsAuthorityId = application.CsAuthorityId,
                                        ApplicantName = application.ApplicantName,
                                        Firstname = application.Firstname,
                                        Surname = application.Surname,
                                        Familyname = application.Familyname,
                                        Fullname = application.Fullname,
                                        BirthDate = application.BirthDate,
                                        Egn = application.Egn,
                                        Lnch = application.Lnch,
                                        WApplicationId = application.WApplicationId
                                    };

            var applicationsByLnch = from application in _dbContext.AApplications.AsNoTracking()
                                     join lnch in _dbContext.PPersonIds.AsNoTracking() on application.LnchId equals lnch.Id
                                     where lnch.PersonId == personId
                                     select new ApplicationsByPersonIdDTO
                                     {
                                         ApplicationId = application.Id,
                                         ApplicationTypeId = application.ApplicationTypeId,
                                         CsAuthorityId = application.CsAuthorityId,
                                         ApplicantName = application.ApplicantName,
                                         Firstname = application.Firstname,
                                         Surname = application.Surname,
                                         Familyname = application.Familyname,
                                         Fullname = application.Fullname,
                                         BirthDate = application.BirthDate,
                                         Egn = application.Egn,
                                         Lnch = application.Lnch,
                                         WApplicationId = application.WApplicationId
                                     };

            var applicationsByLn = from application in _dbContext.AApplications.AsNoTracking()
                                   join ln in _dbContext.PPersonIds.AsNoTracking() on application.LnId equals ln.Id
                                   where ln.PersonId == personId
                                   select new ApplicationsByPersonIdDTO
                                   {
                                       ApplicationId = application.Id,
                                       ApplicationTypeId = application.ApplicationTypeId,
                                       CsAuthorityId = application.CsAuthorityId,
                                       ApplicantName = application.ApplicantName,
                                       Firstname = application.Firstname,
                                       Surname = application.Surname,
                                       Familyname = application.Familyname,
                                       Fullname = application.Fullname,
                                       BirthDate = application.BirthDate,
                                       Egn = application.Egn,
                                       Lnch = application.Lnch,
                                       WApplicationId = application.WApplicationId
                                   };

            var applicationsBySuid = from application in _dbContext.AApplications.AsNoTracking()
                                     join suid in _dbContext.PPersonIds.AsNoTracking() on application.SuidId equals suid.Id
                                     where suid.PersonId == personId
                                     select new ApplicationsByPersonIdDTO
                                     {
                                         ApplicationId = application.Id,
                                         ApplicationTypeId = application.ApplicationTypeId,
                                         CsAuthorityId = application.CsAuthorityId,
                                         ApplicantName = application.ApplicantName,
                                         Firstname = application.Firstname,
                                         Surname = application.Surname,
                                         Familyname = application.Familyname,
                                         Fullname = application.Fullname,
                                         BirthDate = application.BirthDate,
                                         Egn = application.Egn,
                                         Lnch = application.Lnch,
                                         WApplicationId = application.WApplicationId
                                     };

            var allApplications = applicationsByEgn
                .Union(applicationsByLnch)
                .Union(applicationsByLn)
                .Union(applicationsBySuid);

            return allApplications;
        }

        public IQueryable<FbbcByPersonIdDTO> GetAllFbbcsByPersonId(string personId)
        {
            var fbbcByEgn = from fbbc in _dbContext.Fbbcs.AsNoTracking()
                            join egn in _dbContext.PPersonIds.AsNoTracking() on fbbc.PersonId equals egn.Id
                            where egn.PersonId == personId
                            select new FbbcByPersonIdDTO
                            {
                                Id = fbbc.Id,
                                DocTypeId = fbbc.DocTypeId,
                                ReceiveDate = fbbc.ReceiveDate,
                                CountryId = fbbc.CountryId,
                                Egn = fbbc.Egn,
                                Firstname = fbbc.Firstname,
                                Surname = fbbc.Surname,
                                Familyname = fbbc.Familyname,
                                BirthDate = fbbc.BirthDate,
                                CreatedOn = fbbc.CreatedOn
                            };

            var fbbcBySuid = from fbbc in _dbContext.Fbbcs.AsNoTracking()
                             join suid in _dbContext.PPersonIds.AsNoTracking() on fbbc.SuidId equals suid.Id
                             where suid.PersonId == personId
                             select new FbbcByPersonIdDTO
                             {
                                 Id = fbbc.Id,
                                 DocTypeId = fbbc.DocTypeId,
                                 ReceiveDate = fbbc.ReceiveDate,
                                 CountryId = fbbc.CountryId,
                                 Egn = fbbc.Egn,
                                 Firstname = fbbc.Firstname,
                                 Surname = fbbc.Surname,
                                 Familyname = fbbc.Familyname,
                                 BirthDate = fbbc.BirthDate,
                                 CreatedOn = fbbc.CreatedOn
                             };

            var allFbbcs = fbbcByEgn.Union(fbbcBySuid);

            return allFbbcs;
        }
    }
}
