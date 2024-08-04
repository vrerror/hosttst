
namespace TST.DataAccess.Interfaces
{
    public interface IUserDa
    {
        Task<GetUserRes> GetUser(GetUserDtReq req);
        Task<ApplicationUser> GetByUsername(string username);
        Task<ApplicationUser> GetByUserId(string userId);
        Task Insert(ApplicationUser data);
        Task Delete(string id, string user);
        Task Update(ApplicationUser data);
    }
}
