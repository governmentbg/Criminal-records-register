using MJ_CAIS.DTO.Application.Public;

namespace MJ_CAIS.WebPortal.Public.Models.Application
{
    public class ApplicationViewModel
    {
        public ApplicationViewModel()
        {
            this.Applications = new List<PublicApplicationGridDTO>().AsQueryable();
        }

        public IQueryable<PublicApplicationGridDTO> Applications { get; set; }
    }
}
