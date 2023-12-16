using Microsoft.AspNetCore.Mvc;
using Tranning.Models;
using Tranning.Queries;

namespace Tranning.Controllers
{
    
    public class LoginController : Controller

    { 
        [HttpGet]
    public IActionResult Index()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }
        //[HttpPost]
        //public IActionResult Index(LoginModel model)
        //{
        //    try
        //    {
        //        model = new LoginQueries().CheckLoginUser(model.Username, model.Password);

        //        if (string.IsNullOrEmpty(model.UserID) || string.IsNullOrEmpty(model.Username))
        //        {
        //            // Login with invalid credentials
        //            ViewData["MessageLogin"] = "Account invalid";
        //            return View(model);
        //        }

        //        // Store user information in session
        //        if (string.IsNullOrEmpty(HttpContext.Session.GetString("SessionUserID")))
        //        {
        //            HttpContext.Session.SetString("SessionUserID", model.UserID);
        //            HttpContext.Session.SetInt32("SessionRoleID", model.RoleID); // Assuming RoleID is an integer
        //            HttpContext.Session.SetString("SessionUsername", model.Username);
        //            HttpContext.Session.SetString("SessionEmail", model.EmailUser);
        //            HttpContext.Session.SetString("SessionPhone", model.PhoneUser);
        //            HttpContext.Session.SetString("SessionFullName", model.FullName);
        //            // Add more session variables as needed
        //        }

        //        // Check if RoleID is 1 or 2
        //        if (model.RoleID == 1)
        //        {
        //            // Redirect to Home page for RoleID = 1
        //            return RedirectToAction(nameof(HomeController.Index), "Home");
        //        }
        //        else if (model.RoleID == 2)
        //        {
        //            // Redirect to Admin page for RoleID = 2
        //            return RedirectToAction(nameof(AdminController.Index), "Admin");
        //        }

        //        else
        //        {
        //            // Handle the case where RoleID is neither 1 nor 2
        //            ViewData["MessageLogin"] = "Invalid Role";
        //            return View(model);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception or handle it appropriately
        //        ViewData["MessageLogin"] = "An error occurred during login.";
        //        return View(model);
        //    }
        //}

        [HttpPost]
        public IActionResult Index(LoginModel model)
        {
            try
            {
                model = new LoginQueries().CheckLoginUser(model.Username, model.Password);

                if (string.IsNullOrEmpty(model.UserID) || string.IsNullOrEmpty(model.Username))
                {
                    // Login with invalid credentials
                    ViewData["MessageLogin"] = "Account invalid";
                    return View(model);
                }

                // Store user information in session
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("SessionUserID")))
                {
                    HttpContext.Session.SetString("SessionUserID", model.UserID);
                    HttpContext.Session.SetInt32("SessionRoleID", model.RoleID); // Assuming RoleID is an integer
                    HttpContext.Session.SetString("SessionUsername", model.Username);
                    HttpContext.Session.SetString("SessionEmail", model.EmailUser);
                    HttpContext.Session.SetString("SessionPhone", model.PhoneUser);
                    HttpContext.Session.SetString("SessionFullName", model.FullName);
                    // Add more session variables as needed
                }

                // Check RoleID and redirect accordingly
                switch (model.RoleID)
                {
                    case 1:
                        // Redirect to Home page for RoleID = 1
                        return RedirectToAction(nameof(HomeController.Index), "Home");

                    case 2:
                        // Redirect to Admin page for RoleID = 2
                        return RedirectToAction(nameof(AdminController.Index), "Admin");

                    case 3:
                        HttpContext.Session.SetString("SessionUserID", model.UserID); // Assuming TraineeID is a string
                                                                                      // Redirect to Trainee Home page for RoleID = 3
                        return RedirectToAction(nameof(TraineeHomeController.Index), "TraineeHome");
                    //case 4:
                    //    // Redirect to RoleID = 4 page
                    //    return RedirectToAction(nameof(Role4Controller.Index), "Role4");

                    default:
                        // Handle the case where RoleID is neither 1, 2, 3, nor 4
                        ViewData["MessageLogin"] = "Invalid Role";
                        return View(model);
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                ViewData["MessageLogin"] = "An error occurred during login.";
                return View(model);
            }
        }




        [HttpPost]
        public IActionResult Logout()
        {
            if(!string.IsNullOrEmpty(HttpContext.Session.GetString("SessionUserID")))
            {
                //xoa cac session da dc tao 
                HttpContext.Session.Remove("SessionUserID");
                HttpContext.Session.Remove("SessionRoleID");
                HttpContext.Session.Remove("SessionUsername");
                HttpContext.Session.Remove("SessionEmail");

            }
            return RedirectToAction(nameof(LoginController.Index), "Login");
        }
    }
}
