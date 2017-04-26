using System.Linq;
using System.Linq.Dynamic;


namespace Freelance.Extensions
{
    public class PagingOptions
    {
        internal PagingOptions(int current, int size)
        {
            Current = current;
            Size = size;
        }

        public int Current { get; private set; }
        public int Size { get; private set; }
        public int Total { get; private set; }

        public IQueryable<TEntity> TakePage<TEntity>(IQueryable<TEntity> source)
        {
            Total = source.Count();
            return source.Skip((Current - 1) * Size).Take(Size);
        }
    }
}
