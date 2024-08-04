namespace TST.DataAccess.Interfaces
{
    public interface IContactDa
    {
        Task<Contact> Get1();
        Task<GetContactWebRes> GetWebRes();
        Task Update(Contact data);

    }
}
