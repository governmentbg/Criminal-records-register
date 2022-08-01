using MJ_CAIS.AutoMapperContainer.MappingProfiles;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Common;
using MJ_CAIS.DTO.Person;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Tests.Factories;
using MJ_CAIS.Tests.Helpers;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MJ_CAIS.Tests.ServiceTests.ManagePerson
{
    public class CreatePersonTests
    {
        private IManagePersonService _peopleService;
        private Mock<IPersonRepository> _repository;

        [SetUp]
        public void Setup()
        {
            var mapper = InitObjectHelper.GetMapper<PersonProfile>();
            _repository = new Mock<IPersonRepository>();
            _peopleService = new ManagePersonService(_repository.Object, mapper);
            _repository.Setup(x => x.GetPersonIdsAsync(It.IsAny<List<PersonIdTypeDTO>>(), It.IsAny<string>()))
                .ReturnsAsync(new List<PPersonId> { PersonFactory.GetFilledPersonId() });
        }

        #region When pid it does not exists in db

        [Test]
        public async Task PersonIsEqualToPersonDto_WhenAllDataIsFilledIn()
        {
            var src = PersonFactory.GetFilledInPersonDto();


            var dest = await _peopleService.CreatePersonAsync(src);

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
        public async Task PersonWithEntityStateAdded_WhenAllDataIsFilledIn()
        {
            var src = PersonFactory.GetFilledInPersonDto();
            var dest = await _peopleService.CreatePersonAsync(src);

            Assert.AreEqual(dest.EntityState, EntityStateEnum.Added);
        }

        [Test]
        public async Task PersonHasPersonIds_WhenIdsAreNotNullOrEmpty()
        {
            var src = PersonFactory.GetFilledInPersonDto(true);
            src.Lnch = null;
            src.AfisNumber = null;
            src.Ln = null;

            var dest = await _peopleService.CreatePersonAsync(src);

            Assert.IsNotEmpty(dest.PPersonIds);
            Assert.AreEqual(dest.PPersonIds.Count, 1);
            Assert.AreEqual(dest.PPersonIds.First().Pid, PersonFactory.GetFilledPersonId().Pid);
        }

        [Test]
        public async Task PersonIdIsEqual_WhenPidIsFilledIn()
        {
            var src = PersonFactory.GetFilledInPersonDto();

            src.Lnch = null;
            src.AfisNumber = null;
            src.Ln = null;

            var pids = new List<PPersonId> { new()
            {
                Pid = "1010101010",
                PidTypeId = PersonConstants.PidType.Egn,
                Issuer = PersonConstants.IssuerType.MVR,
                EntityState = EntityStateEnum.Added
            },
                new()
                {
                    Pid = "13231321",
                    PidTypeId = PersonConstants.PidType.Lnch,
                    Issuer = PersonConstants.IssuerType.EU,
                    EntityState = EntityStateEnum.Added
                }
            };

            _repository.Setup(x => x.GetPersonIdsAsync(It.IsAny<List<PersonIdTypeDTO>>(), It.IsAny<string>())).ReturnsAsync(pids);

            var dest = await _peopleService.CreatePersonAsync(src);

            Assert.AreEqual(dest.PPersonIds.Count, pids.Count);

            Assert.AreEqual(dest.PPersonIds.First().Pid, pids.First().Pid);
            Assert.AreEqual(dest.PPersonIds.First().PidTypeId, pids.First().PidTypeId);
            Assert.AreEqual(dest.PPersonIds.First().Issuer, pids.First().Issuer);

            Assert.AreEqual(dest.PPersonIds.Last().Pid, pids.Last().Pid);
            Assert.AreEqual(dest.PPersonIds.Last().PidTypeId, pids.Last().PidTypeId);
            Assert.AreEqual(dest.PPersonIds.Last().Issuer, pids.Last().Issuer);
        }

        [Test]
        public async Task PersonPidsWithEntityStateAdded_WhenAllDataIsFilledIn()
        {
            var src = PersonFactory.GetFilledInPersonDto();
            var person = await _peopleService.CreatePersonAsync(src);

            Assert.That(person.PPersonIds.Count(p => p.EntityState == EntityStateEnum.Added), Is.EqualTo(person.PPersonIds.Count));
        }

        #endregion

        #region When pid it does not exists in db

        [Test]
        public async Task PersonIsEqualToPersonDto_WhenPersonExist()
        {
            var src = PersonFactory.GetFilledInPersonDto();

            var pids = new List<PPersonId>
            {
                PersonFactory.GetFilledPersonId(PersonConstants.PidType.Suid,entityState:EntityStateEnum.Unchanged)
            };

            _repository.Setup(x => x.GetPersonIdsAsync(It.IsAny<List<PersonIdTypeDTO>>(), It.IsAny<string>()))
                .ReturnsAsync(pids);

            _repository.Setup(x => x.GetExistingPersonWithPidsDataAsync(It.IsAny<string>()))
                .ReturnsAsync(new PPerson { PPersonIds = pids });

            var dest = await _peopleService.CreatePersonAsync(src);

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
        public async Task PersonIdIsEqualToPidPersonId_WhenOnlyOnePersonExist()
        {
            var src = PersonFactory.GetFilledInPersonDto();

            var pids = new List<PPersonId>
            {
                PersonFactory.GetFilledPersonId(PersonConstants.PidType.Suid,entityState:EntityStateEnum.Unchanged)
            };

            _repository.Setup(x => x.GetPersonIdsAsync(It.IsAny<List<PersonIdTypeDTO>>(), It.IsAny<string>()))
                .ReturnsAsync(pids);

            _repository.Setup(x => x.GetExistingPersonWithPidsDataAsync(It.IsAny<string>()))
                .ReturnsAsync(new PPerson { PPersonIds = pids, Id = "fd44544sds54d7sds5d" });

            var dest = await _peopleService.CreatePersonAsync(src);

            Assert.IsNotEmpty(dest.Id);
            Assert.AreEqual(dest.Id, pids.First().PersonId);
        }

        [Test]
        public async Task PersonContainsNewAndSavedPids_WhenOnlyOnePersonExist()
        {
            var src = PersonFactory.GetFilledInPersonDto();

            var pids = new List<PPersonId>
            {
                PersonFactory.GetFilledPersonId(PersonConstants.PidType.Lnch,entityState:EntityStateEnum.Unchanged),
                PersonFactory.GetFilledPersonId()
            };

            _repository.Setup(x => x.GetPersonIdsAsync(It.IsAny<List<PersonIdTypeDTO>>(), It.IsAny<string>()))
                .ReturnsAsync(pids);

            _repository.Setup(x => x.GetExistingPersonWithPidsDataAsync(It.IsAny<string>()))
                .ReturnsAsync(new PPerson { PPersonIds = pids, Id = pids.First()?.PersonId });

            var dest = await _peopleService.CreatePersonAsync(src);

            Assert.IsNotEmpty(dest.PPersonIds);
            Assert.AreEqual(dest.PPersonIds.Count, pids.Count + 1); // +1 for generated suid
        }

        [Test]
        public async Task PersonPidsWithEntityState_WhenOnlyOnePersonExist()
        {
            var src = PersonFactory.GetFilledInPersonDto();

            var  newPids = new List<PPersonId>
            {
                PersonFactory.GetFilledPersonId(PersonConstants.PidType.Egn)
            };

            _repository.Setup(x => x.GetPersonIdsAsync(It.IsAny<List<PersonIdTypeDTO>>(), It.IsAny<string>()))
                .ReturnsAsync(newPids);

            var existingPid = PersonFactory.GetFilledPersonId(PersonConstants.PidType.Lnch, entityState: EntityStateEnum.Unchanged);
            _repository.Setup(x => x.GetExistingPersonWithPidsDataAsync(It.IsAny<string>()))
                .ReturnsAsync(new PPerson { PPersonIds = new List<PPersonId> { existingPid } });

            var dest = await _peopleService.CreatePersonAsync(src);

            Assert.That(dest.PPersonIds.Any(x => x.PidTypeId == PersonConstants.PidType.Egn && x.Pid == newPids.First().Pid && x.EntityState == EntityStateEnum.Added), Is.EqualTo(true));
            Assert.That(dest.PPersonIds.Any(x => x.PidTypeId == PersonConstants.PidType.Lnch && x.Pid == existingPid.Pid && x.EntityState == EntityStateEnum.Unchanged), Is.EqualTo(true));
        }

        #endregion

        #region Shared

        [Test]
        public async Task SetCitizenship_WhenHasNationalities()
        {
            // arrange 
            var personDTO = PersonFactory.GetFilledInPersonDto();

            // act
            var person = await _peopleService.CreatePersonAsync(personDTO);

            // assert
            Assert.AreEqual(person.PPersonCitizenships.Count, 1);
            Assert.That(person.PPersonCitizenships.Any(p => p.CountryId == personDTO.Nationalities.SelectedForeignKeys.First()), Is.EqualTo(true));
        }

        [Test]
        public async Task SetCitizenshipH_WhenHasNationalities()
        {
            // arrange 
            var personDTO = PersonFactory.GetFilledInPersonDto();

            // act
            var person = await _peopleService.CreatePersonAsync(personDTO);
            var personH = _peopleService.CreatePersonHistory(person);

            // assert
            Assert.AreEqual(personH.PPersonHCitizenships.Count, 1);
            Assert.That(personH.PPersonHCitizenships.Any(p => p.CountryId == personDTO.Nationalities.SelectedForeignKeys.First()), Is.EqualTo(true));
        }

        [Test]
        public async Task CountryCitizenshipsMustBeEquals_WhenHasNationalities()
        {
            // arrange 
            var personDTO = PersonFactory.GetFilledInPersonDto();

            // act
            var person = await _peopleService.CreatePersonAsync(personDTO);
            var personH = _peopleService.CreatePersonHistory(person);

            // assert
            Assert.AreEqual(person.PPersonCitizenships.First().CountryId, personH.PPersonHCitizenships.First().CountryId);
        }

        [Test]
        public async Task CitizenshipsWithEntityStateAdded_WhenHasNationalities()
        {
            // arrange 
            var personDTO = PersonFactory.GetFilledInPersonDto();

            // act
            var person = await _peopleService.CreatePersonAsync(personDTO);
            var personH = _peopleService.CreatePersonHistory(person);

            // assert
            Assert.AreEqual(person.PPersonCitizenships.First().EntityState, EntityStateEnum.Added);
            Assert.AreEqual(personH.PPersonHCitizenships.First().EntityState, EntityStateEnum.Added);
        }

        [Test]
        public async Task CitizenshipsAreEmpty_WhenDoesNotHaveNationalities()
        {
            // arrange 
            var personDTO = PersonFactory.GetFilledInPersonDto();
            personDTO.Nationalities = new MultipleChooseDTO();
            // act
            var person = await _peopleService.CreatePersonAsync(personDTO);
            var personH = _peopleService.CreatePersonHistory(person);

            // assert
            Assert.AreEqual(person.PPersonCitizenships.Count, 0);
            Assert.AreEqual(personH.PPersonHCitizenships.Count, 0);
        }

        [Test]
        public async Task CitizenshipsAreEmpty_WhenNationalitiesIsNull()
        {
            // arrange 
            var personDTO = PersonFactory.GetFilledInPersonDto();
            personDTO.Nationalities = null;
            // act
            var person = await _peopleService.CreatePersonAsync(personDTO);
            var personH = _peopleService.CreatePersonHistory(person);

            // assert
            Assert.AreEqual(person.PPersonCitizenships.Count, 0);
            Assert.AreEqual(personH.PPersonHCitizenships.Count, 0);
        }

        #endregion
    }
}
