using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.ComponentModel.Composition;
using System.IO;
using TechnoLogica.Authentication.Common;

namespace MJ_CAIS.IdentityServer
{
    [Export(typeof(IDataProtectionKeyStoreProvider))]
    public class FileSystemKeyPersistance : IDataProtectionKeyStoreProvider
    {
        public FileSystemKeyPersistance()
        {

        }
        public IDataProtectionBuilder AddPersistance(IDataProtectionBuilder builder, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            return builder.PersistKeysToFileSystem(new DirectoryInfo("."));
        }
    }
}
