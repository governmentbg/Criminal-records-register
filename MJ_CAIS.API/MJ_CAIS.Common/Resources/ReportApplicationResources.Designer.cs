//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MJ_CAIS.Common.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ReportApplicationResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ReportApplicationResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MJ_CAIS.Common.Resources.ReportApplicationResources", typeof(ReportApplicationResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Одобрено искане.
        /// </summary>
        public static string approved {
            get {
                return ResourceManager.GetString("approved", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Анулирано.
        /// </summary>
        public static string canceled {
            get {
                return ResourceManager.GetString("canceled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Доставено.
        /// </summary>
        public static string delivered {
            get {
                return ResourceManager.GetString("delivered", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Основание.
        /// </summary>
        public static string lblCancelDesc {
            get {
                return ResourceManager.GetString("lblCancelDesc", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Доставенo искане.
        /// </summary>
        public static string msgDelivered {
            get {
                return ResourceManager.GetString("msgDelivered", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Доставена справка.
        /// </summary>
        public static string msgDeliveredReport {
            get {
                return ResourceManager.GetString("msgDeliveredReport", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ново искане.
        /// </summary>
        public static string statusNew {
            get {
                return ResourceManager.GetString("statusNew", resourceCulture);
            }
        }
    }
}
