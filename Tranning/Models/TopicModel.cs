using System.ComponentModel.DataAnnotations;
using Tranning.Validations;

namespace Tranning.Models
{
    public class TopicModel
    {
        public List<TopicDetail> TopicDetailLists { get; set; }
    }
    public class TopicDetail
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Enter Course, please")]
        public int course_id { get; set; }

        [Required(ErrorMessage = "Enter name, please")]
        public string? name { get; set; }
        public string? namecourse { get; set; }

        public string? description { get; set; }



        [Required(ErrorMessage = "Choose Status, please")]
        public string? status { get; set; }

        
        public string? videos { get; set; }
        [Required(ErrorMessage = "Please choose a file.")]
        [AllowedExtensionFile(new string[] { ".mp4", ".mov", ".avi" })]
        [AllowedSizeFile(70 * 1024 * 1024)]
        public IFormFile photo { get; set; }
        public string? documents { get; set; }

        [Required(ErrorMessage = "Please choose a file.")]
        [AllowedExtensionFile(new string[] { ".doc", ".docx", ".pdf" })]
        [AllowedSizeFile(50 * 1024 * 1024)]
        public IFormFile document_file { get; set; }
        public string? attach_file { get; set; }

        [Required(ErrorMessage = "Please choose a file.")]
        [AllowedExtensionFile(new string[] { ".zip", ".rar" })]
        [AllowedSizeFile(8 * 1024 * 1024)]
        public IFormFile file { get; set; }

      

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        public DateTime? deleted_at { get; set; }
    }
}


