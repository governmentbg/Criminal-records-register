using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNBFileProcessor.Configurations.Elements
{
    public class EventLogSetting //: ConfigurationElement
    {
        private readonly IConfiguration configuration;
        public EventLogSetting(IConfiguration config)
        {
            configuration = config;
        }
        //[ConfigurationProperty("eventLogSource", IsRequired = true)]
        public string EventLogSource
        {

            get; set;
        }

//        [ConfigurationProperty("eventLogName", IsRequired = true)]
        public string EventLogName
        {
            get;set;
            //get
            //{
            //    return (string)this["eventLogName"];
            //}
            //set
            //{
            //    value = (string)this["eventLogName"];
            //}
        }

  //      [ConfigurationProperty("maximumKilobytes", IsRequired = false, DefaultValue = (long)200000)]
        public long MaximumKilobytes
        {
            get;set;
            //{
            //    //return (long)this["maximumKilobytes"];
            //}
            //set
            //{
            //   // value = (long)this["maximumKilobytes"];
            //}
        }

    }
}
