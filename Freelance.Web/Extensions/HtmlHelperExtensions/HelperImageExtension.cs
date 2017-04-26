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
    public static class HelperImageExtension
    {
        public static MvcHtmlString Image(this HtmlHelper helper, string src, object htmlAttributes)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", src);
            builder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));

        }
        public static MvcHtmlString Image(this HtmlHelper helper, string src)
        {
            return helper.Image(src, null);
        }
        public static MvcHtmlString Image(this HtmlHelper helper, string imageName, string folderPath, string holder, object htmlAttributes)
        {
            if (imageName != null)
            {
                var path = String.Format(@"/{0}/{1}", folderPath, imageName);
                return helper.Image(path, htmlAttributes: htmlAttributes);
            }

            return helper.Image(holder, htmlAttributes: htmlAttributes);
        }
        public static string Holder(int width, int height)
        {
            return String.Format("holder.js/{0}x{1}", width, height);
        }
    }

}