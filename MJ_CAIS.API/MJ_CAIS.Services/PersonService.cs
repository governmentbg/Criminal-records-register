using AutoMapper;
using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Home;
using MJ_CAIS.DTO.Person;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;
using System.Data;
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

        public override async Task<PersonDTO> SelectAsync(string aId)
        {
            var personDb = await _personRepository.SelectAsync(aId);
            var person = MapPerson(personDb);
            return person;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }

        public async Task<IgPageResult<PersonGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<PersonGridDTO> aQueryOptions, PersonSearchParamsDTO searchParams)
        {
            var pageSize = base.CalculateTop(aQueryOptions);
            var currentPage = base.CalculateCurrentPage(aQueryOptions);
            var pageResult = new IgPageResult<PersonGridDTO>();
            pageResult.CurrentPage = currentPage;
            pageResult.PerPage = pageSize;

            var resultInPage = await _personRepository.SelectInPageAsync(searchParams, pageSize, currentPage);
            pageResult.Data = resultInPage;
            pageResult.Total = resultInPage.FirstOrDefault()?.TotalCount ?? 0;

            return await Task.FromResult(pageResult);
        }

        public IQueryable<ObjectStatusCountDTO> GetBulletinsCountByPersonId(string personId)
            => _personRepository.GetBulletinsCountByPersonId(personId);

        public async Task<IgPageResult<PersonBulletinGridDTO>> SelectPersonBulletinAllWithPaginationAsync(ODataQueryOptions<PersonBulletinGridDTO> aQueryOptions, string personId)
        {
            var entityQuery = _personRepository.GetBulletinsByPersonId(personId);
            return await GetPagedResultAsync(aQueryOptions, entityQuery);
        }

        public async Task<IgPageResult<PersonApplicationGridDTO>> SelectPersonApplicationAllWithPaginationAsync(ODataQueryOptions<PersonApplicationGridDTO> aQueryOptions, string personId)
        {
            var entityQuery = _personRepository.GetApplicationsByPersonId(personId);
            return await GetPagedResultAsync(aQueryOptions, entityQuery);
        }

        public async Task<IgPageResult<PersonEApplicationGridDTO>> SelectPersonEApplicationAllWithPaginationAsync(ODataQueryOptions<PersonEApplicationGridDTO> aQueryOptions, string personId)
        {
            var entityQuery = _personRepository.GetEApplicationsByPersonId(personId);
            return await GetPagedResultAsync(aQueryOptions, entityQuery);
        }

        public async Task<IgPageResult<PersonGeneratedReportGridDTO>> SelectPersonReportApplAllWithPaginationAsync(ODataQueryOptions<PersonGeneratedReportGridDTO> aQueryOptions, string personId)
        {
            var entityQuery = _personRepository.GetAllReportApplByPersonId(personId);
            return await GetPagedResultAsync(aQueryOptions, entityQuery);
        }

        public async Task<IgPageResult<PersonFbbcGridDTO>> SelectPersonFbbcAllWithPaginationAsync(ODataQueryOptions<PersonFbbcGridDTO> aQueryOptions, string personId)
        {
            var entityQuery = _personRepository.GetFbbcByPersonId(personId);
            return await GetPagedResultAsync(aQueryOptions, entityQuery);
        }

        public async Task<IgPageResult<PersonPidGridDTO>> SelectPersonPidAllWithPaginationAsync(ODataQueryOptions<PersonPidGridDTO> aQueryOptions, string personId)
        {
            var entityQuery = _personRepository.GetPidsByPersonId(personId);
            return await GetPagedResultAsync(aQueryOptions, entityQuery);
        }

        private async Task<IgPageResult<T>> GetPagedResultAsync<T>(ODataQueryOptions<T> aQueryOptions, IQueryable<T> entityQuery)
        {
            var resultQuery = await this.ApplyOData(entityQuery, aQueryOptions);
            var pageResult = new IgPageResult<T>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, entityQuery, resultQuery);
            return pageResult;
        }

        private PersonDTO MapPerson(PPerson personDb)
        {
            var person = mapper.Map<PPerson, PersonDTO>(personDb);
            var personIds = personDb.PPersonIds.OrderByDescending(x => x.CreatedOn);
            // last updating of a person
            person.Egn = personIds.FirstOrDefault(x => x.PidTypeId == PidType.Egn)?.Pid;
            person.Lnch = personIds.FirstOrDefault(x => x.PidTypeId == PidType.Lnch)?.Pid;
            person.Ln = personIds.FirstOrDefault(x => x.PidTypeId == PidType.Ln)?.Pid;
            person.AfisNumber = personIds.FirstOrDefault(x => x.PidTypeId == PidType.AfisNumber)?.Pid;
            return person;
        }
    }
}
