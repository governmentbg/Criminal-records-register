using MJ_CAIS.AutoMapperContainer.MappingProfiles;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Tests.Helpers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MJ_CAIS.Tests.ServiceTests.ManagePerson
{
    public class GeneratePersonCitizenshipTests
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
        {
            Assert.Throws<ArgumentNullException>(() =>
                _peopleService.GeneratePersonCitizenship(null, new PPersonH(), null));
        }

        [Test]
        public void ThrowArgumentNullEx_WhenPersonHIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                _peopleService.GeneratePersonCitizenship(new PPerson(), null, null));
        }

        [Test]
        public void SetCitizenship_WhenHasNationalities()
        {
            // arrange 
            var person = new PPerson();
            var personH = new PPersonH();
            var nationalities = new List<string> { "BG" };

            // act
            _peopleService.GeneratePersonCitizenship(person, personH, nationalities);

            // assert
            Assert.AreEqual(person.PPersonCitizenships.Count, 1);
            Assert.That(person.PPersonCitizenships.Count(p => p.CountryId == nationalities[0]), Is.EqualTo(1));
        }

        [Test]
        public void SetCitizenshipH_WhenHasNationalities()
        {
            // arrange 
            var person = new PPerson();
            var personH = new PPersonH();
            var nationalities = new List<string> { "BG" };

            // act
            _peopleService.GeneratePersonCitizenship(person, personH, nationalities);

            // assert
            Assert.AreEqual(personH.PPersonHCitizenships.Count, 1);
            Assert.That(personH.PPersonHCitizenships.Count(p => p.CountryId == nationalities[0]), Is.EqualTo(1));
        }

        [Test]
        public void CountryCitizenshipsMustBeEquals_WhenHasNationalities()
        {
            // arrange 
            var person = new PPerson();
            var personH = new PPersonH();
            var nationalities = new List<string> { "BG" };

            // act
            _peopleService.GeneratePersonCitizenship(person, personH, nationalities);

            // assert
            Assert.AreEqual(person.PPersonCitizenships.First().CountryId, personH.PPersonHCitizenships.First().CountryId);
        }

        [Test]
        public void CitizenshipsWithEntityStateAdded_WhenHasNationalities()
        {
            // arrange 
            var person = new PPerson();
            var personH = new PPersonH();
            var nationalities = new List<string> { "BG" };

            // act
            _peopleService.GeneratePersonCitizenship(person, personH, nationalities);

            // assert
            Assert.AreEqual(person.PPersonCitizenships.First().EntityState, EntityStateEnum.Added);
            Assert.AreEqual(personH.PPersonHCitizenships.First().EntityState, EntityStateEnum.Added);
        }

        [Test]
        public void CitizenshipsAreEmpty_WhenDoesNotHaveNationalities()
        {
            // arrange 
            var person = new PPerson();
            var personH = new PPersonH();
            var nationalities = new List<string>();

            // act
            _peopleService.GeneratePersonCitizenship(person, personH, nationalities);

            // assert
            Assert.AreEqual(person.PPersonCitizenships.Count, 0);
            Assert.AreEqual(personH.PPersonHCitizenships.Count, 0);
        }

        [Test]
        public void CitizenshipsAreEmpty_WhenNationalitiesIsNull()
        {
            // arrange 
            var person = new PPerson();
            var personH = new PPersonH();

            // act
            _peopleService.GeneratePersonCitizenship(person, personH, null);

            // assert
            Assert.AreEqual(person.PPersonCitizenships.Count, 0);
            Assert.AreEqual(personH.PPersonHCitizenships.Count, 0);
        }
    }
}
