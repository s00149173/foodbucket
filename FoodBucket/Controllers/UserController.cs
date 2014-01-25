using System.Web.Mvc;
using System.Web.Security;
using FoodBucket.Models;


namespace FoodBucket.Controllers
{   
    public class UserController : Controller
    {
        private readonly foodbucketEntities _db;
        
        public UserController()
        {
            _db = new foodbucketEntities();
        }

        

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
 
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.IsValid(user.UserName, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, user.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Login data is incorrect!");
            }
            return View(user);
        }

        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel newUser)
        {
          var user= new System_Users {Username = newUser.UserName,
                                      Password= Models.User.Encrypt(newUser.Password),
                                      RegDate = System.DateTime.Now,
                                      Email = newUser.Email
                                      };


            _db.System_Users.Add(user);
            _db.SaveChanges();
            TempData["ConfirmationMessage"] = "Use your new account to login. Enjoy!";
            return RedirectToAction("Login");
        }

    }
    }
