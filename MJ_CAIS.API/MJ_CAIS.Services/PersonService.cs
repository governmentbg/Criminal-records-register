using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DTO.Person;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using System.Collections.Generic;
using Microsoft.AspNet.OData.Query;
using MJ_CAIS.Services.Contracts.Utils;
using System.Text.RegularExpressions;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace MJ_CAIS.Services
{
    public class PersonService : BaseAsyncService<PersonDTO, PersonDTO, PersonGridDTO, PPerson, string, CaisDbContext>, IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IMapper mapper, IPersonRepository personRepository)
            : base(mapper, personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<IgPageResult<PersonGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<PersonGridDTO> aQueryOptions, bool isPageInit)
        {
            var pageSize = base.CalculateTop(aQueryOptions);
            var currentPage = base.CalculateCurrentPage(aQueryOptions);

            var pageResult = new IgPageResult<PersonGridDTO>();
            pageResult.CurrentPage = currentPage;
            pageResult.PerPage = pageSize;

            if (isPageInit)
            {
                pageResult.Data = new List<PersonGridDTO>();
                return pageResult;
            }

            var queryValidator = new CustomQueryValidator<PersonGridDTO>();

            if (aQueryOptions?.Filter != null)
            {
                queryValidator.Validate(aQueryOptions.Filter, new ODataValidationSettings());
            }

            var searchObj = new PersonGridDTO();
            if (!string.IsNullOrEmpty(aQueryOptions?.Filter?.RawValue))
            {
                var searchParams = aQueryOptions.Filter.RawValue;
                var containsReg = new Regex("contains\\((\\w+?)\\s*,\\s*\'(.+?)'\\)\\s*");
                var matches = containsReg.Matches(searchParams);
                searchObj.Identifier = matches.FirstOrDefault(x => x.Groups?.Count > 1 && x.Groups[1].Value?.ToLower() == nameof(PersonGridDTO.Identifier).ToLower())?.Groups[2]?.Value;
                searchObj.FirstName = matches.FirstOrDefault(x => x.Groups?.Count > 1 && x.Groups[1].Value?.ToLower() == nameof(PersonGridDTO.FirstName).ToLower())?.Groups[2]?.Value;
            }

            var resultInPage = await _personRepository.SelectInPageAsync(searchObj, pageSize, currentPage);
            pageResult.Data = resultInPage;

            return await Task.FromResult(pageResult);
        }



        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
    }
}
