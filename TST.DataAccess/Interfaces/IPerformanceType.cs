using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TST.DataAccess.Interfaces
{
    public interface IPerformanceType
    {
        Task<PerformanceType> GetById(int id);
        Task<GetPerformanceTypeDtRes> GetDt(GetPerformanceTypeDtReq req);
        Task Insert(PerformanceType data);
        Task Update(PerformanceType data);
        Task Delete(int id, string user);
        Task<int> GetNextRanking();
    }
}
