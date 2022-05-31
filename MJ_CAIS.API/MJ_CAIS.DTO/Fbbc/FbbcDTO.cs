using MJ_CAIS.DTO.Common;
using MJ_CAIS.DTO.Person;

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
        public PersonDTO  Person { get; set; }      
        public DateTime? OffenceStartDate { get; set; }
        public DateTime? OffenceEndDate { get; set; }
        public string? Annotation { get; set; }     
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
        public string? StatusCode { get; set; }
        public decimal? OldId { get; set; }
    }
}
