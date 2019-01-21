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
    public class ClienteDocumentoController : Controller
    {
        private DBContext db = new DBContext();

        // GET: ClienteDocumento
        public ActionResult Index()
        {
            var clienteDocumentoes = db.ClienteDocumentoes.Include(c => c.Cliente).Include(c => c.Documento);
            return View(clienteDocumentoes.ToList());
        }

        // GET: ClienteDocumento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClienteDocumento clienteDocumento = db.ClienteDocumentoes.Find(id);
            if (clienteDocumento == null)
            {
                return HttpNotFound();
            }
            return View(clienteDocumento);
        }

        // GET: ClienteDocumento/Create
        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "Nome");
            ViewBag.DocumentoId = new SelectList(db.Documentoes, "DocumentoId", "Nome");
            return View();
        }

        // POST: ClienteDocumento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClienteDocumentoId,ClienteId,DocumentoId,Comentario,Entregue,Link")] ClienteDocumento clienteDocumento)
        {
            if (ModelState.IsValid)
            {
                db.ClienteDocumentoes.Add(clienteDocumento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "Nome", clienteDocumento.ClienteId);
            ViewBag.DocumentoId = new SelectList(db.Documentoes, "DocumentoId", "Nome", clienteDocumento.DocumentoId);
            return View(clienteDocumento);
        }

        // GET: ClienteDocumento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClienteDocumento clienteDocumento = db.ClienteDocumentoes.Find(id);
            if (clienteDocumento == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "Nome", clienteDocumento.ClienteId);
            ViewBag.DocumentoId = new SelectList(db.Documentoes, "DocumentoId", "Nome", clienteDocumento.DocumentoId);
            return View(clienteDocumento);
        }

        // POST: ClienteDocumento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClienteDocumentoId,ClienteId,DocumentoId,Comentario,Entregue,Link")] ClienteDocumento clienteDocumento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clienteDocumento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "Nome", clienteDocumento.ClienteId);
            ViewBag.DocumentoId = new SelectList(db.Documentoes, "DocumentoId", "Nome", clienteDocumento.DocumentoId);
            return View(clienteDocumento);
        }

        // GET: ClienteDocumento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClienteDocumento clienteDocumento = db.ClienteDocumentoes.Find(id);
            if (clienteDocumento == null)
            {
                return HttpNotFound();
            }
            return View(clienteDocumento);
        }

        // POST: ClienteDocumento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClienteDocumento clienteDocumento = db.ClienteDocumentoes.Find(id);
            db.ClienteDocumentoes.Remove(clienteDocumento);
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
