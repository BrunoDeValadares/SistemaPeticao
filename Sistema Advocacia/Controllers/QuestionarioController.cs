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
    public class QuestionarioController : Controller
    {
        private DBContext db = new DBContext();


        // GET: Questionario
        //lista de questionário de uma ProcessoPeticao
        public ActionResult Index(int? processoPeticaoId)
        {
            List<Questionario> questionarios = new List<Questionario>();
            if (processoPeticaoId != null)
            {
                questionarios = db.Questionarios.Where(x => x.ProcessoPeticaoId == processoPeticaoId).ToList();
                return View(questionarios.ToList());
            }
            questionarios = db.Questionarios.Include(q => q.ProcessoPeticao).ToList();
            return View(questionarios.ToList());
        }



        /*
        // GET: Questionario                                //TEXTO ORIGINAL
        public ActionResult Index()
        {
            var questionarios = db.Questionarios.Include(q => q.ProcessoPeticao);
            return View(questionarios.ToList());
        }
        */


        // GET: Questionario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questionario questionario = db.Questionarios.Find(id);
            if (questionario == null)
            {
                return HttpNotFound();
            }
            return View(questionario);
        }





                                                       
        // GET: Questionario/Create
        public ActionResult Create()
        {
            ViewBag.ProcessoPeticaoId = new SelectList(db.ProcessoPeticaos, "ProcessoPeticaoId", "LinkQuestionario");
            return View();
        }

        // POST: Questionario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuestionarioId,ProcessoPeticaoId,TituloTrecho,Pergunta,Resposta,Exemplo,DataModificacao")] Questionario questionario)
        {
            if (ModelState.IsValid)
            {
                db.Questionarios.Add(questionario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProcessoPeticaoId = new SelectList(db.ProcessoPeticaos, "ProcessoPeticaoId", "LinkQuestionario", questionario.ProcessoPeticaoId);
            return View(questionario);
        }

        // GET: Questionario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questionario questionario = db.Questionarios.Find(id);
            if (questionario == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProcessoPeticaoId = new SelectList(db.ProcessoPeticaos, "ProcessoPeticaoId", "LinkQuestionario", questionario.ProcessoPeticaoId);
            return View(questionario);
        }

        // POST: Questionario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuestionarioId,ProcessoPeticaoId,TituloTrecho,Pergunta,Resposta,Exemplo,DataModificacao")] Questionario questionario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProcessoPeticaoId = new SelectList(db.ProcessoPeticaos, "ProcessoPeticaoId", "LinkQuestionario", questionario.ProcessoPeticaoId);
            return View(questionario);
        }

        // GET: Questionario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questionario questionario = db.Questionarios.Find(id);
            if (questionario == null)
            {
                return HttpNotFound();
            }
            return View(questionario);
        }

        // POST: Questionario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Questionario questionario = db.Questionarios.Find(id);
            db.Questionarios.Remove(questionario);
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
