using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.DTO.Fbbc
{
    public class FbbcDocumentDTO
    {//depricaeted
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Descr { get; set; }
        public string? DocTypeId { get; set; }
        public string? DocTypeName { get; set; }
        public byte[]? DocumentContent { get; set; }
        public string? DocumentContentId { get; set; }
        public string? MimeType { get; set; }
    }
}
