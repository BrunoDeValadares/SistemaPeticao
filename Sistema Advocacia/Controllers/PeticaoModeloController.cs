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
    public class PeticaoModeloController : Controller
    {
        private DBContext db = new DBContext();

        // GET: PeticaoModelo
        public ActionResult Index()
        {
            var peticaoModeloes = db.PeticaoModeloes.Include(p => p.NaturezaAcao);
            return View(peticaoModeloes.ToList());
        }

        // GET: PeticaoModelo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PeticaoModelo peticaoModelo = db.PeticaoModeloes.Find(id);
            if (peticaoModelo == null)
            {
                return HttpNotFound();
            }
            return View(peticaoModelo);
        }

        // GET: PeticaoModelo/Create
        public ActionResult Create()
        {
            ViewBag.NaturezaAcaoId = new SelectList(db.NaturezaAcaos, "NaturezaAcaoID", "Nome");
            return View();
        }

        // POST: PeticaoModelo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PedicaoModeloId,Nome,NaturezaAcaoId,Peticao,Comentario")] PeticaoModelo peticaoModelo)
        {
            if (ModelState.IsValid)
            {
                db.PeticaoModeloes.Add(peticaoModelo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NaturezaAcaoId = new SelectList(db.NaturezaAcaos, "NaturezaAcaoID", "Nome", peticaoModelo.NaturezaAcaoId);
            return View(peticaoModelo);
        }

        // GET: PeticaoModelo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PeticaoModelo peticaoModelo = db.PeticaoModeloes.Find(id);
            if (peticaoModelo == null)
            {
                return HttpNotFound();
            }
            ViewBag.NaturezaAcaoId = new SelectList(db.NaturezaAcaos, "NaturezaAcaoID", "Nome", peticaoModelo.NaturezaAcaoId);
            return View(peticaoModelo);
        }

        // POST: PeticaoModelo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PedicaoModeloId,Nome,NaturezaAcaoId,Peticao,Comentario")] PeticaoModelo peticaoModelo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(peticaoModelo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NaturezaAcaoId = new SelectList(db.NaturezaAcaos, "NaturezaAcaoID", "Nome", peticaoModelo.NaturezaAcaoId);
            return View(peticaoModelo);
        }

        // GET: PeticaoModelo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PeticaoModelo peticaoModelo = db.PeticaoModeloes.Find(id);
            if (peticaoModelo == null)
            {
                return HttpNotFound();
            }
            return View(peticaoModelo);
        }

        // POST: PeticaoModelo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PeticaoModelo peticaoModelo = db.PeticaoModeloes.Find(id);
            db.PeticaoModeloes.Remove(peticaoModelo);
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
