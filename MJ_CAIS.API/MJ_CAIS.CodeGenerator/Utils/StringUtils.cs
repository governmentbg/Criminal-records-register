namespace MJ_CAIS.CodeGenerator.Utils
{
    public static class StringUtils
    {
        /// <summary>
        /// Sample: JobTestBuild => job-test-build
        /// </summary>
        public static string ConvertToLowerCaseWithDash(string text)
        {
            var parts = GetNameParts(text);
            var result = string.Join('-', parts.Select(x => x.ToLower()));
            return result;
        }

        /// <summary>
        /// Sample: JobTestBuild => jobTestBuild
        /// </summary>
        public static string ConvertToCamelCase(string text)
        {
            var result = char.ToLower(text[0]) + text[1..];
            return result;
        }

        public static string ConvertCSharpPropertyToTypescript(string propertyType)
        {
            if (propertyType.StartsWith("DateTime"))
            {
                return "Date";
            }

            if (propertyType.StartsWith("string"))
            {
                return "string";
            }

            if (Constants.NumericTypes.Any(numType => propertyType.ToLower().StartsWith(numType)))
            {
                return "number";
            }

            throw new Exception($"PropertyType {propertyType} cannot be converted to TypeScript type");
        }

        private static List<string> GetNameParts(string text)
        {
            var indexes = new List<int>();
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                {
                    indexes.Add(i);
                }
            }

            if (indexes.Count <= 1)
            {
                return new List<string> { text };
            }

            indexes.Add(text.Length);
            var parts = new List<string>();
            for (int i = 0; i < indexes.Count - 1; i++)
            {
                var start = indexes[i];
                var end = indexes[i + 1];
                var part = text.Substring(start, end - start);
                parts.Add(part);
            }

            return parts;
        }
    }
}
