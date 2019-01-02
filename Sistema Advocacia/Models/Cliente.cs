using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_Advocacia.Models
{
    //Teste commit 1
    public enum EstadoCivil
    {
        casado = 1,
        casada = 2,
        solteiro = 3,
        soleteira = 4,
        viúvo = 5,
        viúva = 6
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



        public string CPF { get; set; }

        public string RG { get; set; }

        [Display(Name = "Nome")]
        [Required]
        public string Nome { get; set; }

        public string Nacionalidade { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Selecione um registro")]
        [Display(Name = "Estado Civil")]
        public EstadoCivil EstadoCivil { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Nascimento { get; set; }

        public Sexo Sexo { get; set; }

        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        public string Logradouro { get; set; }

        [Display(Name = "Número")]
        public string Numero { get; set; }

        public string Setor { get; set; }

        public string Complemento { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }


        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //public string NomeCompleto { get { return string.Format("{0} {1}", Nome, Sobrenome); } }

        public string VogalSexo_
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
                string cpf = string.Format("inscrit{0} no CPF sob o nº {1}", VogalSexo_, CPF);
                string rg = string.Format("com cédula de identidade nº {0}", RG);

                return string.Format("{0}, {1}, {2}, {3}, {4}, {5}", Nome, Nacionalidade, EstadoCivil, cpf, rg, Endereco_);
            }
        }


    }
}