using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TST.Core.Models
{
    public class GetSlideDtRes : DtRes
    {
        public List<GetSlideDtRes2> data { get; set; }
    }

    public class GetSlideDtRes2
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string KeywordEn { get; set; }
        public string KeywordTh { get; set; }
        public int Ranking { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }

}
