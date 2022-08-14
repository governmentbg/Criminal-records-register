using BNBFileProcessor.Configurations.Elements;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNBFileProcessor.Configuration
{
    public class EventListenerSetting //: ConfigurationSection
    {
        private readonly IConfiguration _configuration;
        public EventListenerSetting(IConfiguration config)
        {
            _configuration = config;

        }
        //[ConfigurationProperty("WatcherSettingsList")]
        public WatcherSettingscollection WatcherSettingsList
        {
            get;set;
            //get { return ((WatcherSettingscollection)(base["WatcherSettingsList"])); }
            //set { base["WatcherSettingsList"] = value; }
        }

       //[ConfigurationProperty("EventLogSettings", IsRequired = true)]
        public EventLogSetting EventLogSettings
        { get;set;
            //get
            //{
            //   return (EventLogSetting)this["EventLogSettings"];
            //}
            //set
            //{
            //    value = (EventLogSetting)this["EventLogSettings"];
            //}
        }

       //[ConfigurationProperty("ServiceInstallerSettings", IsRequired = true)]
        public ServiceInstallerSetting ServiceInstallerSettings
        {
            get;set;
            //get
            //{
            //    return (ServiceInstallerSetting)this["ServiceInstallerSettings"];
            //}
            //set
            //{
            //    value = (ServiceInstallerSetting)this["ServiceInstallerSettings"];
            //}
        }

        //public static EventListenerSetting EventListenerSettings
        //{
        //    get
        //    {
        //        return ConfigurationManager.GetSection("EventListenerConfiguration") as EventListenerSetting;
        //    }
        //}

    }
}
