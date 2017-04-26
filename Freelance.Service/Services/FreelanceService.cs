using System;
using Freelance.Service.Interfaces;
using Freelance.Provider.Interfaces;
using Freelance.Service.ServicesModel;
using AutoMapper;
namespace Freelance.Service.Services
{
    public abstract class FreelanceService<TModelService, TProviderModel> : IService<TModelService>
        where TModelService : class
        where TProviderModel : class

    {
        protected IProvider<TProviderModel> Provider { get; set; }

        public FreelanceService()
        {

        }
        public FreelanceService(IProvider<TProviderModel> provider)
        {
            Provider = provider;

        }

        public virtual Guid Create(TModelService item)
        {
            return Provider.Create(Mapper.Map<TProviderModel>(item));
        }

        public virtual void Delete(Guid id)
        {
            Provider.Delete(id);
        }

        public virtual TModelService GetItem(Guid id)
        {
            return Mapper.Map<TModelService>(Provider.GetItem(id));
        }

        public virtual IFreelanceList<TModelService> GetList()
        {
            return new FreelanceList<TModelService, TProviderModel>(Provider.GetList());
        }

        public virtual void Update(TModelService item)
        {
            Provider.Update(Mapper.Map<TProviderModel>(item));
        }


    }
}
