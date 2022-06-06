using MJ_CAIS.DataAccess;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.DTO.Person;
using MJ_CAIS.Services.Contracts;

namespace MJ_CAIS.ExternalWebServices.DbServices
{
    public class SearchByIdentifierService : ISearchByIdentifierService
    {
        private readonly IRegixService _regixService;
        private readonly IRegisterTypeService _registerTypeService;
        private readonly IApplicationService _applicationService;
        private readonly IUserContext _userContext;
        private readonly CaisDbContext _dbContext;

        public SearchByIdentifierService(IRegixService regixService, IRegisterTypeService registerTypeService, IUserContext userContext, IApplicationService applicationService, CaisDbContext dbContext)
        {
            _regixService = regixService;
            _registerTypeService = registerTypeService;
            _userContext = userContext;
            _applicationService = applicationService;
            _dbContext = dbContext;
        }


        public async void SearchByIdentifier(string id)
        {
            var currentUserAuth = "660"; // _userContext.CsAuthorityId;
            var registrationNumber = await this._registerTypeService.GetRegisterNumberForApplicationOnDesk(currentUserAuth);
            var applicaiton = new ApplicationInDTO()
                { RegistrationNumber = registrationNumber, Person = new PersonDTO() { Egn = id } };
            var applicationId = await _applicationService.InsertAsync(applicaiton);

            _dbContext.ChangeTracker.Clear();
            this._regixService.SyncCallPersonDataSearch(id, applicationId: applicationId);
        }

    }
}
