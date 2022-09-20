using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MJ_CAIS.Common;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.ExternalServicesHost;
using MJ_CAIS.DTO.Home;
using MJ_CAIS.DTO.Person;
using MJ_CAIS.Repositories.Contracts;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Text;
using static MJ_CAIS.Common.Constants.ApplicationConstants;
using static MJ_CAIS.Common.Constants.PersonConstants;

namespace MJ_CAIS.Repositories.Impl
{
    public class PersonRepository : BaseAsyncRepository<PPerson, CaisDbContext>, IPersonRepository
    {
        private readonly IPersonHelperRepository _personHelperRepository;

        public PersonRepository(CaisDbContext dbContext,
            IPersonHelperRepository personHelperRepository)
            : base(dbContext)
        {
            _personHelperRepository = personHelperRepository;
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
                .Include(x => x.PPersonCitizenships)
                    .ThenInclude(x => x.Country)
                .Include(x => x.BirthCity)
                   .ThenInclude(x => x.Municipality)
                   .ThenInclude(x => x.District)
                .FirstAsync(x => x.Id == id);
        }

        public IQueryable<PersonBulletinGridDTO> GetBulletinsByPersonId(string personId)
        {
            var bulletins = _personHelperRepository.GetAllBulletinsByPersonId(personId);

            var query = from bulletin in bulletins
                        join auth in _dbContext.GDecidingAuthorities.AsNoTracking() on bulletin.BulletinAuthorityId equals auth.Id
                            into authLeft
                        from auth in authLeft.DefaultIfEmpty()

                        join status in _dbContext.BBulletinStatuses.AsNoTracking() on bulletin.StatusId equals status.Code
                        select new PersonBulletinGridDTO
                        {
                            Id = bulletin.Id,
                            BulletinType = bulletin.BulletinType == BulletinConstants.Type.Bulletin78A ? BulletinResources.Bulletin78A :
                                                        bulletin.BulletinType == BulletinConstants.Type.ConvictionBulletin ? BulletinResources.ConvictionBulletin :
                                                             BulletinResources.Unspecified,
                            RegistrationNumber = bulletin.RegistrationNumber,
                            StatusName = status.Name,
                            BulletinAuthorityName = auth.Name,
                            CaseNumberAndYear = bulletin.CaseNumber + "/" + bulletin.CaseYear,
                            Egn = bulletin.Egn,
                            Lnch = bulletin.Lnch,
                            FullName = !string.IsNullOrEmpty(bulletin.FullName) ? bulletin.FullName :
                             bulletin.FirstName + " " + bulletin.SurName + " " + bulletin.FamilyName,
                            BirthDate = bulletin.BirthDate,
                            CreatedOn = bulletin.CreatedOn,
                            CsAuthorityId = bulletin.CsAuthorityId
                        };

            return query;
        }

        public IQueryable<PersonApplicationGridDTO> GetApplicationsByPersonId(string personId)
        {
            var allApplications = _personHelperRepository.GetAllAplicationsByPersonId(personId);

            var query = from certificate in _dbContext.ACertificates
                        join application in allApplications on certificate.ApplicationId equals application.ApplicationId
                        join status in _dbContext.AApplicationStatuses.AsNoTracking() on certificate.StatusCode equals status.Code
                        join applicationType in _dbContext.AApplicationTypes.AsNoTracking() on application.ApplicationTypeId equals applicationType.Id
                        join auth in _dbContext.GCsAuthorities.AsNoTracking() on application.CsAuthorityId equals auth.Id
                        where application.ApplicationTypeId == ApplicationTypes.DeskCertificate ||
                                        application.ApplicationTypeId == ApplicationTypes.ConvictionRequest ||
                                        application.ApplicationTypeId == ApplicationTypes.ApplicationRequestOld ||
                                        application.ApplicationTypeId == ApplicationTypes.ConvictionRequestOld
                        select new PersonApplicationGridDTO
                        {
                            Id = application.ApplicationId,
                            Type = applicationType.Name,
                            CertificateStatus = status.Name,
                            CertifivateRegistrationNumber = certificate.RegistrationNumber,
                            CertifivateValidDate = certificate.ValidTo,
                            CsAuthority = auth.Name,
                            ApplicantName = application.ApplicantName,
                            Egn = application.Egn,
                            Lnch = application.Lnch,
                            FullName = !string.IsNullOrEmpty(application.Fullname) ? application.Fullname :
                                                            application.Firstname + " " + application.Surname + " " + application.Familyname,
                            BithDate = application.BirthDate,
                            CreatedOn = certificate.CreatedOn
                        };

            return query;
        }

