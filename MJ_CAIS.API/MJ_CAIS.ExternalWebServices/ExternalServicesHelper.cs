using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace MJ_CAIS.ExternalWebServices
{
    public static class ExternalServicesHelper
    {
        public static string GetValidationString(byte[] fileContent)
        {
            return Convert.ToHexString(new SHA1CryptoServiceProvider().ComputeHash(fileContent));
        }

        public static Dictionary<string, string> GetDictionaryMetadata(string certificateId, string validationString)
        {
            return new Dictionary<string, string>
            {
                { "ValidationString", validationString },
                { "CertificateID", certificateId }
            };

        }
    }
}
