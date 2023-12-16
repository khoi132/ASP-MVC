using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tranning.DBContext
{
    public class TrainerTestUsers
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("RoleId")]
        public int RoleId { get; set; }
        public TrainerTestRoles? TrainerTestRoles { get; set; }//kieu khoa ngoai

        [Column("Extra_code", TypeName = "Varchar(50)")]

        public string? Extra_code { get; set; }
        [Column("UserName", TypeName = "Varchar(50)")]

        public string? UserName { get; set; }

        [Column("Password", TypeName = "Varchar(50)")]

        public string? Password { get; set; }
        [Column("Gender", TypeName = "Varchar(50)")]
        public String? Gender { get; set; }

        [Column("Avatar", TypeName = "Varchar(200)")]
        public String? Avatar { get; set; }

        [Column("Status", TypeName = "Varchar(50)")]
        public String? Status { get; set; }
        [Column("Full_name", TypeName = "Varchar(50)")]
        public String? Full_name { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }


    }
}
