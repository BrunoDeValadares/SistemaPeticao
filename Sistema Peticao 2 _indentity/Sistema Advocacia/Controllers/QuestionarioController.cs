using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Sistema_Advocacia.Context;
using Sistema_Advocacia.gerador;
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
                var itemProcessoPeticao = db.ProcessoPeticaos.Find(processoPeticaoId);
                ViewBag.Peticao = itemProcessoPeticao.PeticaoModelo.Nome;
                ViewBag.Cliente = itemProcessoPeticao.Processo.Cliente.Nome;
                questionarios = db.Questionarios.Where(x => x.ProcessoPeticaoId == processoPeticaoId).ToList();
                /*
                questionarios = db.Questionarios.
                    Where(x => x.ProcessoPeticaoId == processoPeticaoId).
                    Include(x => x.ProcessoPeticao).Include(x => x.ProcessoPeticao.PeticaoModelo).ToList();     
                */

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
            questionario.DataModificacao = DateTime.Now;
            if (ModelState.IsValid)
            {                
                db.Entry(questionario).State = EntityState.Modified;
                db.SaveChanges();


                return RedirectToAction("Index", "Questionario", new { processoPeticaoId = questionario.ProcessoPeticaoId}); 
                //return View()
            }
            ViewBag.ProcessoPeticaoId = new SelectList(db.ProcessoPeticaos, "ProcessoPeticaoId", "LinkQuestionario", questionario.ProcessoPeticaoId);
            return View(questionario);
        }


        // public ActionResult Finalizar(List<Questionario> questionarios)

       public string GetExemplo(string peticao, string titulo)
        {
            string padrao = @"" + titulo + @"([\}]*)";
            Regex regex = new Regex(padrao);
            return regex.Match(peticao).Groups[1].Value;
        }

        public ActionResult Finalizar(int? processoPeticaoId)
        {
            var questionarios = db.Questionarios.Where(q => q.ProcessoPeticaoId == processoPeticaoId).ToList();
            var processoPeticao = db.ProcessoPeticaos.Find(processoPeticaoId);
            var peticao = processoPeticao.PeticaoModelo.PeticaoModificada;
            int processoId = (int)processoPeticao.ProcessoId;

            GerarQuestionario gerarQuestionario = new GerarQuestionario();
            gerarQuestionario.GerarAnexosNoBD(peticao, questionarios);          
            

            return RedirectToAction("TodoProcesso", "Processo", new { id = processoId });
            //@Html.ActionLink("Voltar", "TodoProcesso", "Processo", new { id = Model.First().ProcessoPeticao.ProcessoId }, null)
        }
        


        /*
        public ActionResult Voltar(int? processoId)
        {
            var processoId = int.Parse(Request["TipoRecebimento"]);

            return
        }
        */

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
