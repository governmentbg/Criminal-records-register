using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNBFileProcessor.Configurations.Elements
{
    public class WatcherSettingsWrapper
    {
        public WatcherSetting WatcherSettings { get; set; }
    }


    public class WatcherSetting //: ConfigurationElement
    {
        
        // [ConfigurationProperty("key", IsRequired = true)]
        public string WatcherKey
        {
            get; set;
            //get
            //{
            //    return (string)this["key"];
            //}
            //set
            //{
            //    value = (string)this["key"];
            //}
        }

       // [ConfigurationProperty("sourcePath", IsRequired = true)]
        public string SourcePath
        {
            get; set;
            ////get
            ////{
            ////    //return (string)this["sourcePath"];
            ////}
            ////set
            ////{
            ////    //value = (string)this["sourcePath"];
            ////}
        }

        //[ConfigurationProperty("compleatePath", IsRequired = false)]
        public string CompleatePath
        {
            get; set;
            //get
            //{
            //    //if (string.IsNullOrEmpty((string)this["compleatePath"]))
            //    //{
            //    //    var result = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "Compleated");
            //    //    return result;
            //    //}

            //    //return (string)this["compleatePath"];
            //}
            //set
            //{
            //   // value = (string)this["compleatePath"];
            //}
        }

        //[ConfigurationProperty("processingPath", IsRequired = false)]
        public string ProcessingPath
        { get; set; }
            //get
            //{
                //if (string.IsNullOrEmpty((string)this["processingPath"]))
                //{
                //    var result = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "ProcessingCases");
                //    return result;
                //}

                //return (string)this["processingPath"];
        //    }
        //    set
        //    {
        //        //value = (string)this["processingPath"];
        //    }
        //}

       // [ConfigurationProperty("errorPath", IsRequired = false)]
        public string ErrorPath
        {
            get; set;
            //get
            //{
                //if (string.IsNullOrEmpty((string)this["errorPath"]))
                //{
                //    var result = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "Error");
                //    return result;
                //}
            //    //return (string)this["errorPath"];
            //}
            //set
            //{
                //value = (string)this["errorPath"];
            //}
        }

       public int ElapsedTimeInSeconds { get; set; }
       //[ConfigurationProperty("elapsedTimeInSeconds", IsRequired = true, DefaultValue = 30)]
        public int ElapsedTime
        {
            get
            {
                //var elapsedTime = (int)this["elapsedTimeInSeconds"];
                return MiliSecondsToSeconds(ElapsedTimeInSeconds);
            }
            set
            {
                // var elapsedTime = (int)this["elapsedTimeInSeconds"];
                // value = elapsedTime;
                ElapsedTimeInSeconds = value;
            }
        }

        //[ConfigurationProperty("fileFilter", IsRequired = true, DefaultValue = "*.json")]
        public string FileFilter
        {
            //get;
            // {
            //      return (string)this["fileFilter"];
            // }
            //set
            //{
            //    value = (string)this["fileFilter"];
            //}
            get; set;
        }


      //[ConfigurationProperty("deleteCompletedFiles", IsRequired = false, DefaultValue = false)]
        public bool DeleteCompletedFiles
        {
            get; set;
            //get
            //{
            //   // return (bool)this["deleteCompletedFiles"];
            //}
            //set
            //{
            //   // value = (bool)this["deleteCompletedFiles"];
            //}
        }

       //[ConfigurationProperty("maxFileProcess", IsRequired = true, DefaultValue = 50)]
        public int MaxFileProcess
        {
            get; set;
            //get
            //{
            //   // return (int)this["maxFileProcess"];
            //}
            //set
            //{
            //   // value = (int)this["elapsedTimeInSeconds"];
            //}
        }

        public int MiliSecondsToSeconds(int miliseconds)
        {
            return miliseconds * 1000;
        }
    }

   // [ConfigurationCollection(typeof(WatcherSetting))]
    public class WatcherSettingscollection //: ConfigurationElementCollection
    {
        private readonly IConfiguration configuration;
        public WatcherSettingscollection(IConfiguration config)
        {
            configuration = config;
            Watchers = new List<WatcherSetting>();
        }
        internal const string PropertyName = "WatcherSettings";

        public  ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMapAlternate;
            }
        }
        protected  string ElementName
        {
            get
            {
                return PropertyName;
            }
        }

        protected  bool IsElementName(string elementName)
        {
            return elementName.Equals(PropertyName,
              StringComparison.InvariantCultureIgnoreCase);
        }

        public  bool IsReadOnly()
        {
            return false;
        }

        protected WatcherSetting CreateNewElement()
        {
            return new WatcherSetting();
        }

        protected  object GetElementKey(WatcherSetting element)
        {
            return ((WatcherSetting)(element)).WatcherKey;
        }
        public List<WatcherSetting> Watchers { get; set; }
        public WatcherSetting this[int idx]
        {
            get { return (WatcherSetting)Watchers.ElementAt(idx); }
            set { Watchers.Insert(idx,  value); }
        }
    }

}
