using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.DTO.Person;
using MJ_CAIS.Services.Contracts;
using TechnoLogica.RegiX.GraoNBDAdapter;
using TechnoLogica.RegiX.MVRERChAdapterV2;

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


        public async Task<(string, EWebRequest)> SearchByIdentifier(string id)
        {
            string currentUserAuth = "660"; // _userContext.CsAuthorityId;
            string registrationNumber = await this._registerTypeService.GetRegisterNumberForApplicationOnDesk(currentUserAuth);
            ApplicationInDTO applicaiton = new ApplicationInDTO()
                { RegistrationNumber = registrationNumber, Person = new PersonDTO() { Egn = id } };
            string applicationId = await _applicationService.InsertAsync(applicaiton);

            _dbContext.ChangeTracker.Clear();
            (PersonDataResponseType, EWebRequest) result = this._regixService.SyncCallPersonDataSearch(id, applicationId: applicationId);
            return (applicationId, result.Item2);
            
        }

        public async Task<(string, EWebRequest)> SearchByIdentifierLNCH(string id)
        {
            string currentUserAuth = "660"; // _userContext.CsAuthorityId;
            string registrationNumber = await this._registerTypeService.GetRegisterNumberForApplicationOnDesk(currentUserAuth);
            ApplicationInDTO applicaiton = new ApplicationInDTO()
                { RegistrationNumber = registrationNumber, Person = new PersonDTO() { Egn = id } };
            string applicationId = await _applicationService.InsertAsync(applicaiton);

            _dbContext.ChangeTracker.Clear();
            (ForeignIdentityInfoResponseType, EWebRequest) result = this._regixService.SyncCallPersonDataSearchByLNCH(id, applicationId: applicationId);
            return (applicationId, result.Item2);

        }

    }
}
