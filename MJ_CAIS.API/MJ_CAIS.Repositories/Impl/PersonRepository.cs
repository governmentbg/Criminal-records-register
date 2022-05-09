using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Person;
using MJ_CAIS.Repositories.Contracts;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Transactions;
using static MJ_CAIS.Common.Constants.PersonConstants;

namespace MJ_CAIS.Repositories.Impl
{
    public class PersonRepository : BaseAsyncRepository<PPerson, CaisDbContext>, IPersonRepository
    {
        public PersonRepository(CaisDbContext dbContext) : base(dbContext)
        {
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

                    cmd.Parameters.Add(new OracleParameter("p_egn", OracleDbType.Varchar2, null, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_firstname", OracleDbType.Varchar2, searchObj.FirstName, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_surname", OracleDbType.Varchar2, null, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_familyname", OracleDbType.Varchar2, null, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_fullname", OracleDbType.Varchar2, null, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_birthdate", OracleDbType.Date, null, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_precision", OracleDbType.Varchar2, null, ParameterDirection.Input));
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
                        x.Pid == pid &&
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
                };
            }

            return pidDb;
        }

        /// <summary>
        /// Insert new person with its history object.
        /// Insert person pids and its pids history
        /// </summary>
        /// <param name="entity">New person entity</param>
        /// <param name="personH">Old version of Person object</param>
        /// <returns></returns>
        public async Task<PPerson> InsertAsync(PPerson entity, PPersonH personH)
        {
            //using TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled);

            _dbContext.ApplyChanges(entity, new List<BaseEntity>(), true);
            _dbContext.ApplyChanges(personH, new List<BaseEntity>(), true);
            await _dbContext.SaveChangesAsync();

            //await _dbContext.SaveEntityAsync(entity, true);
            //await _dbContext.SaveEntityAsync(personH, true);
            //scope.Complete();
            return entity;
        }

        /// <summary>
        /// Update person object
        /// save onld data in P_PERSON_H and P_PERSON_IDS_H
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="personH"></param>
        /// <returns></returns>
        public async Task<PPerson> UpdateAsync(PPerson entity, PPersonH personH)
        {
            try
            {
                _dbContext.ApplyChanges(entity, new List<BaseEntity>(), false);
                _dbContext.ApplyChanges(personH, new List<BaseEntity>(), true);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }

            return entity;
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
            person.Identifier = dataRow["egn"]?.ToString();
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
