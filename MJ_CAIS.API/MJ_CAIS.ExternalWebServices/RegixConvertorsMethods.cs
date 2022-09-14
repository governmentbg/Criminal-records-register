using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.RegixIntegration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.ExternalWebServices
{
    public class RegixConvertorsMethods
    {
        public object Identity(CaisDbContext c, ERegixCache cache, string field)
        {
            return cache.GetType().GetProperty(field).GetValue(cache);
        }
        public string ToUpper(CaisDbContext c, ERegixCache cache, string field)
        {
            return ((string)cache.GetType().GetProperty(field).GetValue(cache))?.ToUpper();
        }
        public decimal? StringToDecimal(CaisDbContext c, ERegixCache cache, string field)
        {
            decimal result;
            if (decimal.TryParse((string)cache.GetType().GetProperty(field).GetValue(cache), out result))
            {
                return result;
            }
            else return null;


        }

        public string GetBirthCityId(CaisDbContext c, ERegixCache cache, string field)
        {
            string? birtCityId = null;

            if (cache.BirthDistrictName != null && cache.BirthMunName != null && cache.BirthCityName != null)
            {
                birtCityId = TryGetCityIdByNames(cache.BirthDistrictName.Trim(), cache.BirthMunName.Trim(), cache.BirthCityName.Trim(), c).GetAwaiter().GetResult();
                if (!string.IsNullOrEmpty(birtCityId))
                {
                    return birtCityId;
                }
            }

            // var graoData = GetCityMotherFatherFromGraoByEGN(cache.Egn, c).GetAwaiter().GetResult();
            // birtCityId = graoData.Item1;
            return birtCityId;
        }

        //private async Task<(string?, string?, string?)> GetCityMotherFatherFromGraoByEGN(string egn, CaisDbContext context)
        //{
        //    var graoPerson = await context.GraoPeople.AsNoTracking()
        //                .FirstOrDefaultAsync(a => a.Egn == egn);
        //    if (graoPerson != null)
        //    {
        //        if (graoPerson.BirthplaceCode != null)
        //        {
        //            var gCity = await context.GCities.AsNoTracking()
        //                                .FirstOrDefaultAsync(a => a.EkatteCode == graoPerson.BirthplaceCode);
        //            if (gCity != null)
        //            {
        //                return (gCity.Id, graoPerson.MothersNames, graoPerson.FathersNames);
        //            }
        //        }
        //        return (null, graoPerson.MothersNames, graoPerson.FathersNames);
        //    }
        //    return (null, null, null);
        //}

        private async Task<string?> TryGetCityIdByNames(string districtName, string munName, string cityname, CaisDbContext context)
        {
            string? districtId = await GetDistrictIdByName(districtName, context);
            if (districtId != null)
            {
                string? munId = await GetMunicipalityIdByName(munName, districtId, context);
                if (munId != null)
                {
                    string? cityId = await GetCityIdByName(cityname, munId, context);
                    return cityId;
                }
            }
            return null;
        }
        private async Task<string?> GetDistrictIdByName(string name, CaisDbContext context)
        {
            var district = await context.GBgDistricts.AsNoTracking().OrderByDescending(c => c.ValidFrom)
                                       .FirstOrDefaultAsync(x => x.Name.ToUpper() == name.ToUpper());
            if (district != null)
            {
                return district.Id;
            }
            return null;
        }

        private async Task<string?> GetMunicipalityIdByName(string name, string? districtId, CaisDbContext context)
        {
            var municipality = await context.GBgMunicipalities.AsNoTracking().OrderByDescending(c => c.ValidFrom)
                                       .FirstOrDefaultAsync(x => x.Name.ToUpper() == name.ToUpper() && x.DistrictId == districtId);
            if (municipality != null)
            {
                return municipality.Id;
            }
            return null;
        }

        private async Task<string?> GetCityIdByName(string name, string? municipalityId, CaisDbContext context)
        {
            var municipality = await context.GCities.AsNoTracking().OrderByDescending(c => c.ValidFrom)
                                       .FirstOrDefaultAsync(x =>
                                                x.Name.ToUpper().EndsWith(". " + name.ToUpper()) && x.MunicipalityId == municipalityId);
            if (municipality != null)
            {
                return municipality.Id;
            }
            return null;
        }

        public string GetBirthPlaceOther(CaisDbContext c, ERegixCache cache, string field)
        {

            //todo: дали е нужно това?!
            if (string.IsNullOrEmpty(GetBirthCityId(c, cache, field)))
            {
                string birthPlaceOther = (cache.BirthDistrictName != null ? (cache.BirthDistrictName + " ") : "")
                    + (cache.BirthMunName != null ? (cache.BirthMunName + " ") : "")
                    + (cache.BirthCityName != null ? (cache.BirthCityName + " ") : "")
                    + cache.BirthPlace;
                return birthPlaceOther;
            }
            else
            {
                return cache.BirthPlace;
            }
        }

        public string? GetCountryId(CaisDbContext c, ERegixCache cache, string field)
        {

            string value = (string)cache.GetType().GetProperty(field).GetValue(cache);
            if (!string.IsNullOrEmpty(value))
            {
                string? countryId = GetCounrtyByCode(value.ToUpper(), c).GetAwaiter().GetResult();
                if (countryId != null)
                {
                    return countryId;
                }

            }
            return null;
        }


        public List<AAppCitizenshipValues> GetAAppCitizenship(CaisDbContext c, ERegixCache cache, string field)
        {
            var result = new List<AAppCitizenshipValues>();
            //string value = (string)cache.GetType().GetProperty(field).GetValue(cache);
            if (!string.IsNullOrEmpty(cache.NationalityCode1))
            {
                string? countryId = GetCounrtyByCode(cache.NationalityCode1.ToUpper(), c).GetAwaiter().GetResult();
                if (countryId != null)
                {
                    AAppCitizenshipValues appCitizenship = new AAppCitizenshipValues();
                    appCitizenship.CountryId = countryId;
                    result.Add(appCitizenship);

                }

            }
            if (!string.IsNullOrEmpty(cache.NationalityCode2))
            {
                string? countryId = GetCounrtyByCode(cache.NationalityCode2.ToUpper(), c).GetAwaiter().GetResult();
                if (countryId != null)
                {
                    AAppCitizenshipValues appCitizenship = new AAppCitizenshipValues();
                    appCitizenship.CountryId = countryId;
                    result.Add(appCitizenship);

                }

            }
            return result;
        }
        public List<PersonAliasesValues> GetAAppPersAliases(CaisDbContext c, ERegixCache cache, string field)
        {
            var result = new List<PersonAliasesValues>();

            if ((!string.IsNullOrEmpty(cache.ForeignFirstname)
            || !string.IsNullOrEmpty(cache.ForeignSurname)
            || !string.IsNullOrEmpty(cache.ForeignFamilyname)
            || !string.IsNullOrEmpty(cache.Alias))
            )
            {
                var newAlias = new PersonAliasesValues
                {
                   
                    Firstname = !string.IsNullOrEmpty(cache.ForeignFirstname) ? cache.ForeignFirstname.ToUpper() : null,
                    Surname = !string.IsNullOrEmpty(cache.ForeignSurname) ? cache.ForeignSurname.ToUpper() : null,
                    Familyname = !string.IsNullOrEmpty(cache.ForeignFamilyname) ? cache.ForeignFamilyname.ToUpper() : null,
                    Type = "previous" //todo да стане foreign
                };
                result.Add(newAlias);

            }
            if (!string.IsNullOrEmpty(cache.Alias))
            {
                var newAlias = new PersonAliasesValues
                {
                 
                    Fullname = cache.Alias.ToUpper(),
                    Type = "nickname" //todo да стане constant
                };
                result.Add(newAlias);

            }

            return result;
        }

        private async Task<string?> GetCounrtyByCode(string code, CaisDbContext context)
        {
            int codeLen = code.Length;
            if (codeLen == 2)
            {
                var country = await context.GCountries.AsNoTracking().OrderByDescending(c => c.ValidFrom)
                                       .FirstOrDefaultAsync(x => x.Iso3166Alpha2 == code);
                if (country != null && country != null)
                {
                    return country.Id;
                }
            }
            if (codeLen == 3)
            {
                var country = await context.GCountries.AsNoTracking().OrderByDescending(c => c.ValidFrom)
                                        .FirstOrDefaultAsync(x => x.Iso31662Code == code);
                if (country != null)
                {
                    return country.Id;
                }
            }
            return null;
        }

    }
}
