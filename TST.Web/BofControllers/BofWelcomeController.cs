using Microsoft.AspNetCore.Mvc;
using TST.Core.Common;
using TST.Core.Models;
using TST.DataAccess.Da;
using TST.Web.Core;

namespace TST.Web.BofControllers
{

    public class BofWelcomeController : Controller
    {
        private readonly FileHelper fileHelper;
        private readonly IWelcomeDa welcomeDa;

        public BofWelcomeController(FileHelper fileHelper, IWelcomeDa welcomeDa)
        {
            this.fileHelper = fileHelper;
            this.welcomeDa = welcomeDa;
        }

        public async Task<IActionResult> Index()
        {
            var data = await welcomeDa.GetAll();

            return View(data);
        }

        public async Task<IActionResult> Save(Welcome data, IFormFile file1, IFormFile file2, IFormFile file3)
        {
            BaseRes res = new();
            try
            {
                if (data.Id == 0)
                {
                    if (file1 == null || file2 == null || file3 == null)
                        throw new ArgumentException("Image is required.");

                    data.Image1 = await fileHelper.Upload(file1, UploadFolder.Welcome);
                    data.Image2 = await fileHelper.Upload(file2, UploadFolder.Welcome);
                    data.Image3 = await fileHelper.Upload(file3, UploadFolder.Welcome);

                    data.UpdateBy = User.Identity.Name;
                    data.UpdateDate = DateTime.Now;

                    await welcomeDa.Insert(data);
                }
                else
                {
                    var a = await welcomeDa.GetById(data.Id);

                    if (file1 != null)
                    {
                        data.Image1 = await fileHelper.Upload(file1, UploadFolder.Welcome);
                        DeleteFile(a.Image1);
                    }

                    if (file2 != null)
                    {
                        data.Image2 = await fileHelper.Upload(file2, UploadFolder.Welcome);
                        DeleteFile(a.Image2);
                    }

                    if (file3 != null)
                    {
                        data.Image3 = await fileHelper.Upload(file3, UploadFolder.Welcome);
                        DeleteFile(a.Image3);
                    }

                    data.UpdateBy = User.Identity.Name;
                    data.UpdateDate = DateTime.Now;

                    await welcomeDa.Save(data);
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


        public void DeleteFile(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
                fileHelper.Delete(UploadFolder.Welcome, fileName);
        }

    }
}






