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
using static MJ_CAIS.Common.Constants.PersonConstants;

namespace MJ_CAIS.Services
{
    public class ManagePersonService : IManagePersonService
    {
        private readonly IPersonRepository _personRepository;
        protected IMapper _mapper;
        protected IConfigurationProvider mapperConfiguration => _mapper.ConfigurationProvider;

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
                return CreateNewPerson(aInDto, pids, personId);
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
                allPids.AddRange(pids.Where(x => x.EntityState == EntityStateEnum.Added));
                existingPerson.PPersonIds = allPids;
                return UpdatePersonDataWhenHasOnePerson(aInDto, existingPerson);
            }

            // when create person from bulletin, app or fbbc
            // auto marge is not allowed
            // user will be notified for existing pids connected to another person
            if (!autoMergePeople)
            {
                return CreateNewPerson(aInDto, pids, personId);
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
        /// The method is executed by manually merging one person with another through a user interface
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="personToBeConnected"></param>
        /// <returns></returns>
        public async Task ConnectPeopleAsync(string aId, string personToBeConnected)
        {
            var people = await _personRepository.GetPeopleToBeConectedWithPidData(aId, personToBeConnected).ToListAsync();
            var personToUpdate = MergePeople(new List<PPersonId>(), people);
            personToUpdate.EntityState = EntityStateEnum.Modified;

            // call logic for only one person
            UpdatePersonDataWhenHasOnePerson(personToUpdate, false);
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

            // add person history object with pids
            var personH = _mapper.MapToEntity<PPerson, PPersonH>(newPersonData, true);

            var pidH = _mapper.MapToEntity<PPersonId, PPersonIdsH>(pidToBeRemoved, true);
            pidH.Id = BaseEntity.GenerateNewId();
            personH.PPersonIdsHes = new List<PPersonIdsH> { pidH };

            GeneratePersonCitizenship(newPersonData, personH, aInDto.Nationalities.SelectedForeignKeys);

            _personRepository.ApplyChanges(newPersonData, new List<IBaseIdEntity>(), true);
            _personRepository.ApplyChanges(personH, new List<IBaseIdEntity>(), true);

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
            return person;
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
        public async Task<List<PersonIdTypeDTO>> GetPidsFromFormAsync(PersonDTO aInDto)
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

            var suid = await GenerateSuidAsync(aInDto);
            pidsFromForm.Add(new PersonIdTypeDTO(suid.ToUpper(), PidType.Suid, IssuerType.CRR));

            return pidsFromForm;
        }

        /// <summary>
        /// The method is executed when no identifiers are found in the database
        /// Create P_PERSON, P_PERSON_IDS, P_PERSON_H and P_PERSON_IDS_H objects with applied changes.
        /// </summary>
        /// <param name="aInDto">Person data</param>
        /// <param name="pids">Identifiers</param>
        /// <param name="personId">Person identifier</param>
        /// <returns>The newly created person includes the identifiers</returns>
        private PPerson CreateNewPerson(PersonDTO aInDto, List<PPersonId> pids, string personId)
        {
            // add person with pids
            var person = _mapper.MapToEntity<PersonDTO, PPerson>(aInDto, true);
            person.Id = personId;
            person.PPersonIds = pids;

            // add person history object with pids
            var personH = _mapper.MapToEntity<PPerson, PPersonH>(person, true);
            personH.PPersonIdsHes = _mapper.MapToEntityList<PPersonId, PPersonIdsH>(pids, true);

            GeneratePersonCitizenship(person, personH, aInDto.Nationalities.SelectedForeignKeys);

            _personRepository.ApplyChanges(person, new List<IBaseIdEntity>(), true);
            _personRepository.ApplyChanges(personH, new List<IBaseIdEntity>(), true);
            return person;
        }

        /// <summary>
        /// Add citizenship data to person and person history obkect
        /// </summary>
        private void GeneratePersonCitizenship(PPerson person, PPersonH personH, IEnumerable<string> nationalities)
        {
            // add person nationalities and nationalities history
            person.PPersonCitizenships = new List<PPersonCitizenship>();
            personH.PPersonHCitizenships = new List<PPersonHCitizenship>();
            foreach (var nationality in nationalities)
            {
                person.PPersonCitizenships.Add(new PPersonCitizenship
                {
                    Id = BaseEntity.GenerateNewId(),
                    EntityState = EntityStateEnum.Added,
                    CountryId = nationality,
                    // PersonId = person.Id
                });

                personH.PPersonHCitizenships.Add(new PPersonHCitizenship
                {
                    Id = BaseEntity.GenerateNewId(),
                    EntityState = EntityStateEnum.Added,
                    CountryId = nationality,
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

            // check person data
            var isPersonEqueals = personToUpdate.Equals(existingPerson);
            return UpdatePersonDataWhenHasOnePerson(personToUpdate, isPersonEqueals);
        }

        /// <summary>
        /// The method is executed when a person with the specified identifiers is found. (ONLY ONE!)
        /// Create P_PERSON, P_PERSON_IDS, P_PERSON_H and P_PERSON_IDS_H objects with applied changes.
        /// <param name="personToUpdate">Person with updated data</param>
        /// <returns>Updated person includes the identifiers</returns>
        /// </summary>
        private PPerson UpdatePersonDataWhenHasOnePerson(PPerson personToUpdate, bool isPersonEqueals)
        {
            if (personToUpdate.ModifiedProperties == null)
            {
                personToUpdate.ModifiedProperties = new List<string>()
                {
                    nameof(personToUpdate.UpdatedOn),
                    nameof(personToUpdate.Version)
                };
            }

            var allPidsExists = personToUpdate.PPersonIds.All(x => x.EntityState == EntityStateEnum.Unchanged);
            var allNationalitiesExists = personToUpdate.PPersonCitizenships.All(x => x.EntityState == EntityStateEnum.Unchanged);
            if (allPidsExists && allNationalitiesExists && isPersonEqueals) return personToUpdate;

            // create person history object with old data
            var personHistoryToBeAdded = _mapper.MapToEntity<PPerson, PPersonH>(personToUpdate, true);
            personHistoryToBeAdded.Id = BaseEntity.GenerateNewId();

            // existing and new pids
            var allPids = _mapper.MapToEntityList<PPersonId, PPersonIdsH>(personToUpdate.PPersonIds.ToList(), true, true);
            personHistoryToBeAdded.PPersonIdsHes = allPids;

            // existing and new nationalities
            var allNationalities = _mapper.MapToEntityList<PPersonCitizenship, PPersonHCitizenship>(personToUpdate.PPersonCitizenships.ToList(), true, true);
            personHistoryToBeAdded.PPersonHCitizenships = allNationalities;

            _personRepository.ApplyChanges(personToUpdate, new List<IBaseIdEntity>(), true);
            _personRepository.ApplyChanges(personHistoryToBeAdded, new List<IBaseIdEntity>(), true);
            return personToUpdate;
        }

        /// <summary>
        /// Merge information of more than one person in one object.
        /// Move identifier and delete people
        /// </summary>
        /// <param name="pids"></param>
        /// <param name="existingPersons"></param>
        /// <returns></returns>
        private PPerson MergePeople(List<PPersonId> pids, List<PPerson> existingPersons)
        {
            // get last added or modified person object
            var lastPerson = existingPersons.OrderByDescending(x => x.UpdatedOn).ThenByDescending(x => x.CreatedOn).FirstOrDefault();
            var lastPersonId = lastPerson?.Id;

            // locally added pids
            var pidsToBeAdded = pids.Where(x => x.EntityState == EntityStateEnum.Added).ToList();
            // move connections to this person
            foreach (var person in existingPersons)
            {
                //just add unchanged entity for history object
                if (person.Id == lastPersonId)
                {
                    pidsToBeAdded.AddRange(person.PPersonIds);
                    continue;
                }

                foreach (var personPidId in person.PPersonIds)
                {
                    personPidId.PersonId = lastPersonId;
                    personPidId.EntityState = EntityStateEnum.Modified;
                    personPidId.ModifiedProperties = new List<string>
                    {
                        nameof(personPidId.PersonId),
                        nameof(personPidId.Version)
                    };
                }

                // pids from other person
                pidsToBeAdded.AddRange(person.PPersonIds);

                // remove other people 
                person.EntityState = EntityStateEnum.Deleted;
                _personRepository.ApplyChanges(person, new List<IBaseIdEntity>());
            }

            lastPerson.PPersonIds = pidsToBeAdded;
            return lastPerson;
        }

        private async Task<string> GenerateSuidAsync(PersonDTO person)
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

        #endregion
    }
}
