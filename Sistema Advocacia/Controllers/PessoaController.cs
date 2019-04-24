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
    public class PessoaController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Pessoa
        public ActionResult Index()
        {
            return View(db.Pessoas.ToList());
        }

        // GET: Pessoa/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoas.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // GET: Pessoa/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pessoa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PessoJuridica,PessoaId,CPF,RG,Nome,Nacionalidade,EstadoCivil,Nascimento,Sexo,Telefone,Logradouro,Numero,Quadra,Lote,Setor,Complemento,Cidade,Estado,Email,Comentario,RepresentadaPor,ClienteAdvocaticia,Autor,Reu,Colaborador")] Pessoa pessoa)
        {
            if (pessoa.CPF != null)
            {
                bool cpfValido = Validacao.ValidaCPF.IsCpf(pessoa.CPF) || Validacao.ValidaCNPJ.IsCnpj(pessoa.CPF);
                if (cpfValido == false)
                    ModelState.AddModelError("CPF", "CPF/CNPJ inválido!");
            }
            if (ModelState.IsValid)
            {
                db.Pessoas.Add(pessoa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pessoa);
        }

        // GET: Pessoa/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoas.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // POST: Pessoa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PessoJuridica,PessoaId,CPF,RG,Nome,Nacionalidade,EstadoCivil,Nascimento,Sexo,Telefone,Logradouro,Numero,Quadra,Lote,Setor,Complemento,Cidade,Estado,Email,Comentario,RepresentadaPor,ClienteAdvocaticia,Autor,Reu,Colaborador")] Pessoa pessoa)
        {
            if (pessoa.CPF != null)
            {
                bool cpfValido = Validacao.ValidaCPF.IsCpf(pessoa.CPF) || Validacao.ValidaCNPJ.IsCnpj(pessoa.CPF);
                if (cpfValido == false)
                    ModelState.AddModelError("CPF", "CPF/CNPJ inválido!");
            }

            if (ModelState.IsValid)
            {
                db.Entry(pessoa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pessoa);
        }

        // GET: Pessoa/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoas.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // POST: Pessoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pessoa pessoa = db.Pessoas.Find(id);
            db.Pessoas.Remove(pessoa);
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
