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
    public class ProcessoTabelaValorController : Controller
    {
        private DBContext db = new DBContext();

        // GET: ProcessoTabelaValor
        public ActionResult Index()
        {
            return View(db.ProcessoTabelaValors.ToList());
        }

        // GET: ProcessoTabelaValor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessoTabelaValor processoTabelaValor = db.ProcessoTabelaValors.Find(id);
            if (processoTabelaValor == null)
            {
                return HttpNotFound();
            }
            return View(processoTabelaValor);
        }

        // GET: ProcessoTabelaValor/Create
        public ActionResult Create(int? processoId)
        {
            if (processoId != null)
            {
                ProcessoTabelaValor processoTabelaValor = new ProcessoTabelaValor { ProcessoId = (int)processoId };
                return View(processoTabelaValor);
            }
            return View();
        }

        // POST: ProcessoTabelaValor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProcessoTabelaValorId, ProcessoId,LinkDoc,DataDebito,ValorOriginal,DataAtualizacao,ValorAtualizado, OrigemCredito")] ProcessoTabelaValor processoTabelaValor)
        {
            if (ModelState.IsValid)
            {
                db.ProcessoTabelaValors.Add(processoTabelaValor);
                db.SaveChanges();
                //return RedirectToAction("Index");
                //return RedirectToAction("TodoProcesso","Processo");

                return RedirectToAction("TodoProcesso", "Processo", new {id = processoTabelaValor.ProcessoId});                
            }

            return View(processoTabelaValor);
        }

        // GET: ProcessoTabelaValor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessoTabelaValor processoTabelaValor = db.ProcessoTabelaValors.Find(id);
            if (processoTabelaValor == null)
            {
                return HttpNotFound();
            }
            return View(processoTabelaValor);
        }

        // POST: ProcessoTabelaValor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProcessoTabelaValorId, ProcessoId,LinkDoc,DataDebito,ValorOriginal,DataAtualizacao,ValorAtualizado, OrigemCredito")] ProcessoTabelaValor processoTabelaValor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(processoTabelaValor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TodoProcesso", "Processo", new { id = processoTabelaValor.ProcessoId });
                //return RedirectToAction("Index"); //unica alteração
            }
            return View(processoTabelaValor); 
           
        }

        // GET: ProcessoTabelaValor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessoTabelaValor processoTabelaValor = db.ProcessoTabelaValors.Find(id);
            if (processoTabelaValor == null)
            {
                return HttpNotFound();
            }
            return View(processoTabelaValor);
        }

        // POST: ProcessoTabelaValor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProcessoTabelaValor processoTabelaValor = db.ProcessoTabelaValors.Find(id);
            db.ProcessoTabelaValors.Remove(processoTabelaValor);
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
