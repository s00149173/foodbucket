using System;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using FoodBucket.Mailers;
using FoodBucket.Models;

namespace FoodBucket.Controllers
{
    public class UserController : Controller
    {
        private static foodbucketEntities _db;

        private delegate void RegisterUser(RegisterModel newUser);

        private IUserMailer _userMailer;

        public IUserMailer UserMailer
        {
            get { return _userMailer; }
            set { _userMailer = value; }
        }

        private readonly RegisterUser _registerDelegate;

        public UserController()
        {
            _db = new foodbucketEntities();

            _registerDelegate += AddSystemUser;
            _registerDelegate += AddUserData;
            _registerDelegate += MailUser;

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
            _registerDelegate(newUser);
            TempData["ConfirmationMessage"] = "Use your new account to login. Enjoy!";
            return RedirectToAction("Login");
        }

        private void MailUser(RegisterModel newUser)
        {
            _userMailer = new UserMailer(newUser);
            UserMailer.Welcome().Send();
        }

        private void AddSystemUser(RegisterModel newUser)
        {
            var user = new System_Users
            {
                Username = newUser.UserName,
                Password = Models.User.Encrypt(newUser.Password),
                RegDate = DateTime.Now,
                Email = newUser.Email,

            };

            _db.System_Users.Add(user);
            _db.SaveChanges();
        }

        private static byte[] ConvertImage(RegisterModel newUser)
        {
                   
            var image = newUser.ProfileImage;
            if (image != null && image.ContentLength > 0)
            {
                var img = new WebImage(image.InputStream);
                if (img.Width > 50)
                    img.Resize(50, 50);
                return img.GetBytes();
            }

            //if (image != null && image.ContentLength > 0)
            //{
            //    byte[] imageData;
            //    using (var binaryReader = new BinaryReader(image.InputStream))
            //    {
            //        imageData = binaryReader.ReadBytes(image.ContentLength);
            //    }
            //    return imageData;
            //}
            return null;
        }

        private static void AddUserData(RegisterModel newUser)
        {
            var image = ConvertImage(newUser);
            var user = new UserData
            {
                UserName = newUser.UserName,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Photo = image
            };

            _db.UserData.Add(user);
            _db.SaveChanges();
        }

        [HttpPost]
        public JsonResult DoesUserNameExist(string username)
        {

            var user = _db.System_Users.Where(u => u.Username == username);

            return Json(!user.Any());
        }

        public void ShowImg(string id)
        {

            byte[] image = _db.UserData.Where(x => x.UserName == id).FirstOrDefault().Photo;
            
            if (image != null)
            {
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = "image/gif";
                Response.BinaryWrite(image);
                Response.End();

            }
        }
      
    }
}
