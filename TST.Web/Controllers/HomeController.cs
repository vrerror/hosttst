using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TST.Core.Common;
using TST.DataAccess.Da;
using TST.Web.Models;

namespace TST.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPerformanceDa performanceDa;
        private readonly ISlideDa slideDa;
        private readonly IClientDa clientDa;
        private readonly IContactDa contactDa;
        private readonly IWelcomeDa welcomeDa;

        public HomeController(ILogger<HomeController> logger, IPerformanceDa performanceDa, ISlideDa slideDa,IClientDa clientDa,
            IContactDa contactDa, IWelcomeDa welcomeDa)
        {
            _logger = logger;
            this.performanceDa = performanceDa;
            this.slideDa = slideDa;
            this.clientDa = clientDa;
            this.contactDa = contactDa;
            this.welcomeDa = welcomeDa;
        }

        public async Task<IActionResult> Index(string id)
        {
            ViewBag.HrefSec = id != null ? id : "";

            ViewBag.Slides = await slideDa.GetWeb();
            
            ViewBag.PerformanceTypes = await performanceDa.GetTypeDdl();
            ViewBag.Performances = await performanceDa.GetWeb();
            ViewBag.Contact = await contactDa.GetWebRes();
            ViewBag.Clients = await clientDa.GetClient();
            ViewBag.Welcome = await welcomeDa.GetWeb();


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SetLanguage(string lang)
        {
            HttpContext.Response.Cookies.Append("Lang", lang, new CookieOptions
            {
                Expires = new DateTimeOffset(DateTime.Now.AddMonths(3))
            });

            return Json(true);
        }
    }
}
