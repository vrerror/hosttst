using Microsoft.AspNetCore.Mvc;
using TST.Core.Common;
using TST.Core.Models;
using TST.DataAccess.Da;
using TST.Web.Core;

namespace TST.Web.BofControllers
{
    public class BofClientController : Controller
    {
       
        private readonly FileHelper fileHelper;
        private readonly IClientDa clientsDa;

        public BofClientController(FileHelper fileHelper, IClientDa clientsDa)
        {
            this.fileHelper = fileHelper;
            this.clientsDa = clientsDa;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetDt(GetClientDtReq req)
        {
            var data = await clientsDa.GetDt(req);

            return Json(data);
        }

        public async Task<IActionResult> GetById(int id)
        {
            var data = await clientsDa.GetById(id);

            return Json(data);
        }

        public async Task<IActionResult> Save(Client data, IFormFile file1)
        {
            BaseRes res = new();
            try
            {
                if (data.Id == 0)
                {
                    if (file1 == null)
                        throw new ArgumentException("Image is required.");

                    data.Image = await fileHelper.Upload(file1, UploadFolder.Client);
                    data.CreateBy = User.Identity.Name;
                    data.CreateDate = DateTime.Now;

                    await clientsDa.Insert(data);
                }
                else
                {
                    if (file1 != null)
                    {
                        data.Image = await fileHelper.Upload(file1, UploadFolder.Client);

                        await DeleteFile(data.Id);
                    }

                    data.UpdateBy = User.Identity.Name;
                    data.UpdateDate = DateTime.Now;
                    await clientsDa.Update(data);
                }

                res.Success = true;
            }
            catch (ArgumentException ex)
            {
                res.Message = ex.Message;
            }
            catch (Exception ex)
            {
                res.Message = $"ERROR: {ex.Message}";
            }
            return Json(res);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await DeleteFile(id);

            await clientsDa.Delete(id, User.Identity.Name);

            return Json(true);
        }

        #region
        private async Task DeleteFile(int id)
        {
            var o = await clientsDa.GetById(id);

            if (!string.IsNullOrEmpty(o.Image))
                fileHelper.Delete(UploadFolder.Client, o.Image);
        }
        #endregion

        public async Task<IActionResult> GetNextRanking()
        {
            var data = await clientsDa.GetNextRanking();

            return Json(data);
        }

    }
}
