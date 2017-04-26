using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Freelance.Extensions.Interfaces;
using System.Linq.Expressions;

namespace Freelance.Extensions
{
    public class AppQuery<TModel> : IAppQuery<TModel>
    {
        private IQueryable<TModel> Query { get; set; }
        public PagingOptions Paging { get; set; }
        public SortingOptions Sorting { get; set; }
        public FilteringOptions<TModel> Filtering { get; set; }
        public AppQuery(IQueryable<TModel> query)
        {
            Query = query;
        }
        public int CountItem()
        {
            return Paging.Total;
        }
        public IAppQuery<TModel> TakePage(int current, int size)
        {
            Paging = new PagingOptions(current, size);
            return this;
        }

        public IAppQuery<TModel> Sort(string property, bool ascending)
        {
            Sorting = new SortingOptions(property, ascending);
            return this;
        }

        public IAppQuery<TModel> Filter(string predicate, object[] values)
        {
            Filtering = new FilteringOptions<TModel>(predicate, values);
            return this;
        }
        public IAppQuery<TModel> AndAlsoFilter(string predicate, object[] values)
        {
            if (Filtering == null)
            {
                return Filter(predicate, values);
            }
            Filtering.And(Filtering.GetExpression(predicate, values));

            return this;
        }

        public IAppQuery<TModel> OrElseFilter(string predicate, object[] values)
        {
            if (Filtering == null)
            {
                return Filter(predicate, values);
            }
            Filtering.Predicate.OrElse(Filtering.GetExpression(predicate, values));
            return this;
        }


        public IEnumerator<TModel> GetEnumerator()
        {
            return Query.Filter(Filtering).Sort(Sorting).TakePage(Paging).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }



    }
}