        public async Task<IQueryable<PersonArchiveGridDTO>> GetArchiveByPersonIdAsync(string personId)
        {
            if (string.IsNullOrEmpty(personId)) return new List<PersonArchiveGridDTO>().AsQueryable();

            var personPids = await _dbContext.PPersonIds.AsNoTracking()
                .Where(x => x.PersonId == personId &&
                (x.PidTypeId == PidType.Egn || x.PidTypeId == PidType.Lnch || x.PidTypeId == PidType.Suid))
                .Select(x => new { Pid = x.Pid, PidType = x.PidTypeId })
                .ToListAsync();

            var egns = personPids.Where(x => x.PidType == PidType.Egn).Select(x => x.Pid);
            var lnchs = personPids.Where(x => x.PidType == PidType.Lnch).Select(x => x.Pid);
            var suids = personPids.Where(x => x.PidType == PidType.Suid).Select(x => x.Pid);

            var archiveByEgn = _dbContext.AArchives.AsNoTracking()
                                  .Where(x => egns.Contains(x.Egn))
                                  .Select(x => new PersonArchiveGridDTO
                                  {
                                      Id = x.Id,
                                      Lnch = x.Lnch,
                                      ApplicantName = x.ApplicantName,
                                      BithDate = x.BirthDate,
                                      Egn = x.Egn,
                                      FullName = x.Fullname,
                                      Type = x.ApplicationTypeName,
                                      CertifivateValidDate = x.ValidTo,
                                      CsAuthority = x.CsAuthorityName,
                                      CreatedOn = x.CreatedOn,
                                  });

            var archiveByLnch = _dbContext.AArchives.AsNoTracking()
                             .Where(x => lnchs.Contains(x.Lnch))
                             .Select(x => new PersonArchiveGridDTO
                             {
                                 Id = x.Id,
                                 Lnch = x.Lnch,
                                 ApplicantName = x.ApplicantName,
                                 BithDate = x.BirthDate,
                                 Egn = x.Egn,
                                 FullName = x.Fullname,
                                 Type = x.ApplicationTypeName,
                                 CertifivateValidDate = x.ValidTo,
                                 CsAuthority = x.CsAuthorityName,
                                 CreatedOn = x.CreatedOn,
                             });

            var archiveBySuids = _dbContext.AArchives.AsNoTracking()
                             .Where(x => suids.Contains(x.Suid))
                             .Select(x => new PersonArchiveGridDTO
                             {
                                 Id = x.Id,
                                 Lnch = x.Lnch,
                                 ApplicantName = x.ApplicantName,
                                 BithDate = x.BirthDate,
                                 Egn = x.Egn,
                                 FullName = x.Fullname,
                                 Type = x.ApplicationTypeName,
                                 CertifivateValidDate = x.ValidTo,
                                 CsAuthority = x.CsAuthorityName,
                                 CreatedOn = x.CreatedOn,
                             });

            var query = archiveByEgn
                .Union(archiveByLnch)
                .Union(archiveBySuids);

            return query;
        }

        public IQueryable<PersonEApplicationGridDTO> GetEApplicationsByPersonId(string personId)
        {
            var allApplications = _personHelperRepository.GetAllAplicationsByPersonId(personId);

            var query = from certificate in _dbContext.ACertificates
                        join application in allApplications on certificate.ApplicationId equals application.ApplicationId
                        join applicationType in _dbContext.AApplicationTypes.AsNoTracking() on application.ApplicationTypeId equals applicationType.Id
                        join wApp in _dbContext.WApplications.AsNoTracking() on application.WApplicationId equals wApp.Id
                        join extUser in _dbContext.GUsersExts.AsNoTracking() on wApp.UserExtId equals extUser.Id
                        join extAdministration in _dbContext.GExtAdministrations.AsNoTracking() on extUser.AdministrationId equals extAdministration.Id
                        join status in _dbContext.AApplicationStatuses.AsNoTracking() on certificate.StatusCode equals status.Code
                        where application.ApplicationTypeId == ApplicationTypes.WebCertificate ||
                              application.ApplicationTypeId == ApplicationTypes.WebExternalCertificate
                        select new PersonEApplicationGridDTO
                        {
                            Id = application.ApplicationId,
                            Type = applicationType.Name,
                            CertificateStatus = status.Name,
                            CertifivateRegistrationNumber = certificate.RegistrationNumber,
                            CertifivateValidDate = certificate.ValidTo,
                            ExtAdministration = extAdministration.Name,
                            Egn = application.Egn,
                            Lnch = application.Lnch,
                            FullName = !string.IsNullOrEmpty(application.Fullname) ? application.Fullname :
                                                                            application.Firstname + " " + application.Surname + " " + application.Familyname,
                            BithDate = application.BirthDate,
                            CreatedOn = certificate.CreatedOn,
                        };

            return query;
        }

