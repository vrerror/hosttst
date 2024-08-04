using System.ComponentModel.DataAnnotations.Schema;

namespace TST.Core.Models
{
    public class GetUserRes : DtRes
    {
        public List<GetUserRes2> data { get; set; }
    }
    public class GetUserRes2
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstNameEn { get; set; }
        public string FirstNameTh { get; set; }
        public string LastNameEn { get; set; }
        public string LastNameTh { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}