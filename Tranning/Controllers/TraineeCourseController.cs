using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tranning.DataDBContext;
using Tranning.Models;

namespace Tranning.Controllers
{
    public class TraineeCourseController : Controller
    {
        private readonly TranningDBContext _dbContext;

        public TraineeCourseController(TranningDBContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public IActionResult Index(string SearchString)
        {
            // Use camelCase for parameter names
            Trainee_CourseModel traineecourseModel = new Trainee_CourseModel();
            traineecourseModel.Trainee_CourseDetailLists = new List<Trainee_CourseDetail>();

            //var data = from m in _dbContext.Trainner_Topic
            //           select m;
            var data = from trainee_course in _dbContext.Trainee_Course
                       join users in _dbContext.Users on trainee_course.trainee_id equals users.id
                       join courses in _dbContext.Courses on trainee_course.course_id equals courses.id
                       select new { Trainee_Course = trainee_course, User = users, Course = courses };
            
            data = data.Where(m => m.Trainee_Course.deleted_at == null);
            if (!string.IsNullOrEmpty(SearchString))
            {
                data = data.Where(m => m.User.full_name.Contains(SearchString) || m.Course.name.Contains(SearchString));
            }
            data.ToList();

            foreach (var item in data)
            {
                // Use object initialization for better readability
                traineecourseModel.Trainee_CourseDetailLists.Add(new Trainee_CourseDetail
                {
                    trainee_id = item.Trainee_Course.trainee_id,
                    course_id = item.Trainee_Course.course_id,
                    created_at = item.Trainee_Course.created_at,
                    
                    updated_at = item.Trainee_Course.updated_at,
                    full_name = item.User.full_name,
                    name = item.Course.name,
                });

            }



            return View(traineecourseModel);
        }
        [HttpGet]
        public IActionResult Add()
        {
            Trainee_CourseDetail trainee_course = new Trainee_CourseDetail();

            var traineecourseList = _dbContext.Courses
                .Where(m => m.deleted_at == null)
                .Select(m => new SelectListItem { Value = m.id.ToString(), Text = m.name })
                .ToList();

            var userList = _dbContext.Users
                .Where(u => u.deleted_at == null && u.role_id == 3)
                .Select(u => new SelectListItem { Value = u.id.ToString(), Text = u.full_name })
                .ToList();

            ViewBag.Stores = traineecourseList;
            ViewBag.Users = userList;

            return View(trainee_course);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Trainee_CourseDetail trainee_course)
        {
            if (ModelState.IsValid && trainee_course != null)
            {
                try
                {
                    var traineecourseData = new Trainee_Course()
                    {
                        trainee_id = trainee_course.trainee_id,
                        course_id = trainee_course.course_id,
                        created_at = DateTime.UtcNow // Use DateTime.UtcNow directly
                    };

                    _dbContext.Trainee_Course.Add(traineecourseData);
                    await _dbContext.SaveChangesAsync(); // Use asynchronous SaveChangesAsync
                    TempData["saveStatus"] = true;
                }
                catch (Exception ex)
                {
                    // Log the exception details for debugging
                    // Logger.LogError(ex, "An error occurred while adding TrainerTopic.");
                    TempData["saveStatus"] = false;
                }

                return RedirectToAction(nameof(TraineeCourseController.Index), "TraineeCourse");
            }

            // Model is not valid or trainertopic is null, return to the Add view
            var traineecourseList = _dbContext.Courses
                .Where(m => m.deleted_at == null)
                .Select(m => new SelectListItem { Value = m.id.ToString(), Text = m.name })
                .ToList();

            var userList = _dbContext.Users
                .Where(u => u.deleted_at == null && u.role_id == 4)
                .Select(u => new SelectListItem { Value = u.id.ToString(), Text = u.full_name })
                .ToList();

            ViewBag.Stores = traineecourseList;
            ViewBag.Users = userList;

            // Print ModelState.IsValid to the console for debugging
            Console.WriteLine(ModelState.IsValid);

            return View(trainee_course);
        }
        [HttpGet]
        public IActionResult Delete(int id = 0)
        {
            try
            {
  ;
                var data = _dbContext.Trainee_Course.Where(m => m.trainee_id == id).FirstOrDefault();
                if (data != null)
                {
                    data.deleted_at = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    _dbContext.SaveChanges(true);
                    TempData["DeleteStatus"] = true;
                }
                else
                {
                    TempData["DeleteStatus"] = false;
                }
            }
            catch
            {
                TempData["DeleteStatus"] = false;
            }
            return RedirectToAction(nameof(TraineeCourseController.Index), "TraineeCourse");
        }
        [HttpGet]
        public IActionResult Update(int id = 0)
        {
            Trainee_CourseDetail trainee_course = new Trainee_CourseDetail();

          
            var traineecourseList = _dbContext.Courses
                .Where(m => m.deleted_at == null)
                .Select(m => new SelectListItem { Value = m.id.ToString(), Text = m.name })
                .ToList();

            var userList = _dbContext.Users
                .Where(u => u.deleted_at == null && u.role_id == 3)
                .Select(u => new SelectListItem { Value = u.id.ToString(), Text = u.full_name })
                .ToList();

            var data = _dbContext.Trainee_Course.Where(m => m.trainee_id == trainee_course.trainee_id).FirstOrDefault();
            //if (data != null)
            //{
            //    trainee_course.trainee_id = data.trainee_id;
            //    trainee_course.course_id = data.course_id;
            //}
            ViewBag.Stores = traineecourseList;
            ViewBag.Users = userList;

            return View(trainee_course);
        }

        [HttpPost]
        public IActionResult Update(Trainee_CourseDetail trainee_course)
        {
            try
            {

                var data = _dbContext.Trainee_Course.Where(m => m.trainee_id == trainee_course.trainee_id).FirstOrDefault();
               

                if (data != null)
                {
                    // gan lai du lieu trong db bang du lieu tu form model gui len
                    data.trainee_id = trainee_course.trainee_id;
                    data.course_id = trainee_course.course_id;
                    
                    _dbContext.SaveChanges(true);
                    TempData["UpdateStatus"] = true;
                }
                else
                {
                    TempData["UpdateStatus"] = false;
                }
            }
            catch (Exception ex)
            {
                TempData["UpdateStatus"] = false;
            }
            return RedirectToAction(nameof(TraineeCourseController.Index), "TraineeCourse");
        }
    }
}

