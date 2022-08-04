using MJ_CAIS.DTO.Common;
using MJ_CAIS.DTO.Person;

namespace MJ_CAIS.DTO.ReportApplication
{
    public class ReportApplicationDTO : BaseDTO
    {
        public string? RegistrationNumber { get; set; }
        public string? RegistrationNumberDisplay { get; set; }
        public string? Purpose { get; set; }
        public PersonDTO? Person { get; set; }      
        public string? ApplicantName { get; set; }    
        public string? AddrName { get; set; }
        public string? AddrStr { get; set; }
        public string? AddrDistrict { get; set; }
        public string? AddrTown { get; set; }
        public string? AddrState { get; set; }
        public string? AddrPhone { get; set; }
        public string? AddrEmail { get; set; }
        public string? Description { get; set; }
        public string? StatusCode { get; set; }
        public string? StatusName { get; set; }
        public string? CsAuthorityId { get; set; }
        public string? ApplicantDescr { get; set; } 
        public string? ApplicantId { get; set; }
        public string? PurposeId { get; set; }
    }
}
