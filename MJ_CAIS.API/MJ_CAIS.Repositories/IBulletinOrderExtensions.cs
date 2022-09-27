using MJ_CAIS.DataAccess;

namespace MJ_CAIS.Repositories
{
    public static class IBulletinOrderExtensions
    {
        public static IQueryable<T> OrderBulletins<T>(this IQueryable<T> queryable)
        where T : IBulletinOrder
        {
            //order by b.decision_final_date, b.decision_date, b.case_year, b.created_on, b.updated_on, b.id
            return queryable
            .OrderBy(b => b.DecisionFinalDate)
            .OrderBy(b => b.DecisionDate)
            .OrderBy(b => b.CaseYear)
            .OrderBy(b => b.CreatedOn.HasValue ? b.CreatedOn.Value.Date : DateTime.Now)
            .OrderBy(b => b.UpdatedOn.HasValue ? b.UpdatedOn.Value.Date : DateTime.Now)
            .OrderBy(b => b.Id);
        }
    }
}
