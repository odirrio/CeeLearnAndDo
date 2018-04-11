﻿using System;
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
    [RoutePrefix("referenties")]
    [Route("{action=index}")]
    public class ReferencesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: referenties
        public ActionResult Index()
        {
            return View(db.References.ToList());
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
