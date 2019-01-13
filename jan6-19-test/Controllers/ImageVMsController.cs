using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using jan6_19_test.Models;
using jan6_19_test.Models.ViewModels;
using System.IO;
using AutoMapper;

namespace jan6_19_test.Controllers
{
    public class ImageVMsController : Controller
    {
        public TestDbContext db = new TestDbContext();

        // GET: ImageVMs
        public ActionResult Index()

        {
           return View();
            //return View(db.TextInfoDbSet.ToList());
        }
        /*
        public ActionResult Index(IEnumerable<ImageVM> Image)
        {
            return View(db.TextInfoDbSet.ToList());
        }
*/

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


        //Multi
        public JsonResult UploadFiles(ImageVM model)
        {
            TestDbContext db = new TestDbContext();
            int imageId = 0;
           
            var file = model.ImageFile;
            byte[] imagebyte = null;
            for (int i = 0; i < Request.Files.Count; i++)
            {
              //  HttpPostedFileBase file = Request.Files[i];//Uploaded file
                //Use the following properties to get file's name, size and MIMEType
                int fileSize = file.ContentLength;
                string fileName = file.FileName;
                string mimeType = file.ContentType;
                System.IO.Stream fileContent = file.InputStream;
                //To save file, use SaveAs method
                file.SaveAs(Server.MapPath("~/") + fileName); //File will be saved in application root
            }
            return Json("Uploaded " + Request.Files.Count + " files");
            /*String Path = Server.MapPath("/UploadedImage/");
            String[] FileNames = Directory.GetFiles(Path);*/

            /*string[] filePaths = Directory.GetFiles(@"\UploadedImage\"); //Get File List in chosen directory
            foreach (string path in filePaths) //iterate the file list
            {
                FileInfo file1 = new FileInfo(path); //get individual file info
                Response.Write(Path.GetFileName(file1.FullName) + "<br>"); //output individual file name
            }*/


            /*var files = Request.Files.Cast<HttpPostedFile>()
                .Select(file => new HttpPostedFileWrapper(file))
                .Where(file => file.ContentLength > 0
                            && file.ContentType.StartsWith("image/"));

            foreach (var file in files)
            {
                SaveNonAutoExtractedThumbnails(doc, file);
            }*/

            /*String TempFileName;
            HttpFileCollection MyFileCollection = Request.Files;

            for (int Loop1 = 0; Loop1 < MyFileCollection.Count; Loop1++)
            {
                // Create a new file name.
                TempFileName = "C:\\TempFiles\\File_" + Loop1.ToString();
                // Save the file.
                MyFileCollection[Loop1].SaveAs(TempFileName);
            }*/
            /*

                        if (file != null)
                        {
                            file.SaveAs(Server.MapPath("/UploadedImage/" + file.FileName));
                            BinaryReader reader = new BinaryReader(file.InputStream);
                            imagebyte = reader.ReadBytes(file.ContentLength);
                            MultiImage img = new MultiImage();
                            img.ImageName = file.FileName;
                            img.ImageByte = imagebyte;
                            img.ImagePath = "/UploadedImage/" + file.FileName;
                            // img.IsDeleted = false;
                            db.MultiImageDbSet.Add(img);
                            db.SaveChanges();
                            imageId = img.ImageId;
                        }
                        return Json(imageId, JsonRequestBehavior.AllowGet);*/

            


            /*TestDbContext db = new TestDbContext();
            int imageId = 0;
            var file = model.ImageFile;
            byte[] imagebyte = null;
            // string[]fname = Path.GetFileName(file.FileName);

         //   string[] fname = Path.GetFileName(file.FileName);

            for (int i = 0; i < Request.Files.Count; i++)
            {
                BinaryReader reader = new BinaryReader(file.InputStream);
                imagebyte = reader.ReadBytes(file.ContentLength);
               
                MultiImage imgM = new MultiImage();
                //HttpPostedFile file = Request.Files[i];
                if (file.ContentLength > 0)
                {
                   
                    file.SaveAs(Server.MapPath(Path.Combine("/UploadedImage/" + file.FileName)));
                   
                    imgM.ImageName = file.FileName;
                    imgM.ImageByte = imagebyte;
                    imgM.ImagePath = "/UploadedImage/" + file.FileName;


                }
                db.SaveChanges();
                imageId = imgM.ImageId;
            }
           // db.MultiImageDbSet = Request.Files.Count + " Images Has Been Saved Successfully";
            return Json(db.MultiImageDbSet.ToList(), JsonRequestBehavior.AllowGet);*/

        }
        // GET: ImageVMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestInfo imageVM = db.TextInfoDbSet.Find(id);
            if (imageVM == null)
            {
                return HttpNotFound();
            }
            return View(imageVM);
        }

        // GET: ImageVMs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ImageVMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImageId,ImageName,ImagePath,ImageFile")] TestInfo imageVM)
        {
            if (ModelState.IsValid)
            {
                db.TextInfoDbSet.Add(imageVM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(imageVM);
        }

        // GET: ImageVMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestInfo imageVM = db.TextInfoDbSet.Find(id);
            if (imageVM == null)
            {
                return HttpNotFound();
            }
            return View(imageVM);
        }

        // POST: ImageVMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImageId,ImageName,ImagePath,ImageFile")] TestInfo imageVM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(imageVM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(imageVM);
        }

        // GET: ImageVMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestInfo imageVM = db.TextInfoDbSet.Find(id);
            if (imageVM == null)
            {
                return HttpNotFound();
            }
            return View(imageVM);
        }

        // POST: ImageVMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestInfo imageVM = db.TextInfoDbSet.Find(id);
            db.TextInfoDbSet.Remove(imageVM);
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
        public ActionResult MultiUp(IEnumerable<HttpPostedFileBase> Image)

        {
            return View();
          
        }
    }
}
