using MJ_CAIS.DTO.Nomenclature;

namespace MJ_CAIS.DTO.InternalRequest
{
    public class SelectedPersonBulletinGridDTOExtended 
    {
        public List<BaseNomenclatureDTO> Pids { get; set; } = new List<BaseNomenclatureDTO>();
        public List<SelectedPersonBulletinGridDTO> Bulletins { get; set; } = new List<SelectedPersonBulletinGridDTO>();
    }
}
