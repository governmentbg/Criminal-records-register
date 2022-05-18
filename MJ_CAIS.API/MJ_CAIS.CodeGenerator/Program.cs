using MJ_CAIS.CodeGenerator.Utils;

namespace MJ_CAIS.CodeGenerator
{
    /// <summary>
    /// This program generates .NET 6 compatible objects
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var parameters = new Parameters()
            {
                EntityName = "ACertificate",
                SingleName = "Certificate",
                MultipleName = "Certificates",
                AngularModuleName = "certificate",
            };

            var projectPath = ClassGenerator.GetCurrentProjectPath();
            ClassGenerator.GenerateDTO(projectPath, parameters);
            ClassGenerator.GenerateGridDTO(projectPath, parameters);
            ClassGenerator.GenerateRepositoryInterface(projectPath, parameters);
            ClassGenerator.GenerateRepository(projectPath, parameters);
            ClassGenerator.GenerateInterface(projectPath, parameters);
            ClassGenerator.GenerateService(projectPath, parameters);
            ClassGenerator.GenerateController(projectPath, parameters);

            //ClassGenerator.GenerateAngularFormControlModel(projectPath, parameters);
            //ClassGenerator.GenerateAngularFormModel(projectPath, parameters);
            //ClassGenerator.GenerateAngularService(projectPath, parameters);
        }
    }
}