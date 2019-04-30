using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Sistema_Advocacia.Context;
using Sistema_Advocacia.Models;

namespace Sistema_Advocacia.Controllers
{
    public class DocumentoModeloController : Controller
    {
        private DBContext db = new DBContext();

        // GET: DocumentoModelo
        public ActionResult Index()
        {
            return View(db.DocumentoModeloes.ToList());
        }

        // GET: DocumentoModelo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentoModelo documentoModelo = db.DocumentoModeloes.Find(id);
            if (documentoModelo == null)
            {
                return HttpNotFound();
            }
            return View(documentoModelo);
        }

        // GET: DocumentoModelo/Create
        public ActionResult Create()
        {
            return View();
        }

        /*Create antigo
         POST: DocumentoModelo/Create
         Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
         obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        public ActionResult Create([Bind(Include = "DocumentoModeloId,Nome,Comentarios,DataCriacao")] DocumentoModelo documentoModelo, HttpPostedFileBase UploadArquivo)
        */


        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult Create(DocumentoModelo documentoModelo, HttpPostedFileBase UploadArquivo)
        {
            //Faça upoload do arquivo. Se ele não existir gere um erro. 
            if (UploadArquivo != null && UploadArquivo.ContentLength > 0)
            {
                string arquivoFileName = Path.GetFileName(UploadArquivo.FileName);
                string FolderPath = Path.Combine(Server.MapPath("~/Templates"), arquivoFileName);
                UploadArquivo.SaveAs(FolderPath);
                documentoModelo.EnderecoArquivo = FolderPath;

                if (ModelState.IsValid)
                {
                    db.DocumentoModeloes.Add(documentoModelo);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
                ViewBag.Message = "Selecione um arquivo";

            return View(documentoModelo);
        }

        // GET: DocumentoModelo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentoModelo documentoModelo = db.DocumentoModeloes.Find(id);
            if (documentoModelo == null)
            {
                return HttpNotFound();
            }
            return View(documentoModelo);
        }        
        /*Edit Antigo
        // POST: DocumentoModelo/Edit/5
        //public ActionResult Edit([Bind(Include = "DocumentoModeloId,Nome,Comentarios,DataCriacao")] DocumentoModelo documentoModelo)
        */


        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult Edit(DocumentoModelo documentoModelo, HttpPostedFileBase file)
        {            
         //Veja se um arquivo foi selecionado, senão gere erro. Depois veja se tem nomes identicos e então  substitua o arquivo antigo pelo novo. Senão gere erro. 
            if (file != null && file.ContentLength > 0)
            {
                var nomeArquivo = new Regex(@"[^\\]*$").Match(file.FileName).Value;

                if (documentoModelo.NomeArquivo == nomeArquivo)
                {
                    string arquivoFileName = Path.GetFileName(file.FileName);
                    string FolderPath = Path.Combine(Server.MapPath("~/Templates"), arquivoFileName);
                    file.SaveAs(FolderPath);
                    documentoModelo.EnderecoArquivo = FolderPath;
                }

                else
                {
                    ModelState.AddModelError("NomeArquivo", "O nome do arquivo atualizado deve ser igual ao original!");
                }
            }

            else
            {
                ModelState.AddModelError("NomeArquivo", "Selecione o arquivo atualizado!");
            }
            
            //senão incorreu em nehum erro (gerado no ModelState então)
            if (ModelState.IsValid)
            {
                db.Entry(documentoModelo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(documentoModelo);
        }

        // GET: DocumentoModelo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentoModelo documentoModelo = db.DocumentoModeloes.Find(id);
            if (documentoModelo == null)
            {
                return HttpNotFound();
            }
            return View(documentoModelo);
        }

        // POST: DocumentoModelo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DocumentoModelo documentoModelo = db.DocumentoModeloes.Find(id);

            if (System.IO.File.Exists(documentoModelo.EnderecoArquivo))
            {
                System.IO.File.Delete(documentoModelo.EnderecoArquivo);
            }

            db.DocumentoModeloes.Remove(documentoModelo);
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
