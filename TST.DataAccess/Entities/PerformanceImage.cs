using System.ComponentModel.DataAnnotations.Schema;

namespace TST.DataAccess.Entities
{
    public class PerformanceImage
    {
        public int Id { get; set; }
        public int PerformanceId { get; set; }
        [Column(TypeName = "tinytext")]
        public string Image { get; set; }
        public int Ranking { get; set; }
        public bool IsDelete { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
