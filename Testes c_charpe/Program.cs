using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Testes_c_charpe
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Regex regex = new Regex(@"[\w]*");
            string PrimeiroNome = regex.Match("ADAOstrogildo-da Silva Sauro").Value;
            Console.WriteLine(PrimeiroNome);
            Console.ReadKey();
            */
            //PrimeiroNome();
            //RegexAnexo();
            AprenderIndexOf();
            

        }

        static void PrimeiroNome()
        {
            Regex regex = new Regex(@"[\w]*");
            var PrimeiroNome = regex.Match("AdãoAnacredo").Value;
            Console.WriteLine(PrimeiroNome);
            Console.ReadKey();

        }

        static void RegexAnexo()
        {
            string Padrao = @"doc_[0-9]*";

            Regex regex = new Regex(Padrao);

            string Texto = "{jogador gogue doc_35 e que pode ir pra csa doc_37, na sua casa}";
            var resultado = regex.Matches(Texto);
        }

        static void AprenderIndexOf()
        {
            string anexo = "doc_125456812548";
            var anexoId = anexo.Substring(4);
            Console.WriteLine("anexo: " + anexoId);
            Console.ReadKey();
        }
    }



}
