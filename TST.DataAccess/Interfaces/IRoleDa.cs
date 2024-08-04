using Microsoft.AspNetCore.Identity;

namespace TST.DataAccess.Interfaces
{
    public interface IRoleDa
    {
        Task<IdentityRole> GetById(string id);
        Task<bool> IsExist(string name);
        Task Insert(IdentityRole data);
    }
}
