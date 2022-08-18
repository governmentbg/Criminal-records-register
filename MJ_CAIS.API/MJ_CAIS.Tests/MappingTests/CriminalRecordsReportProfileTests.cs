using AutoMapper;
using MJ_CAIS.AutoMapperContainer.MappingProfiles;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.ExternalServicesHost;
using MJ_CAIS.Tests.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task BulletinFromDbIsEqualToBulletinXsd_AfterMap()
        {
            //var src = PersonFactory.GetFilledInPersonDto();
            //var dest = await _peopleService.CreatePersonAsync(src);          
            //Assert.AreEqual(src.Firstname, dest.Firstname);
        }

    }
}
