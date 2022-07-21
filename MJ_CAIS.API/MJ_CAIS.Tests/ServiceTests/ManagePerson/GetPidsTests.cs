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
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
            Assert.AreEqual(result.Count(), 1);
            Assert.AreEqual(result.First().Type, PersonConstants.PidType.Suid);
            Assert.NotNull(result.First().Pid);
            Assert.IsNotEmpty(result.First().Pid);
        }
    }
}
