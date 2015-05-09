using MongoRepository;
using PocRazorFromDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PocRazorFromDatabase.Controllers
{
    public class DatabaseViewsController : Controller
    {
        MongoRepository<DatabaseView> db = new MongoRepository<DatabaseView>();

        // GET: DatabaseViews
        public ActionResult Index()
        {
            return View(db.ToList());
        }

        public ActionResult Add()
        {
            return View(new DatabaseView());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(DatabaseView entity)
        {
            db.Collection.Save(entity);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            var model = db.FirstOrDefault(x => x.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(string id, DatabaseView entity)
        {
            var model = db.FirstOrDefault(x => x.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            TryUpdateModel(model);
            db.Collection.Save(model);
            return RedirectToAction("Index");
        }
    }
}