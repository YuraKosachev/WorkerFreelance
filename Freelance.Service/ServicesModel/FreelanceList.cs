using System;
using System.Collections.Generic;
using System.Linq;
using Freelance.Service.Interfaces;
using Freelance.Extensions.Interfaces;
using AutoMapper;

namespace Freelance.Service.ServicesModel
{
    public class FreelanceList<TModel, TEntity> : IFreelanceList<TModel>
    {
        private IAppQuery<TEntity> ProviderList { get; set; }
        public FreelanceList(IAppQuery<TEntity> providerList)
        {
            ProviderList = providerList;
        }
        public void Filter()
        {
            throw new NotImplementedException();
        }

        public int ItemCount()
        {
            return ProviderList.CountItem();
        }

        public IEnumerable<TModel> List()
        {
            return ProviderList.Select(item => Mapper.Map<TModel>(item));
        }

        public void SortPage(string property, bool ascending)
        {
            ProviderList.Sort(property, ascending);
        }

        public void TakePage(int current, int size)
        {
            ProviderList.TakePage(current, size);
        }

        public void Filter(string predicate, params object[] values)
        {
            ProviderList.Filter(predicate, values);
        }
        public void FilterAnd(string predicate, params object[] values)
        {
            ProviderList.AndAlsoFilter(predicate, values);
        }

        public void FilterOr(string predicate, params object[] values)
        {
            ProviderList.OrElseFilter(predicate, values);
        }
    }
}
