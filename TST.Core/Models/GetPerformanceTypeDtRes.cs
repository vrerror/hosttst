namespace TST.Core.Models
{
    public class GetPerformanceTypeDtRes : DtRes
    {
        public List<GetPerformanceTypeDtRes2> data { get; set; }
    }

    public class GetPerformanceTypeDtRes2
    {
        public int Id { get; set; }
        public string NameEn { get; set; }
        public string NameTh { get; set; }
        public int Ranking { get; set; }
        public bool IsActive { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
