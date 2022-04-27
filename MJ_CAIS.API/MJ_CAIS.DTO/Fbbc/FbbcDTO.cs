using MJ_CAIS.DTO.Common;

namespace MJ_CAIS.DTO.Fbbc
{
    public class FbbcDTO : BaseDTO
    {
        public LookupDTO CountryLookup { get; set; } = new LookupDTO();
        public string? DocTypeId { get; set; }
        public string? SanctionTypeId { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public DateTime? IssueDate { get; set; }
        public string? CountryDescr { get; set; }
        public string? Egn { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public string? BirthPlace { get; set; }
        public string? BirthCityId { get; set; }
        public string? BirtyCountryDescr { get; set; }
        public string? BirthCountryId { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthDatePrec { get; set; }
        public DateTime? OffenceStartDate { get; set; }
        public DateTime? OffenceEndDate { get; set; }
        public string? Annotation { get; set; }
        public string? MotherFirstname { get; set; }
        public string? MotherSurname { get; set; }
        public string? MotherFamilyname { get; set; }
        public string? FatherFirstname { get; set; }
        public string? FatherSurname { get; set; }
        public string? FatherFamilyname { get; set; }
        public string? GdkpNumber { get; set; }
        public string? GdkpDate { get; set; }
        public string? GdkpCaseNumber { get; set; }
        public string? GdkpTom { get; set; }
        public string? GdkpStr { get; set; }
        public string? NjrCountry { get; set; }
        public string? NjrIdentifier { get; set; }
        public string? NjrFirstId { get; set; }
        public string? EcrisMsgId { get; set; }
        public string? EcrisConvId { get; set; }
        public string? EcrisUpdConvTypeId { get; set; }
        public string? EcrisUpdConvId { get; set; }
        public bool? IsAdministrative { get; set; }
        public DateTime? ConvDecisionDate { get; set; }
        public DateTime? ConvDecFinalDate { get; set; }
        public decimal? SequentialIndex { get; set; }
        public DateTime? DestroyedDate { get; set; }
        public string? PersonId { get; set; }
        public decimal? Version { get; set; }
        public AddressDTO Address { get; set; } = new AddressDTO();
        public string? StatusCode { get; set; }
        public decimal? OldId { get; set; }
    }
}
