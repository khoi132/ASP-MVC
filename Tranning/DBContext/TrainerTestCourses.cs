using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tranning.DBContext
{
    public class TrainerTestCourses
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId  { get;set; }
        public TrainerTestCategories? TrainerTestCategories {  get; set; }//kieu khoa ngoai
            
        [Column("NameCourse", TypeName = "Varchar(50)")]

        public string? NameCourse { get; set; }
        [Column("Description", TypeName = "Varchar(200)")]

        public string? Description { get; set; }

        [Column("Startdate", TypeName = "DateTime")]
        public DateTime? StartDate { get; set; }

        [Column("Enddate", TypeName = "DateTime")]
        public DateTime? Enddate { get; set; }

        [Column("Vote", TypeName = "Integer")]
        public int Vote { get; set; }

        [Column("Avatar", TypeName = "Varchar(200)")]
        public String? Avatar { get; set; }

        [Column("Status", TypeName = "Varchar(50)")]
        public string? Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        

    }
}
