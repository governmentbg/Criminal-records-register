using MJ_CAIS.DTO.Certificate;

namespace MJ_CAIS.WebPortal.External.Models.Certificates
{
    public class CertificateViewModel
    {
        public CertificateViewModel()
        {
            this.Certificates = new List<CertificateExternalDTO>().AsQueryable();
        }
        public IQueryable<CertificateExternalDTO> Certificates { get; set; }
    }

}
