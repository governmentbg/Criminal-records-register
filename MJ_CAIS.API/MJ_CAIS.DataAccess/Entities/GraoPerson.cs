namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GraoPerson : BaseEntity
    {
        public GraoPerson()
        {
            EEcrisIdentifications = new HashSet<EEcrisIdentification>();
        }

        public string? Egn { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthplaceCode { get; set; }
        public decimal? Sex { get; set; }
        public string? MothersNames { get; set; }
        public string? FathersNames { get; set; }
        public string? BirthplaceText { get; set; }

        public virtual ICollection<EEcrisIdentification> EEcrisIdentifications { get; set; }
    }
}
