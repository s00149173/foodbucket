using System.Linq;
using System.Web.Mvc;
using FoodBucket.Mailers;
using FoodBucket.Models;
using System.Web;
using System.Web.Helpers;

namespace FoodBucket.Controllers
{
    public class RecipesController : Controller
    {
        private foodbucketEntities db = new foodbucketEntities();

        private delegate void NotificationDelegate(Recipies recipies);

        private event NotificationDelegate NotifyEvent;

        public RecipesController()
        {
            NotifyEvent += new NotificationDelegate(UserNotification);
        }
    

        private IUserMailer _userMailer;

        public IUserMailer UserMailer
        {
            get { return _userMailer; }
            set { _userMailer = value; }
        }

        public ActionResult Index(int id)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            //select all recipes for country

            var query = db.Recipies.Where(c => c.id_country == id);

            //get country from country table
            var ctry = db.Countries.SingleOrDefault(c => c.id_country == id);
            ViewBag.Country = ctry.name;

            return View(query);
        }



        public ActionResult Details(int id)
        {
            var query = db.Recipies.SingleOrDefault(c => c.id_recipe == id);
            ViewBag.Message = "How to create " + query.title;

            return View(query);
        }


        public ActionResult Create()
        {
            ViewBag.countriesList2 = db.Countries.Select(c => new { id = c.id_country, value = c.name }).Distinct();
            

            if (!Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

     
        [HttpPost]
        public ActionResult Create(AddRecipieModel recep)
        {
            
            
            var rec = new Recipies
            {
                id_country = recep.id_country,
                description = recep.description,
                image_rec = ConvertImage(recep.ImageRec),
                title = recep.title,
                ingredients = recep.ingredients,
                preparation = recep.preparation
            };

            try
            {

                db.Recipies.Add(rec);
                db.SaveChanges();
                NotifyEvent(rec);

                return RedirectToAction("Index", "Recipes", new { id = rec.id_country });
            }
            catch
            {
                return RedirectToAction("Create", "Recipes");
            }
        }

        public void UserNotification(Recipies recipie)
        {
            var mails = db.System_Users.Select(s => s.Email).ToList();

            foreach (var mail in mails)
            {
                _userMailer = new UserMailer(recipie, mail);
                UserMailer.newRecipie().Send();
            }
        }

        private static byte[] ConvertImage(HttpPostedFileBase newImage)
        {


            if (newImage != null && newImage.ContentLength > 0)
            {
                var img = new WebImage(newImage.InputStream);
                if (img.Width > 255 || img.Height > 170)
                    img.Resize(255, 170, true, true);
                return img.GetBytes();
            }


            return null;
        }

        public void ShowImg(int id)
        {

            byte[] image = db.Recipies.Find(id).image_rec;

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

    public class country
    {
        public string Name { get; set; }

    }
}
