
using System.ComponentModel.DataAnnotations.Schema;

namespace TST.Core.Models
{
    public class GetClientWebRes
    {
        [Column(TypeName = "tinytext")]
        public string NameEn { get; set; }

        [Column(TypeName = "tinytext")]
        public string NameTh { get; set; }

        [Column(TypeName = "tinytext")]
        public string Image { get; set; }
    }
}
