using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TST.Web.Core
{
    public static class HtmlHelper
    {
        public static bool RouteIfIn(this IHtmlHelper helper, string values)
        {
            var currentController =
                (helper.ViewContext.RouteData.Values["controller"] ?? string.Empty).ToString().UnDash();

            string[] spl = values.ToLower().Split(',');

            var hasController = spl.Contains(currentController.ToLower());

            return hasController;
        }

        public static IHtmlContent FriendlyName(this IHtmlHelper helper, string name)
        {
            string f = name.Replace(" ", "-").Replace(",", "-").Replace("(", "-").Replace(")", "-").Replace("/", "-").Replace("--", "-");
            return new HtmlString(f);
        }

        public static bool ExceptRoute(this IHtmlHelper helper)
        {
            var currentController =
                (helper.ViewContext.RouteData.Values["controller"] ?? string.Empty).ToString().UnDash();

            var startwithBof = new string[] { "Account", "Bof", "Performance" };

            return startwithBof.Any(w => currentController.StartsWith(w));
        }

        #region private
        private static string UnDash(this string value)
        {
            return (value ?? string.Empty).Replace("-", string.Empty);
        }
        #endregion  
    }
}
