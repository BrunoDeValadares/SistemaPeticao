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
using Sistema_Advocacia.ViewModels;

namespace Sistema_Advocacia.Controllers
{
    public class ProcessoController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Processo
        public ActionResult Index()
        {
            var processoes = db.Processoes.Include(p => p.Cliente).Include(p => p.NaturezaAcao);
            return View(processoes.ToList());
        }

        public ActionResult TodoProcesso(int? id)
        {
            TodoProcesso todoProcesso = new TodoProcesso();

            todoProcesso.Processo = db.Processoes.Find(id);
            todoProcesso.ProcessoPeticoes = db.ProcessoPeticaos.Where(x => x.ProcessoId == id).Include(x => x.PeticaoModelo).ToList();
            todoProcesso.ProcessoDocumentos = db.ProcessoDocumentoes.Where(x => x.ProcessoId == id).Include(x => x.Documento).ToList();
            todoProcesso.ProcessoTabelaValores = db.ProcessoTabelaValors.Where(x => x.ProcessoId == id).ToList();

            return View(todoProcesso); 
        }

        // GET: Processo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Processo processo = db.Processoes.Find(id);
            if (processo == null)
            {
                return HttpNotFound();
            }
            return View(processo);
        }

        // GET: Processo/Create
        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "Nome");
            ViewBag.NaturezaAcaoId = new SelectList(db.NaturezaAcaos, "NaturezaAcaoID", "Nome");
            return View();
        }

        // POST: Processo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProcessoId,ClienteId,NaturezaAcaoId,Ativo,Comentario,ResumoDoCaso,NumeroProcesso,LinkProcesso,Vara")] Processo processo)
        {
            if (ModelState.IsValid)
            {
                db.Processoes.Add(processo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "Nome", processo.ClienteId);
            ViewBag.NaturezaAcaoId = new SelectList(db.NaturezaAcaos, "NaturezaAcaoID", "Nome", processo.NaturezaAcaoId);
            return View(processo);
        }

        // GET: Processo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Processo processo = db.Processoes.Find(id);
            if (processo == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "Nome", processo.ClienteId);
            ViewBag.NaturezaAcaoId = new SelectList(db.NaturezaAcaos, "NaturezaAcaoID", "Nome", processo.NaturezaAcaoId);
            return View(processo);
        }

        // POST: Processo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProcessoId,ClienteId,NaturezaAcaoId,Ativo,Comentario,ResumoDoCaso,NumeroProcesso,LinkProcesso,Vara")] Processo processo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(processo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "Nome", processo.ClienteId);
            ViewBag.NaturezaAcaoId = new SelectList(db.NaturezaAcaos, "NaturezaAcaoID", "Nome", processo.NaturezaAcaoId);
            return View(processo);
        }

        // GET: Processo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Processo processo = db.Processoes.Find(id);
            if (processo == null)
            {
                return HttpNotFound();
            }
            return View(processo);
        }

        // POST: Processo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Processo processo = db.Processoes.Find(id);
            db.Processoes.Remove(processo);
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
