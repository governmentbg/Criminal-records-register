namespace MJ_CAIS.CodeGenerator.Utils
{
    public static class Constants
    {
        public const string DbContextName = "CaisDbContext";

        public const string WebPath = "MJ_CAIS.Web";
        public const string ServicesPath = "MJ_CAIS.Services";
        public const string InterfacesPath = "MJ_CAIS.Services.Contracts";
        public const string RepositoriesPath = "MJ_CAIS.Repositories";
        public const string EntitiesPath = "MJ_CAIS.DataAccess";
        public const string DTOPath = "MJ_CAIS.DTO";

        public const string PkType = "string";
        public const EditorTypeEnum EditorType = EditorTypeEnum.Spaces;

        public static readonly string ControllersNamespace = $"{WebPath}.Controllers";
        public static readonly string InterfacesNamespace = $"{InterfacesPath}";
        public static readonly string ServicesNamespace = $"{ServicesPath}";
        public static readonly string DTONamespace = $"{DTOPath}";
        public static readonly string DbContextNamespace = $"MJ_CAIS.DataAccess";
        public static readonly string RepositoryNamespace = $"{RepositoriesPath}.Impl";
        public static readonly string RepositoryInterfaceNamespace = $"{RepositoriesPath}.Contracts";
        public static readonly string EntityNamespace = "MJ_CAIS.DataAccess.Entities";

        public static readonly string EntityPath = @$"{EntitiesPath}\Entities";
        public static readonly string ControllersPath = @$"{WebPath}\Controllers";
        public static readonly string RepositoryPath = @$"{RepositoriesPath}\Impl";
        public static readonly string RepositoryInterfacePath = @$"{RepositoriesPath}\Contracts";

        public static readonly List<string> SystemProperties = new List<string>()
        {
            "CreatedOn", "CreatedBy", "UpdatedOn", "UpdatedBy"
        };
    }
}
