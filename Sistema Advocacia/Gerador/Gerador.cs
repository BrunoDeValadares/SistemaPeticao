using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Advocacia.Gerador
{
    public class Gerador
    {
        List<TrechoPeticao> trechos = new List<TrechoPeticao>();
        /*
        struct ListElement
        {
            public int Id;
            public string Name
               ;
        } 
        */





        public void ExtrairTrechoPeticao(string peticao)
        {    
            





            int PosicaoInicio = 0;
            while (peticao.IndexOf("{")>0) // refazer com any
            {
                TrechoPeticao trechoPeticao = new TrechoPeticao();

                int IndexChaveAbertura = peticao.IndexOf("{");
                int IndexChaveFechamento = peticao.IndexOf("}");

                trechoPeticao.Trecho = peticao.Substring(IndexChaveAbertura, IndexChaveFechamento);

                trechos.Add(trechoPeticao);

                PosicaoInicio = IndexChaveFechamento + 1;
                peticao = peticao.Substring(PosicaoInicio);
            }
        }

        public void ExtrairPerguntaPeticao()
        {
            List<PerguntaPeticao> perguntas = new List<PerguntaPeticao>();

            foreach (var trecho in trechos)
            {
                string trechoPeticao = trecho.Trecho;

                while (trechoPeticao.IndexOf("[")>0)
                {                   

                    PerguntaPeticao perguntaPeticao = new PerguntaPeticao();

                    //int IndexColcheteAbertura = trecho.Trecho.IndexOf("[");
                    int IndexColcheteAbertura = trechoPeticao.IndexOf("[");
                    int IndexColcheteFechamento = trechoPeticao.IndexOf("]");

                    perguntaPeticao.Pergunta = trechoPeticao.Substring(IndexColcheteAbertura, IndexColcheteFechamento);

                    trechoPeticao= trechoPeticao.Substring(IndexColcheteFechamento + 1);
                }



            }

        }

    }
}