using Microsoft.EntityFrameworkCore;

namespace TST.DataAccess.Da
{
    public class ContactDa : IContactDa
    {
        private readonly TstContext db;

        public ContactDa(TstContext db)
        {
            this.db = db;
        }

        public async Task<Contact> Get1() => await db.Contact.FirstOrDefaultAsync() ?? new Contact();

        public async Task<GetContactWebRes> GetWebRes()
        {
            return await (from c in db.Contact
                          select new GetContactWebRes
                          {
                              AddressEn = c.AddressEn,
                              AddressTh = c.AddressTh,
                              CompanyNameEn = c.CompanyNameEn,
                              CompanyNameTh = c.CompanyNameTh,
                              ContactNumber1 = c.ContactNumber1,
                              ContactNumber2 = c.ContactNumber2,
                              Email = c.Email,
                              FacebookUrl = c.FacebookUrl,
                              InstagramUrl = c.InstagramUrl,
                              LineId = c.LineId,
                              LineUrl = c.LineUrl,
                              WeChatId = c.WeChatId,
                              WhatsAppId = c.WhatsAppId,
                              YoutubeUrl = c.YoutubeUrl                              
                          }).FirstOrDefaultAsync() ?? new GetContactWebRes();
        }

        public async Task Update(Contact data)
        {
            var a = await db.Contact.FirstOrDefaultAsync();

            if (a != null)
            {
                if (a.CompanyNameEn != data.CompanyNameEn || a.CompanyNameTh != data.CompanyNameTh || a.AddressEn != data.AddressEn || a.AddressTh != data.AddressTh ||
                    a.ContactNumber1 != data.ContactNumber1 || a.ContactNumber2 != data.ContactNumber2 || a.Email != data.Email || a.LineId != data.LineId || a.WeChatId != data.WeChatId ||
                    a.WhatsAppId != data.WhatsAppId || a.LineUrl != data.LineUrl || a.YoutubeUrl != data.YoutubeUrl || a.FacebookUrl != data.FacebookUrl || a.InstagramUrl != data.InstagramUrl)
                {
                    a.CompanyNameEn = data.CompanyNameEn;
                    a.CompanyNameTh = data.CompanyNameTh;
                    a.AddressEn = data.AddressEn;
                    a.AddressTh = data.AddressTh;
                    a.ContactNumber1 = data.ContactNumber1;
                    a.ContactNumber2 = data.ContactNumber2;
                    a.Email = data.Email;
                    a.LineId = data.LineId;
                    a.WeChatId = data.WeChatId;
                    a.WhatsAppId = data.WhatsAppId;
                    a.LineUrl = data.LineUrl;
                    a.YoutubeUrl = data.YoutubeUrl;
                    a.FacebookUrl = data.FacebookUrl;
                    a.InstagramUrl = data.InstagramUrl;
                    a.UpdateDate = data.UpdateDate;
                    a.UpdateBy = data.UpdateBy;
                }

            }
            else
            {
                await db.AddAsync(data);
            }

            await db.SaveChangesAsync();
        }



    }
}
