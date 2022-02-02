using AutoMapper;
using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services
{
	public class BulletinService : BaseAsyncService<BulletinDTO, BulletinDTO, BulletinGridDTO, BBulletin, string, CaisDbContext>, IBulletinService
	{
		private readonly IBulletinRepository _bulletinRepository;

		public BulletinService(IMapper mapper, IBulletinRepository bulletinRepository) 
			: base(mapper, bulletinRepository)
		{
			_bulletinRepository = bulletinRepository;
		}

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
		{
			return false;
		}
	}
}
