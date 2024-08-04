using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TST.DataAccess.Da
{
    public class UserDa : IUserDa
    {
        private readonly TstContext db;

        public UserDa(TstContext db)
        {
            this.db = db;
        }

        public async Task<GetUserRes> GetUser(GetUserDtReq req)
        {
            var raw = db.Users.Where(d => !d.IsDelete);
            if (!string.IsNullOrEmpty(req.UserName))
                raw = raw.Where(w => w.UserName.Contains(req.UserName));
            int count = await raw.CountAsync();

            var data = await raw.Skip(req.Start).Take(req.Length)
                              .OrderBy(d => d.UserName)
                              .Select(d => new GetUserRes2
                              {
                                  Id = d.Id,
                                  FirstNameEn = d.FirstNameEn,
                                  LastNameEn = d.LastNameEn,
                                  FirstNameTh = d.FirstNameTh,
                                  LastNameTh = d.LastNameTh,
                                  UserName = d.UserName,
                                  IsActive = d.IsActive,
                                  CreateBy = d.UpdateBy == null ? d.CreateBy : d.UpdateBy,
                                  CreateDate = d.UpdateDate == null ? d.CreateDate : d.UpdateDate.Value
                              }).ToListAsync();
            return new GetUserRes
            {
                data = data,
                draw = req.Draw,
                recordsFiltered = count,
                recordsTotal = count
            };
        }

        public async Task<ApplicationUser> GetByUsername(string username) => await db.Users.FirstOrDefaultAsync(f => !f.IsDelete && f.UserName.ToLower() == username.ToLower());

        public async Task<ApplicationUser> GetByUserId(string userId) => await db.Users.FirstOrDefaultAsync(f => f.Id == userId);

        public async Task Insert(ApplicationUser data)
        {
            if (data.Id == null)
                data.Id = Guid.NewGuid().ToString();

            data.NormalizedUserName = data.UserName.ToUpper();
            data.UserName = data.UserName.ToLower();

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            data.PasswordHash = passwordHasher.HashPassword(data, data.Password);

            await db.AddAsync(data);
            await db.SaveChangesAsync();
        }

        public async Task Update(ApplicationUser data)
        {
            var o = await db.Users.FirstOrDefaultAsync(f => f.Id == data.Id);
            if (o.FirstNameEn != data.FirstNameEn || o.LastNameEn != data.LastNameEn ||
                o.FirstNameTh != data.FirstNameTh || o.LastNameTh != data.LastNameTh ||
                o.IsActive != data.IsActive || !string.IsNullOrEmpty(data.Password))
            {

                o.FirstNameEn = data.FirstNameEn;
                o.LastNameEn = data.LastNameEn;
                o.FirstNameTh = data.FirstNameTh;
                o.LastNameTh = data.LastNameTh;
                o.IsActive = data.IsActive;
                o.UpdateBy = data.UpdateBy;
                o.UpdateDate = data.UpdateDate;

                if (!string.IsNullOrEmpty(data.Password))
                {
                    var passwordHasher = new PasswordHasher<ApplicationUser>();
                    o.PasswordHash = passwordHasher.HashPassword(data, data.Password);
                }

                await db.SaveChangesAsync();
            }
        }

        public async Task Delete(string id, string user)
        {
            var o = await db.Users.FirstOrDefaultAsync(f => f.Id == id);
            o.IsDelete = true;
            o.UpdateDate = DateTime.Now;
            o.UpdateBy = user;

            await db.SaveChangesAsync();
        }


    }
}
