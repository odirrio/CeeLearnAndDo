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
    public class AdminReferencesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminReferences
        public ActionResult Index()
        {
            return View(db.References.ToList());
        }

        // GET: AdminReferences/Details/5
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

        // GET: AdminReferences/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminReferences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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

                // at dates
                reference.CreatedAt = DateTime.Now;
                reference.UpdatedAt = DateTime.Now;

                // save reference
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reference);
        }

        // GET: AdminReferences/Edit/5
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

        // POST: AdminReferences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,URL,ImagePath,CreatedAt,UpdatedAt")] Reference reference)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reference).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reference);
        }

        // GET: AdminReferences/Delete/5
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

        // POST: AdminReferences/Delete/5
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
