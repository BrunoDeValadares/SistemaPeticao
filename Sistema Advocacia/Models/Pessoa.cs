using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sistema_Advocacia.Models
{
    public enum PFPJ
    {
        PF = 1,
        PJ = 2
    };

    [DisplayColumn("Nome", "Nome")]
    public class Pessoa
    {
        [Key]
        public int PessoaId { get; set; }

        [Display(Name = "CPF/CNPJ")]
        [StringLength(18), Column(TypeName = "char")]
        public string CPF { get; set; }

        ///[Required]
        [StringLength(20)]
        public string RG { get; set; }

        [StringLength(maximumLength: 80, MinimumLength = 3)]
        [Display(Name = "Nome")]
        [Required]
        public string Nome { get; set; }

        ///[Required]
        [StringLength(50)]
        public string Nacionalidade { get; set; }

        ///[Required]
        [Range(0, int.MaxValue, ErrorMessage = "Selecione um registro")]
        [Display(Name = "Estado Civil")]
        public EstadoCivil EstadoCivil { get; set; }

        [Display(Name = "Data do Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? Nascimento { get; set; }

        ///[Required]
        public Sexo Sexo { get; set; }

        [StringLength(maximumLength: 30, MinimumLength = 3)]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        ///[Required]
        [StringLength(50)]
        public string Logradouro { get; set; }


        [Display(Name = "Número")]
        public int? Numero { get; set; }

        [StringLength(10)]
        public string Quadra { get; set; }

        [StringLength(10)]
        public string Lote { get; set; }


        [StringLength(50, MinimumLength = 3)]
        public string Setor { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Complemento { get; set; }

        ///[Required]
        [StringLength(50, MinimumLength = 3)]
        public string Cidade { get; set; }

        ///[Required]        
        [StringLength(2), Column(TypeName = "char")]
        public string Estado { get; set; }


        [StringLength(50)]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Digite um email válido!")]
        public string Email { get; set; }

        [Display(Name = "Comentário")]
        [DataType(DataType.MultilineText)]
        public string Comentario { get; set; }

        [Required]
        [Display(Name = "Pessoa Jurídica?")]
        public bool PessoJuridica { get; set; }

        [Display(Name = "Representada por?")]
        [DataType(DataType.MultilineText)]
        public string RepresentadaPor { get; set; }

        [NotMapped]
        public string Qualificacao_
        {
            get
            {
                string vogalSexo = (int)Sexo == 1 ? "o" : "a";

                string logradouro = (Logradouro != null) ? ", residente e domiciliado à " + Logradouro : null;
                string numero = (Numero != null) ? ", nº " + Numero : null;
                string quadra = (Quadra != null) ? ", quadra " + Quadra : null;
                string lote = (Lote != null) ? ", lote " + Lote : null;
                string setor = (Setor != null) ? ", Setor " + Setor : null;
                string cidade = (Cidade != null) ? ", " + Cidade : null;
                string estado = (Quadra != null) ? "-" + Estado : null;
                string endereco = string.Concat(logradouro, numero, quadra, lote, setor, cidade, estado);

                string cpf = (CPF != null) ? string.Format(", inscrit{0} no CPF sob o nº {1}", vogalSexo, CPF) : null;
                string rg = (RG != null) ? string.Format(", com cédula de identidade nº {0}", RG) : null;

                return string.Format("{0}, {1}, {2}{3}{4}{5}", Nome, Nacionalidade, EstadoCivil, cpf, rg, endereco);
            }
        }

        [NotMapped]
        public string VogalSexo
        {
            get { return (int)Sexo == 1 ? "o" : "a"; }
        }

        [NotMapped]
        public string QualificacaoComNomeVazio_
        {
            get
            {
                string vogalSexo = (int)Sexo == 1 ? "o" : "a";

                string nacionalidade = (Nacionalidade != null) ? ", " + Nacionalidade : null;
                string estadoCivil = ((int)EstadoCivil != 0) ? ", " + EstadoCivil : null;
                string logradouro = (Logradouro != null) ? ", residente e domiciliado à " + Logradouro : null;
                string numero = (Numero != null) ? ", nº " + Numero : null;
                string quadra = (Quadra != null) ? ", quadra " + Quadra : null;
                string lote = (Lote != null) ? ", lote " + Lote : null;
                string setor = (Setor != null) ? ", Setor " + Setor : null;
                string cidade = (Cidade != null) ? ", " + Cidade : null;
                string estado = (Estado != null) ? "-" + Estado : null;
                string endereco = string.Concat(logradouro, numero, quadra, lote, setor, cidade, estado);

                string cpf = (CPF != null) ? string.Format(", inscrit{0} no CPF sob o nº {1}", vogalSexo, CPF) : null;
                string rg = (RG != null) ? string.Format(", com cédula de identidade nº {0}", RG) : null;

                return string.Concat(nacionalidade, estadoCivil, cpf, rg, endereco);
                //return string.Format("{0}{1}{2}{3}{4}", Nacionalidade, EstadoCivil, cpf, rg, endereco);
            }
        }

        [Display(Name = "Qualificação")]
        [NotMapped]
        public string QualificacaoPJComNomeVazio_
        {
            get
            {
                string vogalSexo = (int)Sexo == 1 ? "o" : "a";
                string nacionalidade = (Nacionalidade != null) ? ", " + Nacionalidade : null;
                string estadoCivil = ((int)EstadoCivil != 0) ? ", " + EstadoCivil : null;
                string rg = (RG != null) ? string.Format(", com cédula de identidade nº {0}", RG) : null;

                string introducaoEndereco = PessoJuridica == false ? ", residente e domiciliado à " : ", com sede à ";
                string logradouro = (Logradouro != null) ? Logradouro : null;
                string numero = (Numero != null) ? ", nº " + Numero : null;
                string quadra = (Quadra != null) ? ", quadra " + Quadra : null;
                string lote = (Lote != null) ? ", lote " + Lote : null;
                string setor = (Setor != null) ? ", Setor " + Setor : null;
                string cidade = (Cidade != null) ? ", " + Cidade : null;
                string estado = (Estado != null) ? "-" + Estado : null;
                string endereco = string.Concat(logradouro, numero, quadra, lote, setor, cidade, estado);
                if (endereco != "")
                    endereco = introducaoEndereco + endereco;

                string IntrucaoCPFCNPJ = (PessoJuridica == true) ?
                    " pessoa jurídica de direito privado, inscrita no CNPJ sob o nº " : " , inscrit" + vogalSexo + " no CPF sob o nº ";
                string cpf = (CPF != null) ? IntrucaoCPFCNPJ + CPF : null;

                string representadaPor = (RepresentadaPor != null) ? ", neste ato representada por " + RepresentadaPor : null;

                return string.Format("{0}{1}{2}{3}{4}{5}", nacionalidade, estadoCivil, cpf, rg, endereco, representadaPor);
            }
        }

        [Display(Name = "Cliente Advocacia")]
        public bool ClienteAdvocaticia { get; set; }

        public bool Autor { get; set; }

        [Display(Name = "Réu")]
        public bool Reu { get; set; }

        public bool Colaborador { get; set; }

    }
}