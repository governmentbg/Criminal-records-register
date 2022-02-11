using System.Reflection;
using System.Text;

namespace MJ_CAIS.CodeGenerator.Utils
{
    public class ClassGenerator
    {
        public static void GenerateController(string rootPath, Parameters parameters)
        {
            var sb = new StringBuilder();

            string entityName = parameters.EntityName;
            string multipleName = parameters.MultipleName;
            string pkType = parameters.PkType;
            string routeName = parameters.GetRouteName();
            string dtoName = parameters.GetDTOName();
            string gridDtoName = parameters.GetGridDTOName();
            string interfaceName = parameters.GetInterfaceName();
            string serviceField = parameters.GetServiceInstanceFieldName("_");
            string service = parameters.GetServiceInstanceFieldName();
            string controllerName = parameters.GetControllerName();
            string controllerPath = Path.Combine(rootPath, Constants.ControllersPath, controllerName + ".cs");

            sb.AppendLine("using Microsoft.AspNetCore.Mvc;");
            sb.AppendLine($"using {Constants.DTOPath}.{parameters.SingleName};");
            sb.AppendLine($"using {Constants.EntityNamespace};");
            sb.AppendLine($"using {Constants.InterfacesNamespace};");
            sb.AppendLine($"using {Constants.ControllersNamespace}.Common;");
            sb.AppendLine();
            sb.AppendLine($"namespace {Constants.ControllersNamespace}");
            sb.AppendLine("{");
            sb.AppendLine($"\t[Route(\"{routeName}\")]");

            var genericParameters = new List<string> { dtoName, dtoName, gridDtoName, entityName, pkType };
            var genericParamsText = string.Join(", ", genericParameters);
            sb.AppendLine($"\tpublic class {controllerName} : BaseApiCrudController<{genericParamsText}>");
            sb.AppendLine("\t{");
            sb.AppendLine($"\t\tprivate readonly {interfaceName} {serviceField};");
            sb.AppendLine();
            sb.AppendLine($"\t\tpublic {multipleName}Controller({interfaceName} {service}) : base({service})");
            sb.AppendLine("\t\t{");
            sb.AppendLine($"\t\t\t{serviceField} = {service};");
            sb.AppendLine("\t\t}");
            sb.AppendLine("\t}");
            sb.AppendLine("}");

            var result = parameters.ToStringFormattedText(sb);
            File.WriteAllText(controllerPath, result);
        }

        public static void GenerateDTO(string rootPath, Parameters parameters)
        {
            var sb = new StringBuilder();

            string entityName = parameters.EntityName;
            string singleName = parameters.SingleName;
            string dtoName = parameters.GetDTOName();
            string entityFilePath = Path.Combine(rootPath, Constants.EntityPath, entityName + ".cs");
            string folderPath = Path.Combine(rootPath, Constants.DTOPath, singleName);
            string dtoPath = Path.Combine(folderPath, dtoName + ".cs");
            Directory.CreateDirectory(folderPath);

            sb.AppendLine($"namespace {Constants.DTOPath}.{singleName}");
            sb.AppendLine("{");
            sb.AppendLine($"\tpublic class {dtoName} : BaseDTO");
            sb.AppendLine("\t{");
            
            var properties = GetPropertiesFromFile(entityFilePath, entityName);
            foreach (var property in properties)
            {
                sb.AppendLine($"\t\t{property}");
            }
            sb.AppendLine("\t}");
            sb.AppendLine("}");

            var result = parameters.ToStringFormattedText(sb);
            File.WriteAllText(dtoPath, result);
        }

        public static void GenerateGridDTO(string rootPath, Parameters parameters)
        {
            var sb = new StringBuilder();

            string singleName = parameters.SingleName;
            string gridDtoName = parameters.GetGridDTOName();
            string folderPath = Path.Combine(rootPath, Constants.DTOPath, singleName);
            string dtoPath = Path.Combine(folderPath, gridDtoName + ".cs");
            Directory.CreateDirectory(folderPath);

            sb.AppendLine($"namespace {Constants.DTOPath}.{singleName}");
            sb.AppendLine("{");
            sb.AppendLine($"\tpublic class {gridDtoName} : BaseDTO");
            sb.AppendLine("\t{");
            sb.AppendLine("\t}");
            sb.AppendLine("}");

            var result = parameters.ToStringFormattedText(sb);
            File.WriteAllText(dtoPath, result);
        }

