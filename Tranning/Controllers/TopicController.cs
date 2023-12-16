using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tranning.DataDBContext;
using Tranning.Models;
using Tranning.DBContext;

namespace Tranning.Controllers
{
    public class TopicController : Controller
    {
        private readonly TranningDBContext _dbContext;
        private readonly ILogger<TopicController> _logger;

        public TopicController(TranningDBContext context, ILogger<TopicController> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        //[HttpGet]
        //public IActionResult Index(string searchString)
        //{
        //    TopicModel topicModel = new TopicModel();
        //    topicModel.TopicDetailLists = new List<TopicDetail>();

        //    var data = from topic in _dbContext.Topics
        //               join course in _dbContext.Courses on topic.course_id equals course.id
        //               select new { Course = course, Topic = topic };

        //    // Apply additional conditions
        //    data = data.Where(m => m.Topic.deleted_at == null);

        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        data = data.Where(m => m.Topic.name.Contains(searchString) || m.Topic.description.Contains(searchString));
        //    }

        //    // Execute the query and materialize the results
        //    var dataList = data.ToList();

        //    foreach (var item in dataList)
        //    {
        //        topicModel.TopicDetailLists.Add(new TopicDetail
        //        {
        //            id = item.Topic.id,
        //            course_id = item.Topic.course_id,
        //            namecourse = item.Course.name, // Replace CourseName with the actual property name
        //            name = item.Topic.name,
        //            description = item.Topic.description,
        //            attach_file = item.Topic.attach_file,
        //            videos = item.Topic.videos,
        //            documents = item.Topic.documents,
        //            status = item.Topic.status,
        //            created_at = item.Topic.created_at,
        //            updated_at = item.Topic.updated_at
        //        });
        //    }

        //    ViewData["CurrentFilter"] = searchString;
        //    return View(topicModel);
        //}


        //[HttpGet]
        //public IActionResult Add()
        //{
        //    TopicDetail topic = new TopicDetail();
        //    PopulateCategoryDropdown();

        //    return View(topic);
        //}

        [HttpGet]
        public IActionResult Index(string searchString)
        {
            TopicModel topicModel = new TopicModel();
            topicModel.TopicDetailLists = new List<TopicDetail>();

            var data = _dbContext.Topics
                .Where(topic => topic.deleted_at == null)
                .Join(
                    _dbContext.Courses,
                    topic => topic.course_id,
                    course => course.id,
                    (topic, course) => new { Course = course, Topic = topic }
                );

            if (!string.IsNullOrEmpty(searchString))
            {
                data = data.Where(item =>
                    item.Topic.name.Contains(searchString) || item.Topic.description.Contains(searchString)
                );
            }

            var dataList = data.ToList();

            foreach (var item in dataList)
            {
                topicModel.TopicDetailLists.Add(new TopicDetail
                {
                    id = item.Topic.id,
                    course_id = item.Topic.course_id,
                    namecourse = item.Course.name, // Replace CourseName with the actual property name
                    name = item.Topic.name,
                    description = item.Topic.description,
                    attach_file = item.Topic.attach_file,
                    videos = item.Topic.videos,
                    documents = item.Topic.documents,
                    status = item.Topic.status,
                    created_at = item.Topic.created_at,
                    updated_at = item.Topic.updated_at
                });
            }

            ViewData["CurrentFilter"] = searchString;
            return View(topicModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            TopicDetail topic = new TopicDetail();
            PopulateCategoryDropdown();

            return View(topic);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Add(TopicDetail topic)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                string VideoFileName = await UploadVideo(topic.photo);
        //                string AttachFileName = await UploadAttachFile(topic.file);
        //                string DocumentName = await UploadDocuments(topic.document_file);
        //                var topicData = new Topic()
        //                {
        //                    course_id = topic.course_id,
        //                    name = topic.name,
        //                    description = topic.description,
        //                    videos = VideoFileName,
        //                    status = topic.status,
        //                    documents = DocumentName,
        //                    attach_file = AttachFileName,
        //                    created_at = DateTime.Now
        //                };

        //                _dbContext.Topics.Add(topicData);
        //                _dbContext.SaveChanges();
        //                TempData["saveStatus"] = true;
        //            }
        //            catch (Exception ex)
        //            {
        //                _logger.LogError(ex, "An error occurred while processing a valid model state.");
        //                TempData["saveStatus"] = false;
        //            }
        //            return RedirectToAction(nameof(TopicController.Index), "Topic");
        //        }

        //        foreach (var modelState in ModelState.Values)
        //        {
        //            foreach (var error in modelState.Errors)
        //            {
        //                _logger.LogError($"ModelState Error: {error.ErrorMessage}");
        //            }
        //        }
        //        PopulateCategoryDropdown();
        //        return View(topic);

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An unexpected error occurred while processing the request.");
        //        TempData["saveStatus"] = false;
        //        return RedirectToAction(nameof(TopicController.Index), "Topic");

        //    }
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(TopicDetail topic)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        _logger.LogError($"ModelState Error: {error.ErrorMessage}");
                    }
                }
                PopulateCategoryDropdown();
                return View(topic);
            }

