using MJ_CAIS.DataAccess;
using MJ_CAIS.Services.Contracts;

namespace MJ_CAIS.ExternalWebServices.DbServices
{
    public class SearchByIdentifierService : ISearchByIdentifierService
    {
        private readonly IRegixService _regixService;
        private readonly IRegisterTypeService _registerTypeService;
        private readonly IUserContext _userContext;

        public SearchByIdentifierService(IRegixService regixService, IRegisterTypeService registerTypeService, IUserContext userContext)
        {
            _regixService = regixService;
            _registerTypeService = registerTypeService;
            _userContext = userContext;
        }


        public async void SearchByIdentifier(string id)
        {
            var currentUserAuth = _userContext.CsAuthorityId;
            var registrationNumber = await this._registerTypeService.GetRegisterNumberForApplicationOnDesk(currentUserAuth);
            this._regixService.SyncCallPersonDataSearch(id, null, registrationNumber);
        }

    }
}
