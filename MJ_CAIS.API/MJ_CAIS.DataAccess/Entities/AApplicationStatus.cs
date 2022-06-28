namespace MJ_CAIS.DataAccess.Entities
{
    public partial class AApplicationStatus //: BaseEntity, IBaseIdEntity
    {
        public AApplicationStatus()
        {
            AApplications = new HashSet<AApplication>();
            ACertificates = new HashSet<ACertificate>();
            AStatusHes = new HashSet<AStatusH>();
        }

        public string Code { get; set; } = null!;
        public string? Name { get; set; }
        public string? StatusType { get; set; }
        public string? Descr { get; set; }

        public virtual ICollection<AApplication> AApplications { get; set; }
        public virtual ICollection<ACertificate> ACertificates { get; set; }
        public virtual ICollection<AStatusH> AStatusHes { get; set; }
        // public string? Id { get; set; }
    }
}
