using System;
using System.Linq;
using System.Linq.Dynamic;

namespace Freelance.Extensions
{
    public class SortingOptions
    {
        public string Property { get; set; }
        public bool Ascending { get; set; }
        public SortingOptions(string property, bool ascending)
        {
            Property = property;
            Ascending = ascending;

        }
        public IQueryable<TModel> Sort<TModel>(IQueryable<TModel> source)
        {
            return source.OrderBy(String.Format("{0} {1}", Property, Ascending ? "ASC" : "DESC"));
        }

    }
}

