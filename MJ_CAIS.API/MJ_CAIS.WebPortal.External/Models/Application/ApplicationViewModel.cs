using MJ_CAIS.DTO.Application.External;

namespace MJ_CAIS.WebPortal.External.Models.Application
{
    public class ApplicationViewModel
    {
        public ApplicationViewModel()
        {
            this.Applications = new List<ExternalApplicationGridDTO>().AsQueryable();
        }

        public IQueryable<ExternalApplicationGridDTO> Applications { get; set; }
    }
}
