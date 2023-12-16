using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Tranning.DataDBContext;

namespace Tranning.DBContext
{
    public class TrainerTestTrainnerTopic
    {
        public class Trainner_Topic
        {
            [Key]
            [ForeignKey("TrainnerId")]
            public int Id { get; set; }
            public TrainerTestUsers? TrainerTestUsers { get; set; }//kieu khoa ngoai
            [Column("NameUser", TypeName = "Varchar(50)")]
            public string? NameUser { get; set; }

            [ForeignKey("TopicId")]
            public int TopicId { get; set; }
            public TrainerTestTopics? TrainerTestTopic { get; set; }//kieu khoa ngoai

            [Column("NameTopic", TypeName = "Varchar(50)")]

            public string? NameTopic { get; set; }
          

            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
            public DateTime DeletedAt { get; set; }

        }
    }

}
