namespace TST.Core.Models
{
    public class GetClientDtRes : DtRes
    {
        public List<GetClientDtRes2> data { get; set; }
    }

    public class GetClientDtRes2
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Keywork { get; set; }
        public int Ranking { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }

        public string NameEn { get; set; }
        public string NameTh { get; set; }
    }
}