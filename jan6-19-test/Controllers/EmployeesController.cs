using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using jan6_19_test.Models;
using System.IO;
using jan6_19_test.Models.ViewModels;

namespace jan6_19_test.Controllers
{
    public class EmployeesController : Controller
    {
        private TestDbContext db = new TestDbContext();

        // GET: Employees
        public ActionResult Index()
        {
            var employeeDbSet = db.EmployeeDbSet.Include(e => e.EmployeeImage);
            return View(employeeDbSet.ToList());
        }
        public JsonResult ImageUpload(ImageVM model)
        {
            TestDbContext db = new TestDbContext();
            int imageId = 0;
            var file = model.ImageFile;
            byte[] imagebyte = null;
            if (file != null)
            {
                file.SaveAs(Server.MapPath("/UploadedImage/" + file.FileName));
                BinaryReader reader = new BinaryReader(file.InputStream);
                imagebyte = reader.ReadBytes(file.ContentLength);
                TestInfo img = new TestInfo();
                img.ImageName = file.FileName;
                img.ImageByte = imagebyte;
                img.ImagePath = "/UploadedImage/" + file.FileName;
                // img.IsDeleted = false;
                db.TextInfoDbSet.Add(img);
                db.SaveChanges();
                imageId = img.ImageId;
            }
            return Json(imageId, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ImageRetrieve(int imgID)
        {
            TestDbContext db = new TestDbContext();
            var img = db.TextInfoDbSet.SingleOrDefault(x => x.ImageId == imgID);
            return File(img.ImageByte, "image/jpg");
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.EmployeeDbSet.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeImageId = new SelectList(db.TextInfoDbSet, "ImageId", "ImageName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FullName,Email,ContactNo,NID,BloodGroup,Department,Designation,Addresses,EmployeeImageId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeDbSet.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeImageId = new SelectList(db.TextInfoDbSet, "ImageId", "ImageName", employee.EmployeeImageId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.EmployeeDbSet.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeImageId = new SelectList(db.TextInfoDbSet, "ImageId", "ImageName", employee.EmployeeImageId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FullName,Email,ContactNo,NID,BloodGroup,Department,Designation,Addresses,EmployeeImageId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeImageId = new SelectList(db.TextInfoDbSet, "ImageId", "ImageName", employee.EmployeeImageId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.EmployeeDbSet.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.EmployeeDbSet.Find(id);
            db.EmployeeDbSet.Remove(employee);
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
