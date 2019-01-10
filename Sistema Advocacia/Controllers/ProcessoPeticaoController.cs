using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sistema_Advocacia.Context;
using Sistema_Advocacia.gerador;
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
            //return RedirectToAction("TodoProcesso", "Processo", new { id = processoPeticao.ProcessoId });
        }


        // GET: ProcessoPeticao/Create
        /*
        public ActionResult Create()
        {
            ViewBag.PeticaoModeloId = new SelectList(db.PeticaoModeloes, "PeticaoModeloId", "Nome");
            return View();
        }
        */

        public ActionResult Create(int? processoId)
        {
            ViewBag.PeticaoModeloId = new SelectList(db.PeticaoModeloes, "PeticaoModeloId", "Nome");

            if (processoId != null)
            {
                ProcessoPeticao processoPeticao = new ProcessoPeticao { ProcessoId = (int)processoId, DataCadastro =  DateTime.Today};                
                //return RedirectToAction("TodoProcesso", "Processo", new { id = processoPeticao.ProcessoId }); //APAGAR LIXO
                return View(processoPeticao);  //APAGAR LIXO
            }

            return View();
        }


        // POST: ProcessoPeticao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.


        /*                                             \\TEXTO ORIGINAL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProcessoPeticaoId,ProcessoId,PeticaoModeloId,LinkQuestionario,Comentario,LinkPeticao,Finalizada")] ProcessoPeticao processoPeticao)
        {
            if (ModelState.IsValid)
            {
                db.ProcessoPeticaos.Add(processoPeticao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PeticaoModeloId = new SelectList(db.PeticaoModeloes, "PeticaoModeloId", "Nome", processoPeticao.PeticaoModeloId);
            return View(processoPeticao);
        }
        */

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProcessoPeticaoId,ProcessoId,PeticaoModeloId,LinkQuestionario,Comentario,LinkPeticao,Finalizada, DataCadastro")] ProcessoPeticao processoPeticao)
        {
            if (ModelState.IsValid)
            {

                db.ProcessoPeticaos.Add(processoPeticao);
                db.SaveChanges();

                GerarQuestionario gerarQuestionario = new GerarQuestionario();
                gerarQuestionario.CriarQuestionario(processoPeticao.ProcessoPeticaoId);
                //gerarQuestionario.CriarQuestionario(processoPeticao.PeticaoModeloId);
                //CriarQuestionario(processoPeticao.ProcessoPeticaoId); //linha alterada
                //return RedirectToAction("Index");
                return RedirectToAction("TodoProcesso", "Processo", new { id = processoPeticao.ProcessoId });
            }
            ViewBag.PeticaoModeloId = new SelectList(db.PeticaoModeloes, "PeticaoModeloId", "Nome", processoPeticao.PeticaoModeloId);
           
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
            ViewBag.PeticaoModeloId = new SelectList(db.PeticaoModeloes, "PeticaoModeloId", "Nome", processoPeticao.PeticaoModeloId);
            return View(processoPeticao);
        }

        // POST: ProcessoPeticao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProcessoPeticaoId,ProcessoId,PeticaoModeloId,LinkQuestionario,Comentario,LinkPeticao,Finalizada, DataCadastro, DataFinalizacao, DataProtocolizacao")] ProcessoPeticao processoPeticao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(processoPeticao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TodoProcesso", "Processo", new { id = processoPeticao.ProcessoId });
                //return RedirectToAction("Index"); //unica linha alterada
            }
            ViewBag.PeticaoModeloId = new SelectList(db.PeticaoModeloes, "PeticaoModeloId", "Nome", processoPeticao.PeticaoModeloId);
            return View(processoPeticao);
        }

        /*
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
        */




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
            //return RedirectToAction("Index");
            return RedirectToAction("TodoProcesso", "Processo", new { id = processoPeticao.ProcessoId });
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
