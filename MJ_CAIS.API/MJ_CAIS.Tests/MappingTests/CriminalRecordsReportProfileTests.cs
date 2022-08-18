using AutoMapper;
using MJ_CAIS.AutoMapperContainer.MappingProfiles;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.ExternalServicesHost;
using MJ_CAIS.Tests.Factories;
using MJ_CAIS.Tests.Helpers;
using NUnit.Framework;

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
            this.bulletinXsd = BulletinFactory.GetFilledInBulletinXSD();
        }

        [Test]
        public void BulletinFromXsdIsEqualToBulletinFromDB_AfterMap()
        {
            BBulletin bulletinFromXsd = _mapper.Map<BBulletin>(bulletinXsd);
            BulletinType mappedBulletinFromDd = _mapper.Map<BulletinType>(bulletinFromXsd);

            var comparator = new ObjectCompare<BulletinType>(bulletinXsd, mappedBulletinFromDd);
            var isEquals = comparator.IsEquals();
            var pathToDiff = comparator.GetPathOfDifference;

            Assert.AreEqual(isEquals, true);
            Assert.AreEqual(pathToDiff, null);
        }
    }
}