            try
            {
                string VideoFileName = await UploadVideo(topic.photo);
                string AttachFileName = await UploadAttachFile(topic.file);
                string DocumentName = await UploadDocuments(topic.document_file);

                var topicData = new Topic()
                {
                    course_id = topic.course_id,
                    name = topic.name,
                    description = topic.description,
                    videos = VideoFileName,
                    status = topic.status,
                    documents = DocumentName,
                    attach_file = AttachFileName,
                    created_at = DateTime.Now
                };

                _dbContext.Topics.Add(topicData);
                await _dbContext.SaveChangesAsync();

                TempData["saveStatus"] = true;
                return RedirectToAction(nameof(TopicController.Index), "Topic");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing a valid model state.");
                TempData["saveStatus"] = false;
                return RedirectToAction(nameof(TopicController.Index), "Topic");
            }
        }

        private void PopulateCategoryDropdown()
        {
            try
            {
                var courses = _dbContext.Courses
                    .Where(m => m.deleted_at == null)
                    .Select(m => new SelectListItem { Value = m.id.ToString(), Text = m.name })
                    .ToList();

                if (courses != null)
                {
                    ViewBag.Stores = courses;
                }
                else
                {
                    _logger.LogError("Course is null");
                    ViewBag.Stores = new List<SelectListItem>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while populating category dropdown.");
                ViewBag.Stores = new List<SelectListItem>();
            }
        }

        //private async Task<string> UploadDocuments(IFormFile file)
        //{
        //    try
        //    {
        //        string uploadPath = GetFilePath("uploads\\documents", file.FileName);
        //        await SaveFileAsync(file, uploadPath);
        //        return Path.GetFileName(uploadPath);
        //    }
        //    catch (Exception ex)
        //    {
        //        return HandleFileUploadError(ex);
        //    }
        //}

        //private async Task<string> UploadVideo(IFormFile file)
        //{
        //    try
        //    {
        //        string uploadPath = GetFilePath("uploads\\videos", file.FileName);
        //        await SaveFileAsync(file, uploadPath);
        //        return Path.GetFileName(uploadPath);
        //    }
        //    catch (Exception ex)
        //    {
        //        return HandleFileUploadError(ex);
        //    }
        //}

        //private async Task<string> UploadAttachFile(IFormFile file)
        //{
        //    try
        //    {
        //        string uploadPath = GetFilePath("uploads\\attachfiles", file.FileName);
        //        await SaveFileAsync(file, uploadPath);
        //        return Path.GetFileName(uploadPath);
        //    }
        //    catch (Exception ex)
        //    {
        //        return HandleFileUploadError(ex);
        //    }
        //}

        //private string GetFilePath(string relativePath, string fileName)
        //{
        //    string uniqueStr = Guid.NewGuid().ToString();
        //    string uniqueFileName = $"{uniqueStr}-{Path.GetFileName(fileName)}";
        //    return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath, uniqueFileName);
        //}

        //private async Task SaveFileAsync(IFormFile file, string uploadPath)
        //{
        //    using (var stream = new FileStream(uploadPath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }
        //}

        //private string HandleFileUploadError(Exception ex)
        //{
        //    return ex.Message.ToString();
        //}
        private async Task<string> UploadDocuments(IFormFile file)
        {
            string DocumentFileName;
            try
            {
                string pathUploadServer = "wwwroot\\uploads\\documents";
                string documentName = file.FileName;
                documentName = Path.GetFileName(documentName);
                string uniqueStr = Guid.NewGuid().ToString();
                documentName = uniqueStr + "-" + documentName;
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), pathUploadServer, documentName);
                var stream = new FileStream(uploadPath, FileMode.Create);
                await file.CopyToAsync(stream);
                DocumentFileName = documentName;
            }
            catch (Exception ex)
            {
                DocumentFileName = ex.Message.ToString();
            }
            return DocumentFileName;
        }
        private async Task<string> UploadVideo(IFormFile file)
        {
            string VideoFileName;
            try
            {
                string pathUploadServer = "wwwroot\\uploads\\videos";
                string videoName = file.FileName;
                videoName = Path.GetFileName(videoName);
                string uniqueStr = Guid.NewGuid().ToString();
                videoName = uniqueStr + "-" + videoName;
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), pathUploadServer, videoName);
                var stream = new FileStream(uploadPath, FileMode.Create);
                await file.CopyToAsync(stream);
                VideoFileName = videoName;
            }
            catch (Exception ex)
            {
                VideoFileName = ex.Message.ToString();
            }
            return VideoFileName;
        }
        private async Task<string> UploadAttachFile(IFormFile file)
        {
            string AttachFileName;
            try
            {
                string pathUploadServer = "wwwroot\\uploads\\attachfiles";
                string attachfileName = file.FileName;
                attachfileName = Path.GetFileName(attachfileName);
                string uniqueStr = Guid.NewGuid().ToString();
                attachfileName = uniqueStr + "-" + attachfileName;
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), pathUploadServer, attachfileName);
                var stream = new FileStream(uploadPath, FileMode.Create);
                await file.CopyToAsync(stream);
                AttachFileName = attachfileName;
            }
            catch (Exception ex)
            {
                AttachFileName = ex.Message.ToString();
            }
            return AttachFileName;
        }
        [HttpGet]
        public IActionResult Update(int id = 0)
        {
            TopicDetail topic = new TopicDetail();
            var courseList = _dbContext.Courses
                .Where(m => m.deleted_at == null)
                .Select(m => new SelectListItem { Value = m.id.ToString(), Text = m.name })
                .ToList();
            ViewBag.Stores = courseList;

            // Fetch course data by id
            var data = _dbContext.Topics.FirstOrDefault(m => m.id == id);
            if (data != null)
            {
                topic.id = data.id;
                topic.name = data.name;
                topic.course_id = data.course_id;
                topic.attach_file = data.attach_file;
                topic.videos = data.videos;
                topic.documents = data.documents;
                topic.description = data.description;
                topic.status = data.status;


            }

            return View(topic);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Update(TopicDetail topic, IFormFile file)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var data = _dbContext.Topics.FirstOrDefault(m => m.id == topic.id);



        //            if (data != null)
        //            {
        //                data.name = topic.name;
        //                data.description = topic.description;
        //                data.status = topic.status;
        //                data.course_id = topic.course_id;

        //                // Update the file fields if a new file is provided
        //                if (topic.file != null)
        //                {
        //                    data.attach_file = await UploadAttachFile(topic.file);
        //                }

        //                if (topic.photo != null)
        //                {
        //                    data.videos = await UploadVideo(topic.photo);
        //                }

        //                if (topic.documents != null)
        //                {
        //                    data.documents = await UploadDocuments(topic.document_file);
        //                }

        //                data.updated_at = DateTime.Now;

        //                _dbContext.SaveChanges();
        //                TempData["UpdateStatus"] = true;
        //            }
        //            else
        //            {
        //                TempData["UpdateStatus"] = false;
        //            }

        //            return RedirectToAction(nameof(TopicController.Index), "Topic");
        //        }

        //        // If ModelState is not valid, repopulate the dropdown and return to the view
        //        PopulateCategoryDropdown();
        //        return View(topic);
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["UpdateStatus"] = false;
        //        // Log the exception if needed: _logger.LogError(ex, "An error occurred while updating the topic.");
        //        return RedirectToAction(nameof(Index));
        //    }
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(TopicDetail topic, IFormFile file)
        {
            try
            {
                string videoFileName = "";
                string attachFileName = "";
                string documentName = "";

                if (topic.photo != null)
                {
                    videoFileName = await UploadVideo(topic.photo);
                }

                if (topic.file != null)
                {
                    attachFileName = await UploadAttachFile(topic.file);
                }

                if (topic.document_file != null)
                {
                    documentName = await UploadDocuments(topic.document_file);
                }

                var data = _dbContext.Topics.FirstOrDefault(m => m.id == topic.id);

                if (data != null)
                {
                    data.name = topic.name;
                    data.description = topic.description;
                    data.status = topic.status;
                    data.course_id = topic.course_id;
                    data.updated_at = DateTime.Now;

                    // Update the file fields only if a new file is provided
                    // Handle file upload
                    if (!string.IsNullOrEmpty(videoFileName))
                    {
                        data.videos = videoFileName;
                    }

                    if (!string.IsNullOrEmpty(attachFileName))
                    {
                        data.attach_file = attachFileName;
                    }

                    if (!string.IsNullOrEmpty(documentName))
                    {
                        data.documents = documentName;
                    }

                    _dbContext.SaveChanges();

                    TempData["UpdateStatus"] = true;

                }
                else
                {
                    TempData["UpdateStatus"] = false;
                }
                // If ModelState is not valid, repopulate the dropdown and return to the view
                PopulateCategoryDropdown();
                return RedirectToAction(nameof(TopicController.Index), "Topic");
            }
            
            catch (Exception ex)
            {
                TempData["UpdateStatus"] = false;
                // Log the exception if needed: _logger.LogError(ex, "An error occurred while updating the topic.");
                return RedirectToAction(nameof(TopicController.Index), "Topic");
            }
        }
        


        [HttpGet]
        public IActionResult Delete(int id = 0)
        {
            try
            {
                var data = _dbContext.Topics.Where(m => m.id == id).FirstOrDefault();
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
            return RedirectToAction(nameof(TopicController.Index), "Topic");
        }
    }
}
