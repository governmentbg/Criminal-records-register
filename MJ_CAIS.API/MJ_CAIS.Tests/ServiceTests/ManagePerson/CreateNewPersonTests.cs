using MJ_CAIS.AutoMapperContainer.MappingProfiles;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Person;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Tests.Factories;
using MJ_CAIS.Tests.Helpers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using MJ_CAIS.Common.Constants;

namespace MJ_CAIS.Tests.ServiceTests.ManagePerson
{
    public class CreateNewPersonTests
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
            => Assert.Throws<ArgumentNullException>(() =>
                    _peopleService.CreateNewPerson(null, new List<PPersonId> { new() }, "test"));

        [Test]
        public void ThrowArgumentNullEx_WhenPersonPidsAreNull()
            => Assert.Throws<ArgumentNullException>(() =>
                _peopleService.CreateNewPerson(new PersonDTO(), null, "test"));

        [Test]
        public void ThrowArgumentNullEx_WhenPersonPidsAreEmpty()
            => Assert.Throws<ArgumentNullException>(() =>
                _peopleService.CreateNewPerson(new PersonDTO(), new List<PPersonId>(), "test"));

        [Test]
        public void ThrowArgumentNullEx_WhenPersonIdIsNull()
            => Assert.Throws<ArgumentNullException>(() =>
                _peopleService.CreateNewPerson(new PersonDTO(), new List<PPersonId> { new() }, null));

        [Test]
        public void ThrowArgumentNullEx_WhenPersonIdIsEmpty()
            => Assert.Throws<ArgumentNullException>(() =>
                _peopleService.CreateNewPerson(new PersonDTO(), new List<PPersonId> { new() }, ""));

        [Test]
        public void PersonIsEqualToPersonDto_WhenAllDataIsFilledIn()
        {
            var src = PersonFactory.GetFilledInPersonDto();
            var dest = _peopleService.CreateNewPerson(src, new List<PPersonId> { new() }, "123");

            Assert.AreEqual(src.Firstname, dest.Firstname);
            Assert.AreEqual(src.Surname, dest.Surname);
            Assert.AreEqual(src.Familyname, dest.Familyname);
            Assert.AreEqual(src.Fullname, dest.Fullname);
            Assert.AreEqual(src.Sex, dest.Sex);
            Assert.AreEqual(src.BirthDate, dest.BirthDate);
            Assert.AreEqual(src.BirthPlace.ForeignCountryAddress, dest.BirthPlaceOther);
            Assert.AreEqual(src.MotherFirstname, dest.MotherFirstname);
            Assert.AreEqual(src.FatherSurname, dest.FatherSurname);
            Assert.AreEqual(src.MotherSurname, dest.MotherSurname);
            Assert.AreEqual(src.MotherFamilyname, dest.MotherFamilyname);
            Assert.AreEqual(src.MotherFullname, dest.MotherFullname);
            Assert.AreEqual(src.FatherFirstname, dest.FatherFirstname);
            Assert.AreEqual(src.FatherFamilyname, dest.FatherFamilyname);
            Assert.AreEqual(src.FatherFullname, dest.FatherFullname);
            Assert.AreEqual(src.BirthPlace.CityId, dest.BirthCityId);
            Assert.AreEqual(src.BirthPlace.Country.Id, dest.BirthCountryId);
            Assert.AreEqual(src.FirstnameLat, dest.FirstnameLat);
            Assert.AreEqual(src.SurnameLat, dest.SurnameLat);
            Assert.AreEqual(src.FamilynameLat, dest.FamilynameLat);
            Assert.AreEqual(src.FullnameLat, dest.FullnameLat);
            Assert.AreEqual(src.Firstname, dest.Firstname);
            Assert.AreEqual(src.Firstname, dest.Firstname);
            Assert.AreEqual(src.Firstname, dest.Firstname);
            Assert.AreEqual(src.Firstname, dest.Firstname);
        }

        [Test]
        public void PersonWithEntityStateAdded_WhenAllDataIsFilledIn()
        {
            var src = PersonFactory.GetFilledInPersonDto();
            var dest = _peopleService.CreateNewPerson(src, new List<PPersonId>{new ()}, "123");

            Assert.AreEqual(dest.EntityState, EntityStateEnum.Added);
        }

