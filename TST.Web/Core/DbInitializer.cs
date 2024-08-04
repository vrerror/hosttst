using Microsoft.AspNetCore.Identity;
using TST.Core.Common;
using TST.DataAccess.Entities;

namespace TST.Web.Core
{
    public class DbInitializer
    {
        private readonly IUserDa userDa;
        private readonly IRoleDa roleDa;

        public DbInitializer(IUserDa userDa, IRoleDa roleDa)
        {
            this.userDa = userDa;
            this.roleDa = roleDa;
        }

        public async Task Run()
        {
            await CreateRole();
            await CreateDefaultUser();
        }

        #region private
        private async Task CreateRole()
        {
            if (!await roleDa.IsExist(Role.System))
                await roleDa.Insert(new IdentityRole { Name = Role.System });
        }

        private async Task CreateDefaultUser()
        {
            string defaultUser = "system";
            var system = await userDa.GetByUsername(defaultUser);
            if (system == null)
            {
                var user = new ApplicationUser
                {
                    UserName = defaultUser,
                    Password = defaultUser,
                    FirstNameEn = "Thongchai",
                    LastNameEn = "Arunothaiwattana",
                    FirstNameTh = "ธงไชย",
                    LastNameTh = "อรุโณทัยวัฒนา",
                    IsActive = true,
                    CreateBy = defaultUser,
                    CreateDate = DateTime.Now,
                    Role = Role.System
                };

                await userDa.Insert(user);
            }
        }
        #endregion
    }
}
