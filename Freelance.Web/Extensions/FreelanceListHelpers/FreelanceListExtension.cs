using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Freelance.Service.Interfaces;
using Freelance.Web.Models;
using PagedList;
using AutoMapper;
using Freelance.Web.Properties;

namespace Freelance.Web.Extensions
{
    public static class FreelanceListExtension
    {
        //TakePage helper
        public static IFreelanceList<TModel> Page<TModel>(this IFreelanceList<TModel> freelanceList, IndexState state)
        {
            if (state.Page == null)
                state.Page = 1;
            freelanceList.TakePage((int)state.Page, Settings.Default.CountItemInPage);

            return freelanceList;
        }
        //Sorting helper
        public static IFreelanceList<TModel> Sort<TModel>(this IFreelanceList<TModel> freelanceList, IndexState state, string defaultName, bool asceding = true)
        {
            if (string.IsNullOrEmpty(state.SortProperty))
                freelanceList.SortPage(defaultName, asceding);
            else
                freelanceList.SortPage(state.SortProperty, state.SortAscending);

            return freelanceList;
        }
        //List helper
        public static StaticPagedList<TViewModel> StaticList<TViewModel, TModel>(this IFreelanceList<TModel> freelanceList, IndexState state)
        {
            var list = freelanceList.List().Select(model => Mapper.Map<TViewModel>(model)).ToList();

            return new StaticPagedList<TViewModel>(list, (int)state.Page, Settings.Default.CountItemInPage, freelanceList.ItemCount());

        }
    }
}