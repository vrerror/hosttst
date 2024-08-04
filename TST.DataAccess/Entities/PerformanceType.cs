using System.ComponentModel.DataAnnotations.Schema;

namespace TST.DataAccess.Entities
{
    public class PerformanceType
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string NameEn { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string NameTh { get; set; }
        public int Ranking { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
