using System.ComponentModel.DataAnnotations.Schema;

namespace TST.DataAccess.Entities
{
    public class Slide
    {
        public int Id { get; set; }
        [Column(TypeName = "tinytext")]
        public string Image { get; set; }
        [Column(TypeName = "tinytext")]
        public string KeywordEn { get; set; }
        [Column(TypeName = "tinytext")]
        public string KeywordTh { get; set; }
        public int Ranking { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
