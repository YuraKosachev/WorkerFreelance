using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;

namespace Freelance.Extensions
{
    public class FilteringOptions<TModel>

    {

        public Expression<Func<TModel, bool>> Predicate { get; private set; }
        public FilteringOptions(string predicate, params object[] values)
        {
            Predicate = GetExpression(predicate, values);
        }
        public Expression<Func<TModel, bool>> GetExpression(string predicate, object[] values)
        {
            return System.Linq.Dynamic.DynamicExpression.ParseLambda<TModel, bool>(predicate, values);
        }
        public FilteringOptions<TModel> And(Expression<Func<TModel, bool>> predicate)
        {
            Predicate = Predicate.AndAlso(predicate);

            return this;
        }
        public FilteringOptions<TModel> Or(Expression<Func<TModel, bool>> predicate)
        {
            Predicate = Predicate.OrElse(predicate);

            return this;
        }
        public IQueryable<TModel> Filter(IQueryable<TModel> context)
        {
            return context.Where(Predicate);
        }
    }
}
