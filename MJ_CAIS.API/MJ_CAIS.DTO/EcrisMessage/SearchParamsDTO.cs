using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.DTO.EcrisMessage
{
    public class SearchParamsDTO
    {
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public decimal? Sex { get; set; }
        public DateTime BirthDate { get; set; }
    }
}