using System.ComponentModel.DataAnnotations.Schema;

namespace TST.DataAccess.Entities
{
    public class Performance
    {
        public int Id { get; set; }
        public int PerformanceTypeId { get;set; }
        [Column(TypeName = "varchar(100)")]
        public string NameEn { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string NameTh { get; set; }
        [Column(TypeName = "varchar(120)")]
        public string TitleEn { get; set; }
        [Column(TypeName = "varchar(120)")]
        public string TitleTh { get; set; }
        [Column(TypeName = "tinytext")]
        public string ShortDetailEn { get; set; }
        [Column(TypeName = "tinytext")]
        public string ShortDetailTh { get; set; }
        [Column(TypeName = "text")]
        public string DetailEn { get; set; }
        [Column(TypeName = "text")]
        public string DetailTh { get; set; }
        [Column(TypeName = "tinytext")]
        public string YoutubeUrl { get; set; }
        public int Ranking { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }

        [NotMapped]
        public string Rankings { get; set; }
    }
}
