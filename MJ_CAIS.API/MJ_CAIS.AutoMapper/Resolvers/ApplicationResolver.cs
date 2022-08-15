namespace MJ_CAIS.AutoMapperContainer.Resolvers
{
    public static class ApplicationResolver
    {
        public static string GetConcatIDs(string egn, string lnch, string ln)
        {
            var result = new List<string>();
            if (!String.IsNullOrEmpty(egn))
            {
                result.Add(egn);
            }
            if (!String.IsNullOrEmpty(lnch))
            {
                result.Add(lnch);
            }
            if (!String.IsNullOrEmpty(ln))
            {
                result.Add(ln);
            }
            return string.Join(",", result);
        }
    }
}