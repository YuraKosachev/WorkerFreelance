
using System.Collections.Generic;

namespace Freelance.Extensions.Interfaces
{
    public interface IAppQuery<TModel> : IEnumerable<TModel>
    {
        IAppQuery<TModel> TakePage(int current, int size);
        IAppQuery<TModel> Sort(string property, bool ascending);
        IAppQuery<TModel> Filter(string predicate, params object[] values);
        IAppQuery<TModel> AndAlsoFilter(string predicate, params object[] values);
        IAppQuery<TModel> OrElseFilter(string predicate, params object[] values);

        int CountItem();
    }
}
