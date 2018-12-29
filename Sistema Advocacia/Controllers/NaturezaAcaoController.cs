using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sistema_Advocacia.Context;
using Sistema_Advocacia.Models;

namespace Sistema_Advocacia.Controllers
{
    public class NaturezaAcaoController : Controller
    {
        private DBContext db = new DBContext();

        // GET: NaturezaAcao
        public ActionResult Index()
        {
            return View(db.NaturezaAcaos.ToList());
        }

        // GET: NaturezaAcao/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NaturezaAcao naturezaAcao = db.NaturezaAcaos.Find(id);
            if (naturezaAcao == null)
            {
                return HttpNotFound();
            }
            return View(naturezaAcao);
        }

        // GET: NaturezaAcao/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NaturezaAcao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NaturezaAcaoID,RamoDireito,Nome,Comentario")] NaturezaAcao naturezaAcao)
        {
            if (ModelState.IsValid)
            {
                db.NaturezaAcaos.Add(naturezaAcao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(naturezaAcao);
        }

        // GET: NaturezaAcao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NaturezaAcao naturezaAcao = db.NaturezaAcaos.Find(id);
            if (naturezaAcao == null)
            {
                return HttpNotFound();
            }
            return View(naturezaAcao);
        }

        // POST: NaturezaAcao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NaturezaAcaoID,RamoDireito,Nome,Comentario")] NaturezaAcao naturezaAcao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(naturezaAcao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(naturezaAcao);
        }

        // GET: NaturezaAcao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NaturezaAcao naturezaAcao = db.NaturezaAcaos.Find(id);
            if (naturezaAcao == null)
            {
                return HttpNotFound();
            }
            return View(naturezaAcao);
        }

        // POST: NaturezaAcao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NaturezaAcao naturezaAcao = db.NaturezaAcaos.Find(id);
            db.NaturezaAcaos.Remove(naturezaAcao);
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
