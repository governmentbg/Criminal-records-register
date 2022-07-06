using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Person;
using MJ_CAIS.Repositories.Contracts;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using static MJ_CAIS.Common.Constants.PersonConstants;

namespace MJ_CAIS.Repositories.Impl
{
    public class PersonRepository : BaseAsyncRepository<PPerson, CaisDbContext>, IPersonRepository
    {
        public PersonRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<PPerson> SelectAsync(string id)
        {
            return await this._dbContext.PPeople.AsNoTracking()
                .Include(x => x.PPersonIds)
                .FirstAsync(x => x.Id == id);
        }

        public async Task<PPerson> SelectWithBirthInfoAsync(string id)
        {
            return await this._dbContext.PPeople.AsNoTracking()
                .Include(x => x.PPersonIds)
                .Include(x => x.BirthCountry)
                .ThenInclude(x => x.PPersonCitizenships)
                .Include(x => x.BirthCity)
                   .ThenInclude(x => x.Municipality)
                   .ThenInclude(x => x.District)
                .FirstAsync(x => x.Id == id);
        }

        public IQueryable<PersonBulletinGridDTO> GetBulletinsByPersonId(string personId)
        {
            var query = _dbContext.BBulletins.AsNoTracking()
                        .Include(x => x.CsAuthority)
                        .Include(x => x.Status)
                        .Include(x => x.EgnNavigation)
                        .Include(x => x.LnchNavigation)
                        .Include(x => x.LnNavigation)
                        .Include(x => x.IdDocNumberNavigation)
                        .Include(x => x.SuidNavigation)
                        .Where(x => x.EgnNavigation.PersonId == personId ||
                          x.LnchNavigation.PersonId == personId ||
                          x.LnNavigation.PersonId == personId ||
                          x.IdDocNumberNavigation.PersonId == personId ||
                          x.SuidNavigation.PersonId == personId)
                        .Select(bulletin => new PersonBulletinGridDTO
                        {
                            Id = bulletin.Id,
                            AlphabeticalIndex = bulletin.AlphabeticalIndex,
                            BulletinType = bulletin.BulletinType == BulletinConstants.Type.Bulletin78A ? BulletinResources.Bulletin78A :
                                                        bulletin.BulletinType == BulletinConstants.Type.ConvictionBulletin ? BulletinResources.ConvictionBulletin :
                                                             BulletinResources.Unspecified,
                            BulletinAuthorityName = bulletin.CsAuthority.Name,
                            CreatedOn = bulletin.CreatedOn,
                            FamilyName = bulletin.Familyname,
                            FirstName = bulletin.Firstname,
                            RegistrationNumber = bulletin.RegistrationNumber,
                            StatusName = bulletin.Status.Name,
                            SurName = bulletin.Surname,
                            BirthDate = bulletin.BirthDate
                        }).Distinct();

            return query;
        }

        public async Task<IQueryable<PersonApplicationGridDTO>> GetApplicationsByPersonIdAsync(string personId)
        {
            var query = _dbContext.AApplications.Where(a => a.EgnNavigation.PersonId == personId
                                                            || a.LnNavigation.PersonId == personId
                                                            || a.LnchNavigation.PersonId == personId
                                                            || a.SuidNavigation.PersonId == personId
                                                            ).Select(application => new PersonApplicationGridDTO
                                                            {
                                                                Id = application.Id,
                                                                RegistrationNumber = application.RegistrationNumber,
                                                                Firstname = application.Firstname,
                                                                Surname = application.Surname,
                                                                Familyname = application.Familyname,
                                                                CreatedOn = application.CreatedOn
                                                            })
                                                            .Distinct();

            return await Task.FromResult(query);
        }

        public async Task<IQueryable<PersonFbbcGridDTO>> GetFbbcByPersonIdAsync(string personId)
        {
            var query = _dbContext.Fbbcs.AsNoTracking()
                .Include(x => x.Person)
                .Include(x => x.SuidNavigation)
                .Where(x => x.Person.PersonId == personId || x.SuidNavigation.PersonId == personId)
                .Select(fbbc => new PersonFbbcGridDTO
                {
                    Id = fbbc.Id,
                    Surname = fbbc.Surname,
                    Firstname = fbbc.Firstname,
                    Familyname = fbbc.Familyname,
                    BirthDate = fbbc.BirthDate,
                    DestroyedDate = fbbc.DestroyedDate,
                    Egn = fbbc.Egn,
                    ReceiveDate = fbbc.ReceiveDate,
                    CreatedOn = fbbc.CreatedOn
                });

            return await Task.FromResult(query);
        }

