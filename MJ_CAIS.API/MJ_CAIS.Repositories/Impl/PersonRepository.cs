using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
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
                .Include(x => x.BirthCity)
                   .ThenInclude(x => x.Municipality)
                .FirstAsync(x => x.Id == id);
        }

        public async Task<IQueryable<PersonBulletinGridDTO>> GetBulletinByPersonIdAsync(string personId)
        {
            var query = (from bulletin in _dbContext.BBulletins.AsNoTracking()
                         join bulletinAuth in _dbContext.GCsAuthorities.AsNoTracking() on bulletin.CsAuthorityId equals bulletinAuth.Id
                                 into bulletinAuthLeft
                         from bulletinAuth in bulletinAuthLeft.DefaultIfEmpty()

                         join bulletinStatuses in _dbContext.BBulletinStatuses.AsNoTracking() on bulletin.StatusId equals bulletinStatuses.Code
                                     into bulletinStatusesLeft
                         from bulletinStatuses in bulletinStatusesLeft.DefaultIfEmpty()

                         join bulletinPersonId in _dbContext.PBulletinIds.AsNoTracking() on bulletin.Id equals bulletinPersonId.BulletinId
                                   into bulletinPersonLeft
                         from bulletinPersonId in bulletinPersonLeft.DefaultIfEmpty()

                         join personIds in _dbContext.PPersonIds.AsNoTracking() on bulletinPersonId.PersonId equals personIds.Id
                                     into personIdsLeft
                         from personIds in personIdsLeft.DefaultIfEmpty()

                         join personIdType in _dbContext.PPersonIdTypes.AsNoTracking() on personIds.PidTypeId equals personIdType.Code
                                   into personIdTypeLeft
                         from personIdType in personIdTypeLeft.DefaultIfEmpty()

                         where personIds.PersonId == personId
                         select new PersonBulletinGridDTO
                         {
                             Id = bulletin.Id,
                             AlphabeticalIndex = bulletin.AlphabeticalIndex,
                             BulletinType = bulletin.BulletinType == nameof(BulletinConstants.Type.Bulletin78A) ? BulletinConstants.Type.Bulletin78A :
                                                        bulletin.BulletinType == nameof(BulletinConstants.Type.ConvictionBulletin) ? BulletinConstants.Type.ConvictionBulletin :
                                                             BulletinConstants.Type.Unspecified,
                             BulletinAuthorityName = bulletinAuth.Name,
                             CreatedOn = bulletinAuth.CreatedOn,
                             FamilyName = bulletin.Familyname,
                             FirstName = bulletin.Firstname,
                             RegistrationNumber = bulletin.RegistrationNumber,
                             StatusName = bulletinStatuses.Name,
                             SurName = bulletin.Surname,
                             BirthDate = bulletin.BirthDate
                         }).Distinct();

            return await Task.FromResult(query);
        }

        public async Task<IQueryable<PersonApplicationGridDTO>> GetApplicationsByPersonIdAsync(string personId)
        {
            var query = (from application in _dbContext.AApplications.AsNoTracking()

                         join appPersonId in _dbContext.PAppIds.AsNoTracking() on application.Id equals appPersonId.ApplicationId
                                   into appPersonIdLeft
                         from appPersonId in appPersonIdLeft.DefaultIfEmpty()

                         join personIds in _dbContext.PPersonIds.AsNoTracking() on appPersonId.PersonId equals personIds.Id
                                     into personIdsLeft
                         from personIds in personIdsLeft.DefaultIfEmpty()

                         where personIds.PersonId == personId

                         select new PersonApplicationGridDTO
                         {
                             Id = application.Id,
                             RegistrationNumber = application.RegistrationNumber,
                             Firstname = application.Firstname,
                             Surname = application.Surname,
                             Familyname = application.Familyname,
                         }).Distinct();

            return await Task.FromResult(query);
        }

        public async Task<IQueryable<PersonFbbcGridDTO>> GetFbbcByPersonIdAsync(string personId)
        {
            var query = from fbbc in _dbContext.Fbbcs.AsNoTracking()

                        join personIds in _dbContext.PPersonIds.AsNoTracking() on fbbc.PersonId equals personIds.Id
                                    into personIdsLeft
                        from personIds in personIdsLeft.DefaultIfEmpty()

                        where personIds.PersonId == personId
                        select new PersonFbbcGridDTO
                        {
                            Id = fbbc.Id,
                            Surname = fbbc.Surname,
                            Firstname = fbbc.Firstname,
                            Familyname = fbbc.Familyname,
                            BirthDate = fbbc.BirthDate,
                            DestroyedDate = fbbc.DestroyedDate,
                            Egn = fbbc.Egn,
                            ReceiveDate = fbbc.ReceiveDate
                        };

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

        /// <summary>
        /// Get PersonId object by pid value and pid type
        /// The personId is set only if the object does not exist in the database
        /// </summary>
        /// <param name="pid">Indetifier</param>
        /// <param name="pidType">Identifier type (EGN, LNCH, LN, AfisNumber)</param>
        /// <param name="personId">Identifier of the person to which the object will be added </param>
        /// <returns></returns>
        public async Task<PPersonId> GetPersonIdAsyn(string pid, string pidType, string personId)
        {
            var issuerType = string.Empty;
            switch (pidType)
            {
                case PidType.Egn:
                    issuerType = IssuerType.GRAO;
                    break;
                case PidType.Lnch:
                    issuerType = IssuerType.MVR;
                    break;
                case PidType.Ln:
                    issuerType = IssuerType.EU;
                    break;
                case PidType.AfisNumber:
                    issuerType = IssuerType.MVR;
                    break;
            }

            var pidDb = await _dbContext.PPersonIds
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x =>
                        x.Pid.ToLower() == pid.ToLower() &&
                        x.PidTypeId == pidType &&
                        x.Issuer == issuerType &&
                        x.CountryId == BG);

            // pid does not exist
            if (pidDb == null)
            {
                return new PPersonId
                {
                    Id = BaseEntity.GenerateNewId(),
                    Pid = pid,
                    PidTypeId = pidType,
                    CountryId = BG,
                    Issuer = issuerType,
                    EntityState = EntityStateEnum.Added,
                    PersonId = personId,
                    CreatedOn = DateTime.Now,
                };
            }

            return pidDb;
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
