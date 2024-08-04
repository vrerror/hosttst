using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TST.Core.Models;

namespace TST.Web.BofControllers
{

    public class BofUserController : Controller
    {
        private readonly IUserDa userDa;
        private readonly SignInManager<ApplicationUser> signInManager;

        public BofUserController(IUserDa userDa, SignInManager<ApplicationUser> signInManager)
        {
            this.userDa = userDa;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {

            return View();
        }

        public async Task<IActionResult> GetUser(GetUserDtReq req)
        {
            var data = await userDa.GetUser(req);

            return Json(data);
        }

        public async Task<IActionResult> Save(ApplicationUser data)
        {
            BaseRes res = new();
            try
            {
                if (data.Id == null)
                {
                    var name = await userDa.GetByUsername(data.UserName);
                    if (name == null)
                    {
                        if (data.Password == data.ConfirmPassword)
                        {
                            data.CreateBy = User.Identity.Name;
                            data.CreateDate = DateTime.Now;
                            await userDa.Insert(data);
                            res.Success = true;
                        }
                        else
                        {
                            res.Success = false;
                            res.Message = "Passwords do not match";
                        }
                    }
                    else
                    {
                        res.Success = false;
                        res.Message = "Duplicate Username";
                    }
                }
                else
                {
                    data.UpdateBy = User.Identity.Name;
                    data.UpdateDate = DateTime.Now;
                    await userDa.Update(data);
                    res.Success = true;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"ERROR: {ex.Message}";
            }
            return Json(res);
        }

        public async Task<IActionResult> GetByUserId(string userid)
        {
            var data = await userDa.GetByUserId(userid);

            return Json(data);
        }

        public async Task<IActionResult> Delete(string id, string username)
        {
            await userDa.Delete(id, User.Identity.Name);

            return Json(true);
        }

    }
}






