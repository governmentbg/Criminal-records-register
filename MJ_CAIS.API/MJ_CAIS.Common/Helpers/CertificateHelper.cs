using System.Security.Cryptography.X509Certificates;

namespace MJ_CAIS.Common.Helpers
{
    public class CertificateHelper
    {
        public static X509Certificate2 GetX509Certificate2(StoreName storeName, StoreLocation storeLocation, X509FindType findType, object findValue, bool throwExceptionIfNone = true)
        {
            // Try to open the store.
            X509Store certStore = new X509Store(storeName, storeLocation);
            certStore.Open(OpenFlags.ReadOnly);

            X509Certificate2Collection certCollection;
            try
            {
                // Find the certificate that matches the thumbprint.
                certCollection = certStore.Certificates.Find(findType, findValue, validOnly: false);
            }
            catch (Exception ex)
            {
                certStore.Close();
                certStore.Dispose();

                throw;
            }

            if (throwExceptionIfNone)
            {
                // Check to see if our certificate was added to the collection.
                if (certCollection.Count == 0)
                {
                    string message = string.Format("Certificate \"{0}\" not found in {1}, {2}.", findValue, storeName.ToString(), storeLocation.ToString());
                    throw new ArgumentException(message);
                }

                return certCollection[0];
            }

            return certCollection.Count == 0 ? null : certCollection[0];
        }
    }
}
