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
    public class ProcessoPeticaoController : Controller
    {
        private DBContext db = new DBContext();

        // GET: ProcessoPeticao
        public ActionResult Index()
        {
            var processoPeticaos = db.ProcessoPeticaos.Include(p => p.PeticaoModelo);
            return View(processoPeticaos.ToList());
        }

        // GET: ProcessoPeticao/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessoPeticao processoPeticao = db.ProcessoPeticaos.Find(id);
            if (processoPeticao == null)
            {
                return HttpNotFound();
            }
            return View(processoPeticao);
        }

        // GET: ProcessoPeticao/Create
        public ActionResult Create()
        {
            ViewBag.PedicaoModeloId = new SelectList(db.PeticaoModeloes, "PedicaoModeloId", "Nome");
            return View();
        }

        // POST: ProcessoPeticao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProcessoPeticaoId,ProcessoId,PedicaoModeloId,LinkQuestionario,Comentario,LinkPeticao,Finalizada")] ProcessoPeticao processoPeticao)
        {
            if (ModelState.IsValid)
            {
                db.ProcessoPeticaos.Add(processoPeticao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PedicaoModeloId = new SelectList(db.PeticaoModeloes, "PedicaoModeloId", "Nome", processoPeticao.PedicaoModeloId);
            return View(processoPeticao);
        }

        // GET: ProcessoPeticao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessoPeticao processoPeticao = db.ProcessoPeticaos.Find(id);
            if (processoPeticao == null)
            {
                return HttpNotFound();
            }
            ViewBag.PedicaoModeloId = new SelectList(db.PeticaoModeloes, "PedicaoModeloId", "Nome", processoPeticao.PedicaoModeloId);
            return View(processoPeticao);
        }

        // POST: ProcessoPeticao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProcessoPeticaoId,ProcessoId,PedicaoModeloId,LinkQuestionario,Comentario,LinkPeticao,Finalizada")] ProcessoPeticao processoPeticao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(processoPeticao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PedicaoModeloId = new SelectList(db.PeticaoModeloes, "PedicaoModeloId", "Nome", processoPeticao.PedicaoModeloId);
            return View(processoPeticao);
        }

        // GET: ProcessoPeticao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessoPeticao processoPeticao = db.ProcessoPeticaos.Find(id);
            if (processoPeticao == null)
            {
                return HttpNotFound();
            }
            return View(processoPeticao);
        }

        // POST: ProcessoPeticao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProcessoPeticao processoPeticao = db.ProcessoPeticaos.Find(id);
            db.ProcessoPeticaos.Remove(processoPeticao);
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