        public static void GenerateInterface(string rootPath, Parameters parameters)
        {
            var sb = new StringBuilder();

            string entityName = parameters.EntityName;
            string pkType = parameters.PkType;
            string dtoName = parameters.GetDTOName();
            string gridDtoName = parameters.GetGridDTOName();
            string interfaceName = parameters.GetInterfaceName();
            string interfacesPath = Path.Combine(rootPath, Constants.InterfacesPath, interfaceName + ".cs");

            sb.AppendLine($"using {Constants.DTOPath}.{parameters.SingleName};");
            sb.AppendLine($"using {Constants.EntityNamespace};");
            sb.AppendLine();
            sb.AppendLine($"namespace {Constants.InterfacesNamespace}");
            sb.AppendLine("{");

            var genericParameters = new List<string> { dtoName, dtoName, gridDtoName, entityName, pkType };
            var genericParamsText = string.Join(", ", genericParameters);
            sb.AppendLine($"\tpublic interface {interfaceName} : IBaseAsyncService<{genericParamsText}>");
            sb.AppendLine("\t{");
            sb.AppendLine("\t}");
            sb.AppendLine("}");

            var result = parameters.ToStringFormattedText(sb);
            File.WriteAllText(interfacesPath, result);
        }

        public static void GenerateService(string rootPath, Parameters parameters)
        {
            var sb = new StringBuilder();

            string pkType = parameters.PkType;
            string entityName = parameters.EntityName;
            string multipleName = parameters.MultipleName;
            string routeName = parameters.GetRouteName();
            string dtoName = parameters.GetDTOName();
            string gridDtoName = parameters.GetGridDTOName();
            string interfaceName = parameters.GetInterfaceName();
            string repositoryInterfaceName = parameters.GetRepositoryInterfaceName();
            string repositoryFieldName = parameters.GetRepositoryFieldName("_");
            string repositoryParameter = parameters.GetRepositoryFieldName();
            string serviceName = parameters.GetServiceName();
            string servicePath = Path.Combine(rootPath, Constants.ServicesPath, serviceName + ".cs");

            sb.AppendLine($"using AutoMapper;");
            sb.AppendLine($"using {Constants.DbContextNamespace};");
            sb.AppendLine($"using {Constants.RepositoryInterfaceNamespace};");
            sb.AppendLine($"using {Constants.DTOPath}.{parameters.SingleName};");
            sb.AppendLine($"using {Constants.EntityNamespace};");
            sb.AppendLine($"using {Constants.InterfacesNamespace};");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine();
            sb.AppendLine($"namespace {Constants.ServicesNamespace}");
            sb.AppendLine("{");

            var genericParameters = new List<string> { dtoName, dtoName, gridDtoName, entityName, pkType, Constants.DbContextName };
            var genericParamsText = string.Join(", ", genericParameters);
            sb.AppendLine($"\tpublic class {serviceName} : BaseAsyncService<{genericParamsText}>, {interfaceName}");
            sb.AppendLine("\t{");
            sb.AppendLine($"\t\tprivate readonly {repositoryInterfaceName} {repositoryFieldName};");
            sb.AppendLine();
            sb.AppendLine($"\t\tpublic {serviceName}(IMapper mapper, {repositoryInterfaceName} {repositoryParameter})");
            sb.AppendLine($"\t\t\t: base(mapper, {repositoryParameter})");
            sb.AppendLine("\t\t{");
            sb.AppendLine($"\t\t\t{repositoryFieldName} = {repositoryParameter};");
            sb.AppendLine("\t\t}");
            sb.AppendLine();
            sb.AppendLine($"\t\tprotected override bool IsChildRecord({pkType} aId, List<string> aParentsList)");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\treturn false;");
            sb.AppendLine("\t\t}");
            sb.AppendLine("\t}");
            sb.AppendLine("}");

            var result = parameters.ToStringFormattedText(sb);
            File.WriteAllText(servicePath, result);
        }

