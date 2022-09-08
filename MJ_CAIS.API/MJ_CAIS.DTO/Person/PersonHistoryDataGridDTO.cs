namespace MJ_CAIS.DTO.Person
{
    public class PersonHistoryDataGridDTO : BaseGridDTO
    {
        public string? Name { get; set; }     
        public string? NameLat { get; set; }
        public string? Sex { get; set; }
        public DateTime? BirthDate { get; set; }   
        public string? BirthPlaceOther { get; set; }     
        public string? MotherName { get; set; }
        public string? FatherName { get; set; }  
        public string? BirthCity { get; set; }
        public string? BirthCountry { get; set; }
        public IEnumerable<string> Pids { get; set; }
        public IEnumerable<string> CitizenShips { get; set; }
    }
}
