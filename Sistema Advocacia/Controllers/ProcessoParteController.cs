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
    public class ProcessoParteController : Controller
    {
        private DBContext db = new DBContext();

        // GET: ProcessoParte
        public ActionResult Index()
        {
            var processoPartes = db.ProcessoPartes.Include(p => p.Pessoa).Include(p => p.Processo);
            return View(processoPartes.ToList());
        }

        // GET: ProcessoParte/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessoParte processoParte = db.ProcessoPartes.Find(id);
            if (processoParte == null)
            {
                return HttpNotFound();
            }
            return View(processoParte);
        }

        public ActionResult Create(int? processoId)
        {
            ViewBag.PessoaId = new SelectList(db.Pessoas, "PessoaId", "Nome");
            ViewBag.ProcessoId = new SelectList(db.Processoes, "ProcessoId", "Comentario");

            if (processoId != null)
            {
                ProcessoParte processoParte = new ProcessoParte { ProcessoId = (int)processoId };
                return View(processoParte);
            }

            return View();
        }


    // GET: ProcessoParte/Create
     /*
    public ActionResult Create()
    {
        ViewBag.PessoaId = new SelectList(db.Pessoas, "PessoaId", "Nome");
        ViewBag.ProcessoId = new SelectList(db.Processoes, "ProcessoId", "Comentario");
        return View();
    }
    */

        // POST: ProcessoParte/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProcessoParteId,ProcessoId,PessoaId,Papel")] ProcessoParte processoParte)
        {
            if (ModelState.IsValid)
            {
                db.ProcessoPartes.Add(processoParte);
                db.SaveChanges();               
                return RedirectToAction("TodoProcesso", "Processo", new { id = processoParte.ProcessoId } );         

            }

            ViewBag.PessoaId = new SelectList(db.Pessoas, "PessoaId", "Nome", processoParte.PessoaId);
            ViewBag.ProcessoId = new SelectList(db.Processoes, "ProcessoId", "Comentario", processoParte.ProcessoId);

            return View(processoParte);
        }

        // GET: ProcessoParte/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessoParte processoParte = db.ProcessoPartes.Find(id);
            if (processoParte == null)
            {
                return HttpNotFound();
            }
            ViewBag.PessoaId = new SelectList(db.Pessoas, "PessoaId", "Nome", processoParte.PessoaId);
            ViewBag.ProcessoId = new SelectList(db.Processoes, "ProcessoId", "Comentario", processoParte.ProcessoId);
            return View(processoParte);
        }

        // POST: ProcessoParte/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProcessoParteId,ProcessoId,PessoaId,Papel")] ProcessoParte processoParte)
        {
            if (ModelState.IsValid)
            {
                db.Entry(processoParte).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("TodoProcesso", "Processo", new { id = processoParte.ProcessoId });
            }
            ViewBag.PessoaId = new SelectList(db.Pessoas, "PessoaId", "Nome", processoParte.PessoaId);
            ViewBag.ProcessoId = new SelectList(db.Processoes, "ProcessoId", "Comentario", processoParte.ProcessoId);
            return View(processoParte);
        }

        // GET: ProcessoParte/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessoParte processoParte = db.ProcessoPartes.Find(id);
            if (processoParte == null)
            {
                return HttpNotFound();
            }
            return View(processoParte);
        }

        // POST: ProcessoParte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProcessoParte processoParte = db.ProcessoPartes.Find(id);
            db.ProcessoPartes.Remove(processoParte);
            db.SaveChanges();
            //return RedirectToAction("Index");
            return RedirectToAction("TodoProcesso", "Processo", new { id = processoParte.ProcessoId });
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
