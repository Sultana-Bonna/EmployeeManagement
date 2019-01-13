using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using jan6_19_test.Models;

namespace jan6_19_test.Controllers
{
    public class TextVMsController : Controller
    {
        private TestDbContext db = new TestDbContext();

        // GET: TextVMs
        public ActionResult Index()
        {
            return View(db.TextVMs.ToList());
        }

        // GET: TextVMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TextVM textVM = db.TextVMs.Find(id);
            if (textVM == null)
            {
                return HttpNotFound();
            }
            return View(textVM);
        }

        // GET: TextVMs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TextVMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TextId,TextContent")] TextVM textVM)
        {
            if (ModelState.IsValid)
            {
                db.TextVMs.Add(textVM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(textVM);
        }

        // GET: TextVMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TextVM textVM = db.TextVMs.Find(id);
            if (textVM == null)
            {
                return HttpNotFound();
            }
            return View(textVM);
        }

        // POST: TextVMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TextId,TextContent")] TextVM textVM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(textVM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(textVM);
        }

        // GET: TextVMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TextVM textVM = db.TextVMs.Find(id);
            if (textVM == null)
            {
                return HttpNotFound();
            }
            return View(textVM);
        }

        // POST: TextVMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TextVM textVM = db.TextVMs.Find(id);
            db.TextVMs.Remove(textVM);
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
