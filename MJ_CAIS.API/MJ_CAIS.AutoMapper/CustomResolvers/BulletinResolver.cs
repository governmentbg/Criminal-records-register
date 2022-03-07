using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.AutoMapperContainer.CustomResolvers
{
    public static class BulletinResolver
    {
        public static string GetDecisionDecrition(BDecision src)
        {
            var result = new List<string>();
            if (src == null) return string.Empty;

            if (src.DecisionType != null && !string.IsNullOrEmpty(src.DecisionType.Name))
            {
                result.Add(src.DecisionType.Name);
            }

            if (!string.IsNullOrEmpty(src.DecisionNumber))
            {
                result.Add(src.DecisionNumber);
            }

            if (src.DecisionDate.HasValue)
            {
                result.Add(src.DecisionDate.Value.ToString("dd.MM.yyyy HH:mm"));
            }

            if (src.DecisionFinalDate.HasValue)
            {
                result.Add(src.DecisionFinalDate.Value.ToString("dd.MM.yyyy HH:mm"));
            }

            if (src.DecisionAuth != null && !string.IsNullOrEmpty(src.DecisionAuth.Name))
            {
                result.Add(src.DecisionAuth.Name);
            }

            if (!string.IsNullOrEmpty(src.DecisionEcli))
            {
                result.Add(src.DecisionEcli);
            }

            return string.Join("/", result);
        }
    }
}
