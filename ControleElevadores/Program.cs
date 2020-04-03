using ControleElevadores.Appplication.Interfaces;
using ControleElevadores.Appplication.Services;
using System;

namespace ControleElevadores
{
    class Program
    {
        private static void Main(string[] args)
        {
            IElevadorService _elevadorService = new ElevadorService();

            Console.WriteLine("LEGENDA: M: Matutino; V: Vespertino; N: Noturno");

            //a.Qual é o andar menos utilizado pelos usuários; 
            Console.WriteLine($"\na) Qual é o andar menos utilizado pelos usuários: {string.Join(", ", _elevadorService.andarMenosUtilizado())}");

            //b.Qual é o elevador mais frequentado e o período que se encontra maior fluxo;
            Console.WriteLine(string.Format("\nb) Qual é o elevador mais frequentado e o período que se encontra MAIOR fluxo: O elevador MAIS frequentado é o {0} " +
                "com maior fluxo no período {1}.", string.Join(", ", _elevadorService.elevadorMaisFrequentado()), string.Join(", ", _elevadorService.periodoMaiorFluxoElevadorMaisFrequentado())));

            //c.Qual é o elevador menos frequentado e o período que se encontra menor fluxo;
            Console.WriteLine(string.Format("\nc) Qual é o elevador menos frequentado e o período que se encontra MENOR fluxo: O(s) elevador(es) MENOS frequentado(s) é(são) {0} " +
                "com menor fluxo no período {1}.", string.Join(", ", _elevadorService.elevadorMenosFrequentado()), string.Join(", ", _elevadorService.periodoMenorFluxoElevadorMenosFrequentado())));

            //d.Qual o período de maior utilização do conjunto de elevadores;
            Console.WriteLine($"\nd) Qual o período de maior utilização do conjunto de elevadores: {string.Join(", ", _elevadorService.periodoMaiorUtilizacaoConjuntoElevadores())}");

            //e.Qual o percentual de uso de cada elevador com relação a todos os serviços prestados;
            Console.WriteLine($"\ne) Qual o percentual de uso de cada elevador com relação a todos os serviços prestados: \n\n" +
                $"Elevador A: { string.Join(", ", _elevadorService.percentualDeUsoElevadorA().ToString("#.##")) } %");

            Console.WriteLine($"Elevador B: { string.Join(", ", _elevadorService.percentualDeUsoElevadorB().ToString("#.##")) } %");
            Console.WriteLine($"Elevador C: { string.Join(", ", _elevadorService.percentualDeUsoElevadorC().ToString("#.##")) } %");
            Console.WriteLine($"Elevador D: { string.Join(", ", _elevadorService.percentualDeUsoElevadorD().ToString("#.##")) } %");
            Console.WriteLine($"Elevador E: { string.Join(", ", _elevadorService.percentualDeUsoElevadorE().ToString("#.##")) } %\n");
        }
    }
}
