using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using FoodBucket.Models;
using System.Web;
using System.Web.Helpers;

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
                
                // TODO: Add insert logic here

                return RedirectToAction("Index", "Recipes", new{ id= rec.id_country});
            }
            catch
            {
                return RedirectToAction("Create", "Recipes");
            }
        }

        //
        // GET: /Recipes/Edit/5
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
