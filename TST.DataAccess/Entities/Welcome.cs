using System.ComponentModel.DataAnnotations.Schema;

namespace TST.DataAccess.Entities
{
    public class Welcome
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(80)")]
        public string SlideText1En { get; set; }
        [Column(TypeName = "varchar(80)")]
        public string SlideText1Th { get; set; }
        [Column(TypeName = "varchar(80)")]
        public string SlideText2En { get; set; }
        [Column(TypeName = "varchar(80)")]
        public string SlideText2Th { get; set; }
        [Column(TypeName = "varchar(80)")]
        public string SlideText3En { get; set; }
        [Column(TypeName = "varchar(80)")]
        public string SlideText3Th { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string HeaderEn { get; set; }
        [Column(TypeName = "tinytext")]
        public string HeaderTh { get; set; }
        [Column(TypeName = "text")]
        public string DetailEn { get; set; }
        [Column(TypeName = "text")]
        public string DetailTh { get; set; }
        [Column(TypeName = "text")]
        public string Image1 { get; set; }
        [Column(TypeName = "tinytext")]
        public string Image2 { get; set; }
        [Column(TypeName = "tinytext")]
        public string Image3 { get; set; }
        [Column(TypeName = "tinytext")]
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
