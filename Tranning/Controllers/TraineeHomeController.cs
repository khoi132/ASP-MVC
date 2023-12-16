using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Security.Claims;
using Tranning.DataDBContext;
using Tranning.Models;

namespace Tranning.Controllers
{
    public class TraineeHomeController : Controller
    {

        private readonly ILogger<TraineeHomeController> _logger;
        private readonly TranningDBContext _dbContext;
        public TraineeHomeController(ILogger<TraineeHomeController> logger, TranningDBContext context)
        {
            _logger = logger;
            _dbContext = context;
        }


        [HttpGet]
        public IActionResult Index()
        {
            CourseModel courseModel = new CourseModel();
            courseModel.CourseDetailLists = new List<CourseDetail>();

            var data = from course in _dbContext.Courses
                       join category in _dbContext.Categories on course.category_id equals category.id
                       select new { Course = course, Category = category };

            data = data.Where(m => m.Course.deleted_at == null);


            // Execute the query and materialize the results
            var dataList = data.ToList();

            foreach (var item in data)
            {
                courseModel.CourseDetailLists.Add(new CourseDetail
                {
                    id = item.Course.id,
                    name = item.Course.name,
                    category_id = item.Category.id,
                    namecategory = item.Category.name,
                    description = item.Course.description,
                    avatar = item.Course.avatar,
                    status = item.Course.status,
                    start_date = item.Course.start_date,
                    end_date = item.Course.end_date,
                    created_at = item.Course.created_at,
                    updated_at = item.Course.updated_at
                });
            }
            return View(courseModel);
        }

    }




    //public IActionResult Get(string id)
    //{
    //    if (id.Contains('0'))
    //    {
    //        return StatusCode(StatusCodes.Status406NotAcceptable);
    //    }

    //    return Content(id);
    //}
    //[HttpGet]
    //public IActionResult Index()
    //{
    //    var user_id = User.FindFirstValue(ClaimTypes.NameIdentifier);
    //    UserModel userModel = new UserModel
    //    {
    //        UserDetailLists = new List<UserDetail>()
    //    };

    //    string UserID;

    //    // Check the role to determine the TraineeID
    //    int? roleID = HttpContext.Session.GetInt32("SessionRoleID");
    //    if (roleID.HasValue && roleID.Value == 3)
    //    {
    //        // User has role 3, so use SessionTraineeID
    //        UserID = HttpContext.Session.GetString("SessionUserID);
    //    }
    //    else
    //    {
    //        // User doesn't have role 3, use the provided UserID
    //        traineeID = UserID;
    //    }

    //    // Truy vấn cơ sở dữ liệu để lấy thông tin về khóa học của người dùng hiện tại
    //    var data = from users in _dbContext.Users
    //               join trainee_course in _dbContext.Trainee_Course on users.id equals trainee_course.trainee_id
    //               join courses in _dbContext.Courses on trainee_course.course_id equals courses.id
    //               where trainee_course.deleted_at == null && users.id.ToString() == traineeID
    //               select new { Trainee_Course = trainee_course, User = users, Course = courses };

    //    foreach (var item in data)
    //    {
    //        // Chỉ hiển thị thông tin về course_id của người dùng hiện tại
    //        if (item.User.id.ToString() == traineeID)
    //        {
    //            userModel.UserDetailLists.Add(new UserDetail
    //            {
    //                namecourse = item.Course.name,
    //                avatar = item.Course.avatar,
    //                start_date = item.Course.start_date,
    //                end_date = item.Course.end_date,
    //                description = item.Course.description,
    //            });
    //        }
    //    }

    //    // Kiểm tra nếu có RoleID = 3, lưu SessionUserID là UserID
    //    if (roleID.HasValue && roleID.Value == 3)
    //    {
    //        HttpContext.Session.SetString("SessionUserID", UserID);
    //    }

    //    return View(userModel);
    //}



    //public IActionResult Index()
    //{

    //    if (string.IsNullOrEmpty(HttpContext.Session.GetString("SessionUsername")))
    //    {
    //        return RedirectToAction(nameof(LoginController.Index), "Login");
    //    }
    //    // file index - default file(root file)
    //    // file măc định sẽ chạy trong 1 controller
    //    return View();
    //}

    //public IActionResult Privacy()
    //{
    //    return View();
    //}

    //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //public IActionResult Error()
    //{
    //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    //}
}