        public async Task<List<PersonGridDTO>> SelectInPageAsync(PersonGridDTO searchObj, int pageSize, int pageNumber)
        {
            DataSet ds = new DataSet();
            List<PersonGridDTO> result = new List<PersonGridDTO>();

            try
            {
                using (OracleConnection oracleConnection = new OracleConnection(_dbContext.Database.GetConnectionString()))
                {
                    // Create command
                    OracleCommand cmd = new OracleCommand("search_persons", oracleConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Set parameters

                    cmd.Parameters.Add(new OracleParameter("p_egn", OracleDbType.Varchar2, searchObj.Pid, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_firstname", OracleDbType.Varchar2, searchObj.FirstName, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_surname", OracleDbType.Varchar2, searchObj.SurName, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_familyname", OracleDbType.Varchar2, searchObj.FamilyName, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_fullname", OracleDbType.Varchar2, searchObj.FullName, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_birthdate", OracleDbType.Date, searchObj.BirthDate, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_precision", OracleDbType.Varchar2, searchObj.BirthDatePrec, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_page_size", OracleDbType.Int32, pageSize, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_page_number", OracleDbType.Int32, pageNumber, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_id_type", OracleDbType.Varchar2, searchObj.PidType, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_sex", OracleDbType.Int32, searchObj.Sex, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_out", OracleDbType.RefCursor, null, ParameterDirection.Output));

                    OracleDataAdapter resultDataSet = new OracleDataAdapter(cmd);
                    try
                    {
                        await oracleConnection.OpenAsync();
                        resultDataSet.Fill(ds);
                        result = GetPersons(ds.Tables[0]);
                    }
                    catch (Exception exception)
                    {
                        // todo: log
                        throw;
                    }
                    finally
                    {
                        oracleConnection.Close();
                        oracleConnection.Dispose();
                    }
                }
            }

            catch (Exception ex)
            {
                // todo: log error
                // add message
                throw;
            }

            return result;
        }

        /// Get PersonIds by pid value and pid type
        /// The personId is set only if the object does not exist in the database
        /// </summary>
        /// <param name="pid">Identifier</param>
        /// <param name="pidType">Identifier type (EGN, LNCH, LN, AfisNumber)</param>
        /// <param name="personId">Identifier of the person to which the object will be added </param>
        /// <returns></returns>
        public async Task<List<PPersonId>> GetPersonIdsAsync(List<PersonIdTypeDTO> personIds, string personId)
        {
            var result = new List<PPersonId>();

            // todo: make one call
            foreach (var currentPid in personIds)
            {
                var pidDb = await _dbContext.PPersonIds
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x =>
                                        x.Pid.ToLower() == currentPid.Pid.ToLower() &&
                                        x.PidTypeId == currentPid.Type &&
                                        x.Issuer == currentPid.Issuer &&
                                        x.CountryId == BG);
                // pid does not exist
                if (pidDb == null)
                {
                    result.Add(new PPersonId
                    {
                        Id = BaseEntity.GenerateNewId(),
                        Pid = currentPid.Pid,
                        PidTypeId = currentPid.Type,
                        CountryId = BG,
                        Issuer = currentPid.Issuer,
                        EntityState = EntityStateEnum.Added,
                        PersonId = personId,
                    });
                }
                else
                {
                    result.Add(pidDb);
                }

            }

            return result;
        }

        [Obsolete($"Use {nameof(InsertAsync)} with additional parameter personH instead.", true)]
        public override Task<PPerson> InsertAsync(PPerson entity)
        {
            return base.InsertAsync(entity);
        }

        private static List<PersonGridDTO> GetPersons(DataTable dataTable)
        {
            var result = new List<PersonGridDTO>();

            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(MapDataRowToPersonObj(row));
            }

            return result;
        }

        private static PersonGridDTO MapDataRowToPersonObj(DataRow dataRow)
        {
            var person = new PersonGridDTO();

            person.Id = dataRow["person_id"]?.ToString();
            person.Pid = dataRow["pid"]?.ToString();
            person.PidTypeName = dataRow["pid_type_name"]?.ToString();
            person.FirstName = dataRow["firstname"]?.ToString();
            var isParsedDate = DateTime.TryParse(dataRow["birth_date"]?.ToString(), out DateTime birthDate);
            person.BirthDate = isParsedDate ? birthDate : null;
            person.SurName = dataRow["surname"]?.ToString();
            person.FamilyName = dataRow["familyname"]?.ToString();
            person.FullName = dataRow["fullname"]?.ToString();
            var isParsed = int.TryParse(dataRow["all_records"]?.ToString(), out int allRecords);
            person.TotalCount = isParsed ? allRecords : 0;

            return person;
        }
    }
}
