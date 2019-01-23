using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Sistema_Advocacia.Models
{
    //Teste commit 1
    public enum EstadoCivil
    {
        casado = 1,
        casada = 2,
        solteiro = 3,
        solteira = 4,
        viúvo = 5,
        viúva = 6,
        divorciado = 7,
        divorciada = 8
    
    }

    public enum Sexo
    {
        masculino = 1,
        feminino = 2
    }

    [DisplayColumn("Nome", "Nome")]
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        [Display(Name = "CPF/CNPJ")]
        [StringLength(14), Column(TypeName = "char")]        
        public string CPF { get; set; }
        
        ///[Required]
        [StringLength(20)]
        public string RG { get; set; }

        [StringLength(maximumLength:80, MinimumLength = 3)]
        [Display(Name = "Nome")]
        [Required]
        public string Nome { get; set; }

        ///[Required]
        [StringLength(50)]
        public string Nacionalidade { get; set; }

        ///[Required]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um registro")]
        [Display(Name = "Estado Civil")]
        public EstadoCivil EstadoCivil { get; set; }

        ///[Required]
        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Nascimento { get; set; }

        ///[Required]
        public Sexo Sexo { get; set; }

        [StringLength(maximumLength:30, MinimumLength =3)]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        ///[Required]
        [StringLength(50)]
        public string Logradouro { get; set; }


        [Display(Name = "Número")]
        public int? Numero { get; set; }

        [StringLength(10)]
        public string Quadra{ get; set; }

        [StringLength(10)]
        public string Lote { get; set; }


        [StringLength(50, MinimumLength = 3)]
        public string Setor { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Complemento { get; set; }

        ///[Required]
        [StringLength(50 , MinimumLength = 3)]
        public string Cidade { get; set; }

        ///[Required]        
        [StringLength(2), Column(TypeName ="char") ]
        public string Estado { get; set; }


        [StringLength(50)]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage  = "Digite um email válido!" )]
        public string Email { get; set; }

        [Display(Name = "Comentário")]
        [DataType(DataType.MultilineText)]
        public string Comentario { get; set; }

        //public string NomeCompleto { get { return string.Format("{0} {1}", Nome, Sobrenome); } }

        public string VogalSexo_
        {
            get
            {
                //var vogal = Sexo == "a"
                //b = (a > 0) ? 1 : 2;
                //return vogal;

                if (string.Format("{0}", Sexo).Equals("masculino"))
                {
                    return "o";
                }
                else
                {
                    return "a";
                }
            }
        }


        public string VogalSexo2_
        {
            get
            {               
                if (string.Format("{0}", Sexo).Equals("masculino"))
                {
                    return "o";
                }
                else
                {
                    return "a";
                }
            }
        }

        public string Endereco_
        {
            get
            {
                //string logradouro = string.Format("residente e domiciliado à {0},", Logradouro);
                //string logradouro = (Logradouro != null)? string.Concat("residente e domiciliado à ", Logradouro) : null;
                string logradouro = (Logradouro != null) ? "residente e domiciliado à " + Logradouro : null;





                string numero = string.Format("nº {0},", Numero);
                string setor = string.Format("Setor {0},", Setor);

                string endereco = string.Format("{0} {1} {2} {3}-{4}", logradouro, numero, setor, Cidade, Estado);

                return endereco;
                ;
            }
        }

        public string Endereco_2
        {
            get
            {                
                string logradouro = string.Format("residente e domiciliado à {0},", Logradouro);
                string numero = string.Format("nº {0},", Numero);
                string setor = string.Format("Setor {0},", Setor);

                string endereco = string.Format("{0} {1} {2} {3}-{4}", logradouro, numero, setor, Cidade, Estado);




                return endereco;
                ;
            }
        }

        public string Qualificacao_
        {
            get
            {
                string vogalSexo = (int)Sexo == 1? "o" : "a";

                string logradouro = (Logradouro != null) ? ", residente e domiciliado à " + Logradouro : null;
                string numero = (Numero != null) ? ", nº " + Numero : null;
                string quadra = (Quadra != null) ? ", quadra " + Quadra : null;
                string lote = (Lote != null) ? ", lote " + Lote : null;
                string setor = (Setor != null) ? ", Setor " + Setor : null;
                string cidade = (Cidade != null) ? ", " + Cidade : null;
                string estado = (Quadra != null) ? "-" + Estado : null;
                string endereco = string.Concat(logradouro, numero, quadra, lote, setor, cidade, estado);
                          
                string cpf =  (CPF != null)?  string.Format(", inscrit{0} no CPF sob o nº {1}", vogalSexo, CPF) : null;
                string rg = (RG != null)?  string.Format(", com cédula de identidade nº {0}", RG): null ;

                return string.Format("{0}, {1}, {2}{3}{4}{5}", Nome, Nacionalidade, EstadoCivil, cpf, rg, endereco);
            }
        }






        public string Qualificacao_2
        {
            get
            {
                string cpf = string.Format("inscrit{0} no CPF sob o nº {1}", VogalSexo_, CPF);
                string rg = string.Format("com cédula de identidade nº {0}", RG);

                return string.Format("{0}, {1}, {2}, {3}, {4}, {5}", Nome, Nacionalidade, EstadoCivil, cpf, rg, Endereco_);
            }
        }

        public string PrimeiroNome
        {
            get
            {
                Regex regex = new Regex(@"[\w]*");
                string PrimeiroNome = regex.Match(Nome).Value;
                return PrimeiroNome;
            }

        }



    }
}