        [Test]
        public void PersonHasPersonIds_WhenIdsAreNotNullOrEmpty()
        {
            var src = PersonFactory.GetFilledInPersonDto();
            var pids = new List<PPersonId> { new() { Pid = "1010101010", PidTypeId = PersonConstants.PidType.Egn } };

            var dest = _peopleService.CreateNewPerson(src, pids, "123");

            Assert.IsNotEmpty(dest.PPersonIds);
            Assert.AreEqual(dest.PPersonIds.Count, pids.Count);
            Assert.AreEqual(dest.PPersonIds.First().Pid, pids.First().Pid);
        }

        [Test]
        public void PersonIdIsEqual_WhenPidIsFilledIn()
        {
            var src = PersonFactory.GetFilledInPersonDto();
            var pids = new List<PPersonId> { new()
            {
                Pid = "1010101010", 
                PidTypeId = PersonConstants.PidType.Egn,
                Issuer = PersonConstants.IssuerType.MVR
            } };

            var dest = _peopleService.CreateNewPerson(src, pids, "123");

            Assert.AreEqual(dest.PPersonIds.First().Pid, pids.First().Pid);
            Assert.AreEqual(dest.PPersonIds.First().PidTypeId, pids.First().PidTypeId);
            Assert.AreEqual(dest.PPersonIds.First().Issuer, pids.First().Issuer);
        }

        [Test]
        public void PersonIsEqualToPersonH_WhenAllDataIsFilledIn()
        {
            var src = PersonFactory.GetFilledInPersonDto();
            var dest = _peopleService.CreateNewPerson(src, new List<PPersonId> { new() }, "123");

            Assert.AreEqual(src.Firstname, dest.Firstname);
            Assert.AreEqual(src.Surname, dest.Surname);
            Assert.AreEqual(src.Familyname, dest.Familyname);
            Assert.AreEqual(src.Fullname, dest.Fullname);
            Assert.AreEqual(src.Sex, dest.Sex);
            Assert.AreEqual(src.BirthDate, dest.BirthDate);
            Assert.AreEqual(src.BirthPlace.ForeignCountryAddress, dest.BirthPlaceOther);
            Assert.AreEqual(src.MotherFirstname, dest.MotherFirstname);
            Assert.AreEqual(src.FatherSurname, dest.FatherSurname);
            Assert.AreEqual(src.MotherSurname, dest.MotherSurname);
            Assert.AreEqual(src.MotherFamilyname, dest.MotherFamilyname);
            Assert.AreEqual(src.MotherFullname, dest.MotherFullname);
            Assert.AreEqual(src.FatherFirstname, dest.FatherFirstname);
            Assert.AreEqual(src.FatherFamilyname, dest.FatherFamilyname);
            Assert.AreEqual(src.FatherFullname, dest.FatherFullname);
            Assert.AreEqual(src.BirthPlace.CityId, dest.BirthCityId);
            Assert.AreEqual(src.BirthPlace.Country.Id, dest.BirthCountryId);
            Assert.AreEqual(src.FirstnameLat, dest.FirstnameLat);
            Assert.AreEqual(src.SurnameLat, dest.SurnameLat);
            Assert.AreEqual(src.FamilynameLat, dest.FamilynameLat);
            Assert.AreEqual(src.FullnameLat, dest.FullnameLat);
            Assert.AreEqual(src.Firstname, dest.Firstname);
            Assert.AreEqual(src.Firstname, dest.Firstname);
            Assert.AreEqual(src.Firstname, dest.Firstname);
            Assert.AreEqual(src.Firstname, dest.Firstname);
        }

        [Test]
        public void PersonPidsWithEntityStateAdded_WhenAllDataIsFilledIn()
        {
            var src = PersonFactory.GetFilledInPersonDto();
            var person = _peopleService.CreateNewPerson(src, new List<PPersonId>{new (){EntityState = EntityStateEnum.Added}}, "123");

            Assert.That(person.PPersonIds.Count(p => p.EntityState == EntityStateEnum.Added), Is.EqualTo(person.PPersonIds.Count));
        }
    }
}
