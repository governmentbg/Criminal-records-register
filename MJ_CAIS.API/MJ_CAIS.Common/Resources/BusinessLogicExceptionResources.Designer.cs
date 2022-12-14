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
    public class BusinessLogicExceptionResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal BusinessLogicExceptionResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MJ_CAIS.Common.Resources.BusinessLogicExceptionResources", typeof(BusinessLogicExceptionResources).Assembly);
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
        ///   Looks up a localized string similar to Бюлетин с идентификатор {0} не съществува..
        /// </summary>
        public static string bulletinDoesNotExist {
            get {
                return ResourceManager.GetString("bulletinDoesNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Уведомление с идентификатор {0} вече е доставена и не може да бъде анулирана..
        /// </summary>
        public static string bulletinEventDoesNotExist {
            get {
                return ResourceManager.GetString("bulletinEventDoesNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Свидетелството не е намерено.
        /// </summary>
        public static string certificateDoesNotExist {
            get {
                return ResourceManager.GetString("certificateDoesNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Документ с идентификатор {0} не съществува..
        /// </summary>
        public static string documentDoesNotExist {
            get {
                return ResourceManager.GetString("documentDoesNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Прикачения файл е празен..
        /// </summary>
        public static string documentIsEmpty {
            get {
                return ResourceManager.GetString("documentIsEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Нямате право да извършите това действие!.
        /// </summary>
        public static string editIsUnauthorized {
            get {
                return ResourceManager.GetString("editIsUnauthorized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Полето {0} е задължително.
        /// </summary>
        public static string fieldIsRequired {
            get {
                return ResourceManager.GetString("fieldIsRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Изтърпяно наказание с идентификатор {0} не съществува..
        /// </summary>
        public static string isinDataDoesNotExist {
            get {
                return ResourceManager.GetString("isinDataDoesNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to За посочените идентификатори съществуват две различни лица в системата..
        /// </summary>
        public static string mgsMoreThenOnePersonWithPids {
            get {
                return ResourceManager.GetString("mgsMoreThenOnePersonWithPids", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Заявката не е чернова и не може да бъде изтрита.
        /// </summary>
        public static string mgsNotAllowedToDeleteRequest {
            get {
                return ResourceManager.GetString("mgsNotAllowedToDeleteRequest", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Заявката не е чернова и не може да бъде актуализирана.
        /// </summary>
        public static string mgsNotAllowedToEditRequest {
            get {
                return ResourceManager.GetString("mgsNotAllowedToEditRequest", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Искане за справка с идентификатор {0} не съществува..
        /// </summary>
        public static string msgAppReportDoesNotExist {
            get {
                return ResourceManager.GetString("msgAppReportDoesNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Невалиден статус {0}.
        /// </summary>
        public static string msgInvalidStatus {
            get {
                return ResourceManager.GetString("msgInvalidStatus", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Маркирани са повече от позволените {0} на брой заявки..
        /// </summary>
        public static string msgMoreThenAllowedMsgIsReaded {
            get {
                return ResourceManager.GetString("msgMoreThenAllowedMsgIsReaded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Нямате права да маркирате необработена заявка, като прочетене.
        /// </summary>
        public static string msgReadIsNotAllowed {
            get {
                return ResourceManager.GetString("msgReadIsNotAllowed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Заявката е била обработена.
        /// </summary>
        public static string msgReplayExist {
            get {
                return ResourceManager.GetString("msgReplayExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Нямате права да обработите заявката.
        /// </summary>
        public static string msgReplayNotAllowed {
            get {
                return ResourceManager.GetString("msgReplayNotAllowed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Справка с идентификатор {0} не съществува..
        /// </summary>
        public static string msgReportDoesNotExist {
            get {
                return ResourceManager.GetString("msgReportDoesNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Справка с идентификатор {0} вече е била анулирана..
        /// </summary>
        public static string msgReportIsCanceled {
            get {
                return ResourceManager.GetString("msgReportIsCanceled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Справка с идентификатор {0} вече е доставена и не може да бъде анулирана..
        /// </summary>
        public static string msgReportIsDelivered {
            get {
                return ResourceManager.GetString("msgReportIsDelivered", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Заявка с идентификатор {0} не съществува..
        /// </summary>
        public static string msgRequestDoesNotExist {
            get {
                return ResourceManager.GetString("msgRequestDoesNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Нямате права да обработвате заявки към друго БС..
        /// </summary>
        public static string msgRequestForDifferentAuth {
            get {
                return ResourceManager.GetString("msgRequestForDifferentAuth", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Заявката не е чернова и не може да бъде изпратена..
        /// </summary>
        public static string msgRequestIsNotDraft {
            get {
                return ResourceManager.GetString("msgRequestIsNotDraft", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Искане за справка с идентификатор {0} вече е било анулирано..
        /// </summary>
        public static string msgАppReportIsCanceled {
            get {
                return ResourceManager.GetString("msgАppReportIsCanceled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Статус {0} не съществува..
        /// </summary>
        public static string statusDoesNotExist {
            get {
                return ResourceManager.GetString("statusDoesNotExist", resourceCulture);
            }
        }
    }
}