        public IQueryable<PersonGeneratedReportGridDTO> GetAllReportApplByPersonId(string personId)
        {
            var reportAppl = _personHelperRepository.GetAllReportApplByPersonId(personId);

            return reportAppl;
        }


        public IQueryable<PersonFbbcGridDTO> GetFbbcByPersonId(string personId)
        {
            var allFbbcs = _personHelperRepository.GetAllFbbcsByPersonId(personId);

            var query = from fbbc in allFbbcs
                        join docType in _dbContext.FbbcDocTypes.AsNoTracking() on fbbc.DocTypeId equals docType.Id
                            into docTypeLeft
                        from docType in docTypeLeft.DefaultIfEmpty()

                        join country in _dbContext.GCountries.AsNoTracking() on fbbc.CountryId equals country.Id
                            into countryLeft
                        from country in countryLeft.DefaultIfEmpty()
                        select new PersonFbbcGridDTO
                        {
                            Id = fbbc.Id,
                            DocType = docType.Name,
                            ReceiveDate = fbbc.ReceiveDate,
                            Country = country.Name,
                            Egn = fbbc.Egn,
                            FullName = fbbc.Firstname + " " + fbbc.Surname + " " + fbbc.Familyname,
                            BirthDate = fbbc.BirthDate,
                            CreatedOn = fbbc.CreatedOn
                        };

            return query;
        }

        public IQueryable<PersonPidGridDTO> GetPidsByPersonId(string personId)
        {
            var query = _dbContext.PPersonIds.AsNoTracking()
                .Include(x => x.PidType)
                .Where(x => x.PersonId == personId)
                .Select(x => new PersonPidGridDTO
                {
                    Id = x.Id,
                    Type = x.PidType.Name,
                    Pid = x.Pid,
                    Issuer = x.Issuer,
                    CreatedOn = x.CreatedOn
                });

            return query;
        }

        public IQueryable<PersonHistoryDataGridDTO> GetPersonHistoryDataByPersonId(string personId)
        {
            var personHIds = (from pids in _dbContext.PPersonIds.AsNoTracking()
                              join pidsH in _dbContext.PPersonIdsHes.AsNoTracking()
                          on new { pid = pids.Pid, type = pids.PidTypeId, issuer = pids.Issuer, country = pids.CountryId }
                      equals new { pid = pidsH.Pid, type = pidsH.PidTypeId, issuer = pidsH.Issuer, country = pidsH.CountryId }
                              where pids.PersonId == personId
                              select pidsH.PersonHId).Distinct();


            var result = _dbContext.PPersonHs.AsNoTracking()
                .Include(x => x.BirthCity)
                .Include(x => x.BirthCountry)
                .Include(x => x.PPersonIdsHes)
                .Include(x => x.PPersonHCitizenships)
                    .ThenInclude(x => x.Country)
                .Where(x => personHIds.Contains(x.Id))
                .Select(x => new PersonHistoryDataGridDTO
                {
                    Id = x.Id,
                    BirthCity = x.BirthCity.Name,
                    BirthCountry = x.BirthCountry.Name,
                    Name = !string.IsNullOrEmpty(x.Fullname) ? x.Fullname :
                           x.Firstname + " " + x.Surname + " " + x.Familyname,
                    NameLat = !string.IsNullOrEmpty(x.FullnameLat) ? x.FullnameLat :
                           x.FirstnameLat + " " + x.SurnameLat + " " + x.FamilynameLat,
                    BirthDate = x.BirthDate,
                    BirthPlaceOther = x.BirthPlaceOther,
                    CreatedOn = x.CreatedOn,
                    FatherName = !string.IsNullOrEmpty(x.FatherFullname) ? x.FatherFullname :
                           x.FatherFirstname + " " + x.FatherSurname + " " + x.FatherFamilyname,
                    MotherName = !string.IsNullOrEmpty(x.MotherFullname) ? x.MotherFullname :
                           x.MotherFirstname + " " + x.MotherSurname + " " + x.MotherFamilyname,
                    Sex = !x.Sex.HasValue ? PersonResources.unknown : (x.Sex == SexType.Male ? PersonResources.male : PersonResources.female),
                    Pids = x.PPersonIdsHes.Select(x => x.Pid),
                    CitizenShips = x.PPersonHCitizenships.Select(x => x.Country.Name),
                    TableId = x.TableId,
                    Tablename = x.Tablename == ContextTable.Bulletins ? PersonResources.tableBulletins :
                      x.Tablename == ContextTable.Application ? PersonResources.tableApplication :
                      x.Tablename == ContextTable.WApplication ? PersonResources.tableWApplication :
                      x.Tablename == ContextTable.Report ? PersonResources.tableReport :
                      x.Tablename == ContextTable.Fbbc ? PersonResources.tableFbbc :
                      x.Tablename == ContextTable.Manual ? PersonResources.tableManual : string.Empty,
                    Descr = x.Descr
                });

            return result;
        }

