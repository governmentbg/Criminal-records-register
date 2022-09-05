using MJ_CAIS.DTO.Common;

namespace MJ_CAIS.DTO.Person
{
    public class RemovePidDTO
    {
        public string? ExistinPersonId { get; set; }
        public string? PidId { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public string? Fullname { get; set; }
        public string? FirstnameLat { get; set; }
        public string? SurnameLat { get; set; }
        public string? FamilynameLat { get; set; }
        public string? FullnameLat { get; set; }
        public decimal? Sex { get; set; }
        public DateTime? BirthDate { get; set; }
        public MultipleChooseDTO Nationalities { get; set; } = new MultipleChooseDTO();
        public string? MotherFirstname { get; set; }
        public string? MotherSurname { get; set; }
        public string? MotherFamilyname { get; set; }
        public string? MotherFullname { get; set; }
        public string? FatherFirstname { get; set; }
        public string? FatherSurname { get; set; }
        public string? FatherFamilyname { get; set; }
        public string? FatherFullname { get; set; }
        public string? Desc { get; set; }
    }
}
