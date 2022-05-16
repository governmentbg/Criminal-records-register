using MJ_CAIS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Repositories
{
    public static class IAuthorityContextExtensions
    {
        public static IQueryable<T> FilterByAuthority<T>(this IUserContext context, IQueryable<T> queryable)
            where T : ICsAuthorityIdFilter
        {
            if (context?.IsGlobalAdmin == true)
            {
                return queryable;
            }
            else if (!string.IsNullOrEmpty(context?.CsAuthorityId))
            {
                return queryable.Where(t => t.CsAuthorityId == context.CsAuthorityId);
            }
            else
            {
                throw new ArgumentException("Argument should have value!", "AuthorityId");
            }
        }

        public static IQueryable<T> FilterByAuthority<T>(this IUserContext context, IQueryable<T> queryable, Expression<Func<T, int>> selector)
        {
            if (context?.IsGlobalAdmin == true)
            {
                return queryable;
            }
            else if (!string.IsNullOrEmpty(context?.CsAuthorityId))
            {
                var selectorBodyExpression = selector.Body;
                var expressionBody = Expression.Equal(selectorBodyExpression, Expression.Constant(context.CsAuthorityId));
                var whereExpression = Expression.Lambda<Func<T, bool>>(expressionBody, selector.Parameters.First());
                return queryable.Where(whereExpression);
            }
            else
            {
                throw new ArgumentException("Argument should have value!", "CsAuthorityId");
            }
        }
    }
}
