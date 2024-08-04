using Microsoft.EntityFrameworkCore;

namespace TST.DataAccess.Da
{
    public class SlideDa : ISlideDa
    {
        private readonly TstContext db;

        public SlideDa(TstContext db)
        {
            this.db = db;
        }

        public async Task<Slide> GetById(int id) => await db.Slide.FindAsync(id);

        public async Task<GetSlideDtRes> GetDt(GetSlideDtReq req)
        {
            var raw = db.Slide.Where(w => !w.IsDelete);

            if (!string.IsNullOrEmpty(req.Keyword))
                raw = raw.Where(w => w.KeywordEn.Contains(req.Keyword) || w.KeywordTh.Contains(req.Keyword));

            int count = await raw.CountAsync();

            var data = await raw.Skip(req.Start).Take(req.Length)
                .OrderBy(o => o.Ranking)
                .Select(w => new GetSlideDtRes2
                {
                    Id = w.Id,
                    Image = w.Image,
                    Ranking = w.Ranking,
                    KeywordEn = w.KeywordEn,
                    KeywordTh = w.KeywordTh,
                    IsActive = w.IsActive,
                    CreateBy = w.UpdateBy == null ? w.CreateBy : w.UpdateBy,
                    CreateDate = w.UpdateDate == null ? w.CreateDate : w.UpdateDate.Value
                }).ToListAsync();

            return new GetSlideDtRes
            {
                data = data,
                draw = req.Draw,
                recordsFiltered = count,
                recordsTotal = count
            };
        }

        public async Task Insert(Slide data)
        {
            await db.AddAsync(data);
            await db.SaveChangesAsync();
        }

        public async Task Update(Slide data)
        {
            var o = await GetById(data.Id);
            o.KeywordEn = data.KeywordEn;
            o.KeywordTh = data.KeywordTh;
            o.Ranking = data.Ranking;
            o.IsActive = data.IsActive;
            o.UpdateBy = data.UpdateBy;
            o.UpdateDate = data.UpdateDate;

            if (!string.IsNullOrEmpty(data.Image))
                o.Image = data.Image;

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
            var x = await db.Slide.Where(w => !w.IsDelete).OrderByDescending(u => u.Ranking).Select(s => s.Ranking).FirstOrDefaultAsync();

            return x + 1;
        }

        public async Task<List<GetSlideWedRes>> GetWeb()
        {
            var data = await (from w in db.Slide
                              where !w.IsDelete && w.IsActive
                              orderby w.Ranking
                              select new GetSlideWedRes
                              {
                                  KeywordEn = w.KeywordEn,
                                  KeywordTh = w.KeywordTh,
                                  Image = w.Image,
                              })
            .ToListAsync();
            return data;
        }


    }
}
