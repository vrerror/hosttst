
namespace TST.DataAccess.Interfaces
{
    public interface IClientDa
    {
        Task<Client> GetById(int id);
        Task<GetClientDtRes> GetDt(GetClientDtReq req);
        Task Insert(Client data);
        Task Update(Client data);
        Task Delete(int id, string user);
        Task<int> GetNextRanking();
        Task<List<GetClientWebRes>> GetClient();
    }
}
