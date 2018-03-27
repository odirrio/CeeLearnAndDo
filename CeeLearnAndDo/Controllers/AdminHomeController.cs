using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CeeLearnAndDo.Models;

namespace CeeLearnAndDo.Controllers
{
    public class AdminHomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminHome
        [Authorize]
        public ActionResult Index()
        {
            return View();
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
