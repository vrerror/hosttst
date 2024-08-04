using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TST.DataAccess.Entities
{
    public class Client
    {
        public int Id { get; set; }
        [Column(TypeName = "tinytext")]
        public string Image { get; set; }

        [Column(TypeName = "tinytext")]
        public string Keyword { get; set; }
        public int Ranking { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }

        [Column(TypeName = "tinytext")]
        public string NameEn { get; set; }
        [Column(TypeName = "tinytext")]
        public string NameTh { get; set; }


    }
}