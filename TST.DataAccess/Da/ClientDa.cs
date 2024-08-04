using Microsoft.EntityFrameworkCore;
using TST.Core.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TST.DataAccess.Da
{
    public class ClientDa : IClientDa
    {
        private readonly TstContext db;

        public ClientDa(TstContext db)
        {
            this.db = db;
        }

        public async Task<GetClientDtRes> GetDt(GetClientDtReq req)
        {
            var raw = db.Client.Where(w => !w.IsDelete);

            if (!string.IsNullOrEmpty(req.Name))
                raw = raw.Where(w => w.NameEn.Contains(req.Name) || w.NameTh.Contains(req.Name));

            int count = await raw.CountAsync();

            var data = await raw.Skip(req.Start).Take(req.Length)
                .OrderBy(w => w.Ranking)
                .Select(s => new GetClientDtRes2
                {
                    Id = s.Id,
                    Image = s.Image,
                    NameEn = s.NameEn,
                    NameTh = s.NameTh,
                    Ranking = s.Ranking,
                    IsActive = s.IsActive,
                    CreateBy = s.UpdateBy == null ? s.CreateBy : s.UpdateBy,
                    CreateDate = s.UpdateDate == null ? s.CreateDate : s.UpdateDate.Value
                }).ToListAsync();

            return new GetClientDtRes
            {
                data = data,
                draw = req.Draw,
                recordsFiltered = count,
                recordsTotal = count
            };
        }

        public async Task<Client> GetById(int id) => await db.Client.FindAsync(id);

        public async Task Insert(Client data)
        {
            await db.AddAsync(data);
            await db.SaveChangesAsync();
        }

        public async Task<int> GetNextRanking()
        {
            var x = await db.Client.Where(w => !w.IsDelete).OrderByDescending(u => u.Ranking).Select(s => s.Ranking).FirstOrDefaultAsync();

            return x + 1;
        }

        public async Task Update(Client data)
        {
            var o = await GetById(data.Id);
            o.Ranking = data.Ranking;
            o.NameEn = data.NameEn;
            o.NameTh = data.NameTh;
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

        public async Task<List<GetClientWebRes>> GetClient()
        {
            var data = await (from r in db.Client
                              where !r.IsDelete && r.IsActive
                              orderby r.Ranking
                              select new GetClientWebRes
                              {
                                  NameEn = r.NameEn,
                                  NameTh = r.NameTh,
                                  Image = r.Image,
                              })
                       .ToListAsync();
            return data;
        }
    }
}
