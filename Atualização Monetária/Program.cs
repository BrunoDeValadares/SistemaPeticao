using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atualização_Monetária
{
    class Program
    {
        static void Main(string[] args)
        {
            #region
            //Console.WriteLine(Atualizar(0.01, 1000, 10));
            //Console.WriteLine(Atualizar2(taxa: 0.01, capital: 1000, inicio: DateTime.Parse("2018-01-01"), fim: DateTime.Parse("2018-11-01"), proRata: null));
            #endregion
            //Console.WriteLine(Atualizar4(taxa: 0.01m, capital: 90000, inicio: DateTime.Parse("2018-01-01"), fim: DateTime.Parse("2018-01-15"), proRata: true));
            Console.WriteLine(GetJurosCompostos(taxa: 1.0, capital: 1000000, inicio: DateTime.Parse("2018-01-05"), fim: DateTime.Parse("2018-01-15"), proRata: true));
            Console.ReadKey();
        }

        static decimal GetJurosCompostos(double taxa, decimal capital, DateTime inicio, DateTime fim, bool? proRata = false)
        {
            decimal montante = capital;
            int meses = (int)((fim.Year - inicio.Year) * 12.0) + (fim.Month - inicio.Month);
            if (meses == 0) meses = -1;


            //pro rata data inicial            
            int diasInicio = inicio.Day;
            int diasQPossuiMesInicial = DateTime.DaysInMonth(inicio.Year, inicio.Month);
            decimal proRataInicio = (decimal)(diasQPossuiMesInicial - diasInicio + 1) / diasQPossuiMesInicial;

            //pro rata data final
            int diasFim = fim.Day;
            int diasQPossuiMesFinal = DateTime.DaysInMonth(fim.Year, fim.Month);
            decimal proRataFim = (decimal)(diasFim - 1) / diasQPossuiMesFinal;
            #region Mensagens
            /*
            Console.WriteLine("diasInicio" + diasInicio);
            Console.WriteLine("diasQPossuiMesInicial" + diasQPossuiMesInicial);
            Console.WriteLine("diasQPossuiMesInicial - diasInicio + 1:   " + (diasQPossuiMesInicial - diasInicio + 1));
            Console.WriteLine("pro rata inicio: " + proRataInicio);


            Console.WriteLine("proRataFim: " + proRataFim);
            //Console.WriteLine("proRataFim: " + proRataFim);           

            /*
            Console.WriteLine("pro rata inicio: " + proRataInicio);
            Console.WriteLine("proRataFim: " + proRataFim);
            Console.WriteLine("periodo: " + (proRataInicio + proRataFim - 1));
            Console.WriteLine("11.0 / 31: " +11.0 / 31);
            */
            #endregion

            //calculo do periodo total 
            decimal periodo = proRataInicio + meses + proRataFim;
            periodo = Math.Round(periodo, 4);
            Console.WriteLine("periodo: " + periodo);
            //Console.WriteLine("periodo: " + Math.Round(periodo,4));


            //formula dos juros compostos
            double juros = (Math.Pow(1 + taxa, (double)periodo) - 1);


            decimal jurosDecimais = (decimal)(Math.Pow((1 + taxa / 100.0), (double)periodo) - 1)*100;
            Console.WriteLine("juros não arredendadoss: " + jurosDecimais );
            jurosDecimais = Math.Round(jurosDecimais, 5);
            //decimal d = Math.Round((decimal)juros, 4);
            //juros = Math.Round((Math.Pow(1 + taxa, periodo) - 1), 4);
            //Console.WriteLine("juros: " + juros);            
            Console.WriteLine("juros decimais arredondados: " + jurosDecimais);
            montante = capital * (1 + (jurosDecimais/100));
            montante = Math.Round(montante, 2);
            //montante = (capital * Math.Pow(1 + taxa, periodo)) - 1;
            return montante;
        }


        static double GetJurosCompostos2(double taxa, double capital, DateTime inicio, DateTime fim, bool? proRata = false)
        {
            double montante = capital;
            double meses = ((fim.Year - inicio.Year) * 12.0) + (fim.Month - inicio.Month);
            if (meses == 0) meses = -1;
            

            //pro rata data inicial            
            int diasInicio = inicio.Day;
            int diasQPossuiMesInicial = DateTime.DaysInMonth(inicio.Year, inicio.Month);
            double proRataInicio = (double)(diasQPossuiMesInicial - diasInicio + 1) / diasQPossuiMesInicial;

            //pro rata data final
            int diasFim = fim.Day;
            int diasQPossuiMesFinal = DateTime.DaysInMonth(fim.Year, fim.Month);
            double proRataFim = (double)(diasFim - 1) / diasQPossuiMesFinal;
            #region Mensagens
            
            Console.WriteLine("diasInicio" + diasInicio);
            Console.WriteLine("diasQPossuiMesInicial" + diasQPossuiMesInicial);            
            Console.WriteLine("diasQPossuiMesInicial - diasInicio + 1:   " + (diasQPossuiMesInicial - diasInicio + 1));
            Console.WriteLine("pro rata inicio: " + proRataInicio);

            
            Console.WriteLine("proRataFim: " + proRataFim);
            //Console.WriteLine("proRataFim: " + proRataFim);           
            
            /*
            Console.WriteLine("pro rata inicio: " + proRataInicio);
            Console.WriteLine("proRataFim: " + proRataFim);
            Console.WriteLine("periodo: " + (proRataInicio + proRataFim - 1));
            Console.WriteLine("11.0 / 31: " +11.0 / 31);
            */
            #endregion

            //calculo do periodo total 
            double periodo = proRataInicio + meses + proRataFim;
            Console.WriteLine("periodo: " + periodo);

            //formula dos juros compostos
            var juros = Math.Round( (Math.Pow(1 + taxa, periodo) - 1), 4);
            Console.WriteLine("juros: " + juros * 100);
            montante = capital * (1 + juros);
            //montante = (capital * Math.Pow(1 + taxa, periodo)) - 1;
            return montante;
        }

        static double Atualizar0(double taxa, float capital, int tempo)
        {
            double montante = capital;

            for (int i = 0; i < tempo; i++)
            {
                montante = montante * (1 + taxa);
            }
            return montante;
        }
        static decimal Atualizar4(decimal taxa, decimal capital, DateTime inicio, DateTime fim, bool? proRata = false)
        {
            decimal montante = capital;
            int meses = ((fim.Year - inicio.Year) * 12) + (fim.Month - inicio.Month);

            //Console.WriteLine(meses);


            for (int i = 0; i < meses; i++)
            {
                montante = montante * (1 + taxa);
            }

            if (proRata == true)
            {
                int diasFim = fim.Day;
                int diasQPossuiOMes = DateTime.DaysInMonth(fim.Year, fim.Month);
                //Console.WriteLine(diasQPossuiOMes);
                // int  diasQPossuiOMes = 30;

                // double v = diasQPossuiOMes / 4.0;
                //Console.WriteLine(v);


                //double jurosProRata = (diasFim - 1.0) * (1.0 / diasQPossuiOMes) * taxa;
                decimal jurosProRata = (diasFim - 1) * (1m / diasQPossuiOMes) * taxa;


                // Console.WriteLine(diasQPossuiOMes);

                //Console.WriteLine("dias que possui o mes:" + diasQPossuiOMes);
                //Console.WriteLine("razão: 1/dias mes: " + (1.0 / diasQPossuiOMes));
                //Console.WriteLine("razão: 1/dias mes: " + (1.0 / 30));
                Console.WriteLine("jurosProRata: " + jurosProRata);

                //double f = 10.0 / 3.0;                
                //Console.WriteLine("razão: 1/10: "+ f);

                decimal valorProRata = montante * (1 + jurosProRata);
                montante = valorProRata;

            }


            return montante;
        }
        static double Atualizar3(double taxa, float capital, DateTime inicio, DateTime fim, bool? proRata = false)
        {
            double montante = capital;
            int meses = ((fim.Year - inicio.Year) * 12) + (fim.Month - inicio.Month);

            //Console.WriteLine(meses);


            for (int i = 0; i < meses; i++)
            {
                montante = montante * (1 + taxa);
            }

            if (proRata == true)
            {
                int diasFim = fim.Day;
                double diasQPossuiOMes = DateTime.DaysInMonth(fim.Year, fim.Month);
                double jurosProRata = (diasFim - 1) * (1 / diasQPossuiOMes) * taxa;
                double valorProRata = montante * (1 + jurosProRata);
                montante = valorProRata;
            }


            return montante;
        }
    }
}
