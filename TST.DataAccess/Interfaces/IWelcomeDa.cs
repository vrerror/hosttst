
namespace TST.DataAccess.Interfaces
{
    public interface IWelcomeDa
    {
        Task<Welcome> GetAll();
        Task Save(Welcome data);
        Task Insert(Welcome data);
        Task<Welcome> GetById(int id);
        Task<GetWelcomeWebRes> GetWeb();
    }
}
