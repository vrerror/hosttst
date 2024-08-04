using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TST.DataAccess.Da
{
    public class RoleDa : IRoleDa
    {
        private readonly TstContext db;

        public RoleDa(TstContext db)
        {
            this.db = db;
        }

        public async Task<IdentityRole> GetById(string id) => await db.Roles.FirstOrDefaultAsync(x => x.Id.ToString() == id);

        public async Task<bool> IsExist(string name) => await db.Roles.AnyAsync(a => a.Name == name);

        public async Task Insert(IdentityRole data)
        {
            data.NormalizedName = data.Name.ToUpper();

            await db.AddAsync(data);
            await db.SaveChangesAsync();
        }        
    }
}
