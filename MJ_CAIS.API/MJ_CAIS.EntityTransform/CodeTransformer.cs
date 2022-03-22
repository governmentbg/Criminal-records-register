using MJ_CAIS.CodeGenerator.Utils;
using System.Text;

namespace MJ_CAIS.EntityTransform
{
    public class CodeTransformer
    {
        public static void TransformEntities(string rootPath, List<string> nomenclatures)
        {
            var parameters = new Parameters();
            var entitiesPath = parameters.GetEntitiesPath(rootPath);
            var files = Directory.GetFiles(entitiesPath, "*.cs");
            foreach (var file in files)
            {
                var result = ChangeEntityFileContent(file, nomenclatures);
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

                    break; // Breaking because there are no other changes for now
                }
            }

            if (hasChange)
            {
                var text = string.Join(Environment.NewLine, lines) + Environment.NewLine;
                File.WriteAllText(filePath, text);
            }
        }

        private static (string text, bool hasChange) ChangeEntityFileContent(string path, List<string> nomenclatures)
        {
            var lines = File.ReadAllLines(path).ToList();
            var entityName = Path.GetFileNameWithoutExtension(path);

            var idText = "public string Id { get; set; }";
            var hasId = lines.Any(x => x.Contains(idText) || x.Contains(Constants.BaseEntityName));

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

                    var baseClassText = hasId ? Constants.BaseEntityName : "";
                    var interfaces = nomenclatures.Contains(entityName) ? $", {Constants.NomenclatureInterfaceName}" : "";
                    if (baseClassText == "" && interfaces == "")
                    {
                        continue;
                    }
                    else
                    {
                        lines[i] = $"{line} : {baseClassText}{interfaces}";
                        continue;
                    }
                }

                if (line.Contains(idText))
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
