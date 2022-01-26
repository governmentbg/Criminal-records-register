using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.Entities;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;

namespace MJ_CAIS.Services
{
	public class BulletinService : BaseAsyncService<BulletinDTO, BulletinDTO, BulletinGridDTO, Bulletin, string, CaisDbContext>, IBulletinService
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
