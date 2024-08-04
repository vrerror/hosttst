using System.ComponentModel.DataAnnotations.Schema;

namespace TST.DataAccess.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(40)")]
        public string CompanyNameEn { get; set; }
        [Column(TypeName = "varchar(40)")]
        public string CompanyNameTh { get; set; }
        [Column(TypeName = "tinytext")]
        public string AddressEn { get; set; }
        [Column(TypeName = "tinytext")]
        public string AddressTh { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string ContactNumber1 { get; set; }
        [Column(TypeName = "varchar(20)")] 
        public string ContactNumber2 { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string LineId { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string WeChatId { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string WhatsAppId { get; set; }
        [Column(TypeName = "tinytext")]
        public string LineUrl { get; set; }
        [Column(TypeName = "tinytext")]
        public string YoutubeUrl { get; set; }
        [Column(TypeName = "tinytext")]
        public string FacebookUrl { get; set; }
        [Column(TypeName = "tinytext")]
        public string InstagramUrl { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}
