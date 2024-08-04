using System.ComponentModel.DataAnnotations.Schema;

namespace TST.Core.Models
{
    public class GetWelcomeDtRes 
    {
        public List<GetWelcomeDtRes2> data { get; set; }
    }

    public class GetWelcomeDtRes2
    {
        public int Id { get; set; }
        public string SlideText1En { get; set; }
        public string SlideText1Th { get; set; }
        public string SlideText2En { get; set; }
        public string SlideText2Th { get; set; }
        public string HeaderEn { get; set; }
        public string HeaderTh { get; set; }
        public string DetailEn { get; set; }
        public string DetailTh { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}