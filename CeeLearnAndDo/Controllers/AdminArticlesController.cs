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
    [RoutePrefix("beheer/artikelen")]
    [Route("{action=index}")]
    public class AdminArticlesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: beheer/artikelen
        [Authorize]
        public ActionResult Index()
        {
            var articles = from a in db.Articles
                           select a;

            articles = articles.OrderByDescending(s => s.UpdatedAt);

            return View(articles.ToList());
        }

        // GET: beheer/artikelen/5
        [Route("{id}")]
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // Upload image
        [HttpPost]
        [Authorize]
        public string UploadImage()
        {
            string fileName = "";
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                // generate random guid string as fileName
                Guid g = Guid.NewGuid();
                fileName = Convert.ToBase64String(g.ToByteArray());
                fileName = fileName.Replace("=", "");
                fileName = fileName.Replace("+", "");
                fileName = fileName.Replace("/", "");
                fileName = fileName + "-article" + Path.GetExtension(file.FileName);

                var path = Path.Combine(Server.MapPath("~/UploadedFiles/ArticleImages"), fileName);
                file.SaveAs(path);
            }
            return "/UploadedFiles/ArticleImages/" + fileName;
        }

        // GET: beheer/artikelen/nieuw
        [Route("nieuw")]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: beheer/artikelen/nieuw
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("nieuw")]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Title,Description,Content,ImagePath,Approved,CreatedAt,UpdatedAt")] Article article, HttpPostedFileBase ImagePath)
        {
            if (ModelState.IsValid)
            {

                // generate random guid string as fileName
                Guid g = Guid.NewGuid();
                string fileName = Convert.ToBase64String(g.ToByteArray());
                fileName = fileName.Replace("=", "");
                fileName = fileName.Replace("+", "");
                fileName = fileName.Replace("/", "");
                fileName = fileName + "-article" + Path.GetExtension(ImagePath.FileName);

                // save image
                string path = Path.Combine(Server.MapPath("~/UploadedFiles/ArticleImages"), fileName);
                ImagePath.SaveAs(path);
                article.ImagePath = fileName;

                // add article properties
                db.Articles.Add(article);

                // set approved false
                article.Approved = false;

                // at dates
                article.CreatedAt = DateTime.Now;
                article.UpdatedAt = DateTime.Now;

                // save new article
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(article);
        }

        // GET: beheer/artikelen/wijzigen/5
        [Route("wijzigen/{id:int}")]
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: beheer/artikelen/wijzigen/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("wijzigen/{id:int}")]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Content,ImagePath,Approved,CreatedAt,UpdatedAt")] Article article, HttpPostedFileBase ImagePath)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.Entry(article).Property(x => x.ImagePath).IsModified = false;
                db.Entry(article).Property(x => x.CreatedAt).IsModified = false;

                var dbArticle = db.Articles.Where(x => x.Id == article.Id).SingleOrDefault();
                string oldImagePath = dbArticle.ImagePath;

                if (ImagePath != null)
                {

                    // generate random guid string as fileName
                    Guid g = Guid.NewGuid();
                    string fileName = Convert.ToBase64String(g.ToByteArray());
                    fileName = fileName.Replace("=", "");
                    fileName = fileName.Replace("+", "");
                    fileName = fileName.Replace("/", "");
                    fileName = fileName + "-article" + Path.GetExtension(ImagePath.FileName);

                    // delete old image
                    oldImagePath = Request.MapPath("~/UploadedFiles/ArticleImages" + oldImagePath);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    // save image
                    string path = Path.Combine(Server.MapPath("~/UploadedFiles/ArticleImages"), fileName);
                    ImagePath.SaveAs(path);

                    // set properties
                    article.ImagePath = fileName;
                }

                // set dates
                article.UpdatedAt = DateTime.Now;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: beheer/artikelen/verwijderen/5
        [Route("verwijderen/{id:int}")]
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: beheer/artikelen/verwijderen/5
        [Route("verwijderen/{id:int}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
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
