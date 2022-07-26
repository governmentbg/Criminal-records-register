using MJ_CAIS.CodeGenerator.Utils;
using MJ_CAIS.DataAccess;
using System.Text;

namespace MJ_CAIS.EntityTransform
{
    public class CodeTransformer
    {
        public static void TransformEntities(string rootPath, List<string> nomenclatures, Dictionary<string, string> filterInterfaces)
        {
            var parameters = new Parameters();
            var entitiesPath = parameters.GetEntitiesPath(rootPath);
            var files = Directory.GetFiles(entitiesPath, "*.cs");
            foreach (var file in files)
            {
                var result = ChangeEntityFileContent(file, nomenclatures, filterInterfaces);
                if (result.hasChange)
                {
                    File.WriteAllText(file, result.text);
                }
            }
        }

        public static void TransformDbContext(string rootPath)
        {
            var dbContextPath = Constants.DbContextPath;
            var dbContextName = Constants.DbContextName;
            var filePath = Path.Combine(rootPath, dbContextPath, dbContextName + ".cs");
            var lines = File.ReadAllLines(filePath).ToList();

            bool hasChange = false;
            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                if (line.Contains("protected override void OnConfiguring"))
                {
                    hasChange = true;

                    var linesCount = 9;
                    lines.RemoveRange(i, linesCount);
                    i -= linesCount;

                    //break; // Breaking because there are no other changes for now
                }
                if(line.Contains("modelBuilder.HasDefaultSchema(\"MJ_CAIS\");"))
                {
                    hasChange = true;
                    lines.RemoveAt(i);
                    i -= 1;
                }
            }

            if (hasChange)
            {
                var text = string.Join(Environment.NewLine, lines) + Environment.NewLine;
                File.WriteAllText(filePath, text);
            }
        }

        private static (string text, bool hasChange) ChangeEntityFileContent(string path, List<string> nomenclatures, Dictionary<string, string> filterInterfaces)
        {
            var lines = File.ReadAllLines(path).ToList();
            var entityName = Path.GetFileNameWithoutExtension(path);

            var idText = "public string Id { get; set; }";
            var versionText = "public decimal? Version { get; set; }";

            var hasId = lines.Any(x => x.Contains(idText));
            var hasVersion = lines.Any(x => x.Contains(versionText) || x.Contains(Constants.BaseEntityName));

            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                if (line.Contains("public partial class"))
                {
                    var index = line.IndexOf(":");
                    if (index > 0)
                    {
                        line = line.Substring(0, index).TrimEnd();
                    }

                    var extends = new List<string>();
                    if (hasVersion)
                    {
                        extends.Add(Constants.BaseEntityName);
                    }

                    var hasCustomFilterInterface = filterInterfaces.ContainsKey(entityName);
                    if (hasId && !hasCustomFilterInterface)
                    {
                        extends.Add(nameof(IBaseIdEntity));
                    }

                    if (hasCustomFilterInterface)
                    {
                        extends.Add(filterInterfaces[entityName]);
                    }

                    if (nomenclatures.Contains(entityName))
                    {
                        extends.Add(Constants.NomenclatureInterfaceName);
                    }
              
                    if (extends.Any())
                    {
                        var extendsText = string.Join(", ", extends);
                        lines[i] = $"{line} : {extendsText}";
                    }

                    continue;
                }

                if (line.Contains(versionText))
                {
                    lines.RemoveAt(i);
                    i--;
                }
            }

            string result = string.Join(Environment.NewLine, lines) + Environment.NewLine;
            return (text: result, hasChange: true);
        }
    }
}
