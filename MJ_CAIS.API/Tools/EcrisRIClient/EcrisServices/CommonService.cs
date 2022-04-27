using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EcrisService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcrisIntegrationServices
{
    public class CommonService
    {
        public static string GetDocTypeCode(EcrisMessageTypeOrAliasMessageType rEQ, CaisDbContext dbContext)
        {
            var code = dbContext.DDocTypes.FirstOrDefault(d => d.Code == rEQ.ToString());
            if (code == null)
            {
                throw new Exception($"D_DOC_TYPES does not contain record with code {rEQ.ToString()}");
            }
            return code.Id;
        }

        public static DDocument GetDDocument(MJ_CAIS.DTO.EcrisService.EcrisMessageType t, string name, string firstName, string surName, string familyName, CaisDbContext dbContext)
        {
            DDocument d = new DDocument();
            d.Id = BaseEntity.GenerateNewId();
            var docType = dbContext.DDocTypes.FirstOrDefault(dt => dt.Code != null && dt.Code.ToLower() == t.ToString().ToLower());
            d.DocTypeId = docType?.Id;
            d.Name = name;

            //todo: add resources
            if (t == MJ_CAIS.DTO.EcrisService.EcrisMessageType.REQ)
            {
                d.Descr = "Запитване за " + firstName + " " + surName + " " + familyName;
            }
            if (t == MJ_CAIS.DTO.EcrisService.EcrisMessageType.NOT)
            {
                d.Descr = "Нотификация за " + firstName + " " + surName + " " + familyName;
            }
            if (t == MJ_CAIS.DTO.EcrisService.EcrisMessageType.RRS)
            {
                d.Descr = "Отговор на запитване  за " + firstName + " " + surName + " " + familyName;
            }
            return d;
        }
        public static DDocContent GetDDocContent(string contentXML)
        {
            DDocContent content = new DDocContent();
            content.Id = BaseEntity.GenerateNewId();
            content.MimeType = "application/xml";
            content.Content = Encoding.UTF8.GetBytes(contentXML);
            content.Bytes = content.Content.Length;
            //byte[] hashBytes = (new MD5CryptoServiceProvider()).ComputeHash(content.Content);
            // content.Md5Hash = Convert.ToHexString(hashBytes);
            // content.Sha1Hash = Convert.ToHexString(new SHA1CryptoServiceProvider().ComputeHash(content.Content));
            return content;
        }

        public static async Task<string> GetPersonIDForEcrisMessages(string ecrisMsgID, CaisDbContext dbContext)
        {
            return (await dbContext.EEcrisIdentifications.FirstOrDefaultAsync(p => p.EcrisMsgId == ecrisMsgID && p.Approved == 1))?.PersonId;

        }
        public static YesNoUnknownStringEnumerationType GetYesNoType(bool? value)
        {
            if (value == true)
            {
                YesNoUnknownStringEnumerationType res = new YesNoUnknownStringEnumerationType();
                res.Value = "yes";
                return res;
            }
            if (value == false)
            {
                YesNoUnknownStringEnumerationType res = new YesNoUnknownStringEnumerationType();
                res.Value = "no";
                return res;
            }
            return null;
        }
    }
}
