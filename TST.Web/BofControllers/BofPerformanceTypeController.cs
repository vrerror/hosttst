using Microsoft.AspNetCore.Mvc;
using TST.Core.Models;

namespace TST.Web.BofControllers
{
    public class BofPerformanceTypeController : Controller
    {
        private readonly IPerformanceType performanceTypeDa;

        public BofPerformanceTypeController(IPerformanceType PerformanceTypeDa)
        {
            performanceTypeDa = PerformanceTypeDa;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetById(int id)
        {
            var data = await performanceTypeDa.GetById(id);

            return Json(data);
        }

        public async Task<IActionResult> GetDt(GetPerformanceTypeDtReq req)
        {
            var data = await performanceTypeDa.GetDt(req);

            return Json(data);
        }

        public async Task<IActionResult> Save(PerformanceType data)
        {
            BaseRes res = new();
            try
            {
                if (data.Id == 0)
                {
                    data.CreateBy = User.Identity.Name;
                    data.CreateDate = DateTime.Now;

                    await performanceTypeDa.Insert(data);
                }
                else
                {
                    data.UpdateBy = User.Identity.Name;
                    data.UpdateDate = DateTime.Now;

                    await performanceTypeDa.Update(data);
                }

                res.Success = true;
            }
            catch (ArgumentException ae)
            {
                res.Message = ae.Message;
            }
            catch (Exception e)
            {
                res.Message = $"ERROR: {e.Message}";
            }
            return Json(res);

        }

        public async Task<IActionResult> Delete(int id)
        {
            await performanceTypeDa.Delete(id, User.Identity.Name);

            return Json(true);
        }

        public async Task<IActionResult> GetNextRanking()
        {
            var data = await performanceTypeDa.GetNextRanking();

            return Json(data);
        }

    }
}
