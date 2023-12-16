using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Tranning.DataDBContext
{
    public class Trainner_Topic
    {
        [Key]
        [ForeignKey("user_id")]
        public int trainner_id { get; set; }

        [ForeignKey("topic_id")]
        public int topic_id { get; set; }

        [AllowNull]
        public DateTime? created_at { get; set; }
        [AllowNull]
        public DateTime? updated_at { get; set; }
        [AllowNull]
        public DateTime? deleted_at { get; set; }

    }
}
