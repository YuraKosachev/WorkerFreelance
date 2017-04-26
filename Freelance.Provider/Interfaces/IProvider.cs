using System;
using Freelance.Extensions.Interfaces;

namespace Freelance.Provider.Interfaces
{
    public interface IProvider<TModel>
         where TModel : class
    {
        IAppQuery<TModel> GetList();
        TModel GetItem(Guid id);
        Guid Create(TModel item);
        void Update(TModel item);
        void Delete(Guid id);

    }
}
