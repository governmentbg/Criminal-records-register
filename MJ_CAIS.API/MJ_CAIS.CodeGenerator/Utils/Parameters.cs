using System.Text;

namespace MJ_CAIS.CodeGenerator.Utils
{
    public class Parameters
    {
        public Parameters()
        {
            this.PkType = Constants.PkType;
            this.EditorType = Constants.EditorType;
        }

        public string EntityName { get; set; }

        public string SingleName { get; set; }

        public string MultipleName { get; set; }

        public string PkType { get; set; }

        public EditorTypeEnum EditorType { get; set; }

        public string GetRouteName()
        {
            var name = this.MultipleName;

            var indexes = new List<int>();
            for (int i = 0; i < name.Length; i++)
            {
                if (char.IsUpper(name[i]))
                {
                    indexes.Add(i);
                }
            }

            indexes.Add(name.Length);

            var parts = new List<string>();
            for (int i = 0; i < indexes.Count - 1; i++)
            {
                var startIndex = indexes[i];
                var endIndex = indexes[i + 1];
                var part = name.Substring(startIndex, endIndex - startIndex);
                parts.Add(part.ToLower());
            }

            var result = string.Join("-", parts);
            return result;
        }

        public string GetDTOName()
        {
            var result = this.SingleName + "DTO";
            return result;
        }

        public string GetGridDTOName()
        {
            var result = this.SingleName + "GridDTO";
            return result;
        }

        public string GetServiceName()
        {
            var result = this.SingleName + "Service";
            return result;
        }

        public string GetServiceInstanceFieldName(string prefix = "")
        {
            var serviceName = this.GetServiceName();
            var firstChar = char.ToLower(serviceName[0]);
            var result = prefix + firstChar + serviceName.Substring(1);
            return result;
        }

        public string GetInterfaceName()
        {
            var result = "I" + this.GetServiceName();
            return result;
        }

        public string GetControllerName()
        {
            var result = this.MultipleName + "Controller";
            return result;
        }

        public string GetRepositoryName()
        {
            var result = this.SingleName + "Repository";
            return result;
        }

        public string GetRepositoryInterfaceName()
        {
            var result = "I" + this.GetRepositoryName();
            return result;
        }

        public string GetRepositoryFieldName(string prefix = "")
        {
            var serviceName = this.GetRepositoryName();
            var firstChar = char.ToLower(serviceName[0]);
            var result = prefix + firstChar + serviceName.Substring(1);
            return result;
        }

        public string GetDbContextFieldName()
        {
            return "_dbContext";
        }

        public string GetDbContextParameter()
        {
            return "dbContext";
        }

        public string ToStringFormattedText(StringBuilder sb)
        {
            var result = sb.ToString();
            if (this.EditorType == EditorTypeEnum.Spaces)
            {
                result = result.Replace("\t", new string(' ', 4));
            }

            return result;
        }
    }
}
