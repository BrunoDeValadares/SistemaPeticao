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
    public class ProcessoDocumentoController : Controller
    {
        private DBContext db = new DBContext();

        // GET: ProcessoDocumento
        public ActionResult Index()
        {
            var processoDocumentoes = db.ProcessoDocumentoes.Include(p => p.Documento).Include(p => p.Processo);
            return View(processoDocumentoes.ToList());
        }

        // GET: ProcessoDocumento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessoDocumento processoDocumento = db.ProcessoDocumentoes.Find(id);
            if (processoDocumento == null)
            {
                return HttpNotFound();
            }
            return View(processoDocumento);
        }

        //CODIGO ORIGINAL
        // GET: ProcessoDocumento/Create
        /*
        public ActionResult Create()
        {
            ViewBag.DocumentoId = new SelectList(db.Documentoes, "DocumentoId", "Nome");
            ViewBag.ProcessoId = new SelectList(db.Processoes, "ProcessoId", "Comentario");
            return View();
        }
        */
        //UNICA PARTE ALTERADA POR MIM
        //**************************************************************************************************************************************************************
        // GET: ProcessoDocumento/Create
        public ActionResult Create(int? processoId)
        {
            ViewBag.DocumentoId = new SelectList(db.Documentoes, "DocumentoId", "Nome");
            ViewBag.ProcessoId = new SelectList(db.Processoes, "ProcessoId", "ProcessoId");

            if (processoId != null)
            {
                ProcessoDocumento processoDocumento = new ProcessoDocumento { ProcessoId = (int)processoId, Entregue =true, Comentario = "joia" };
                return View(processoDocumento);
            }

            return View();
        }


        // POST: ProcessoDocumento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProcessoDocumentoId,ProcessoId,DocumentoId,Comentario,Entregue,Link")] ProcessoDocumento processoDocumento)
        {
            if (ModelState.IsValid)
            {
                db.ProcessoDocumentoes.Add(processoDocumento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DocumentoId = new SelectList(db.Documentoes, "DocumentoId", "Nome", processoDocumento.DocumentoId);
            ViewBag.ProcessoId = new SelectList(db.Processoes, "ProcessoId", "Comentario", processoDocumento.ProcessoId);
            return View(processoDocumento);
        }

        // GET: ProcessoDocumento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessoDocumento processoDocumento = db.ProcessoDocumentoes.Find(id);
            if (processoDocumento == null)
            {
                return HttpNotFound();
            }
            ViewBag.DocumentoId = new SelectList(db.Documentoes, "DocumentoId", "Nome", processoDocumento.DocumentoId);
            ViewBag.ProcessoId = new SelectList(db.Processoes, "ProcessoId", "Comentario", processoDocumento.ProcessoId);
            return View(processoDocumento);
        }

        // POST: ProcessoDocumento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProcessoDocumentoId,ProcessoId,DocumentoId,Comentario,Entregue,Link")] ProcessoDocumento processoDocumento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(processoDocumento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DocumentoId = new SelectList(db.Documentoes, "DocumentoId", "Nome", processoDocumento.DocumentoId);
            ViewBag.ProcessoId = new SelectList(db.Processoes, "ProcessoId", "Comentario", processoDocumento.ProcessoId);
            return View(processoDocumento);
        }

        // GET: ProcessoDocumento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessoDocumento processoDocumento = db.ProcessoDocumentoes.Find(id);
            if (processoDocumento == null)
            {
                return HttpNotFound();
            }
            return View(processoDocumento);
        }

        // POST: ProcessoDocumento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProcessoDocumento processoDocumento = db.ProcessoDocumentoes.Find(id);
            db.ProcessoDocumentoes.Remove(processoDocumento);
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
