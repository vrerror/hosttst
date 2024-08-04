using Microsoft.AspNetCore.Mvc;

namespace TST.Web.Controllers
{
    public class PerformanceController : Controller
    {
        private readonly IPerformanceDa performanceDa;
        private readonly IContactDa contactDa;

        public PerformanceController(IPerformanceDa performanceDa, IContactDa contactDa)
        {
            this.performanceDa = performanceDa;
            this.contactDa = contactDa;
        }

        [Route("[controller]/{name}/{id}")]
        public async Task<IActionResult> Index(string name, int id)
        {
            ViewBag.Data = await performanceDa.GetDetail(id);
            ViewBag.Contact = await contactDa.GetWebRes();

            return View();
        }
    }
}
