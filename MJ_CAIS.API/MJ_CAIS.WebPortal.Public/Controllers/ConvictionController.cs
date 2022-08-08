using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.WebPortal.Public.Models.Conviction;

namespace MJ_CAIS.WebPortal.Public.Controllers
{
    public class ConvictionController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ICertificateService _certificateService;

        public ConvictionController(IMapper mapper, ICertificateService certificateService)
        {
            _mapper = mapper;
            _certificateService = certificateService;
        }

        [HttpGet]
        public async Task<ActionResult> ViewByCode(string id)
        {
            //var cert = await _certificateService.GetCertificateDocumentContent(id);
            var cert = await _certificateService.GetWebCertificateByAccessCodeAsync(id);
            if (cert == null)
            {
                var empty = new ConvictionCodeDisplayModel
                {
                    IsEmptyResponse = true,
                    SearchCode = id,
                };
                return View(empty);
            }

            return File(cert.Content, cert.MimeType);
        }

        private async Task<ConvictionCodeDisplayModel> GetByCode(string code)
        {
            if (code.StartsWith("1"))
            {
                return new ConvictionCodeDisplayModel
                {
                    RegistrationNumber = "22051001234",
                    Identifier = "9001200101",
                    Firstname = "Иван",
                    Surname = "Иванов",
                    Familyname = "Петров",
                    FirstnameLat = "Ivan",
                    SurnameLat = "Ivanov",
                    FamilynameLat = "Petrov",
                    BirthDate = new DateTime(1990, 01, 20),
                    BirthCountryName = "България",
                    BirthCityName = "Варна",
                };
            }
            if (code.StartsWith("2"))
            {
                return new ConvictionCodeDisplayModel
                {
                    RegistrationNumber = "22051004321",
                    Identifier = "9201200101",
                    Firstname = "Александра",
                    Surname = "Иванова",
                    Familyname = "Петрова",
                    FirstnameLat = "Aleksandra",
                    SurnameLat = "Ivanova",
                    FamilynameLat = "Petrova",
                    BirthDate = new DateTime(1992, 01, 20),
                    BirthCountryName = "България",
                    BirthCityName = "Пловдив",
                };
            }
            return null;
        }
    }
}
