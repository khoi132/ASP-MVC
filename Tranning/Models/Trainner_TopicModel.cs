using System.ComponentModel.DataAnnotations;
using Tranning.Validations;

namespace Tranning.Models
{
    public class Trainner_TopicModel
    {
        public List<Trainner_TopicDetail> Trainner_TopicDetailLists { get; set; }
    }
    public class Trainner_TopicDetail
         
    {
        [Required(ErrorMessage = "Enter User, please")]
        public int trainner_id { get; set; }

        [Required(ErrorMessage = "Enter Topic, please")]
        public int topic_id{ get; set; }

        public string? name { get; set; }

        public string? full_name { get; set; }
        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        public DateTime? deleted_at { get; set; }
    }
}


