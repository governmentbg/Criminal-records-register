using MJ_CAIS.AutoMapperContainer.MappingProfiles;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DTO.Person;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Tests.Helpers;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace MJ_CAIS.Tests.ServiceTests.ManagePerson
{
    public class GetPidsTests
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
        public async Task ReturnOnlySuid_WhenAllPidsAreEmpty()
        {
            // arrange 
            var personDto = new PersonDTO();

            // act
            var result = await _peopleService.GetPidsFromFormAsync(personDto);

            // assert
            Assert.AreEqual(result.Count(), 1);
            Assert.AreEqual(result.First().Type, PersonConstants.PidType.Suid);
        }

        [Test]
        public async Task ReturnNotEmptySuid_WhenAllPidsAreEmpty()
        {
            // arrange 
            var personDto = new PersonDTO();

            // act
            var result = await _peopleService.GetPidsFromFormAsync(personDto);

            // assert
            Assert.That(result.Count(p => p.Type == PersonConstants.PidType.Suid && !string.IsNullOrEmpty(p.Pid)), Is.EqualTo(1));
        }

        [Test]
        public async Task ReturnSuidAndEgn_WhenOnlyEgnIsFilledIn()
        {
            // arrange 
            var personDto = new PersonDTO { Egn = "123456789" };

            // act
            var result = await _peopleService.GetPidsFromFormAsync(personDto);

            // assert     
            Assert.AreEqual(result.Count(), 2);
            Assert.That(result.Count(p => p.Type == PersonConstants.PidType.Suid), Is.EqualTo(1));
            Assert.That(result.Count(p => p.Type == PersonConstants.PidType.Egn), Is.EqualTo(1));
        }

        [Test]
        public async Task ReturnAllPids_WhenAllAreFilledIn()
        {
            // arrange 
            var personDto = new PersonDTO()
            {
                Egn = "123456789",
                Lnch = "15242563140",
                Ln = "1014141515",
                AfisNumber = "789658473"
            };

            // act
            var result = await _peopleService.GetPidsFromFormAsync(personDto);

            // assert     
            Assert.AreEqual(result.Count(), 5);
            Assert.That(result.Count(p => p.Type == PersonConstants.PidType.Egn), Is.EqualTo(1));
            Assert.That(result.Count(p => p.Type == PersonConstants.PidType.Lnch), Is.EqualTo(1));
            Assert.That(result.Count(p => p.Type == PersonConstants.PidType.Ln), Is.EqualTo(1));
            Assert.That(result.Count(p => p.Type == PersonConstants.PidType.AfisNumber), Is.EqualTo(1));
            Assert.That(result.Count(p => p.Type == PersonConstants.PidType.Suid), Is.EqualTo(1));
        }
    }
}