        public async Task<List<PersonGridDTO>> SelectInPageAsync(PersonSearchParamsDTO searchObj, int pageSize, int pageNumber)
        {
            DataSet ds = new DataSet();
            List<PersonGridDTO> result = new List<PersonGridDTO>();

            try
            {
                using (OracleConnection oracleConnection = new OracleConnection(_dbContext.Database.GetConnectionString()))
                {
                    // Create command
                    OracleCommand cmd = new OracleCommand("psearch.search_persons", oracleConnection);

                    cmd.CommandType = CommandType.StoredProcedure;

                    // Set parameters

                    cmd.Parameters.Add(new OracleParameter("p_egn", OracleDbType.Varchar2, searchObj.Egn?.Trim()?.ToUpper(), ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_lnch", OracleDbType.Varchar2, searchObj.Lnch?.Trim()?.ToUpper(), ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_firstname", OracleDbType.Varchar2, searchObj.Firstname?.Trim(), ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_surname", OracleDbType.Varchar2, searchObj.Surname?.Trim(), ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_familyname", OracleDbType.Varchar2, searchObj.Familyname?.Trim(), ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_fullname", OracleDbType.Varchar2, searchObj.Fullname?.Trim(), ParameterDirection.Input));

                    var birthDate = searchObj.BirthDate.HasValue ? searchObj.BirthDate.Value.Date : (DateTime?)null;
                    cmd.Parameters.Add(new OracleParameter("p_birthdate", OracleDbType.Date, birthDate, ParameterDirection.Input));
                    // cmd.Parameters.Add(new OracleParameter("p_precision", OracleDbType.Varchar2, null, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_bg_citizen", OracleDbType.Int32, null, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_nonbg_citizen", OracleDbType.Int32, null, ParameterDirection.Input));

                    cmd.Parameters.Add(new OracleParameter("p_max_persons", OracleDbType.Int32, pageSize, ParameterDirection.Input));
                    // cmd.Parameters.Add(new OracleParameter("p_page_number", OracleDbType.Int32, pageNumber, ParameterDirection.Input));
                    // cmd.Parameters.Add(new OracleParameter("p_id_type", OracleDbType.Varchar2, null, ParameterDirection.Input));
                    //cmd.Parameters.Add(new OracleParameter("p_sex", OracleDbType.Int32, null, ParameterDirection.Input));
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
                var pidToUpper = currentPid.Pid.ToUpper();
                var pidDb = await _dbContext.PPersonIds
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x =>
                                        x.Pid == pidToUpper &&
                                        x.PidTypeId == currentPid.Type &&
                                        x.Issuer == currentPid.Issuer &&
                                        x.CountryId == BG);
                // pid does not exist
                if (pidDb == null)
                {
                    result.Add(new PPersonId
                    {
                        Id = BaseEntity.GenerateNewId(),
                        Pid = pidToUpper,
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

        public IQueryable<ObjectStatusCountDTO> GetBulletinsCountByPersonId(string personId)
        {
            var allBulletins = _personHelperRepository.GetAllBulletinsByPersonId(personId);
            var restult = allBulletins.GroupBy(x => x.BulletinType)
                .Select(x => new ObjectStatusCountDTO
                {
                    Status = x.Key,
                    Count = x.Count(),
                });

            return restult;
        }

        public async Task<PPersonId> GetPersonIdByIdAsync(string pidId)
        {
            var personId = await _dbContext.PPersonIds.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == pidId);

            return personId;
        }

        public async Task<PPerson> GetExistingPersonWithPidsDataAsync(string id)
        {
            var existingPerson = await _dbContext.PPeople
                                .AsNoTracking()
                                .Include(x => x.PPersonIds)
                                .Include(x => x.PPersonCitizenships)
                                .FirstOrDefaultAsync(x => x.Id == id);

            return existingPerson;
        }

        public IQueryable<PPerson> GetExistingPeopleWithPidsData(IEnumerable<string> ids)
        {
            var existingPersons = _dbContext.PPeople
                 .AsNoTracking()
                 .Include(x => x.PPersonIds)
                 .Where(x => ids.Contains(x.Id));

            return existingPersons;
        }

        public IQueryable<PPerson> GetPeopleToBeConectedWithPidData(string firstPersonId, string secondPersonId)
        {
            var people = _dbContext.PPeople
                              .AsNoTracking()
                              .Include(x => x.PPersonIds)
                              .Include(x => x.PPersonCitizenships)
                              .Where(x => x.Id == firstPersonId || x.Id == secondPersonId);

            return people;
        }

        public async Task<string> GetIsoNumberByCountryIdAsync(string countryId)
        {
            var result = (await _dbContext.GCountries
                     .AsNoTracking()
                     .FirstOrDefaultAsync(x => x.Id == countryId))?.Iso31662Number;

            return result;
        }

        public async Task SavePersonAndUpdateSearchAttributesAsync(PPerson person, CancellationToken cancellationToken = default, bool clearTracker = false)
        {
            IDbContextTransaction dbTransaction = null;
            try
            {
                dbTransaction = await this._dbContext.Database.BeginTransactionAsync(cancellationToken);

                // save all person data
                await SaveChangesWithoutTransactionAsync(clearTracker);


                // call precedures
                foreach (var item in person.PPersonIds)
                {
                    await CallRefreshSearchAttributeProcedureAsync(item);
                }

                await dbTransaction.CommitAsync(cancellationToken);

                //туй го добавям, щото става голямо объркване със State И EntityState.
                //След SaveChanges State=Unchanged, докато EntityState остава такъв, каквъто е бил преди SaveChanges().
                //В някои случаи това е ГОЛЯМ проблем
                //Трябва да се види, дали няма нужда и при Exception да се прави нещо с тези EntityState-ове
                //todo: Надя , Може би this.ChangeTracker.Clear(); след извикването на SaveChanges ще свърши работа
                foreach (var dbEntityEntry in this._dbContext.ChangeTracker.Entries())
                {
                    if ((dbEntityEntry.Entity is BaseEntity) && ((BaseEntity)dbEntityEntry.Entity).EntityState != Common.Enums.EntityStateEnum.Unchanged)
                    {
                        ((BaseEntity)dbEntityEntry.Entity).EntityState = Common.Enums.EntityStateEnum.Unchanged;
                    }
                }

                if (clearTracker)
                {
                    this._dbContext.ChangeTracker.Clear();
                }

            }
            catch (Exception ex)
            {
                await dbTransaction.RollbackAsync(cancellationToken);
                throw;
            }
            finally
            {
                await dbTransaction.DisposeAsync();
            }
        }

        private async Task CallRefreshSearchAttributeProcedureAsync(PPersonId pid)
        {
            try
            {
                using OracleConnection oracleConnection = new OracleConnection(_dbContext.Database.GetConnectionString());

                // create command
                var cmd = new OracleCommand("psearch.refresh_search_attributes", oracleConnection);

                cmd.CommandType = CommandType.StoredProcedure;
                await oracleConnection.OpenAsync();

                try
                {
                    // set parameters
                    cmd.Parameters.Add(new OracleParameter("p_pid_type_id", OracleDbType.Varchar2, pid.PidTypeId, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_pid", OracleDbType.Varchar2, pid.Pid, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_pid_issuer", OracleDbType.Varchar2, pid.Issuer, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_country_id", OracleDbType.Varchar2, pid.CountryId, ParameterDirection.Input));

                    cmd.ExecuteNonQuery();
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
            catch (Exception ex)
            {
                // todo: log error
                // add message
                throw;
            }

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
            person.Pids = dataRow["person_pids"]?.ToString();
            var isParsedDate = DateTime.TryParse(dataRow["person_birthdate"]?.ToString(), out DateTime birthDate);
            person.BirthDate = isParsedDate ? birthDate : null;

            var isParsedSex = int.TryParse(dataRow["person_sex"]?.ToString(), out int sex);
            if (isParsedSex && sex == 1)
            {
                person.Sex = PersonResources.male;
            }
            else if (isParsedSex && sex == 2)
            {
                person.Sex = PersonResources.female;
            }
            else
            {
                person.Sex = PersonResources.unknown;
            }

            person.PersonNames = dataRow["person_names"]?.ToString();
            person.MotherNames = dataRow["mother_names"]?.ToString();
            person.FatherNames = dataRow["father_names"]?.ToString();

            person.BgCitizen = dataRow["bg_citizen"]?.ToString();
            person.NonBGCitizen = dataRow["nonbg_citizen"]?.ToString();

            const string isMatchParam = "1";
            person.IsConvicted = dataRow["is_convicted"]?.ToString() == isMatchParam ? PersonResources.msgYes : PersonResources.msgNo;

            var sb = new List<string>();

            if (dataRow["pid_match"]?.ToString() == isMatchParam)
            {
                sb.Add(PersonResources.msgMatchPid);
            }

            if (dataRow["firstname_match"]?.ToString() == isMatchParam)
            {
                sb.Add(PersonResources.msgMatchFirstname);
            }

            if (dataRow["surname_match"]?.ToString() == isMatchParam)
            {
                sb.Add(PersonResources.msgMatchSurname);
            }

            if (dataRow["familyname_match"]?.ToString() == isMatchParam)
            {
                sb.Add(PersonResources.msgMatchFamilyname);
            }

            if (dataRow["fullname_match"]?.ToString() == isMatchParam)
            {
                sb.Add(PersonResources.msgMatchFullname);
            }

            if (dataRow["fullname_match"]?.ToString() == isMatchParam)
            {
                sb.Add(PersonResources.msgMatchFullname);
            }

            if (dataRow["birthdate_match"]?.ToString() == isMatchParam)
            {
                sb.Add(PersonResources.msgMatchBirthdate);
            }

            if (dataRow["birthyear_match"]?.ToString() == isMatchParam)
            {
                sb.Add(PersonResources.msgMatchBirthyear);
            }

            if (dataRow["initials_match"]?.ToString() == isMatchParam)
            {
                sb.Add(PersonResources.msgMatchInitials);
            }

            if (dataRow["two_words_of_name_match"]?.ToString() == isMatchParam)
            {
                sb.Add(PersonResources.msgMatchTwoWordsOfName);
            }

            if (dataRow["two_initials_of_name_match"]?.ToString() == isMatchParam)
            {
                sb.Add(PersonResources.msgMatchTwoInitialsOfName);
            }

            if (dataRow["bg_citizen_match"]?.ToString() == isMatchParam)
            {
                sb.Add(PersonResources.msgMatchBgCitizen);
            }

            if (dataRow["nonbg_citizen_match"]?.ToString() == isMatchParam)
            {
                sb.Add(PersonResources.msgMatchNonBGCitizen);
            }

            person.MatchText = string.Join(", ", sb);
            return person;
        }

        public async Task<List<PPerson>> GetPersonByID(IQueryable<string> personIds)
        {
            return await
                (from p in _dbContext.PPeople.Include(p => p.BirthCity).Include(p => p.BirthCountry).Include(p => p.PPersonIds).ThenInclude(pid => pid.PidType)
                 where personIds.Contains(p.Id)
                 select p).ToListAsync();
        }

        public async Task<IQueryable<string>> GetPersonIDsByPersonData(string? firstname, string? surname, string? familyname, string? birthCountry, DateTime birthdate, string birthDatePrec, string? birthplace, string? fullname, DateTime birthdateFrom, DateTime birthdateTo, int birthdateYear)
        {
            return (
                from ph in _dbContext.PPersonHs.Include(p => p.BirthCountry).Include(p => p.BirthCity)
                join phids in _dbContext.PPersonIdsHes on ph.Id equals phids.PersonHId
                join pids in _dbContext.PPersonIds on new { phids.Pid, phids.PidTypeId } equals new { pids.Pid, pids.PidTypeId }
                where (string.IsNullOrEmpty(firstname) || ph.Firstname.ToUpper().Contains(firstname)) &&
                      (string.IsNullOrEmpty(surname) || ph.Surname.ToUpper().Contains(surname)) &&
                      (string.IsNullOrEmpty(familyname) || ph.Familyname.ToUpper().Contains(familyname)) &&
                      (string.IsNullOrEmpty(fullname) || ph.Fullname.ToUpper().Contains(fullname)) &&
                      (string.IsNullOrEmpty(birthCountry) || ph.BirthCountry.Name.ToUpper().Contains(birthCountry)) &&
                      (string.IsNullOrEmpty(birthplace) || ph.BirthCity.Name.ToUpper().Contains(birthplace)) &&
                      (
                         (!string.IsNullOrEmpty(birthDatePrec) && birthDatePrec.Equals("YM") && ph.BirthDate >= birthdateFrom && ph.BirthDate <= birthdateTo) ||
                         (!string.IsNullOrEmpty(birthDatePrec) && birthDatePrec.Equals("Y") && ph.BirthDate.Value.Year == birthdateYear) ||
                         ph.BirthDate.Equals(birthdate)
                     )

                select pids.PersonId
                ).Distinct().Take(100);
        }

        public async Task<List<CriminalRecordsPersonDataType>> GetPersonsByPersonData(string? firstname, string? surname, string? familyname, string? birthCountry, DateTime birthdate, string birthDatePrec, string? birthplace, string? fullname)
        {
            var crs = new List<CriminalRecordsPersonDataType>();
            DataSet ds = new DataSet();
            var MAX_RECORDS = 500;
            try
            {
                using (OracleConnection oracleConnection = new OracleConnection(_dbContext.Database.GetConnectionString()))
                {
                    // Create command
                    OracleCommand cmd = new OracleCommand("search_persons_for_reports", oracleConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Set parameters

                    cmd.Parameters.Add(new OracleParameter("p_firstname", OracleDbType.Varchar2, firstname?.Trim(), ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_surname", OracleDbType.Varchar2, surname?.Trim(), ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_familyname", OracleDbType.Varchar2, familyname?.Trim(), ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_fullname", OracleDbType.Varchar2, fullname?.Trim(), ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_birthdate", OracleDbType.Date, birthdate, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_precision", OracleDbType.Varchar2, birthDatePrec.Trim().ToUpper(), ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_birth_country", OracleDbType.Varchar2, birthCountry?.Trim(), ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_birth_place", OracleDbType.Varchar2, birthplace?.Trim(), ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_max_records", OracleDbType.Int32, MAX_RECORDS, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("p_out", OracleDbType.RefCursor, null, ParameterDirection.Output));

                    OracleDataAdapter resultDataSet = new OracleDataAdapter(cmd);
                    try
                    {
                        await oracleConnection.OpenAsync();
                        resultDataSet.Fill(ds);
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            var cr = new CriminalRecordsPersonDataType();

                            var isParsedDate = DateTime.TryParse(row["birth_date"]?.ToString(), out DateTime birthDate);
                            var hasPrecision = Enum.TryParse(row["birth_date_prec"]?.ToString(), true, out DatePrecisionEnum datePrecision);
                            cr.BirthDate = new DateType()
                            {
                                Date = birthdate,
                                DatePrecision = datePrecision,
                                DatePrecisionSpecified = hasPrecision
                            };
                            cr.IdentityNumber = new PersonIdentityNumberType();
                            switch (row["pid_type_code"]?.ToString())
                            {
                                case "SYS":
                                    {
                                        cr.IdentityNumber.SUID = row["pid"]?.ToString();
                                        break;
                                    }
                                case "EGN":
                                    {
                                        cr.IdentityNumber.EGN = row["pid"]?.ToString();
                                        break;
                                    }
                                case "LNCH":
                                    {
                                        cr.IdentityNumber.LNCh = row["pid"]?.ToString();
                                        break;
                                    }
                                case "LN":
                                    {
                                        cr.IdentityNumber.LN = row["pid"]?.ToString();
                                        break;
                                    }
                            }
                            cr.NamesBg = new PersonNameType()
                            {
                                FirstName = row["firstname"]?.ToString(),
                                SurName = row["surname"]?.ToString(),
                                FamilyName = row["familyname"]?.ToString(),
                                FullName = row["fullname"]?.ToString(),
                            };
                            cr.NamesEn = new PersonNameType()
                            {
                                FirstName = row["firstname_lat"]?.ToString(),
                                SurName = row["surname_lat"]?.ToString(),
                                FamilyName = row["familyname_lat"]?.ToString(),
                                FullName = row["fullname_lat"]?.ToString(),
                            };
                            cr.Sex = (row["sex"] != DBNull.Value) ? Int32.Parse(row["sex"].ToString()) : 0;
                            cr.MotherNames = new PersonNameType()
                            {
                                FirstName = row["mother_firstname"]?.ToString(),
                                SurName = row["mother_surname"]?.ToString(),
                                FamilyName = row["mother_familyname"]?.ToString(),
                                FullName = row["mother_fullname"]?.ToString(),
                            };
                            cr.FatherNames = new PersonNameType()
                            {
                                FirstName = row["father_firstname"]?.ToString(),
                                SurName = row["father_surname"]?.ToString(),
                                FamilyName = row["father_familyname"]?.ToString(),
                                FullName = row["father_fullname"]?.ToString(),
                            };
                            cr.BirthPlace = (!string.IsNullOrEmpty(row["birthcounry"]?.ToString()) || !string.IsNullOrEmpty(row["birthcity"]?.ToString()) || !string.IsNullOrEmpty(row["birth_place_other"]?.ToString())) ? new PlaceType()
                            {
                                City = (!string.IsNullOrEmpty(row["birthcity"]?.ToString())) ? new CityType()
                                {
                                    CityName = row["birthcity"]?.ToString(),
                                    EKATTECode = row["birthcity_ekatte"]?.ToString(),
                                } : null,
                                Country = (!string.IsNullOrEmpty(row["birthcounry"]?.ToString())) ? new CountryType()
                                {
                                    CountryName = row["birthcounry"]?.ToString(),
                                    CountryISOAlpha3 = row["birthcounry_code"]?.ToString(),
                                    CountryISONumber = row["birthcounry_number"]?.ToString()
                                } : null,
                                Descr = row["birth_place_other"]?.ToString()
                            } : null;
                            crs.Add(cr);
                        }
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

            return crs;
        }

        public async Task<PersonSearchFormDTO> GetPersonByPidAsync(string pid, string pidType)
        {
            var result = ReturnEmptyModel(pid, pidType);
            var personId = (await _dbContext.PPersonIds.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Pid == pid && x.PidTypeId == pidType))?.PersonId;

            if (personId == null) return result;

            var person = await _dbContext.PPeople
                .Include(x => x.PPersonIds)
                 //.Include(x => x.PPersonCitizenships)
                 .FirstOrDefaultAsync(x => x.Id == personId);

            if (result == null) return result;


            result.BirthDate = person.BirthDate;
            if (string.IsNullOrEmpty(result.Egn))
            {
                result.Egn = person.PPersonIds.FirstOrDefault(x => x.PidTypeId == PidType.Egn)?.Pid;
            }

            if (string.IsNullOrEmpty(result.Lnch))
            {
                result.Lnch = person.PPersonIds.FirstOrDefault(x => x.PidTypeId == PidType.Lnch)?.Pid;
            }

            result.Familyname = person.Familyname;
            result.Firstname = person.Firstname;
            result.Fullname = person.Fullname;
            result.Surname = person.Surname;

            return result;
        }

        private static PersonSearchFormDTO ReturnEmptyModel(string pid, string pidType)
        {
            var result = new PersonSearchFormDTO();

            if (pidType == PidType.Egn)
            {
                result.Egn = pid;
            }
            else if (pidType == PidType.Lnch)
            {
                result.Lnch = pid;
            }

            return result;
        }
    }
}
