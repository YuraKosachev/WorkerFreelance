using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Freelance.Web.Models;
using WebGrease.Css.Extensions;

namespace Freelance.Web.HtmlHelperExtensions
{
    public static class HelperExtensions
    {
        public static string Link(this UrlHelper helper, string action, IndexState indexState, string sortProperty)
        {
            var indexStateRoutes = new RouteValueDictionary(new
            {
                indexState.SearchString,
                SortProperty = sortProperty,
                SortAscending = !(indexState.SortAscending && indexState.SortProperty == sortProperty),
                SortCategoryId = indexState.SortCategoryId,
                TimeAvailability = indexState.TimeAvailability

            });

            indexState.GetFilters().ForEach(r => indexStateRoutes.Add(r.Key, r.Value));

            return helper.Action(action, indexStateRoutes);
        }

        public static string ActionToPage(this UrlHelper helper, string action, IndexState indexState, int page)
        {
            var indexStateRoutes = new RouteValueDictionary(new
            {
                page,
                indexState.SearchString,
                indexState.SortProperty,
                indexState.SortAscending,
                indexState.TimeAvailability,
                indexState.SortCategoryId
            });

            indexState.GetFilters().ForEach(r => indexStateRoutes.Add(r.Key, r.Value));

            return helper.Action(action, indexStateRoutes);
        }

        
        public static MvcHtmlString ActionLink(this HtmlHelper helper, string linkText, string action, IndexState indexState)
        {
            return helper.ActionLink(linkText, action, indexState, new RouteValueDictionary(), new Dictionary<string, object>());
        }
        public static MvcHtmlString ActionLink(this HtmlHelper helper, string linkText, string action, IndexState indexState, object routeValues)
        {
            return helper.ActionLink(linkText, action, indexState, new RouteValueDictionary(routeValues), new Dictionary<string, object>());
        }

        public static MvcHtmlString ActionLink(this HtmlHelper helper, string linkText, string action, IndexState indexState, object routeValues, object htmlAttributes)
        {
            return helper.ActionLink(linkText, action, indexState, new RouteValueDictionary(routeValues), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString ActionLink(this HtmlHelper helper, string linkText, string action, IndexState indexState, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            var indexStateRoutes = new RouteValueDictionary(new
            {
                indexState.Page,
                indexState.SearchString,
                indexState.SortProperty,
                indexState.SortAscending,
                indexState.TimeAvailability,
                indexState.SortCategoryId
            });

            indexState.GetFilters().ForEach(r => indexStateRoutes.Add(r.Key, r.Value));

            var routeCombined = new RouteValueDictionary(indexStateRoutes.Union(routeValues).ToDictionary(k => k.Key, k => k.Value));

            return helper.ActionLink(linkText, action, routeCombined, htmlAttributes);
        }

        public static string GetSortClass(this HtmlHelper helper, IndexState indexState, string sortProperty, string onAsc = "glyphicon glyphicon-chevron-up", string onDesc = "glyphicon glyphicon-chevron-down", string offDesc = "glyphicon glyphicon-certificate")
        {
            return indexState.SortProperty == sortProperty ? (indexState.SortAscending ? onAsc : onDesc) : offDesc;
        }
        //----------------------------------------------------------
        public static MvcHtmlString ActionLinkWithImage(this HtmlHelper helper, string linkText, string action, string controller,string imageName,object routeValues, object htmlAttributes)
        {
            var builder = new TagBuilder("a");
            builder.ToString(TagRenderMode.StartTag);
            UrlHelper url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            builder.MergeAttribute("href", url.Action(action, controller, routeValues));
            builder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            builder.InnerHtml = String.Format("<span class='{0}'></span>  {1}",imageName,linkText);
            builder.ToString(TagRenderMode.EndTag);
            return MvcHtmlString.Create(builder.ToString());

        }
    }
}