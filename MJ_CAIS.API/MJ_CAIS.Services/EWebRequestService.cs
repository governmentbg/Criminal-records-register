using AutoMapper;
using MJ_CAIS.Common.XmlData;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EWebRequest;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using System.Text;

namespace MJ_CAIS.Services
{
    public class EWebRequestsService : BaseAsyncService<EWebRequestDTO, EWebRequestDTO, EWebRequestGridDTO, EWebRequest, string, CaisDbContext>, IEWebRequestsService
    {
        public EWebRequestsService(IMapper mapper,
            IEWebRequestsRepository eWebRequestsRepository
            ) : base(mapper, eWebRequestsRepository)
        {
        }

        public async Task<byte[]> GetXmlTransformationById(string aId)
        {
            var repoObj = await baseAsyncRepository.SelectAsync(aId);
            var html = XmlUtils.XmlTransform(repoObj.WebService.ResponseXslt, repoObj.ResponseXml);
            var result = Encoding.UTF8.GetBytes(html);

            return await Task.FromResult(result);
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }

    }
}
