namespace MJ_CAIS.AutoMapperContainer.Resolvers
{
    public static class CertificateResolver
    {
        public static string GetFullName(string fistName, string surName, string familyName)
        {
            var result = new List<string>();
            if (!String.IsNullOrEmpty(fistName))
            {
                result.Add(fistName);
            }
            if (!String.IsNullOrEmpty(surName))
            {
                result.Add(surName);
            }
            if (!String.IsNullOrEmpty(familyName))
            {
                result.Add(familyName);
            }
            return string.Join(" ", result);
        }
    }
}