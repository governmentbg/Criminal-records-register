namespace MJ_CAIS.Common.Helpers
{
    public class StringHelper
    {
        public static string ConvertNameToPascalCase(string name)
        {
            var parts = name.Split("_").Select(x => char.ToUpper(x[0]) + x[1..].ToLower());
            var result = string.Join("", parts);
            return result;
        }
    }
}
