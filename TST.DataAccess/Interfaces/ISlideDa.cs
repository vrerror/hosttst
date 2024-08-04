
namespace TST.DataAccess.Interfaces
{
    public interface ISlideDa
    {
        Task<Slide> GetById(int id);
        Task<GetSlideDtRes> GetDt(GetSlideDtReq req);
        Task<int> GetNextRanking();
        Task Insert(Slide data);
        Task Update(Slide data);
        Task Delete(int id, string user);
        Task<List<GetSlideWedRes>> GetWeb();

    }
}
