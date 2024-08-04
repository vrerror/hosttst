using Microsoft.EntityFrameworkCore;

namespace TST.DataAccess.Da
{
    public class PerformanceTypeDa : IPerformanceType
    {
        private readonly TstContext db;

        public PerformanceTypeDa(TstContext db)
        {
            this.db = db;
        }

        public async Task<PerformanceType> GetById(int id) => await db.PerformanceType.FindAsync(id);

        public async Task<GetPerformanceTypeDtRes> GetDt(GetPerformanceTypeDtReq req)
        {
            var raw = db.PerformanceType.Where(w => !w.IsDelete);

            if (!string.IsNullOrEmpty(req.Name))
                raw = raw.Where(w => w.NameEn.Contains(req.Name) || w.NameTh.Contains(req.Name));

            int count = await raw.CountAsync();

            var data = await raw.Skip(req.Start).Take(req.Length)
                .OrderBy(o => o.Ranking)
                .Select(w => new GetPerformanceTypeDtRes2
                {
                    Id = w.Id,
                    Ranking = w.Ranking,
                    NameEn = w.NameEn,
                    NameTh = w.NameTh,
                    IsActive = w.IsActive,
                    CreateBy = w.UpdateBy == null ? w.CreateBy : w.UpdateBy,
                    CreateDate = w.UpdateDate == null ? w.CreateDate : w.UpdateDate.Value
                }).ToListAsync();

            return new GetPerformanceTypeDtRes
            {
                data = data,
                draw = req.Draw,
                recordsFiltered = count,
                recordsTotal = count
            };
        }

        public async Task Insert(PerformanceType data)
        {
            await db.AddAsync(data);
            await db.SaveChangesAsync();
        }

        public async Task Update(PerformanceType data)
        {
            var o = await GetById(data.Id);

            o.NameEn = data.NameEn;
            o.NameTh = data.NameTh;
            o.Ranking = data.Ranking;
            o.IsActive = data.IsActive;
            o.UpdateBy = data.UpdateBy;
            o.UpdateDate = data.UpdateDate;

            await db.SaveChangesAsync();
        }

        public async Task Delete(int id, string user)
        {
            var o = await GetById(id);

            o.IsDelete = true;
            o.UpdateDate = DateTime.Now;
            o.UpdateBy = user;

            await db.SaveChangesAsync();
        }

        public async Task<int> GetNextRanking()
        {
            var x = await db.PerformanceType.Where(w => !w.IsDelete).OrderByDescending(u => u.Ranking).Select(s => s.Ranking).FirstOrDefaultAsync();

            return x + 1;
        }
    }
}
