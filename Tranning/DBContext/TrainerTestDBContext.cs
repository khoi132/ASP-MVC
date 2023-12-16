using Microsoft.EntityFrameworkCore;
using Tranning.DataDBContext;

namespace Tranning.DBContext
{
    public class TrainerTestDBContext : DbContext
    {
        public TrainerTestDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<TrainerTestCourses> Courses { get; set; }
        public DbSet<TrainerTestTopics> TrainerTestTopics { get; set; }
        public DbSet<TrainerTestTopics> Topics { get; set; }
        public DbSet<TrainerTestRoles> TrainerTestRoles { get; set; }
        public DbSet<TrainerTestCategories> TrainerTestCategories { get; set; }
        public DbSet<TrainerTestUsers> TrainerTestUsers { get; set; }
        public DbSet<TrainerTestTrainnerTopics> TrainerTestTrainnerTopics { get; set; }

    }

    public class TrainerTestTrainnerTopics
    {
    }
}
