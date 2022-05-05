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
using System.Reflection;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.AutoMapperContainer;
using static MJ_CAIS.Common.Constants.PersonConstants;

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

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
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

                foreach (Match match in matches)
                {
                    // group 1 fullmatch, group 1 prop name, group 2 prop value
                    if (match.Groups?.Count == null || match.Groups?.Count < 3) continue;
                    var propName = match.Groups[1].Value?.ToLower();
                    var propValue = match.Groups[2].Value?.ToLower();

                    var propInfo = searchObj.GetType().GetProperty(propName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (propInfo != null)
                    {
                        if (propName.ToLower() == nameof(searchObj.BirthDateDisplay).ToLower())
                        {
                            var isParsed = DateTime.TryParse(propValue, out DateTime paresedDate);
                            if (isParsed) searchObj.BirthDate = paresedDate;
                            continue;
                        }
                        propInfo.SetValue(searchObj, propValue);
                    }
                }
            }

            var resultInPage = await _personRepository.SelectInPageAsync(searchObj, pageSize, currentPage);
            pageResult.Data = resultInPage;
            pageResult.Total = resultInPage.FirstOrDefault()?.TotalCount ?? 0;

            return await Task.FromResult(pageResult);
        }

        public override async Task<string> InsertAsync(PersonDTO aInDto)
        {
            var pids = new List<PPersonId>();

            // todo: make one call 
            if (!string.IsNullOrEmpty(aInDto.Egn))
            {
                pids.Add(await _personRepository.GetPersonIdAsyn(aInDto.Egn, PidType.Egn));
            }

            if (!string.IsNullOrEmpty(aInDto.Lnch))
            {
                pids.Add(await _personRepository.GetPersonIdAsyn(aInDto.Lnch, PidType.Lnch));
            }

            if (!string.IsNullOrEmpty(aInDto.Ln))
            {
                pids.Add(await _personRepository.GetPersonIdAsyn(aInDto.Ln, PidType.Ln));
            }

            if (!string.IsNullOrEmpty(aInDto.AfisNumber))
            {
                pids.Add(await _personRepository.GetPersonIdAsyn(aInDto.AfisNumber, PidType.AfisNumber));
            }

            // must create new person object
            if (pids.All(x => x.EntityState == EntityStateEnum.Added))
            {
                // add person with pids
                var person = mapper.MapToEntity<PersonDTO, PPerson>(aInDto, true);
                person.PPersonIds = pids;

                // add person history object with pids
                var personH = mapper.MapToEntity<PPerson, PPersonH>(person, true);
                personH.PPersionIdsHes = mapper.MapToEntityList<PPersonId, PPersionIdsH>(pids, true);

                var addedPerson = await _personRepository.InsertAsync(person, personH);
                return addedPerson?.Id;
            }

            // todo: когато съществува някои от идентификаторите,
            // да се намери person колко обекта са и да се групират ако са повече от един
            return null;
        }    
    }
}
