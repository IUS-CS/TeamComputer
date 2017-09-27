using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Prototype.DAL;
using Prototype.Models;

namespace Prototype.Views
{
    public class PantryController : Controller
    {
        private FoodContext db = new FoodContext();

        // GET: Pantry
        public ActionResult Index()
        {
            return View(db.Party.ToList());
        }

        // GET: Pantry/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pantry pantry = db.Party.Find(id);
            if (pantry == null)
            {
                return HttpNotFound();
            }
            return View(pantry);
        }

        // GET: Pantry/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pantry/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FoodID,UserID")] Pantry pantry)
        {
            if (ModelState.IsValid)
            {
                db.Party.Add(pantry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pantry);
        }

        // GET: Pantry/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pantry pantry = db.Party.Find(id);
            if (pantry == null)
            {
                return HttpNotFound();
            }
            return View(pantry);
        }

        // POST: Pantry/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FoodID,UserID")] Pantry pantry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pantry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pantry);
        }

        // GET: Pantry/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pantry pantry = db.Party.Find(id);
            if (pantry == null)
            {
                return HttpNotFound();
            }
            return View(pantry);
        }

        // POST: Pantry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pantry pantry = db.Party.Find(id);
            db.Party.Remove(pantry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
