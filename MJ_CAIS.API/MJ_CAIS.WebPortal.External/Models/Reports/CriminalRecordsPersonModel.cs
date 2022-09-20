using MJ_CAIS.DTO.ExternalServicesHost;

namespace MJ_CAIS.WebPortal.External.Models.Reports
{
    public class CriminalRecordsPersonModel
    {
         public string? Id {get;set;}
         public string? Pids {get;set;}
         public string? PersonNames {get;set;}
         public string? MotherNames {get;set;}
         public string? FatherNames {get;set;}
         public string? Sex {get;set;}
         public string? BirthDate {get;set;}
         public string? MatchText { get; set; }
        public string? ActionTemplate { get; set; }
    }
}
