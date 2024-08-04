using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TST.Core.Common;
using TST.Core.Models;
using TST.Web.Core;

namespace TST.Web.BofControllers
{
    [Authorize]

    public class BofSlideController : Controller
    {
        private readonly ISlideDa slideDa;
        private readonly FileHelper fileHelper;

        public BofSlideController(ISlideDa slideDa, FileHelper fileHelper)
        {
            this.slideDa = slideDa;
            this.fileHelper = fileHelper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetById(int id)
        {
            var data = await slideDa.GetById(id);

            return Json(data);
        }

        public async Task<IActionResult> GetDt(GetSlideDtReq req)
        {
            var data = await slideDa.GetDt(req);

            return Json(data);
        }

        public async Task<IActionResult> Insert(Slide data, IFormFile file1)
        {
            BaseRes res = new();
            try
            {
                if (data.Id == 0)
                {
                    if (file1 == null)
                        throw new ArgumentException("Image is required.");

                    data.Image = await fileHelper.Upload(file1, UploadFolder.Slide);
                    data.CreateBy = User.Identity.Name;
                    data.CreateDate = DateTime.Now;

                    await slideDa.Insert(data);
                }
                else
                {
                    if (file1 != null)
                    {
                        data.Image = await fileHelper.Upload(file1, UploadFolder.Slide);

                        await DeleteFile(data.Id);
                    }

                    data.UpdateBy = User.Identity.Name;
                    data.UpdateDate = DateTime.Now;

                    await slideDa.Update(data);
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
            await DeleteFile(id);

            await slideDa.Delete(id, User.Identity.Name);

            return Json(true);
        }

        public async Task<IActionResult> GetNextRanking()
        {
            var data = await slideDa.GetNextRanking();

            return Json(data);
        }

        #region private
        private async Task DeleteFile(int id)
        {
            var o = await slideDa.GetById(id);

            if (!string.IsNullOrEmpty(o.Image))
                fileHelper.Delete(UploadFolder.Slide, o.Image);
        }
        #endregion
    }
}
