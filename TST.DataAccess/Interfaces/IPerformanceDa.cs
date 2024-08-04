
namespace TST.DataAccess.Interfaces
{
    public interface IPerformanceDa
    {
        Task<Performance> GetById(int id);
        Task<PerformanceImage> GetImageById(int id);
        Task<List<PerformanceImage>> GetImage(int id);
        Task<GetPerformanceDtRes> GetDt(GetPerformanceDtReq req);
        Task<GetPerformanceImageDtRes> GetImageDt(GetPerformanceImageDtReq req);
        Task<List<Ddl>> GetTypeDdl();
        Task<int> GetNextRanking(int typeId);
        Task<List<GetPerformanceWebRes>> GetWeb();
        Task<GetPerformanceDetailRes> GetDetail(int id);
        Task Insert(Performance data, List<PerformanceImage> images);
        Task Update(Performance data, List<PerformanceImage> images, List<PerformanceImage> rankings);
        Task Delete(int id, string user);
        Task DeleteImage(int id, string user);
    }
}
