 using Microsoft.EntityFrameworkCore;

namespace TST.DataAccess.Da
{
    public class PerformanceDa : IPerformanceDa
    {
        private readonly TstContext db;

        public PerformanceDa(TstContext db)
        {
            this.db = db;
        }

        public async Task<Performance> GetById(int id) => await db.Performance.FindAsync(id);

        public async Task<PerformanceImage> GetImageById(int id) => await db.PerformanceImage.FindAsync(id);

        public async Task<List<PerformanceImage>> GetImage(int id) => await db.PerformanceImage.Where(w => !w.IsDelete && w.PerformanceId == id).ToListAsync();

        public async Task<GetPerformanceDtRes> GetDt(GetPerformanceDtReq req)
        {
            var raw = db.Performance.Where(w => !w.IsDelete);
            var rawType = db.PerformanceType.Where(w => !w.IsDelete);

            if (req.TypeId != 0)
                rawType = rawType.Where(w => w.Id == req.TypeId);

            if (!string.IsNullOrEmpty(req.Name))
                raw = raw.Where(w => w.NameEn.Contains(req.Name) || w.NameTh.Contains(req.Name)
                    || w.TitleEn.Contains(req.Name) || w.TitleEn.Contains(req.Name));

            int count = await raw.CountAsync();

            var data = await (from r in raw
                              join t in rawType on r.PerformanceTypeId equals t.Id
                              orderby t.Ranking, r.Ranking
                              select new GetPerformanceDtRes2
                              {
                                  Id = r.Id,
                                  NameEn = r.NameEn,
                                  NameTh = r.NameTh,
                                  Ranking = r.Ranking,
                                  TitleEn = r.TitleEn,
                                  TitleTh = r.TitleTh,
                                  TypeEn = t.NameEn,
                                  TypeTh = t.NameTh,
                                  IsActive = t.IsActive,
                                  CreateBy = r.UpdateBy == null ? r.CreateBy : r.UpdateBy,
                                  CreateDate = r.UpdateDate == null ? r.CreateDate : r.UpdateDate.Value
                              })
                       .Skip(req.Start).Take(req.Length)
                       .ToListAsync();

            if (data.Any())
            {
                var ids = data.Select(s => s.Id);
                var images = await db.PerformanceImage.Where(w => !w.IsDelete && ids.Contains(w.PerformanceId)).ToListAsync();

                foreach (var d in data)
                {
                    d.Image = images.OrderBy(o => o.Ranking).FirstOrDefault(f => f.PerformanceId == d.Id)?.Image;
                }
            }

            return new GetPerformanceDtRes
            {
                data = data,
                draw = req.Draw,
                recordsFiltered = count,
                recordsTotal = count
            };
        }

        public async Task<GetPerformanceImageDtRes> GetImageDt(GetPerformanceImageDtReq req)
        {
            var raw = db.PerformanceImage.Where(w => !w.IsDelete && w.PerformanceId == req.PerformanceId).OrderBy(o => o.Ranking);

            int count = await raw.CountAsync();

            var data = await raw.Skip(req.Start).Take(req.Length)
                .Select(s => new GetPerformanceImageDtRes2
                {
                    Id = s.Id,
                    Image = s.Image,
                    Ranking = s.Ranking,
                    CreateBy = s.UpdateBy == null ? s.CreateBy : s.UpdateBy,
                    CreateDate = s.UpdateDate == null ? s.CreateDate : s.UpdateDate.Value
                }).ToListAsync();

            return new GetPerformanceImageDtRes
            {
                data = data,
                draw = req.Draw,
                recordsFiltered = count,
                recordsTotal = count
            };
        }

        public async Task<List<Ddl>> GetTypeDdl() => await db.PerformanceType.Where(w => !w.IsDelete && w.IsActive)
                .OrderBy(o => o.Ranking)
                .Select(s => new Ddl
                {
                    TextEn = s.NameEn,
                    TextTh = s.NameTh,
                    Value = s.Id
                })
                .ToListAsync();

        public async Task<int> GetNextRanking(int typeId)
        {
            var x = await db.Performance.Where(w => !w.IsDelete && w.PerformanceTypeId == typeId).OrderByDescending(u => u.Ranking).Select(s => s.Ranking).FirstOrDefaultAsync();

            return x + 1;
        }

