using Microsoft.AspNetCore.Mvc;

namespace TST.Web.BofControllers
{
    public class BofContactController : Controller
    {
        private readonly IContactDa contactDa;

        public BofContactController(IContactDa contactDa)
        {
            this.contactDa = contactDa;
        }

        public async Task<IActionResult> Index()
        {
            var data = await contactDa.Get1();

            return View(data);
        }

        public async Task<IActionResult> Update(Contact data)
        {
            data.UpdateBy = User.Identity.Name;
            data.UpdateDate = DateTime.Now;
            
            await contactDa.Update(data);

            return Json(true);
        }

    }
}
