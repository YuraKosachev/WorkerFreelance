using System;
using Freelance.Provider.Interfaces;
using Freelance.Provider.Context;
using System.Data.Entity;
using Freelance.Extensions;
using Freelance.Provider.EntityModels;
using Freelance.FreelanceException;
namespace Freelance.Provider.Providers
{
    public abstract class FreelanceProvider<TModel> : IProvider<TModel>, IDisposable
        where TModel : class, IModel
    {
        protected FreelanceDbContext Context { get; set; }
        public FreelanceProvider()
        {
            Context = new FreelanceDbContext();
        }
        public virtual Guid Create(TModel item)
        {
            item.Id = Guid.NewGuid();
            if (item is IModelContainDateTime)
                (item as IModelContainDateTime).DateOfCreate = DateTime.Now;
            Context.Set<TModel>().Add(item);
            Context.SaveChanges();
            return item.Id;
        }

        public virtual void Delete(Guid id)
        {
            var item = Context.Set<TModel>().Find(id);
            if (item == null)
                throw new ItemNotFoundException("Искомый элемент не найден");
            Context.Set<TModel>().Remove(item);
            Context.SaveChanges();
        }

        public TModel GetItem(Guid id)
        {
            var item = Context.Set<TModel>().Find(id);
            if (item == null)
                throw new ItemNotFoundException("Искомый элемент не найден");
            return item;
        }

        public virtual Extensions.Interfaces.IAppQuery<TModel> GetList()
        {
            return new AppQuery<TModel>(Context.Set<TModel>());
        }

        public virtual void Update(TModel item)
        {
            Context.Entry<TModel>(item).State = EntityState.Modified;
            Context.SaveChanges();
        }


        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
