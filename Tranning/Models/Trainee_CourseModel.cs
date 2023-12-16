using System.ComponentModel.DataAnnotations;
using Tranning.Validations;

namespace Tranning.Models
{
    public class Trainee_CourseModel
    {
        public List<Trainee_CourseDetail> Trainee_CourseDetailLists { get; set; }
    }
    public class Trainee_CourseDetail

    {
        [Required(ErrorMessage = "Enter User, please")]
        public int trainee_id { get; set; }

        [Required(ErrorMessage = "Enter Topic, please")]
        public int course_id { get; set; }

        public string? name { get; set; }
        
        public string? full_name { get; set; }
        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        public DateTime? deleted_at { get; set; }
    }
}


