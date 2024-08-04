namespace TST.Core.Models
{
    public class GetPerformanceImageDtRes : DtRes
    {
        public List<GetPerformanceImageDtRes2> data { get; set; }
    }

    public class GetPerformanceImageDtRes2
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int Ranking { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
