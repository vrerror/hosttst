namespace TST.Core.Models
{
    public class GetPerformanceDtRes : DtRes
    {
        public List<GetPerformanceDtRes2> data { get; set; }
    }

    public class GetPerformanceDtRes2
    {
        public int Id { get; set; }
        public string TypeEn { get; set; }
        public string TypeTh { get; set; }
        public string NameEn { get; set; }
        public string NameTh { get; set; }
        public string TitleEn { get; set; }
        public string TitleTh { get; set; }
        public string Image { get; set; }
        public int Ranking { get; set; }
        public bool IsActive { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
