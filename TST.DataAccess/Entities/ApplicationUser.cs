using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TST.DataAccess.Entities
{

    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName = "varchar(30)")]
        public string FirstNameEn { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string FirstNameTh { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string LastNameEn { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string LastNameTh { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }


        [NotMapped]
        public string Password { get; set; }

        [NotMapped]
        public string ConfirmPassword { get; set; }



    }

}
