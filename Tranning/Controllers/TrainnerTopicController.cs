using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tranning.DataDBContext;
using Tranning.Models;

namespace Tranning.Controllers
{
    public class TrainnerTopicController : Controller
    {
        private readonly TranningDBContext _dbContext;

        public TrainnerTopicController(TranningDBContext context)
        {
            _dbContext = context;
        }

        //[HttpGet]
        //public IActionResult Index(string SearchString)
        //{
        //    UserModel userModel = new UserModel();
        //    userModel.UserDetailLists = new List<UserDetail>();

        //    var data = from m in _dbContext.Users select m;

        //    data = data.Where(m => m.deleted_at == null);
        //    if (!string.IsNullOrEmpty(SearchString))
        //    {
        //        data = data.Where(m => m.username.Contains(SearchString) || m.phone.Contains(SearchString));
        //    }
        //    data.ToList();

        //    foreach (var item in data)
        //    {
        //        userModel.UserDetailLists.Add(new UserDetail
        //        {
        //            id = item.id,
        //            username = item.username,

        //            created_at = item.created_at,
        //            updated_at = item.updated_at
        //        });
        //    }
        //    //ViewData["CurrentFilter"] = SearchString;
        //    return View(userModel);
        //}

        [HttpGet]
        public IActionResult Index(string SearchString)
        {
            // Use camelCase for parameter names
            Trainner_TopicModel trainnertopicModel = new Trainner_TopicModel();
            trainnertopicModel.Trainner_TopicDetailLists = new List<Trainner_TopicDetail>();

            //var data = from m in _dbContext.Trainner_Topic
            //           select m;
            var data = from trainner_topic in _dbContext.Trainner_Topic
                       join users in _dbContext.Users on trainner_topic.trainner_id equals users.id
                       join topics in _dbContext.Topics on trainner_topic.topic_id equals topics.id
                       select new { TrainnerTopic = trainner_topic, User = users, Topic = topics };

           
            data = data.Where(m => m.TrainnerTopic.deleted_at == null);
            if (!string.IsNullOrEmpty(SearchString))
            {
                data = data.Where(m => m.User.full_name.Contains(SearchString) || m.Topic.name.Contains(SearchString));
            }
            data.ToList();

            foreach (var item in data)
            {
                // Use object initialization for better readability
                trainnertopicModel.Trainner_TopicDetailLists.Add(new Trainner_TopicDetail
                {
                    trainner_id = item.TrainnerTopic.trainner_id,
                    topic_id = item.TrainnerTopic.topic_id,
                    created_at = item.TrainnerTopic.created_at,
                    updated_at = item.TrainnerTopic.updated_at,
                    full_name = item.User.full_name,
                    name = item.Topic.name,
                });

            }



            return View(trainnertopicModel);
        }
        [HttpGet]
        public IActionResult Add()
        {
            Trainner_TopicDetail trainner_topic = new Trainner_TopicDetail();

            var trainnertopicList = _dbContext.Topics
                .Where(m => m.deleted_at == null)
                .Select(m => new SelectListItem { Value = m.id.ToString(), Text = m.name })
                .ToList();

            var userList = _dbContext.Users
                .Where(u => u.deleted_at == null && u.role_id == 4)
                .Select(u => new SelectListItem { Value = u.id.ToString(), Text = u.full_name })
                .ToList();

            ViewBag.Stores = trainnertopicList;
            ViewBag.Users = userList;

            return View(trainner_topic);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Trainner_TopicDetail trainner_topic)
        {
            if (ModelState.IsValid && trainner_topic != null)
            {
                try
                {
                    var trainnertopicData = new Trainner_Topic()
                    {
                        trainner_id = trainner_topic.trainner_id,
                        topic_id = trainner_topic.topic_id,
                        created_at = DateTime.UtcNow // Use DateTime.UtcNow directly
                    };

                    _dbContext.Trainner_Topic.Add(trainnertopicData);
                    await _dbContext.SaveChangesAsync(); // Use asynchronous SaveChangesAsync
                    TempData["saveStatus"] = true;
                }
                catch (Exception ex)
                {
                    // Log the exception details for debugging
                    // Logger.LogError(ex, "An error occurred while adding TrainerTopic.");
                    TempData["saveStatus"] = false;
                }

                return RedirectToAction(nameof(TrainnerTopicController.Index), "TrainnerTopic");
            }

            // Model is not valid or trainertopic is null, return to the Add view
            var trainnertopicList = _dbContext.Topics
                .Where(m => m.deleted_at == null)
                .Select(m => new SelectListItem { Value = m.id.ToString(), Text = m.name })
                .ToList();

            var userList = _dbContext.Users
                .Where(u => u.deleted_at == null && u.role_id == 4)
                .Select(u => new SelectListItem { Value = u.id.ToString(), Text = u.full_name })
                .ToList();

            ViewBag.Stores = trainnertopicList;
            ViewBag.Users = userList;

            // Print ModelState.IsValid to the console for debugging
            Console.WriteLine(ModelState.IsValid);

            return View(trainner_topic);
        }
        [HttpGet]
        public IActionResult Delete(int id = 0)
        {
            try
            {
                var data = _dbContext.Trainner_Topic.Where(m => m.trainner_id == id).FirstOrDefault();
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
            return RedirectToAction(nameof(TrainnerTopicController.Index), "TrainnerTopic");
        }
        [HttpGet]
        public IActionResult Update(int id = 0)
        {
            Trainner_TopicDetail trainner_topic = new Trainner_TopicDetail();


            var trainnnertopicList = _dbContext.Topics
                .Where(m => m.deleted_at == null)
                .Select(m => new SelectListItem { Value = m.id.ToString(), Text = m.name })
                .ToList();

            var userList = _dbContext.Users
                .Where(u => u.deleted_at == null && u.role_id == 4)
                .Select(u => new SelectListItem { Value = u.id.ToString(), Text = u.full_name })
                .ToList();

            var data = _dbContext.Trainner_Topic.Where(m => m.trainner_id == trainner_topic.trainner_id).FirstOrDefault();
            //if (data != null)
            //{
            //    trainee_course.trainee_id = data.trainee_id;
            //    trainee_course.course_id = data.course_id;
            //}
            ViewBag.Stores = trainnnertopicList;
            ViewBag.Users = userList;

            return View(trainner_topic);
        }

        [HttpPost]
        public IActionResult Update(Trainner_TopicDetail trainner_topic)
        {
            try
            {

                var data = _dbContext.Trainner_Topic.Where(m => m.trainner_id == trainner_topic.trainner_id).FirstOrDefault();


                if (data != null)
                {
                    // gan lai du lieu trong db bang du lieu tu form model gui len
                    data.trainner_id = trainner_topic.trainner_id;
                    data.topic_id = trainner_topic.topic_id;

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
            return RedirectToAction(nameof(TrainnerTopicController.Index), "TrainnerTopic");
        }
    }
}





            // Fetch users where deleted_at is null and populate the ViewBag with the user list
            //var trainertopicList = _dbContext.Topics
            //    .Where(m => m.deleted_at == null)
            //    .Select(m => new SelectListItem { Value = m.id.ToString(), Text = m.name })
            //    .ToList();
            //ViewBag.Stores = trainertopicList;

//// Fetch users where deleted_at is null and user_id is 4
//var userList = _dbContext.Users
//    .Where(u => u.deleted_at == null && u.role_id == 4)
//    .Select(u => new SelectListItem { Value = u.id.ToString(), Text = u.full_name })
//    .ToList();
//ViewBag.Users = userList;




//    [HttpGet]
//        public IActionResult Add()
//        {
//            TrainerTopicDetail trainertopic = new TrainerTopicDetail();
//            var trainertopicList = _dbContext.Topics
//                .Where(m => m.deleted_at == null)
//                .Select(m => new SelectListItem { Value = m.id.ToString(), Text = m.name }).ToList();

//            ViewBag.Stores = trainertopicList;
//            return View(trainertopic);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Add(TrainerTopicDetail trainertopic)
//        {
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    var trainertopicData = new TrainerTopic()
//                    {
//                        user_id = trainertopic.user_id,
//                        topic_id = trainertopic.topic_id,
//                        created_at = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
//                    };

//                    _dbContext.TrainerTopics.Add(trainertopicData);
//                    _dbContext.SaveChanges(true);
//                    TempData["saveStatus"] = true;
//                }
//                catch (Exception ex)
//                {
//                    TempData["saveStatus"] = false;
//                }
//                return RedirectToAction(nameof(TrainerTopicController.Index), "TrainerTopic");
//            }

//            var trainertopicList = _dbContext.Users
//              .Where(m => m.deleted_at == null)
//              .Select(m => new SelectListItem { Value = m.id.ToString(), Text = m.full_name }).ToList();
//            ViewBag.Stores = trainertopicList;
//            Console.WriteLine(ModelState.IsValid);
//            return View(trainertopic);
//        }
//    }
//}


//[HttpGet]
//public IActionResult Update(int id = 0)
//{
//    TopicDetail topic = new TopicDetail();
//    var data = _dbContext.Topics.Where(m => m.id == id).FirstOrDefault();
//    if (data != null)
//    {
//        topic.id = data.id;
//        topic.name = data.name;
//        topic.attack_file = data.attack_file;
//        topic.description = data.description;
//        topic.status = data.status;


//    }

//    return View(topic);
//}

//        [HttpPost]
//        public IActionResult Update(TopicDetail topic, IFormFile file)
//        {
//            try
//            {

//                var data = _dbContext.Topics.Where(m => m.id == topic.id).FirstOrDefault();
//                string uniqueIconAvatar = "";
//                if (topic.Photo != null)
//                {
//                    uniqueIconAvatar = uniqueIconAvatar = UploadFile(topic.Photo);
//                }

//                if (data != null)
//                {
//                    // gan lai du lieu trong db bang du lieu tu form model gui len
//                    data.name = topic.name;
//                    data.description = topic.description;
//                    data.status = topic.status;

//                    if (!string.IsNullOrEmpty(uniqueIconAvatar))
//                    {
//                        data.attack_file = uniqueIconAvatar;
//                    }
//                    _dbContext.SaveChanges(true);
//                    TempData["UpdateStatus"] = true;
//                }
//                else
//                {
//                    TempData["UpdateStatus"] = false;
//                }
//            }
//            catch (Exception ex)
//            {
//                TempData["UpdateStatus"] = false;
//            }
//            return RedirectToAction(nameof(TopicController.Index), "Topic");
//        }
//        [HttpGet]
//        public IActionResult Delete(int id = 0)
//        {
//            try
//            {
//                var data = _dbContext.Topics.Where(m => m.id == id).FirstOrDefault();
//                if (data != null)
//                {
//                    data.deleted_at = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
//                    _dbContext.SaveChanges(true);
//                    TempData["DeleteStatus"] = true;
//                }
//                else
//                {
//                    TempData["DeleteStatus"] = false;
//                }
//            }
//            catch
//            {
//                TempData["DeleteStatus"] = false;
//            }
//            return RedirectToAction(nameof(TopicController.Index), "Topic");
//        }
//    }
//}
