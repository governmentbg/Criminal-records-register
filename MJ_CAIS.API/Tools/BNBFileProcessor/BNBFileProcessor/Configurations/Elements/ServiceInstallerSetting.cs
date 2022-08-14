using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNBFileProcessor.Configurations.Elements
{
    public class ServiceInstallerSetting //: ConfigurationElement
    {
        private readonly IConfiguration _configuration;
        public ServiceInstallerSetting(IConfiguration config)
        {
            _configuration = config;
        }
        // [ConfigurationProperty("description", IsRequired = true)]
        public string Description
        {
            get; set;
            //get
            //{
            //    return (string)this["description"];
            //}
            //set
            //{
            //    value = (string)this["description"];
            //}
        }

       // [ConfigurationProperty("displayName", IsRequired = true)]
        public string DisplayName
        {
            get; set;
            //get
            //{
            //    return (string)this["displayName"];
            //}
            //set
            //{
            //    value = (string)this["displayName"];
            //}
        }

      //  [ConfigurationProperty("serviceName", IsRequired = true)]
        public string ServiceName
        {
            get; set;
            //get
            //{
            //    return (string)this["serviceName"];
            //}
            //set
            //{
            //    value = (string)this["serviceName"];
            //}
        }
    }
}
