using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.AutoMapperContainer;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Person;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using System.Security.Cryptography;
using System.Text;
using MJ_CAIS.Common.Exceptions;
using static MJ_CAIS.Common.Constants.PersonConstants;

namespace MJ_CAIS.Services
{
    public class ManagePersonService : IManagePersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public ManagePersonService(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Generate P_PERSON, P_PERSON_IDS, P_PERSON_H and P_PERSON_IDS_H objects with applied changes.
        /// SaveChanges is not called !!!
        /// </summary>
        /// <param name="aInDto">New person data</param>
        /// <param name="autoMergePeople">Auto merge people when has more then one person with those pids</param>
        /// <returns></returns>
        public async Task<PPerson> CreatePersonAsync(PersonDTO aInDto, bool autoMergePeople = false)
        {
            var personId = BaseEntity.GenerateNewId();
            var pidsFromForm = await GetPidsFromFormAsync(aInDto);
            var pids = await _personRepository.GetPersonIdsAsync(pidsFromForm, personId);

            var pidsDoNotExistExist = pids.All(x => x.EntityState == EntityStateEnum.Added);
            // must create new person object if pids do not exist in db
            if (pidsDoNotExistExist)
            {
                var newPerson = CreateNewPerson(aInDto, pids, personId);
                CreatePersonHistory(newPerson);
                return newPerson;
            }

            // identifiers of a person who exists in the database and the specific pids are attached to it
            var existingPersonsIds = pids.Where(x => x.EntityState != EntityStateEnum.Added).Select(x => x.PersonId).Distinct();
            var onlyOnePersonExist = existingPersonsIds.Count() == 1;

            // when person is only one 
            // update of this one person with the personal data
            // from the primary register (bulletin, fbbc, application or person form)           
            if (onlyOnePersonExist)
            {
                var personToBeUpdatedId = existingPersonsIds.First();

                var existingPerson = await _personRepository.GetExistingPersonWithPidsDataAsync(personToBeUpdatedId);
                // all pids
                var allPids = existingPerson.PPersonIds.ToList();
                //добавено от Надя, защото за новодобавени идентификатор, personId-то оставаше BaseEntity.GenerateNewId();
                foreach (var ppid in pids.Where(x => x.EntityState == EntityStateEnum.Added && x.PersonId != existingPerson.Id))
                {
                    ppid.PersonId = existingPerson.Id;
                    ppid.Person = existingPerson;
                }
                allPids.AddRange(pids.Where(x => x.EntityState == EntityStateEnum.Added));
                existingPerson.PPersonIds = allPids;

                return UpdatePersonDataWhenHasOnePerson(aInDto, existingPerson);
            }

            // when create person from bulletin, app or fbbc
            // auto marge is not allowed
            // user will be notified for existing pids connected to another person
            if (!autoMergePeople)
            {
                // todo: employee must chose what to do in this case
                throw new BusinessLogicException("More then one person exists with those identifier");
            }

            // !!! Automatic merging of more than one person will not be used at this stage
            // get all person related to this pids
            // pids saved in db or locally added
            var personIds = pids.Select(x => x.PersonId).ToList();
            var existingPersons = await _personRepository
                    .GetExistingPeopleWithPidsData(personIds)
                    .ToListAsync();

            var personToUpdate = MergePeople(pids, existingPersons);

            // call logic for only one person
            return UpdatePersonDataWhenHasOnePerson(aInDto, personToUpdate);
        }

        /// <summary>
        /// Create person and person identifiers history from newly created person  
        /// </summary>
        public PPersonH CreatePersonHistory(PPerson person)
        {
            if (person is null) throw new ArgumentNullException(nameof(person));

            // add person history object with pids
            var personH = _mapper.MapToEntity<PPerson, PPersonH>(person, true);
            personH.Id = BaseEntity.GenerateNewId();
            personH.PPersonIdsHes = _mapper.MapToEntityList<PPersonId, PPersonIdsH>(person.PPersonIds.ToList(), true, true);
            personH.PPersonHCitizenships = _mapper.MapToEntityList<PPersonCitizenship, PPersonHCitizenship>(person.PPersonCitizenships.ToList(), true, true);

            _personRepository.ApplyChanges(personH, applyToAllLevels: true);
            return personH;
        }

        /// <summary>
        /// The method is executed by manually merging one person with another through a user interface
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="personToBeConnected"></param>
        /// <returns></returns>
        public async Task ConnectPeopleAsync(string aId, string personToBeConnected)
        {
            var people = await _personRepository.GetPeopleToBeConectedWithPidData(aId, personToBeConnected).ToListAsync();
            var personToUpdate = MergePeople(new List<PPersonId>(), people);

            CreatePersonHistory(personToUpdate);
            // call logic for only one person
            await _personRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Remove pid from existing person to new person object
        /// </summary>
        public async Task<PPersonId> RemovePidAsync(RemovePidDTO aInDto)
        {
            // pid to be updated
            var pidToBeRemoved = await _personRepository.GetPersonIdByIdAsync(aInDto.PidId);
            if (pidToBeRemoved == null) return null;

            // new person 
            var newPersonData = _mapper.MapToEntity<RemovePidDTO, PPerson>(aInDto, true);
            newPersonData.Id = BaseEntity.GenerateNewId();
            newPersonData.PPersonIds = new List<PPersonId> { pidToBeRemoved };
            pidToBeRemoved.PersonId = newPersonData.Id;
            pidToBeRemoved.EntityState = EntityStateEnum.Modified;
            pidToBeRemoved.ModifiedProperties = new List<string> { nameof(pidToBeRemoved.PersonId), nameof(pidToBeRemoved.Version) };
            GeneratePersonCitizenship(newPersonData, aInDto.Nationalities.SelectedForeignKeys);
            _personRepository.ApplyChanges(newPersonData, new List<IBaseIdEntity>(), true);

            CreatePersonHistory(newPersonData);
            await _personRepository.SaveChangesAsync();
            return pidToBeRemoved;
        }

        public async Task<PersonDTO> SelectWithBirthInfoAsync(string aId)
        {
            var personDb = await _personRepository.SelectWithBirthInfoAsync(aId);
            var person = _mapper.Map<PPerson, PersonDTO>(personDb);
            var personIds = personDb.PPersonIds.OrderByDescending(x => x.CreatedOn);
            // last updating of a person
            person.Egn = personIds.FirstOrDefault(x => x.PidTypeId == PidType.Egn)?.Pid;
            person.Lnch = personIds.FirstOrDefault(x => x.PidTypeId == PidType.Lnch)?.Pid;
            person.Ln = personIds.FirstOrDefault(x => x.PidTypeId == PidType.Ln)?.Pid;
            person.AfisNumber = personIds.FirstOrDefault(x => x.PidTypeId == PidType.AfisNumber)?.Pid;

            if (string.IsNullOrEmpty(person.Egn) && string.IsNullOrEmpty(person.Lnch) &&
                string.IsNullOrEmpty(person.Ln) && string.IsNullOrEmpty(person.AfisNumber))
            {
                person.Suid = personIds.FirstOrDefault(x => x.PidTypeId == PidType.Suid)?.Pid;
            }

            return person;
        }

        public async Task<string> GenerateSuidAsync(PersonDTO person)
        {
            string? birthCountryIsoNumber = null;
            string? birthDateText = null;

            if (person.BirthPlace?.Country?.Id != null)
            {

                birthCountryIsoNumber = await _personRepository.GetIsoNumberByCountryIdAsync(person.BirthPlace.Country.Id);
            }

            if (person.BirthDate.HasValue)
            {
                birthDateText = ((DateTime)person.BirthDate).ToString("yyyyMMdd");
            }

            var nullText = "NULL";
            var personString = "{\"Firstname\"=\"" + (person.Firstname ?? nullText) +
            "\",\"Surname\"=\"" + (person.Surname ?? nullText) +
            "\",\"Familyname\"=\"" + (person.Familyname ?? nullText) +
            "\",\"Fullname\"=\"" + (person.Fullname ?? nullText) +
            "\",\"Sex\"=\"" + (person.Sex.HasValue ? person.Sex.Value : nullText) +
            "\",\"BirthDate\"=\"" + (birthDateText ?? nullText) +
            "\",\"BirthCountry\"=\"" + (birthCountryIsoNumber ?? nullText) +
            "\",\"BirthCity\"=\"" + (person.BirthPlace?.CityId ?? nullText) +
            "\",\"BirthPlaceOther\"=\"" + (person.BirthPlace?.ForeignCountryAddress ?? nullText) +
            "\",\"MotherFirstname\"=\"" + (person.MotherFirstname ?? nullText) +
            "\",\"MotherSurname\"=\"" + (person.MotherSurname ?? nullText) +
            "\",\"MotherFamilyname\"=\"" + (person.MotherFamilyname ?? nullText) +
            "\",\"MotherFullname\"=\"" + (person.MotherFullname ?? nullText) +
            "\",\"FatherFirstname\"=\"" + (person.FatherFirstname ?? nullText) +
            "\",\"FatherSurname\"=\"" + (person.FatherSurname ?? nullText) +
            "\",\"FatherFamilyname\"=\"" + (person.FatherFamilyname ?? nullText) +
            "\",\"FatherFullname\"=\"" + (person.FatherFullname ?? nullText) +
            "\",\"FirstnameLat\"=\"" + (person.FirstnameLat ?? nullText) +
            "\",\"SurnameLat\"=\"" + (person.SurnameLat ?? nullText) +
            "\",\"FamilynameLat\"=\"" + (person.FamilynameLat ?? nullText) +
            "\",\"FullnameLat\"=\"" + (person.FullnameLat ?? nullText) +
            "\"}";

            SHA1 mySHA1 = SHA1.Create();
            var result = mySHA1.ComputeHash(Encoding.UTF8.GetBytes(personString));
            var hash = new StringBuilder();
            foreach (byte a in result)
            {
                var h = a.ToString("X2");
                hash.Append(h);
            }

            var suid = birthDateText + birthCountryIsoNumber + hash.ToString().Substring(0, 10) + person.Sex;
            return suid;
        }

        #region Create person helpers

        /// <summary>
        /// Get P_PERSON_ID object by pids from dto. 
        /// When pid exist entity state is unchanged, 
        /// if pid does not exist P_PERSON_ID object is created with state Added
        /// </summary>
        /// <param name="aInDto"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        private async Task<List<PersonIdTypeDTO>> GetPidsFromFormAsync(PersonDTO aInDto)
        {
            var pidsFromForm = new List<PersonIdTypeDTO>();

            if (!string.IsNullOrEmpty(aInDto?.Egn))
            {
                pidsFromForm.Add(new PersonIdTypeDTO(aInDto.Egn.ToUpper(), PidType.Egn, IssuerType.GRAO));
            }

            if (!string.IsNullOrEmpty(aInDto?.Lnch))
            {
                pidsFromForm.Add(new PersonIdTypeDTO(aInDto.Lnch.ToUpper(), PidType.Lnch, IssuerType.MVR));
            }

            if (!string.IsNullOrEmpty(aInDto?.Ln))
            {
                pidsFromForm.Add(new PersonIdTypeDTO(aInDto.Ln.ToUpper(), PidType.Ln, IssuerType.EU));
            }

            if (!string.IsNullOrEmpty(aInDto?.AfisNumber))
            {
                pidsFromForm.Add(new PersonIdTypeDTO(aInDto.AfisNumber.ToUpper(), PidType.AfisNumber, IssuerType.MVR));
            }

            // get suid of an existing persion
            // when create application or another object via person form
            var getPidsFromExistingPerson =
                string.IsNullOrEmpty(aInDto.Egn) && string.IsNullOrEmpty(aInDto.Lnch) &&
                string.IsNullOrEmpty(aInDto.Ln) && string.IsNullOrEmpty(aInDto.AfisNumber) &&
                !string.IsNullOrEmpty(aInDto.Suid);

            if (getPidsFromExistingPerson)
            {
                pidsFromForm.Add(new PersonIdTypeDTO(aInDto.Suid.ToUpper(), PidType.Suid, IssuerType.CRR));
            }

            var suid = await GenerateSuidAsync(aInDto);
            pidsFromForm.Add(new PersonIdTypeDTO(suid.ToUpper(), PidType.Suid, IssuerType.CRR));

            return pidsFromForm;
        }

        /// <summary>
        /// The method is executed when no identifiers are found in the database
        /// Create P_PERSON, P_PERSON_IDS.
        /// </summary>
        /// <param name="aInDto">Person data</param>
        /// <param name="pids">Identifiers</param>
        /// <param name="personId">Person identifier</param>
        /// <returns>The newly created person includes the identifiers</returns>
        private PPerson CreateNewPerson(PersonDTO aInDto, List<PPersonId> pids, string personId)
        {
            if (aInDto is null) throw new ArgumentNullException(nameof(aInDto));
            if (pids is null || pids.Count == 0) throw new ArgumentNullException(nameof(pids));
            if (string.IsNullOrEmpty(personId)) throw new ArgumentNullException(nameof(personId));

            // add person with pids
            var person = _mapper.MapToEntity<PersonDTO, PPerson>(aInDto, true);
            person.Id = personId;
            person.PPersonIds = pids;

            GeneratePersonCitizenship(person, aInDto.Nationalities?.SelectedForeignKeys);

            _personRepository.ApplyChanges(person, new List<IBaseIdEntity>(), true);

            return person;
        }

        /// <summary>
        /// Add citizenship data to person and person history object
        /// </summary>
        private void GeneratePersonCitizenship(PPerson person, IEnumerable<string> nationalities)
        {
            if (person is null) throw new ArgumentNullException(nameof(person));

            // add person nationalities and nationalities history
            person.PPersonCitizenships = new List<PPersonCitizenship>();
            if (nationalities is null) return;

            foreach (var nationality in nationalities)
            {
                person.PPersonCitizenships.Add(new PPersonCitizenship
                {
                    Id = BaseEntity.GenerateNewId(),
                    EntityState = EntityStateEnum.Added,
                    CountryId = nationality,
                    // PersonId = person.Id
                });
            }
        }

        /// <summary>
        /// The method is executed when a person with the specified identifiers is found. (ONLY ONE!)
        /// Create P_PERSON, P_PERSON_IDS, P_PERSON_H and P_PERSON_IDS_H objects with applied changes.
        /// <param name="aInDto">Person data</param>
        /// <param name="existingPerson">Data about the person from the database</param>
        /// <returns>Updated person includes the identifiers</returns>
        /// </summary>
        private PPerson UpdatePersonDataWhenHasOnePerson(PersonDTO aInDto, PPerson existingPerson)
        {
            // update person with new data
            var personToUpdate = _mapper.MapToEntity<PersonDTO, PPerson>(aInDto, false);
            personToUpdate.Id = existingPerson.Id;
            personToUpdate.Version = existingPerson.Version;

            var existingNationalities = existingPerson.PPersonCitizenships.Select(x => x.CountryId);
            var newNationalitiesToBeAdded = aInDto.Nationalities.SelectedForeignKeys.Except(existingNationalities);

            // add person nationalities
            personToUpdate.PPersonCitizenships = existingPerson.PPersonCitizenships ?? new List<PPersonCitizenship>();
            foreach (var nationality in newNationalitiesToBeAdded)
            {
                personToUpdate.PPersonCitizenships.Add(new PPersonCitizenship
                {
                    Id = BaseEntity.GenerateNewId(),
                    EntityState = EntityStateEnum.Added,
                    CountryId = nationality,
                });
            }

            // аll identifiers, both new and old are added
            // so that when the object returns to a registry
            // it can add connection to the those pids
            personToUpdate.PPersonIds = existingPerson.PPersonIds;
            _personRepository.ApplyChanges(personToUpdate, applyToAllLevels: true);

            // check person data
            var isPersonEquals = personToUpdate.Equals(existingPerson);
            // if there is no changes do not add history
            var allPidsExists = personToUpdate.PPersonIds.All(x => x.EntityState == EntityStateEnum.Unchanged);
            var allNationalitiesExists = personToUpdate.PPersonCitizenships.All(x => x.EntityState == EntityStateEnum.Unchanged);
            if (allPidsExists && allNationalitiesExists && isPersonEquals) return personToUpdate;

            CreatePersonHistory(personToUpdate);

            return personToUpdate;
        }

        /// <summary>
        /// Merge information of more than one person in one object.
        /// Move identifier and delete people
        /// </summary>
        /// <param name="pids"></param>
        /// <param name="existingPersons"></param>
        /// <returns></returns>
        private PPerson MergePeople(List<PPersonId> pids, List<PPerson> existingPeople)
        {
            // get last added or modified person object
            var lastPerson = existingPeople.OrderByDescending(x => x.UpdatedOn).ThenByDescending(x => x.CreatedOn).FirstOrDefault();
            if (lastPerson == null) throw new ArgumentNullException(nameof(existingPeople));
            lastPerson.PPersonCitizenships ??= new List<PPersonCitizenship>();

            // locally added pids and nationalities
            var pidsToBeAdded = pids.Where(x => x.EntityState == EntityStateEnum.Added).ToList();
            var nationalitiesToBeAdd = new List<PPersonCitizenship>();
            // move connections to this person
            foreach (var person in existingPeople)
            {
                //just add unchanged entity for history object
                if (person.Id == lastPerson?.Id)
                {
                    pidsToBeAdded.AddRange(person.PPersonIds);
                    continue;
                }

                foreach (var personPidId in person.PPersonIds)
                {
                    personPidId.PersonId = lastPerson?.Id;
                    personPidId.EntityState = EntityStateEnum.Modified;
                    personPidId.ModifiedProperties = new List<string>
                    {
                        nameof(personPidId.PersonId),
                        nameof(personPidId.Version)
                    };
                }

                SetNationalities(lastPerson, nationalitiesToBeAdd, person);

                // pids from other person
                pidsToBeAdded.AddRange(person.PPersonIds);

                // remove other people 
                person.EntityState = EntityStateEnum.Deleted;
                _personRepository.ApplyChanges(person, applyToAllLevels: true);
            }

            lastPerson.PPersonIds = pidsToBeAdded;
            lastPerson.PPersonCitizenships = nationalitiesToBeAdd;
            lastPerson.EntityState = EntityStateEnum.Modified;

            lastPerson.ModifiedProperties ??= new List<string>();
            lastPerson.ModifiedProperties.Add(nameof(lastPerson.Version));
            _personRepository.ApplyChanges(lastPerson, applyToAllLevels: true);
            return lastPerson;
        }

        private void SetNationalities(PPerson? lastPerson, List<PPersonCitizenship> nationalitiesToBeAdd, PPerson person)
        {
            foreach (var citizenship in person.PPersonCitizenships)
            {
                if (!lastPerson.PPersonCitizenships.Any(x => x.CountryId == citizenship.CountryId))
                {
                    var naionality = citizenship;
                    naionality.PersonId = lastPerson.Id;
                    naionality.EntityState = EntityStateEnum.Modified;
                    naionality.ModifiedProperties = new List<string>
                        {
                            nameof(naionality.PersonId),
                            nameof(naionality.Version)
                        };

                    nationalitiesToBeAdd.Add(naionality);
                }
                else
                {
                    citizenship.EntityState = EntityStateEnum.Deleted;
                }
            }
        }

        #endregion
    }
}
