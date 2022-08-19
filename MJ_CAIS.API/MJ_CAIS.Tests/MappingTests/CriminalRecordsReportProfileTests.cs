using AutoMapper;
using MJ_CAIS.AutoMapperContainer.MappingProfiles;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.ExternalServicesHost;
using MJ_CAIS.Tests.Factories;
using MJ_CAIS.Tests.Helpers;
using NUnit.Framework;
using System.Linq;

namespace MJ_CAIS.Tests.MappingTests
{
    public class CriminalRecordsReportProfileTests
    {
        private IMapper _mapper;
        private BBulletin bulletinDb;
        private BulletinType bulletinXsd;

        [SetUp]
        public void Setup()
        {
            _mapper = InitObjectHelper.GetMapper<CriminalRecordsReportProfile>();
        }

        [Test]
        public void BulletinFromXsdIsEqualToBulletinFromDB_AfterMap()
        {
            this.bulletinXsd = BulletinFactory.GetFilledInBulletinXSD(false);

            BBulletin bulletinFromXsd = _mapper.Map<BBulletin>(bulletinXsd);
            BulletinType mappedBulletinFromDd = _mapper.Map<BulletinType>(bulletinFromXsd);

            var comparator = new ObjectCompare<BulletinType>(bulletinXsd, mappedBulletinFromDd);
            var isEquals = comparator.IsEquals();
            var pathToDiff = comparator.GetPathOfDifference;

            Assert.AreEqual(isEquals, true);
            Assert.AreEqual(pathToDiff, null);
        }

        [Test]
        public void BulletinFromXsdIsEqualToBulletinFromDB_ForNomenclature_AfterMap()
        {
            this.bulletinXsd = BulletinFactory.GetFilledInBulletinXSD(true);

            BBulletin bulletinDb = _mapper.Map<BBulletin>(this.bulletinXsd);

            var decision = this.bulletinXsd.Conviction.ConvictionDecisions[0].Decision;
            var offence = this.bulletinXsd.Conviction.ConvictionOffence[0];
            var sanction = this.bulletinXsd.Conviction.ConvictionSanction[0];
            var sanctionProb = this.bulletinXsd.Conviction.ConvictionSanction[0].Probation[0];
            var caseAuth = this.bulletinXsd.Conviction.CriminalCase.CaseAuthority;
            var decisionAuth = this.bulletinXsd.Conviction.Decision.DecidingAuthority;
            var creatorAuth = this.bulletinXsd.IssuerData.BulletinCreatorAuthority;
            var personBithPlace = this.bulletinXsd.Person.BirthPlace;
            var personNationalities = this.bulletinXsd.Person.PersonNationality;

            //todo: трябва да мапят чрез заявки до базата там където се ползва мапинга
            Assert.AreEqual(this.bulletinXsd.RegistrationData.ConvictionStatusAuthority.Code, bulletinDb.CsAuthorityId);
            //Assert.AreEqual(decision.DecidingAuthority.DecidingAuthorityCodeEIK, bulletinDb.DecidingAuthId);
            Assert.AreEqual(offence.NationalCategoryCode, bulletinDb.BOffences.First().OffenceCatId);
            Assert.AreEqual(offence.OffenceCommonCategoryReference.OffenceCode, bulletinDb.BOffences.First().EcrisOffCatId);
            Assert.AreEqual(offence.OffencePlace.City.EKATTECode, bulletinDb.BOffences.First().OffPlaceCityId);
            // Assert.AreEqual(offence.OffencePlace.Country.CountryISONumber, bulletinDb.BOffences.First().OffPlaceCountryId);
            Assert.AreEqual(sanction.NationalCategoryCode, bulletinDb.BSanctions.First().SanctCategoryId);
            Assert.AreEqual(sanctionProb.ProbationCategoryCode, bulletinDb.BSanctions.First().BProbations.First().SanctProbCategId);
            Assert.AreEqual(sanctionProb.ProbationMeasureCode, bulletinDb.BSanctions.First().BProbations.First().SanctProbMeasureId);
            Assert.AreEqual(sanction.SanctionCommonCategoryReference.SanctionCode, bulletinDb.BSanctions.First().EcrisSanctCategId);
            //Assert.AreEqual(caseAuth.DecidingAuthorityCodeEIK, bulletinDb.CaseAuthId);
            //Assert.AreEqual(decisionAuth.DecidingAuthorityCodeEIK, bulletinDb.DecidingAuthId);
            //Assert.AreEqual(creatorAuth.DecidingAuthorityCodeEIK, bulletinDb.BulletinAuthorityId);
            Assert.AreEqual(personBithPlace.City.EKATTECode, bulletinDb.BirthCityId);
            //Assert.AreEqual(personBithPlace.Country.CountryISONumber, bulletinDb.BirthCountryId);

            //Assert.AreEqual(personNationalities.First().CountryISONumber, bulletinDb.BPersNationalities.First().CountryId);
            //Assert.AreEqual(personNationalities.Last().CountryISONumber, bulletinDb.BPersNationalities.Last().CountryId);
        }

