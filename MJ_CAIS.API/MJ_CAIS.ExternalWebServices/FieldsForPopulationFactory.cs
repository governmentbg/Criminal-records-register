using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.RegixIntegration;
using MJ_CAIS.ExternalWebServices.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.ExternalWebServices
{
    public static class FieldsForPopulationFactory
    {
        public static FieldsForPopulation GetFieldsForPopulationApplicationPersonDataSearch(string fkNameCitizenships, string fkPersAliases, string citizenNavPropName, string aliasNavPropName)
        {
            var result = new FieldsForPopulation();
            var cache = new ERegixCache();
            var application = new AApplication();

            result.Fields = new List<FieldForPopulation>();


            result.Fields.Add(GetSimpleField(nameof(cache.Firstname), nameof(application.Firstname),functionName: "ToUpper"));
            result.Fields.Add(GetSimpleField(nameof(cache.Surname), nameof(application.Surname), functionName: "ToUpper"));
            result.Fields.Add(GetSimpleField(nameof(cache.Familyname), nameof(application.Familyname), functionName: "ToUpper"));
            result.Fields.Add(GetSimpleField(nameof(cache.Egn), nameof(application.Egn)));
            result.Fields.Add(GetSimpleField(nameof(cache.Lnch), nameof(application.Lnch)));
            result.Fields.Add(GetSimpleField(nameof(cache.FamilynameLat), nameof(application.FamilynameLat), functionName: "ToUpper"));
            result.Fields.Add(GetSimpleField(nameof(cache.FirstnameLat), nameof(application.FirstnameLat), functionName: "ToUpper"));
            result.Fields.Add(GetSimpleField(nameof(cache.SurnameLat), nameof(application.SurnameLat), functionName: "ToUpper"));

            result.Fields.Add(GetSimpleField(nameof(cache.MotherSurname), nameof(application.MotherSurname), 2, functionName: "ToUpper"));
            result.Fields.Add(GetSimpleField(nameof(cache.MotherFamilyname), nameof(application.MotherFamilyname), 2, functionName: "ToUpper"));
            result.Fields.Add(GetSimpleField(nameof(cache.MotherFirstname), nameof(application.MotherFirstname), 2, functionName: "ToUpper"));
            result.Fields.Add(GetSimpleField(nameof(cache.FatherFirstname), nameof(application.FatherFirstname), 2, functionName: "ToUpper"));
            result.Fields.Add(GetSimpleField(nameof(cache.FatherSurname), nameof(application.FatherSurname), 2, functionName: "ToUpper"));
            result.Fields.Add(GetSimpleField(nameof(cache.FatherFamilyname), nameof(application.FatherFamilyname), 2, functionName: "ToUpper"));

            result.Fields.Add(GetSimpleField(nameof(cache.BirthDate), nameof(application.BirthDate), dateType: "DateTime"));
            result.Fields.Add(GetSimpleField(nameof(cache.GenderCode), nameof(application.Sex), dateType: "decimal", functionName: "StringToDecimal"));
            result.Fields.Add(GetSimpleField(nameof(cache.BirthCityName), nameof(application.BirthCityId), functionName: "GetBirthCityId"));
            result.Fields.Add(GetSimpleField(nameof(cache.BirthCityName), nameof(application.BirthPlaceOther), functionName: "GetBirthPlaceOther"));
            result.Fields.Add(GetSimpleField(nameof(cache.BirthCountryCode), nameof(application.BirthCountryId), functionName: "GetCountryId"));
            if (!string.IsNullOrEmpty(fkNameCitizenships))
            {
                var navNationalityProp = GetSimpleField(nameof(cache.NationalityCode1), citizenNavPropName);
                navNationalityProp.IsNavigation = true;
                navNationalityProp.TransformationFunctionName = "GetAAppCitizenship";
                List<AAppCitizenshipValues> citizenship = new List<AAppCitizenshipValues>();
                var str = citizenship.GetType().FullName.Split(',');
                navNationalityProp.ValueType = $"{str[0]},{ str[1]}]]";
                navNationalityProp.FkName = fkNameCitizenships;
                AAppCitizenshipValues v1 = new AAppCitizenshipValues();
                navNationalityProp.PropEquality = nameof(v1.CountryId);
                result.Fields.Add(navNationalityProp);
            }
            if (!string.IsNullOrEmpty(fkPersAliases))
            {
                var navNationalityProp = GetSimpleField(nameof(cache.NationalityCode1), aliasNavPropName);
                navNationalityProp.IsNavigation = true;
                navNationalityProp.TransformationFunctionName = "GetAAppPersAliases";
                List<PersonAliasesValues> citizenship = new List<PersonAliasesValues>();
                var str = citizenship.GetType().FullName.Split(',');
                navNationalityProp.ValueType = $"{str[0]},{ str[1]}]]";
                navNationalityProp.FkName = fkPersAliases;
                PersonAliasesValues v1 = new PersonAliasesValues();
                navNationalityProp.PropEquality = $"{nameof(v1.Type)},{nameof(v1.Firstname)}";
                result.Fields.Add(navNationalityProp);
            }


            return result;


        }
        public static FieldsForPopulation GetFieldsForPopulationApplicationForeignPerson(string fkNameCitizenships, string fkPersAliases, string citizenNavPropName, string aliasNavPropName)
        {
            var result = new FieldsForPopulation();
            var cache = new ERegixCache();
            var application = new AApplication();

            result.Fields = new List<FieldForPopulation>();


            result.Fields.Add(GetSimpleField(nameof(cache.Firstname), nameof(application.Firstname), functionName: "ToUpper"));
            result.Fields.Add(GetSimpleField(nameof(cache.Surname), nameof(application.Surname), functionName: "ToUpper"));
            result.Fields.Add(GetSimpleField(nameof(cache.Familyname), nameof(application.Familyname), functionName: "ToUpper"));
            result.Fields.Add(GetSimpleField(nameof(cache.Egn), nameof(application.Egn)));
            result.Fields.Add(GetSimpleField(nameof(cache.Lnch), nameof(application.Lnch)));
            result.Fields.Add(GetSimpleField(nameof(cache.FamilynameLat), nameof(application.FamilynameLat), functionName: "ToUpper"));
            result.Fields.Add(GetSimpleField(nameof(cache.FirstnameLat), nameof(application.FirstnameLat), functionName: "ToUpper"));
            result.Fields.Add(GetSimpleField(nameof(cache.SurnameLat), nameof(application.SurnameLat), functionName: "ToUpper"));

            result.Fields.Add(GetSimpleField(nameof(cache.MotherSurname), nameof(application.MotherSurname), 2, functionName: "ToUpper"));
            result.Fields.Add(GetSimpleField(nameof(cache.MotherFamilyname), nameof(application.MotherFamilyname), 2, functionName: "ToUpper"));
            result.Fields.Add(GetSimpleField(nameof(cache.MotherFirstname), nameof(application.MotherFirstname), 2, functionName: "ToUpper"));
            result.Fields.Add(GetSimpleField(nameof(cache.FatherFirstname), nameof(application.FatherFirstname), 2, functionName: "ToUpper"));
            result.Fields.Add(GetSimpleField(nameof(cache.FatherSurname), nameof(application.FatherSurname), 2, functionName: "ToUpper"));
            result.Fields.Add(GetSimpleField(nameof(cache.FatherFamilyname), nameof(application.FatherFamilyname), 2, functionName: "ToUpper"));

            result.Fields.Add(GetSimpleField(nameof(cache.BirthDate), nameof(application.BirthDate),dateType: "DateTime"));
            result.Fields.Add(GetSimpleField(nameof(cache.GenderCode), nameof(application.Sex), dateType: "decimal",functionName: "StringToDecimal"));
            result.Fields.Add(GetSimpleField(nameof(cache.BirthCityName), nameof(application.BirthCityId), functionName: "GetBirthCityId"));
            result.Fields.Add(GetSimpleField(nameof(cache.BirthCityName), nameof(application.BirthPlaceOther), functionName: "GetBirthPlaceOther"));
            result.Fields.Add(GetSimpleField(nameof(cache.BirthCountryCode), nameof(application.BirthCountryId), functionName: "GetCountryId"));

         

            if (!string.IsNullOrEmpty(fkNameCitizenships))
            {
                var navNationalityProp = GetSimpleField(nameof(cache.NationalityCode1), citizenNavPropName);
                navNationalityProp.IsNavigation = true;
                navNationalityProp.TransformationFunctionName = "GetAAppCitizenship";
                List<AAppCitizenshipValues> citizenship = new List<AAppCitizenshipValues>();
                var str = citizenship.GetType().FullName.Split(',');
                navNationalityProp.ValueType = $"{str[0]},{ str[1]}]]";
                navNationalityProp.FkName = fkNameCitizenships;
                AAppCitizenshipValues v1 = new AAppCitizenshipValues();
                navNationalityProp.PropEquality = nameof(v1.CountryId);
                result.Fields.Add(navNationalityProp);
            }
            if (!string.IsNullOrEmpty(fkPersAliases))
            {
                var navNationalityProp = GetSimpleField(nameof(cache.NationalityCode1), aliasNavPropName);
                navNationalityProp.IsNavigation = true;
                navNationalityProp.TransformationFunctionName = "GetAAppPersAliases";
                List<PersonAliasesValues> citizenship = new List<PersonAliasesValues>();
                var str = citizenship.GetType().FullName.Split(',');
                navNationalityProp.ValueType = $"{str[0]},{ str[1]}]]";
                navNationalityProp.FkName = fkPersAliases;
                PersonAliasesValues v1 = new PersonAliasesValues();
                navNationalityProp.PropEquality = $"{nameof(v1.Type)},{nameof(v1.Firstname)}";
                result.Fields.Add(navNationalityProp);
            }

            return result;


        }
        public static FieldsForPopulation GetFieldsForPopulationApplicationRelationsDataSearch()
        {
            var result = new FieldsForPopulation();
            var cache = new ERegixCache();
            var application = new AApplication();

            result.Fields = new List<FieldForPopulation>();


           // result.Fields.Add(GetSimpleField(nameof(cache.Firstname), nameof(application.Firstname),2));
           // result.Fields.Add(GetSimpleField(nameof(cache.Surname), nameof(application.Surname),2));
         //   result.Fields.Add(GetSimpleField(nameof(cache.Familyname), nameof(application.Familyname),2));
          //  result.Fields.Add(GetSimpleField(nameof(cache.Egn), nameof(application.Egn),2));
          //  result.Fields.Add(GetSimpleField(nameof(cache.FamilynameLat), nameof(application.FamilynameLat),2));
         //   result.Fields.Add(GetSimpleField(nameof(cache.FirstnameLat), nameof(application.FirstnameLat),2));
          //  result.Fields.Add(GetSimpleField(nameof(cache.SurnameLat), nameof(application.SurnameLat),2));
            result.Fields.Add(GetSimpleField(nameof(cache.MotherSurname), nameof(application.MotherSurname), functionName: "ToUpper"));
            result.Fields.Add(GetSimpleField(nameof(cache.MotherFamilyname), nameof(application.MotherFamilyname), functionName: "ToUpper"));
            result.Fields.Add(GetSimpleField(nameof(cache.MotherFirstname), nameof(application.MotherFirstname), functionName: "ToUpper"));
            result.Fields.Add(GetSimpleField(nameof(cache.FatherFirstname), nameof(application.FatherFirstname), functionName: "ToUpper"));
            result.Fields.Add(GetSimpleField(nameof(cache.FatherSurname), nameof(application.FatherSurname), functionName: "ToUpper"));
            result.Fields.Add(GetSimpleField(nameof(cache.FatherFamilyname), nameof(application.FatherFamilyname), functionName: "ToUpper"));

            
            return result;


        }

        private static FieldForPopulation GetSimpleField(string sourceField, string destinationField, int priority =1, string functionName = "Identity", string dateType = "String")
        {
            FieldForPopulation fName = new FieldForPopulation();
            fName.Priority = priority;          
            fName.SourceFieldName = sourceField;
            fName.FieldName = destinationField;
            fName.TransformationFunctionName = functionName;
            fName.IsValueSet = false;
            fName.IsNavigation = false;
            fName.ValueType = dateType;
            return fName;
        }
    }
}
