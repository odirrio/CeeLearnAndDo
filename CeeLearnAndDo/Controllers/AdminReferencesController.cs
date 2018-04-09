using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CeeLearnAndDo.Models;
using System.IO;

namespace CeeLearnAndDo.Controllers
{
    [RoutePrefix("beheer/referenties")]
    [Route("{action=index}")]
    public class AdminReferencesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: beheer/referenties
        public ActionResult Index()
        {
            return View(db.References.ToList());
        }

        // GET: beheer/referenties/5
        [Route("{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reference reference = db.References.Find(id);
            if (reference == null)
            {
                return HttpNotFound();
            }
            return View(reference);
        }

        // GET: beheer/referenties/nieuw
        [Route("nieuw")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: beheer/referenties/nieuw
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("nieuw")]
        public ActionResult Create([Bind(Include = "Id,Title,Description,URL,ImagePath,CreatedAt,UpdatedAt")] Reference reference, HttpPostedFileBase ImagePath)
        {
            if (ModelState.IsValid)
            {
                // generate random guid string as fileName
                Guid g = Guid.NewGuid();
                string fileName = Convert.ToBase64String(g.ToByteArray());
                fileName = fileName.Replace("=", "");
                fileName = fileName.Replace("+", "");
                fileName = fileName.Replace("/", "");
                fileName = fileName + "-reference" + Path.GetExtension(ImagePath.FileName);

                // save image
                string path = Path.Combine(Server.MapPath("~/UploadedFiles/ReferenceImages"), fileName);
                ImagePath.SaveAs(path);
                reference.ImagePath = fileName;

                // add reference properties
                db.References.Add(reference);

                // remove http:// or https:// from posted URL
                reference.URL = reference.URL.Replace("https://", "").Replace("http://", "");

                // at dates
                reference.CreatedAt = DateTime.Now;
                reference.UpdatedAt = DateTime.Now;

                // save reference
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reference);
        }

        // GET: beheer/referenties/wijzigen/5
        [Route("wijzigen/{id:int}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reference reference = db.References.Find(id);
            if (reference == null)
            {
                return HttpNotFound();
            }
            return View(reference);
        }

        // POST: beheer/referenties/wijzigen/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("wijzigen/{id:int}")]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,URL,ImagePath,CreatedAt,UpdatedAt")] Reference reference, HttpPostedFileBase ImagePath)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reference).State = EntityState.Modified;
                db.Entry(reference).Property(x => x.ImagePath).IsModified = false;
                if (ImagePath != null)
                {
                    // generate random guid string as fileName
                    Guid g = Guid.NewGuid();
                    string fileName = Convert.ToBase64String(g.ToByteArray());
                    fileName = fileName.Replace("=", "");
                    fileName = fileName.Replace("+", "");
                    fileName = fileName.Replace("/", "");
                    fileName = fileName + "-reference" + Path.GetExtension(ImagePath.FileName);

                    // save image
                    string path = Path.Combine(Server.MapPath("~/UploadedFiles/ReferenceImages"), fileName);
                    ImagePath.SaveAs(path);
                    reference.ImagePath = fileName;
                }



                // remove http:// or https:// from posted URL
                reference.URL = reference.URL.Replace("https://", "").Replace("http://", "");
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reference);
        }

        // GET: beheer/referenties/verwijderen/5
        [Route("verwijderen/{id:int}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reference reference = db.References.Find(id);
            if (reference == null)
            {
                return HttpNotFound();
            }
            return View(reference);
        }

        // POST: beheer/referenties/verwijderen/5
        [Route("verwijderen/{id:int}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reference reference = db.References.Find(id);
            db.References.Remove(reference);
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
