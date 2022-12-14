using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Exceptions;
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


        public async Task<string> SearchByIdentifier(string id)
        {
            string currentUserAuth = _userContext.CsAuthorityId ?? null ?? "660";
            string registrationNumber = await this._registerTypeService.GetRegisterNumberForApplicationOnDesk(currentUserAuth);
            ApplicationInDTO application = new ApplicationInDTO()
            { RegistrationNumber = registrationNumber, StatusCode = ApplicationConstants.ApplicationStatuses.NewId, Person = new PersonDTO() { Egn = id } };
            string applicationId = await _applicationService.InsertAsync(application);

            _dbContext.ChangeTracker.Clear();
            await CallPersonDataSearch(id, registrationNumber, applicationId: applicationId);

            return applicationId;
        }

        public async Task CallPersonDataSearch(string egn, string registrationNumber, string applicationId, string reportApplicationId = null)
        {
            try
            {
                (PersonDataResponseType, EWebRequest) result = await this._regixService.SyncCallPersonDataSearch(egn, applicationId: applicationId, registrationNumber: registrationNumber, reportApplicationId: reportApplicationId);
                if (result.Item1.EGN == null)
                {
                    throw new BusinessLogicException($"Няма намерени данни:{applicationId ?? reportApplicationId}");
                }

                if (result.Item2.HasError == true)
                {
                    throw new BusinessLogicException($"RegiX e недостъпен:{applicationId ?? reportApplicationId}");
                }
            }
            catch (Exception e)
            {
                throw new BusinessLogicException($"Възникна грешка при извършване на операцията:{applicationId ?? reportApplicationId}");
            }
        }

        public async Task<string> SearchByIdentifierLNCH(string id)
        {
            string currentUserAuth = _userContext.CsAuthorityId ?? null ?? "660";
            string registrationNumber = await this._registerTypeService.GetRegisterNumberForApplicationOnDesk(currentUserAuth);
            ApplicationInDTO applicaiton = new ApplicationInDTO()
            { RegistrationNumber = registrationNumber, StatusCode = ApplicationConstants.ApplicationStatuses.NewId, Person = new PersonDTO() { Lnch = id } };
            string applicationId = await _applicationService.InsertAsync(applicaiton);

            _dbContext.ChangeTracker.Clear();
            await CallForeignIdentitySearch(id, registrationNumber, applicationId: applicationId);
            return applicationId;

        }

        public async Task CallForeignIdentitySearch(string id, string registrationNumber, string applicationId, string reportApplicationId = null)
        {
            (ForeignIdentityInfoResponseType, EWebRequest) result = await this._regixService.SyncCallForeignIdentitySearchV2(id, applicationId: applicationId, registrationNumber: registrationNumber, reportApplicationId: reportApplicationId);
            if (result.Item1?.LNCh == null) //TODO: shoud be ==
            {
                throw new BusinessLogicException($"Няма намерени данни:{applicationId ?? reportApplicationId}");
            }

            if (result.Item2.HasError == true)
            {
                throw new BusinessLogicException($"RegiX e недостъпен:{applicationId ?? reportApplicationId}");
            }
        }
    }
}
