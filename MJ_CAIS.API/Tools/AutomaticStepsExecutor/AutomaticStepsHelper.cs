using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticStepsExecutor
{
    public class AutomaticStepsHelper
    {
       public static async Task ProcessWebApplicationToApplicationAsync(WApplication wapplication, CaisDbContext dbContext)
        {
            wapplication.StatusCode = ApplicationConstants.ApplicationStatuses.WebApprovedApplication;
            AApplication appl = new AApplication()
            {
                Id = BaseEntity.GenerateNewId(),
                AddrDistrict = wapplication.AddrDistrict,
                AddrEmail = wapplication.AddrEmail,
                AddrPhone = wapplication.AddrPhone,
                Address = wapplication.Address,
                AddrName = wapplication.AddrName,
                AddrState = wapplication.AddrState,
                AddrStr = wapplication.AddrStr,
                AddrTown = wapplication.AddrTown,
                ApplicantName = wapplication.ApplicantName,
                BirthCityId = wapplication.BirthCityId,
                BirthCountryId = wapplication.BirthCountryId,
                BirthDate = wapplication.BirthDate,
                BirthDatePrecision = wapplication.BirthDatePrecision,
                BirthPlaceOther = wapplication.BirthPlaceOther,
                CsAuthorityBirthId = wapplication.CsAuthorityBirthId,
                CsAuthorityId = wapplication.CsAuthorityId,
                Description = wapplication.Description,
                Egn = wapplication.Egn,
                Email = wapplication.Email,
                Familyname = wapplication.Familyname,
                FatherFamilyname = wapplication.FatherFamilyname,
                FamilynameLat = wapplication.FamilynameLat,
                FatherFirstname = wapplication.FatherFirstname,
                FatherSurname = wapplication.FatherSurname,
                FatherFullname = wapplication.FatherFullname,
                Firstname = wapplication.Firstname,
                FirstnameLat = wapplication.FirstnameLat,
                FromCosul = wapplication.FromCosul,
                Fullname = wapplication.Fullname,
                FullnameLat = wapplication.FullnameLat,
                IsLocal = wapplication.IsLocal,
                Ln = wapplication.Ln,
                Lnch = wapplication.Lnch,
                MotherFamilyname = wapplication.MotherFamilyname,
                MotherFirstname = wapplication.MotherFirstname,
                MotherSurname = wapplication.MotherSurname,
                MotherFullname = wapplication.MotherFullname,
                PaymentMethodId = wapplication.PaymentMethodId,
                PurposeId = wapplication.PurposeId,
                Sex = wapplication.Sex,
                SrvcResRcptMethId = wapplication.SrvcResRcptMethId,
                StatusCode = ApplicationConstants.ApplicationStatuses.ApprovedApplication,
                Surname = wapplication.Surname,
                SurnameLat = wapplication.SurnameLat,
                UserCitizenId = wapplication.UserCitizenId,
                UserExtId = wapplication.UserExtId,
                UserId = wapplication.UserId,
                WApplicationId = wapplication.Id,
                //todo: get registration number
                RegistrationNumber = "123445",
                ApplicationType = wapplication.ApplicationType

            };
            //foreach (var v in wapplication.AAppCitizenships)
            //{
            //    var newObj = new AAppCitizenship()
            //    {
            //        Id = BaseEntity.GenerateNewId(),
            //        ApplicationId = appl.Id,
            //        CountryId = v.CountryId
            //    };
            //    appl.AAppCitizenships.Add(newObj);

            //}
            //todo: какво е това
            // appl.AAppPersAliases

            appl.PAppIds = await dbContext.PPersonIds.Where(x => (x.PidTypeId == PersonConstants.PidType.Egn && x.Issuer == PersonConstants.IssuerType.GRAO && x.Pid == appl.Egn)
                                            || (x.PidTypeId == PersonConstants.PidType.Lnch && x.Issuer == PersonConstants.IssuerType.MVR && x.Pid == appl.Lnch)
                                            || (x.PidTypeId == PersonConstants.PidType.Ln && x.Issuer == PersonConstants.IssuerType.CAIS && x.Pid == appl.Ln))
                                 .Select(x => new PAppId
                                 {
                                     ApplicationId = appl.Id,
                                     Id = BaseEntity.GenerateNewId(),
                                     PersonId = x.Id
                                 }).ToListAsync();


         

            dbContext.AApplications.Add(appl);
            dbContext.PAppIds.AddRange(appl.PAppIds);
            dbContext.WApplications.Update(wapplication);
        }
    }
}
