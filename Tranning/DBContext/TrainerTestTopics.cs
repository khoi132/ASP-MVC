using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Tranning.DBContext
{
    public class TrainerTestTopics
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("CourseId")]
        public int CourseId { get; set; }
        public TrainerTestCourses? TrainerTestCourses { get; set; }//kieu khoa ngoai

        [Column("NameCourse", TypeName = "Varchar(50)")]

        public string? NameTopic { get; set; }
        [Column("Description", TypeName = "Varchar(100)")]

        public string? Description { get; set; }


        [Column("attack_file", TypeName = "Varchar(200)")]
        public String? attack_file { get; set; }

        [Column("Status", TypeName = "Varchar(50)")]
        public int Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }


    }
}