        [Test]
        public void BulletinFromDbIsEqualToBulletinFromXsd_AfterMap()
        {
            this.bulletinDb = BulletinFactory.GetFilledInBulletinDb(false);

            BulletinType bulletinFromDb = _mapper.Map<BulletinType>(this.bulletinDb);
            BBulletin mappedBulletinFromXsd = _mapper.Map<BBulletin>(bulletinFromDb);

            var comparator = new ObjectCompare<BBulletin>(this.bulletinDb, mappedBulletinFromXsd);
            var isEquals = comparator.IsEquals();
            var pathToDiff = comparator.GetPathOfDifference;

            Assert.AreEqual(isEquals, true);
            Assert.AreEqual(pathToDiff, null);
        }

        [Test]
        public void BulletinFromDbIsEqualToBulletinFromXsd_ForNomenclature_AfterMap()
        {
            this.bulletinDb = BulletinFactory.GetFilledInBulletinDb(true);

            BulletinType bulletinXsd = _mapper.Map<BulletinType>(this.bulletinDb);

            var offence = this.bulletinDb.BOffences.First();
            var sanction = this.bulletinDb.BSanctions.First();
            var propbation = sanction.BProbations.First();

            Assert.AreEqual(bulletinDb.CsAuthority.Code, bulletinXsd.RegistrationData.ConvictionStatusAuthority.Code);
            Assert.AreEqual(bulletinDb.CsAuthority.Name, bulletinXsd.RegistrationData.ConvictionStatusAuthority.Name);

            Assert.AreEqual(offence.OffenceCat.Code, bulletinXsd.Conviction.ConvictionOffence[0].NationalCategoryCode);
            Assert.AreEqual(offence.OffenceCat.Name, bulletinXsd.Conviction.ConvictionOffence[0].NationalCategoryTitle);
            Assert.AreEqual(offence.EcrisOffCat.Id, bulletinXsd.Conviction.ConvictionOffence[0].OffenceCommonCategoryReference.OffenceCode);
            Assert.AreEqual(offence.EcrisOffCat.Name, bulletinXsd.Conviction.ConvictionOffence[0].OffenceCommonCategoryReference.OffenceName);

            Assert.AreEqual(sanction.SanctCategory.Code, bulletinXsd.Conviction.ConvictionSanction[0].NationalCategoryCode);
            Assert.AreEqual(sanction.SanctCategory.Name, bulletinXsd.Conviction.ConvictionSanction[0].NationalCategoryTitle);
            Assert.AreEqual(sanction.EcrisSanctCateg.Category, bulletinXsd.Conviction.ConvictionSanction[0].SanctionCommonCategoryReference.SanctionCode);
            Assert.AreEqual(sanction.EcrisSanctCateg.Name, bulletinXsd.Conviction.ConvictionSanction[0].SanctionCommonCategoryReference.SanctionText);

            Assert.AreEqual(propbation.SanctProbCateg.Code, bulletinXsd.Conviction.ConvictionSanction[0].Probation[0].ProbationCategoryCode);
            Assert.AreEqual(propbation.SanctProbCateg.Name, bulletinXsd.Conviction.ConvictionSanction[0].Probation[0].ProbationCategoryTitle);
            Assert.AreEqual(propbation.SanctProbMeasure.Code, bulletinXsd.Conviction.ConvictionSanction[0].Probation[0].ProbationMeasureCode);
            Assert.AreEqual(propbation.SanctProbMeasure.Name, bulletinXsd.Conviction.ConvictionSanction[0].Probation[0].ProbationMeasureTitle);
        }
    }
}
