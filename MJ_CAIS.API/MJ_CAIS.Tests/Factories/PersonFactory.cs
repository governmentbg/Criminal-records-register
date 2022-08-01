using MJ_CAIS.DTO.Common;
using MJ_CAIS.DTO.Person;
using System;
using System.Collections.Generic;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Tests.Factories
{
    internal static class PersonFactory
    {
        public static PersonDTO GetFilledInPersonDto(bool onlyEgn =false)
        {
            return new PersonDTO()
            {
                Id = "1564941",
                BirthDate = DateTime.Now,
                Firstname = "Стефка",
                Surname = "Йонкова",
                Familyname = "Василева",
                Fullname = "Стефка Йонкова Василева",
                FirstnameLat = "Stefka",
                SurnameLat = "Yonkova",
                FamilynameLat = "Vasileva",
                FullnameLat = "Stefka Yonkova Vasileva",
                FatherFirstname = "Йонко",
                FatherSurname = "Стефанов",
                FatherFamilyname = "Василев",
                FatherFullname = "Йонко Стефанов Василев",
                MotherFirstname = "Ирина",
                MotherSurname = "Александрова",
                MotherFamilyname = "Василева",
                MotherFullname = "Ирина Александрова Василева",
                Sex = 2,
                Egn = "1010101010",
                Ln = onlyEgn ? null: "1111111111",
                Lnch = onlyEgn ? null: "1212121212",
                AfisNumber = onlyEgn ? null: "1313131313",
                Suid = "7894155as",
                Version = 1,
                BirthPlace = new AddressDTO()
                {
                    CityId = "1",
                    Country = new LookupDTO()
                    {
                        Id = "2",
                        DisplayName = "България"
                    },
                    CityDisplayName = "България",
                    DistrictDisplayName = "Пазарджик",
                    DistrictId = "2",
                    ForeignCountryAddress = "Тест",
                    MunicipalityDisplayName = "Пазарджик",
                    MunicipalityId = "3"
                },
                Bulletin78ACount = 0,
                BulletinUnspecifiedCount = 0,
                ContextType = "Person",
                ConvictionBulletinCount = 1,
                IdDocCategoryId = "1",
                IdDocIssuingAuthority = "MVR",
                IdDocIssuingDate = new DateTime(2020, 01, 01),
                IdDocNumber = "123",
                IdDocTypeDescr = "desc",
                IdDocValidDate = new DateTime(2030, 01, 01),
                Nationalities = new MultipleChooseDTO()
                {
                    IsChanged = true,
                    SelectedForeignKeys = new List<string> { "BG" },
                    SelectedPrimaryKeys = new List<string> { "BGN" },
                },
                NationalitiesNames = new List<string> { "България" },
            };
        }

        public static PPerson GetFilledInPerson()
        {
            return new PPerson()
            {
                Id = "1564941",
                BirthDate = DateTime.Now,
                Firstname = "Стефка",
                Surname = "Йонкова",
                Familyname = "Василева",
                Fullname = "Стефка Йонкова Василева",
                FirstnameLat = "Stefka",
                SurnameLat = "Yonkova",
                FamilynameLat = "Vasileva",
                FullnameLat = "Stefka Yonkova Vasileva",
                FatherFirstname = "Йонко",
                FatherSurname = "Стефанов",
                FatherFamilyname = "Василев",
                FatherFullname = "Йонко Стефанов Василев",
                MotherFirstname = "Ирина",
                MotherSurname = "Александрова",
                MotherFamilyname = "Василева",
                MotherFullname = "Ирина Александрова Василева",
                Sex = 2,
                PPersonIds = new List<PPersonId>
                {
                    new()
                    {
                        Pid = "1010101010",
                        PidTypeId = PersonConstants.PidType.Egn
                    },
                    new ()
                    {
                        Pid = "1111111111",
                        PidTypeId =  PersonConstants.PidType.Lnch
                    },
                    new ()
                    {
                        Pid = "1212121212",
                        PidTypeId =  PersonConstants.PidType.Ln
                    },
                    new()
                    {
                        Pid = "1313131313",
                        PidTypeId =  PersonConstants.PidType.AfisNumber
                    }
                },

            };
        }

        public static PPersonId GetFilledPersonId(string pidType = PersonConstants.PidType.Suid, EntityStateEnum entityState = EntityStateEnum.Added, string personId = "fd44544sds54d7sds5d")
        {
            return new PPersonId()
            {
                Id = Guid.NewGuid().ToString(),
                Pid = Guid.NewGuid().ToString(),
                PidTypeId = pidType,
                EntityState = entityState,
                PersonId = personId
            };
        }
    }
}
