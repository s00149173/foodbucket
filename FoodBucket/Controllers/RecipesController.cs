using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web.Mvc;
namespace FoodBucket.Controllers
{
    public class RecipesController : Controller
    {
        private foodbucketEntities db = new foodbucketEntities();
        //
        // GET: /Recipes/

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


        //
        // GET: /Recipes/Details/5

        public ActionResult Details(int id)
        {
            var query = db.Recipies.SingleOrDefault(c => c.id_recipe == id);
            ViewBag.Message = "How to create " + query.title;

            return View(query);
        }

        //
        // GET: /Recipes/Create

        public ActionResult Create()
        {
            ViewBag.countriesList2 = db.Countries.Select(c => new {id=c.id_country, value = c.name} ).Distinct();
            //ViewBag.countriesList = new SelectList(db.Countries.OrderBy(g => g.name), "id_country", "name", 0);

            //var query = db.Countries.Select(c => new { c.id_country, c.name});
            //ViewBag.CategoryId = new SelectList(query.AsEnumerable(), "id_country", "name");
          
            if (!Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        //
        // POST: /Recipes/Create

        [HttpPost]
        public ActionResult Create(Recipies rec)
        {
            try
            {
                db.Recipies.Add(rec);
                db.SaveChanges();
                
                // TODO: Add insert logic here

                return RedirectToAction("Index", "Recipes", new{ id= rec.id_country});
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Recipes/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Recipes/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Recipes/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Recipes/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }

    public class country
    {
        public string Name { get; set; }

    }
}