        public static void GenerateRepositoryInterface(string rootPath, Parameters parameters)
        {
            var sb = new StringBuilder();

            string pkType = parameters.PkType;
            string entityName = parameters.EntityName;
            string repositoryInterfaceName = parameters.GetRepositoryInterfaceName();
            string repositoryInterfacePath = Path.Combine(rootPath, Constants.RepositoryInterfacePath, repositoryInterfaceName + ".cs");

            sb.AppendLine($"using {Constants.DbContextNamespace};");
            if (Constants.EntityNamespace != Constants.DbContextNamespace)
            {
                sb.AppendLine($"using {Constants.EntityNamespace};");
            }
            sb.AppendLine();
            sb.AppendLine($"namespace {Constants.RepositoryInterfaceNamespace}");
            sb.AppendLine("{");

            var genericParameters = new List<string> { entityName, pkType, Constants.DbContextName };
            var genericParamsText = string.Join(", ", genericParameters);
            sb.AppendLine($"\tpublic interface {repositoryInterfaceName} : IBaseAsyncRepository<{genericParamsText}>");
            sb.AppendLine("\t{");
            sb.AppendLine("\t}");
            sb.AppendLine("}");

            var result = parameters.ToStringFormattedText(sb);
            File.WriteAllText(repositoryInterfacePath, result);
        }

        public static void GenerateRepository(string rootPath, Parameters parameters)
        {
            var sb = new StringBuilder();

            string entityName = parameters.EntityName;
            string multipleName = parameters.MultipleName;
            string repositoryName = parameters.GetRepositoryName();
            string repositoryInterfaceName = parameters.GetRepositoryInterfaceName();
            string dbContextField = parameters.GetDbContextFieldName();
            string dbContextParameter = parameters.GetDbContextParameter();
            string reposiroryPath = Path.Combine(rootPath, Constants.RepositoryPath, repositoryName + ".cs");

            sb.AppendLine($"using {Constants.RepositoryInterfaceNamespace};");
            sb.AppendLine($"using {Constants.DbContextNamespace};");
            sb.AppendLine($"using {Constants.EntityNamespace};");
            sb.AppendLine();
            sb.AppendLine($"namespace {Constants.RepositoryNamespace}");
            sb.AppendLine("{");

            sb.AppendLine($"\tpublic class {repositoryName} : BaseAsyncRepository<{entityName}, {Constants.DbContextName}>, {repositoryInterfaceName}");
            sb.AppendLine("\t{");
            sb.AppendLine($"\t\tpublic {repositoryName}({Constants.DbContextName} {dbContextParameter}) : base({dbContextParameter})");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t}");
            sb.AppendLine("\t}");
            sb.AppendLine("}");

            var result = parameters.ToStringFormattedText(sb);
            File.WriteAllText(reposiroryPath, result);
        }

        public static string GetCurrentProjectPath()
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            var exePath = Directory.GetCurrentDirectory();
            var index = exePath.IndexOf(assemblyName);
            var projectPath = exePath.Substring(0, index);

            return projectPath;
        }

        private static List<string> GetPropertiesFromFile(string path, string entityName, bool removeVirtualProperty = true)
        {
            var result = new List<string>();

            var lines = File.ReadAllLines(path).ToList();
            var classBeginIndex = GetIndexOfLine(lines, $"public partial class {entityName}");
            var constructorIndex = GetIndexOfLine(lines, $"public {entityName}()", classBeginIndex);

            var startIndex = classBeginIndex + 2;
            if (constructorIndex != -1)
            {
                startIndex = GetIndexOfLine(lines, "}", classBeginIndex) + 1;
            }

            var searchEnd = new string(' ', 4) + "}";
            var endIndex = GetIndexOfLine(lines, searchEnd, startIndex);

            var propertyLines = lines.GetRange(startIndex, endIndex - startIndex)
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrEmpty(x))
                .Where(x => !Constants.SystemProperties.Any(prop => x.Contains(prop + " { get; set; }")))
                .ToList();

            if (removeVirtualProperty)
            {
                propertyLines = propertyLines.Where(x => !x.StartsWith("public virtual")).ToList();
            }

            return propertyLines;
        }

        private static int GetIndexOfLine(List<string> lines, string searchPattern, int startIndex = 0)
        {
            for (int i = startIndex; i < lines.Count; i++)
            {
                string line = lines[i];
                if (line.Contains(searchPattern))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
