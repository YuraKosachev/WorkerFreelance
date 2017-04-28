using System.Web.Optimization;

namespace Freelance.Web
{
    public class BundleConfig
    {
        //Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство сборки на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            // datetimepicker's bundles
            bundles.Add(new ScriptBundle("~/bundles/datetimepicker").Include(
                        "~/Scripts/moment.min.js",
                        "~/Scripts/moment-with-locales.min.js",
                        "~/Scripts/bootstrap-datetimepicker.min.js"));

            bundles.Add(new StyleBundle("~/Content/datetimepicker").Include(
                      "~/Content/bootstrap-datetimepicker.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/OfferConfirmed").Include(
                   "~/Scripts/ConfirmOffer.js"));
            bundles.Add(new ScriptBundle("~/bundles/MyScripts").Include(
                      "~/Scripts/offercreate.js",
                      "~/Scripts/index.js",
                      "~/Scripts/holder.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/Croppie").Include(
                    "~/Scripts/croppie.min.js",
                    "~/Scripts/CropppieScript.js"));
            bundles.Add(new ScriptBundle("~/bundles/Exif").Include(
                    "~/Scripts/exif.js"));
            bundles.Add(new StyleBundle("~/Content/Croppie").Include(
                        "~/Content/croppie.css"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                       "~/Content/bootstrap.css",
                       "~/Content/site.css",
                       "~/Content/PagedList.css"));
            bundles.Add(new ScriptBundle("~/bundles/ProfileCreateEdit").Include(
                   "~/Scripts/ProfileCreateEdit.js")); 
                bundles.Add(new ScriptBundle("~/bundles/Profile").Include(
                   "~/Scripts/profileindex.js"));
        }
    }
}
