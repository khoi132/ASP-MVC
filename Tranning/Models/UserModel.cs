using System.ComponentModel.DataAnnotations;
using Tranning.Validations;

namespace Tranning.Models
{
    public class UserModel
    {
        public List<UserDetail> UserDetailLists { get; set; }
        public List<TopicDetail> TopicDetailLists { get; set; }
    }

    public class UserDetail
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Enter Role, please")]
        public int role_id { get; set; }
        public string? namerole { get; set; }
        public string? namecourse { get;set; }
        public DateTime end_date { get; set; }
        public DateTime start_date { get; set;}
        public string? avatarcourse {  get; set; }    
        public string? description { get; set; }

        [Required(ErrorMessage = "Enter extra_code, please")]
        public string? extra_code { get; set; }


        [Required(ErrorMessage = "Enter username, please")]
        public string? username { get; set; }
        [Required(ErrorMessage = "Enter password, please")]
        public string? password { get; set; }
        [Required(ErrorMessage = "Enter email, please")]
        public string? email { get; set; }
        [Required(ErrorMessage = "Enter phone, please")]
        public string? phone { get; set; }
        [Required(ErrorMessage = "Enter full name, please")]
        public string? full_name { get; set; }





        public string? avatar { get; set; }

        [Required(ErrorMessage = "Choose Status, please")]
        public string? status { get; set; }
        [Required(ErrorMessage = "Choose Gender, please")]
        public string? gender { get; set; }

        [Required(ErrorMessage = "Choose file, please")]
        [AllowedExtensionFile(new string[] { ".png", ".jpg", ".jpeg" })]
        [AllowedSizeFile(5 * 1024 * 1024)]
        public IFormFile? Photo { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        public DateTime? deleted_at { get; set; }
    }
}
