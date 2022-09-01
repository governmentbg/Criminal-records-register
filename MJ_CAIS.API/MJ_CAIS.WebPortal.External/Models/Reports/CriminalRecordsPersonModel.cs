using MJ_CAIS.DTO.ExternalServicesHost;

namespace MJ_CAIS.WebPortal.External.Models.Reports
{
    public class CriminalRecordsPersonModel
    {
        public string FirstNameBg { get; set; }
        public string SurNameBg { get; set; }
        public string FamilyNameBg { get; set; }
        public string FullNameBg { get; set; }
        public string FirstNameEn { get; set; }
        public string SurNameEn { get; set; }
        public string MotherNames { get; set; }
        public string FatherNames { get; set; }
        public string FamilyNameEn { get; set; }
        public string FullNameEn { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public string BirthPlaceDescr { get; set; }
        public DateTime BirthDate { get; set; }
        public DatePrecisionEnum BirthDatePrecision { get; set; }
        public string EGN { get; set; }
        public string SUID { get; set; }
        public string LNCh { get; set; }
        public string LN { get; set; }
        public string ActionTemplate { get; set; }
        public string Remark { get; set; }
        public string LawReason { get; set; }
    }
}
