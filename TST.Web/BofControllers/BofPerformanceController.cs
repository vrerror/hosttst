using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TST.Core.Common;
using TST.Core.Models;
using TST.DataAccess.Da;
using TST.Web.Core;

namespace TST.Web.BofControllers
{
    [Authorize]
    public class BofPerformanceController : Controller
    {
        private readonly IPerformanceDa performanceDa;
        private readonly FileHelper fileHelper;

        public BofPerformanceController(IPerformanceDa performanceDa, FileHelper fileHelper)
        {
            this.performanceDa = performanceDa;
            this.fileHelper = fileHelper;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Types = await performanceDa.GetTypeDdl();

            return View();
        }

        public async Task<IActionResult> GetById(int id)
        {
            var data = await performanceDa.GetById(id);

            return Json(data);
        }

        public async Task<IActionResult> GetDt(GetPerformanceDtReq req)
        {
            var data = await performanceDa.GetDt(req);

            return Json(data);
        }

        public async Task<IActionResult> GetImageDt(GetPerformanceImageDtReq req)
        {
            if (req.PerformanceId == 0)
                return Json(new GetPerformanceImageDtRes { });

            var data = await performanceDa.GetImageDt(req);

            return Json(data);
        }

        public async Task<IActionResult> GetNextRanking(int typeId)
        {
            var data = await performanceDa.GetNextRanking(typeId);

            return Json(data);
        }

        public async Task<IActionResult> Save(Performance data, List<IFormFile> files)
        {
            BaseRes res = new();
            try
            {
                List<PerformanceImage> images = new();

                if (data.Id == 0)
                {
                    if (files.Any())
                        images = await UploadFile(files, 1);

                    data.CreateBy = User.Identity.Name;
                    data.CreateDate = DateTime.Now;

                    await performanceDa.Insert(data, images);
                }
                else
                {
                    var rankings = JsonSerializer.Deserialize<List<PerformanceImage>>(data.Rankings);

                    if (files.Any())
                    {
                        int startRanking = rankings.Any() ? rankings.Max(x => x.Ranking) + 1 : 1;
                        images = await UploadFile(files, startRanking);
                    }

                    data.UpdateBy = data.CreateBy = User.Identity.Name;
                    data.UpdateDate = data.CreateDate = DateTime.Now;

                    await performanceDa.Update(data, images, rankings);
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
            await DeleteAllFile(id);

            await performanceDa.Delete(id, User.Identity.Name);

            return Json(true);
        }

        public async Task<IActionResult> DeleteImage(int id)
        {
            await DeleteFile(id);

            await performanceDa.DeleteImage(id, User.Identity.Name);

            return Json(true);
        }

        #region
        private async Task DeleteFile(int id)
        {
            var o = await performanceDa.GetImageById(id);

            if (!string.IsNullOrEmpty(o.Image))
                fileHelper.Delete(UploadFolder.Performance, o.Image);
        }

        private async Task<List<PerformanceImage>> UploadFile(List<IFormFile> files, int startRanking)
        {
            List<PerformanceImage> images = new();

            int i = startRanking;
            foreach (var f in files)
            {
                images.Add(new PerformanceImage
                {
                    Image = await fileHelper.Upload(f, UploadFolder.Performance),
                    Ranking = i
                });
                i++;
            }
            return images;
        }

        private async Task DeleteAllFile(int id)
        {
            var images = await performanceDa.GetImage(id);

            if (images.Any())
            {
                foreach (var image in images)
                {
                    fileHelper.Delete(UploadFolder.Performance, image.Image);
                }
            }
        }
        #endregion
    }
}
