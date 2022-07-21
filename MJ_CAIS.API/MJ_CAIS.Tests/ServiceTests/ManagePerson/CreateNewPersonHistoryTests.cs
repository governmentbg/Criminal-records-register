using MJ_CAIS.AutoMapperContainer.MappingProfiles;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Tests.Factories;
using MJ_CAIS.Tests.Helpers;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace MJ_CAIS.Tests.ServiceTests.ManagePerson
{
    internal class CreateNewPersonHistoryTests
    {
        private IManagePersonService _peopleService;
        private Mock<IPersonRepository> _repository;

        [SetUp]
        public void Setup()
        {
            var mapper = InitObjectHelper.GetMapper<PersonProfile>();
            _repository = new Mock<IPersonRepository>();
            _peopleService = new ManagePersonService(_repository.Object, mapper);
        }

        [Test]
        public void ThrowArgumentNullEx_WhenPersonIsNull()
            => Assert.Throws<ArgumentNullException>(() => _peopleService.CreateNewPersonHistory(null));

        [Test]
        public void PersonHIsEqual_WhenPersonDataIsFilledIn()
        {
            var src = PersonFactory.GetFilledInPerson();
            var dest = _peopleService.CreateNewPersonHistory(src);

            Assert.AreEqual(src.Firstname, dest.Firstname);
            Assert.AreEqual(src.Surname, dest.Surname);
            Assert.AreEqual(src.Familyname, dest.Familyname);
            Assert.AreEqual(src.Fullname, dest.Fullname);
            Assert.AreEqual(src.Sex, dest.Sex);
            Assert.AreEqual(src.BirthDate, dest.BirthDate);
            Assert.AreEqual(src.MotherFirstname, dest.MotherFirstname);
            Assert.AreEqual(src.FatherSurname, dest.FatherSurname);
            Assert.AreEqual(src.MotherSurname, dest.MotherSurname);
            Assert.AreEqual(src.MotherFamilyname, dest.MotherFamilyname);
            Assert.AreEqual(src.MotherFullname, dest.MotherFullname);
            Assert.AreEqual(src.FatherFirstname, dest.FatherFirstname);
            Assert.AreEqual(src.FatherFamilyname, dest.FatherFamilyname);
            Assert.AreEqual(src.FatherFullname, dest.FatherFullname);
            Assert.AreEqual(src.FirstnameLat, dest.FirstnameLat);
            Assert.AreEqual(src.SurnameLat, dest.SurnameLat);
            Assert.AreEqual(src.FamilynameLat, dest.FamilynameLat);
            Assert.AreEqual(src.FullnameLat, dest.FullnameLat);
            Assert.AreEqual(src.Firstname, dest.Firstname);
            Assert.AreEqual(src.Firstname, dest.Firstname);
            Assert.AreEqual(src.Firstname, dest.Firstname);
            Assert.AreEqual(src.Firstname, dest.Firstname);
            Assert.AreEqual(src.BirthCityId, dest.BirthCityId);
            Assert.AreEqual(src.BirthPlaceOther, dest.BirthPlaceOther);
            Assert.AreEqual(src.BirthCountry, dest.BirthCityId);
        }

        [Test]
        public void PersonHPidsAreEqual_WhenPersonPidsAreFilledIn()
        {
            var person = PersonFactory.GetFilledInPerson();
            var personH = _peopleService.CreateNewPersonHistory(person);

            Assert.AreEqual(person.PPersonIds.Count, personH.PPersonIdsHes.Count);
            Assert.AreEqual(person.PPersonIds.First().Pid, personH.PPersonIdsHes.First().Pid);
            Assert.AreEqual(person.PPersonIds.First().Issuer, personH.PPersonIdsHes.First().Issuer);
            Assert.AreEqual(person.PPersonIds.First().CountryId, personH.PPersonIdsHes.First().CountryId);
        }

        [Test]
        public void PersonHWithEntityStateAdded_WhenAllDataIsFilledIn()
        {
            var person = PersonFactory.GetFilledInPerson();
            var personH = _peopleService.CreateNewPersonHistory(person);

            Assert.AreEqual(personH.EntityState, EntityStateEnum.Added);
        }

        [Test]
        public void PersonPidsHWithEntityStateAdded_WhenAllDataIsFilledIn()
        {
            var person = PersonFactory.GetFilledInPerson();
            var personH = _peopleService.CreateNewPersonHistory(person);

            Assert.That(personH.PPersonIdsHes.Count(p => p.EntityState == EntityStateEnum.Added), Is.EqualTo(personH.PPersonIdsHes.Count));
        }
    }
}
