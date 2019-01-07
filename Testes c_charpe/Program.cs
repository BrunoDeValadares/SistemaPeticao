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
            PrimeiroNome();
        }

        static void PrimeiroNome()
        {
            Regex regex = new Regex(@"[\w]*");
            var PrimeiroNome = regex.Match("AdãoAnacredo").Value;
            Console.WriteLine(PrimeiroNome);
            Console.ReadKey();

        }
    }
}