        public async Task<List<GetPerformanceWebRes>> GetWeb()
        {
            var data = await (from r in db.Performance
                              where !r.IsDelete && r.IsActive
                              orderby r.Ranking
                              select new GetPerformanceWebRes
                              {
                                  Id = r.Id,
                                  PerformanceTypeId = r.PerformanceTypeId,
                                  NameEn = r.NameEn,
                                  NameTh = r.NameTh,
                                  TitleEn = r.TitleEn,
                                  TitleTh = r.TitleTh,
                                  ShortDetailEn = r.ShortDetailEn,
                                  ShortDetailTh = r.ShortDetailTh
                              })
                       .ToListAsync();

            if (data.Any())
            {
                var ids = data.Select(s => s.Id);
                var images = await db.PerformanceImage.Where(w => !w.IsDelete && ids.Contains(w.PerformanceId)).ToListAsync();

                foreach (var d in data)
                {
                    d.Image = images.OrderBy(o => o.Ranking).FirstOrDefault(f => f.PerformanceId == d.Id)?.Image;
                }
            }

            return data;
        }

        public async Task<GetPerformanceDetailRes> GetDetail(int id)
        {
            var r = await GetById(id);

            var t = await db.PerformanceType.FindAsync(r.PerformanceTypeId);

            var data = new GetPerformanceDetailRes
            {
                NameEn = r.NameEn,
                NameTh = r.NameTh,
                TitleEn = r.TitleEn,
                TitleTh = r.TitleTh,
                DetailEn = r.DetailEn,
                DetailTh = r.DetailTh,
                ShortDetailEn = r.ShortDetailEn,
                ShortDetailTh = r.ShortDetailTh,
                YoutubeUrl = r.YoutubeUrl,
                TypeEn = t.NameEn,
                TypeTh = t.NameTh,
                Images = await db.PerformanceImage.Where(w => !w.IsDelete && w.PerformanceId == id).Select(s => s.Image).ToListAsync()
            };

            return data;
        }

        public async Task Insert(Performance data, List<PerformanceImage> images)
        {
            await db.AddAsync(data);
            await db.SaveChangesAsync();

            await InsertImage(data, images);
        }

        public async Task Update(Performance data, List<PerformanceImage> images, List<PerformanceImage> rankings)
        {
            var o = await GetById(data.Id);

            if (o.NameEn != data.NameEn || o.NameTh != data.NameTh || o.TitleEn != data.TitleEn || o.TitleTh != data.TitleTh
                || o.ShortDetailEn != data.ShortDetailEn || o.ShortDetailTh != data.ShortDetailTh
                || o.DetailEn != data.DetailEn || o.DetailTh != data.DetailTh || o.IsActive != data.IsActive
                || o.Ranking != data.Ranking || o.PerformanceTypeId != data.PerformanceTypeId || o.YoutubeUrl != data.YoutubeUrl)
            {
                o.NameEn = data.NameEn;
                o.NameTh = data.NameTh;
                o.TitleEn = data.TitleEn;
                o.TitleTh = data.TitleTh;
                o.ShortDetailEn = data.ShortDetailEn;
                o.ShortDetailTh = data.ShortDetailTh;
                o.DetailEn = data.DetailEn;
                o.DetailTh = data.DetailTh;
                o.IsActive = data.IsActive;
                o.Ranking = data.Ranking;
                o.PerformanceTypeId = data.PerformanceTypeId;
                o.YoutubeUrl = data.YoutubeUrl;
                o.UpdateBy = data.UpdateBy;
                o.UpdateDate = data.UpdateDate;
            }

            var existingImgs = await GetImage(data.Id);
            foreach (var x in existingImgs)
            {
                var r = rankings.FirstOrDefault(f => f.Id == x.Id);
                if (r == null)
                    continue;

                if (x.Ranking != r.Ranking)
                {
                    x.Ranking = r.Ranking;
                    x.UpdateBy = data.UpdateBy;
                    x.UpdateDate = data.UpdateDate;
                }
            }

            await db.SaveChangesAsync();

            await InsertImage(data, images);
        }

        public async Task Delete(int id, string user)
        {
            var o = await GetById(id);
            o.IsDelete = true;
            o.UpdateDate = DateTime.Now;
            o.UpdateBy = user;

            await db.SaveChangesAsync();
        }

        public async Task DeleteImage(int id, string user)
        {
            var o = await db.PerformanceImage.FindAsync(id);

            o.IsDelete = true;
            o.UpdateDate = DateTime.Now;
            o.UpdateBy = user;

            await db.SaveChangesAsync();
        }

        #region private
        private async Task InsertImage(Performance data, List<PerformanceImage> images)
        {
            if (images.Any())
            {
                var x = images.Select(s => new PerformanceImage
                {
                    CreateBy = data.CreateBy,
                    CreateDate = data.CreateDate,
                    Image = s.Image,
                    Ranking = s.Ranking,
                    PerformanceId = data.Id
                });

                await db.AddRangeAsync(x);
                await db.SaveChangesAsync();
            }
        }
        #endregion
    }
}
