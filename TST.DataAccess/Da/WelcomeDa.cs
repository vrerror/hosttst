using Microsoft.EntityFrameworkCore;
using TST.DataAccess.Interfaces;

namespace TST.DataAccess.Da
{
    public class WelcomeDa : IWelcomeDa
    {
        private readonly TstContext db;

        public WelcomeDa(TstContext db)
        {
            this.db = db;
        }

        public async Task<Welcome> GetAll()
        {
            var data = await db.Welcome.FirstOrDefaultAsync();
            if (data == null)
            {
                data = new Welcome();
            }
            return data;
        }

        public async Task Insert(Welcome data)
        {
            await db.AddAsync(data);
            await db.SaveChangesAsync();
        }

        public async Task<Welcome> GetById(int id) => await db.Welcome.FindAsync(id);

        public async Task Save(Welcome data)
        {
            var o = await GetById(data.Id);

            if (o != null)
            {
                if (o.SlideText1En != data.SlideText1En || o.SlideText1Th != data.SlideText1Th || o.SlideText2En != data.SlideText2En || o.SlideText2Th != data.SlideText2Th || 
                    o.HeaderEn != data.HeaderEn || o.HeaderTh != data.HeaderTh || o.DetailEn != data.DetailEn || o.DetailTh != data.DetailTh || o.Image1 != data.Image1 ||
                    o.Image2 != data.Image2 || o.Image3 != data.Image3)
                {
                    o.SlideText1En = data.SlideText1En;
                    o.SlideText1Th = data.SlideText1Th;
                    o.SlideText2En = data.SlideText2En;
                    o.SlideText2Th = data.SlideText2Th;
                    o.HeaderEn = data.HeaderEn;
                    o.HeaderTh = data.HeaderTh;
                    o.DetailEn = data.DetailEn;
                    o.DetailTh = data.DetailTh;
                    o.UpdateBy = data.UpdateBy;
                    o.UpdateDate = data.UpdateDate;

                    if (!string.IsNullOrEmpty(data.Image1))
                        o.Image1 = data.Image1;

                    if (!string.IsNullOrEmpty(data.Image2))
                        o.Image2 = data.Image2;

                    if (!string.IsNullOrEmpty(data.Image3))
                        o.Image3 = data.Image3;
                }
            }
            else
            {
                await db.AddAsync(data);
            }

            await db.SaveChangesAsync();

        }

        public async Task<GetWelcomeWebRes> GetWeb()
        {
            return await (from c in db.Welcome
                          select new GetWelcomeWebRes
                          {
                              SlideText1En = c.SlideText1En,
                              SlideText1Th = c.SlideText1Th,
                              SlideText2En = c.SlideText2En,
                              SlideText2Th = c.SlideText2Th,
                              SlideText3En = c.SlideText3En,
                              SlideText3Th = c.SlideText3Th,
                              HeaderEn = c.HeaderEn,
                              HeaderTh = c.HeaderTh,
                              DetailEn = c.DetailEn,
                              DetailTh = c.DetailTh,
                              Image1 = c.Image1,
                              Image2 = c.Image2,
                              Image3 = c.Image3,
                          }).FirstOrDefaultAsync() ?? new GetWelcomeWebRes();
        }






    }

}
