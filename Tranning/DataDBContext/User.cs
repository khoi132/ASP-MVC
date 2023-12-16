using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Tranning.DataDBContext
{
    public class User
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("role_id")]
        public int role_id { get; set; }

        [Column("extra_code", TypeName = "Varchar(50)"), Required]
        public string? extra_code { get; set; }

        [Column("username", TypeName = "Varchar(50)"), Required]
        public string? username { get; set; }

        [Column("password", TypeName = "Varchar(50)"), Required]
        public string? password { get; set; }

        [Column("email", TypeName = "Varchar(50)"), Required]
        public string? email { get; set; }
        [Column("phone", TypeName = "Varchar(20)"), Required]
        public string? phone { get; set; }

        [Column("avatar", TypeName = "Varchar(200)"), AllowNull]
        public string? avatar { set; get; }

        [Column("gender", TypeName = "Varchar(50)"), Required]
        public string? gender { set; get; }
        [Column("status", TypeName = "Varchar(50)"), Required]
        public string? status { get; set; }
        [Column("full_name", TypeName = "Varchar(50)"), Required]
        public string? full_name { get; set; }

        [AllowNull]
        public DateTime? created_at { get; set; }
        [AllowNull]
        public DateTime? updated_at { get; set; }
        [AllowNull]
        public DateTime? deleted_at { get; set; }

    }
}
