using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
namespace Tranning.DataDBContext
{
    public class Topic
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("course_id")]
        public int course_id { get; set; }

        [Column("name", TypeName = "Varchar(50)"), Required]
        public string? name { get; set; }

        [Column("description", TypeName = "Varchar(200)"), AllowNull]
        public string? description { get; set; }

        [Column("attach_file", TypeName = "Varchar(250)"), AllowNull]
        public string? attach_file { set; get; }
        [Column("videos", TypeName = "Varchar(250)"), AllowNull]
        public string? videos { set; get; }

        [Column("documents", TypeName = "Varchar(250)"), AllowNull]
        public string? documents { set; get; }

        [Column("status", TypeName = "Varchar(50)"), Required]
        public string? status { get; set; }

        [AllowNull]
        public DateTime? created_at { get; set; }
        [AllowNull]
        public DateTime? updated_at { get; set; }
        [AllowNull]
        public DateTime? deleted_at { get; set; }

    }
}
