using Sistema_Advocacia.Context;
using Sistema_Advocacia.gerador;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Sistema_Advocacia.Models
{
    public class ProcessoPeticao
    {
        [Key]
        public int ProcessoPeticaoId { get; set; }

        //[Required]
        [Display(Name = "Processo")]
        public int? ProcessoId { get; set; }

        [Required]
        [Display(Name = "Peticao")]
        public int PeticaoModeloId { get; set; }        

        [Display(Name = "Link Questionário")]
        [DataType(DataType.Url)]
        public string LinkQuestionario { get; set; }

        [Display(Name = "Comentário")]
        [DataType(DataType.MultilineText)]
        public string Comentario { get; set;}

        [Display(Name = "Link Petição")]
        [DataType(DataType.Url)]
        public string LinkPeticao { get; set; }

        [Display(Name = "Finalizada?")]
        public bool Finalizada { get; set; }

        ///[Required]
        [Display(Name = "Data Cadastro")]        
        [DataType(DataType.Date)]
        public DateTime? DataCadastro { get; set; }

        [Display(Name = "Data Finalização")]
        [DataType(DataType.Date)]
        public DateTime? DataFinalizacao { get; set; }

        [Display(Name = "Data Protocolização")]
        [DataType(DataType.Date)]
        public DateTime? DataProtocolizacao { get; set; }

        //Montar documento/petição: Substituir perguntas por respostas
        [NotMapped]
        public string PeticaoRespondida
        {
            get
            {             
                StringBuilder peticaoRespondida = new StringBuilder();
                peticaoRespondida.Append(PeticaoModelo.PeticaoModificada);

                DBContext db = new DBContext();
                var questionarios = db.Questionarios.Where(q => q.ProcessoPeticaoId == ProcessoPeticaoId).ToList();

                if (questionarios.Count > 0)
                {
                    foreach (var questionario in questionarios)
                    {
                        peticaoRespondida.Replace(questionario.Pergunta, questionario.Resposta);
                    }
                }             

                //var peticaoSemMarcadores = Regex.Replace(peticaoRespondida.ToString(), @"\[|\]", "");

                return peticaoRespondida.ToString();
            }
        }



        //apagar
        [NotMapped]
        public string PeticaoRespondida2
        {
            get
            {
                string peticaoRespondida = null;
                DBContext db= new DBContext();
                var questionarios = db.Questionarios.Where(q => q.ProcessoPeticaoId == ProcessoPeticaoId).ToList();
                
                if (questionarios.Count > 0)
                {
                    GerarQuestionario gerarQuestionario = new GerarQuestionario();
                    peticaoRespondida = gerarQuestionario.MontarPeticao(PeticaoModelo.PeticaoModificada, questionarios);
                }

                return peticaoRespondida;
            }            
        }


        public virtual PeticaoModelo PeticaoModelo { get; set; }
        public virtual Processo Processo{ get; set; }
    }
}