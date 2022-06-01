using AutoMapper;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.ExternalServicesHost;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Services
{
    public class EISSIntegrationService : IEISSIntegrationService
    {
        private readonly IBulletinRepository _bulletinRepository;
        private readonly IMapper _mapper;

        public EISSIntegrationService(IMapper mapper, IBulletinRepository bulletinRepository)
        {
            _mapper = mapper;
            _bulletinRepository = bulletinRepository;
        }

        public void SendBulletinsData(SendBulletinsDataRequestType value)
        {
            var ekatteCodes = value.BulletinsList.Bulletin.
                Select(x => x.Person.BirthPlace.City.EKATTECode)
                .Where(x=> !string.IsNullOrEmpty(x))
                .Distinct()
                .ToList();

            var authEkatte = _bulletinRepository.GetAuthIdByEkkate(ekatteCodes);

            var bulletinToBeSaved = new List<BBulletin>();
            foreach (var item in value.BulletinsList.Bulletin)
            {
                var currentBull = _mapper.Map<BBulletin>(item);
                currentBull.StatusId = BulletinConstants.Status.NewEISS;
                currentBull.CaseAuthId = authEkatte.ContainsKey(currentBull.BirthCity.EkatteCode) ?
                    authEkatte[currentBull.BirthCity.EkatteCode] :
                    "660";

                bulletinToBeSaved.Add(currentBull);
            }

            _bulletinRepository.SaveBulletins(bulletinToBeSaved);
        }


        public void SendFinesData(SendFineDataRequestType value)
        {
            throw new NotImplementedException();
        }
    }
}
