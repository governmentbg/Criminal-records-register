using Microsoft.EntityFrameworkCore;
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
            person.SurName = dataRow["surname"]?.ToString();
            person.FamilyName = dataRow["familyname"]?.ToString();
            person.FullName = dataRow["fullname"]?.ToString();
            var isParsed = int.TryParse(dataRow["all_records"]?.ToString(), out int allRecords);
            person.TotalCount = isParsed ? allRecords : 0;

            return person;
        }
    }
}
