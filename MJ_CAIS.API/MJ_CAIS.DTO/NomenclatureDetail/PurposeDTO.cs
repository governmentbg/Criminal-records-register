using MJ_CAIS.DTO.Nomenclature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.DTO.NomenclatureDetail
{
    public class PurposeDTO : BaseNomenclatureDTO
    {
        public bool? RequestInfo { get; set; }
        public string? Description { get; set; }
    }
}
