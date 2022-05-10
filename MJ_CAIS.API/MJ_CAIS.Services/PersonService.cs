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

        //public override async Task<string> InsertAsync(PersonDTO aInDto)
        //{
        //    var personId = await CreatePersonAsync(aInDto);
        //    await dbContext.SaveChangesAsync();
        //    return personId;
        //}

        public async Task<PPerson> CreatePersonAsync(PersonDTO aInDto)
        {
            var pids = new List<PPersonId>();
            var personId = BaseEntity.GenerateNewId();
            // todo: make one call 
            if (!string.IsNullOrEmpty(aInDto.Egn))
            {
                pids.Add(await _personRepository.GetPersonIdAsyn(aInDto.Egn, PidType.Egn, personId));
            }

            if (!string.IsNullOrEmpty(aInDto.Lnch))
            {
                pids.Add(await _personRepository.GetPersonIdAsyn(aInDto.Lnch, PidType.Lnch, personId));
            }

            if (!string.IsNullOrEmpty(aInDto.Ln))
            {
                pids.Add(await _personRepository.GetPersonIdAsyn(aInDto.Ln, PidType.Ln, personId));
            }

            if (!string.IsNullOrEmpty(aInDto.AfisNumber))
            {
                pids.Add(await _personRepository.GetPersonIdAsyn(aInDto.AfisNumber, PidType.AfisNumber, personId));
            }
            // todo: 
            // must create new person object
            if (pids.All(x => x.EntityState == EntityStateEnum.Added))
            {
                // add person with pids
                var person = mapper.MapToEntity<PersonDTO, PPerson>(aInDto, true);
                person.CreatedOn = DateTime.UtcNow; // todo: remove
                person.Id = personId;
                person.PPersonIds = pids;

                // add person history object with pids
                var personH = mapper.MapToEntity<PPerson, PPersonH>(person, true);
                personH.CreatedOn = DateTime.UtcNow; // todo: remove
                personH.PPersonIdsHes = mapper.MapToEntityList<PPersonId, PPersonIdsH>(pids, true);

                dbContext.ApplyChanges(person, new List<BaseEntity>(), true);
                dbContext.ApplyChanges(personH, new List<BaseEntity>(), true);
                return person;
            }

            // identifiers of a person who exists in the database and the specific pids are attached to it
            var existingPersonsIds = pids.Where(x => x.EntityState != EntityStateEnum.Added).Select(x=>x.PersonId);

            // when person is only one 
            // update of this one person with the personal data
            // from the primary register (bulletin, fbbc, application or person form)
            if (existingPersonsIds.Count() == 1)
            {
                var personToBeUpdatedId = existingPersonsIds.First();
                var existingPerson = await dbContext.PPeople
                    .AsNoTracking()
                    .Include(x => x.PPersonIds)
                    .FirstOrDefaultAsync(x => x.Id == personToBeUpdatedId);

                // update person with new data
                var personToUpdate = mapper.MapToEntity<PersonDTO, PPerson>(aInDto, false);
                personToUpdate.Id = existingPerson.Id;
                personToUpdate.UpdatedOn = DateTime.UtcNow; // todo: remove

                // add new identifiers if any
                // �� �� ������� ��� ������ � ������, �� �� ���� �� �� ������ �� ������ 
                // ����� �� �������� � ��� �� �� ������� ��������
                personToUpdate.PPersonIds = pids;//.Where(x => x.EntityState == EntityStateEnum.Added).ToList();

                // create person history object with old data
                var personHistoryToBeAdded = mapper.MapToEntity<PPerson, PPersonH>(personToUpdate, true);
                personHistoryToBeAdded.Id = BaseEntity.GenerateNewId();

                // existing and new pids
                var existingPidsInDb = mapper.MapToEntityList<PPersonId, PPersonIdsH>(existingPerson.PPersonIds.ToList(), true, true);
                var newPids = mapper.MapToEntityList<PPersonId, PPersonIdsH>(personToUpdate.PPersonIds.Where(x=>x.EntityState == EntityStateEnum.Added).ToList(), true,true);
                existingPidsInDb.AddRange(newPids);
                personHistoryToBeAdded.PPersonIdsHes = existingPidsInDb;

                dbContext.ApplyChanges(personToUpdate, new List<BaseEntity>(), true);
                dbContext.ApplyChanges(personHistoryToBeAdded, new List<BaseEntity>(), true);
                return personToUpdate;
            }

            //todo: more then one person object
            // more then one person
            return null;
        }
    }
}
