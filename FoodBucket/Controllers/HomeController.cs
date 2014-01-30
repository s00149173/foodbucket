using System.Linq;
using System.Web.Mvc;
namespace FoodBucket.Controllers
{
    public class HomeController : Controller
    {
        private foodbucketEntities db = new foodbucketEntities();
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.PageTitle = "Home - FoodBuck.com";
            var countries =db.Countries.ToList();
            return View(countries);      
        }

    }
}
