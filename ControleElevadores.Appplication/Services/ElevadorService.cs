using System.Collections.Generic;
using System.IO;
using System.Linq;
using ControleElevadores.Appplication.Interfaces;
using ControlerElevadores.Domain.Model;
using Newtonsoft.Json;

namespace ControleElevadores.Appplication.Services
{
    public class ElevadorService : IElevadorService
    {
        List<PesquisaModel> lstDadosPesquisa = getDadosPesquisa();
        private const float _100Porcento = 100;
        private const int _totalElevadores = 5;

        public List<int> andarMenosUtilizado()
        {
            int[] arrAndaresPesquisa = lstDadosPesquisa.Select(x => x.andar).ToArray();

            var andaresAgrupados = arrAndaresPesquisa
             .GroupBy(x => x)
             .Select(a => new
             {
                 Andar = a.Key,
                 Quantidade = a.Count()
             })
             .ToArray();

            List<int> lstAndaresMenosUtilizados = andaresAgrupados.Where(x => x.Quantidade <= 1).OrderBy(x => x.Andar).Select(x => x.Andar).ToList();
            return lstAndaresMenosUtilizados;
        }

        public List<char> elevadorMaisFrequentado()
        {
            string[] arrElevadoresPesquisa = lstDadosPesquisa.Select(x => x.elevador).ToArray();

            var elevadoresAgrupados = arrElevadoresPesquisa
            .GroupBy(x => x)
            .Select(e => new
            {
                Elevador = e.Key,
                Quantidade = e.Count()
            })
            .ToArray();

            List<char> lstElevadorMaisFrequentado = new List<char>();
            lstElevadorMaisFrequentado.AddRange(elevadoresAgrupados.OrderBy(i => i.Quantidade).LastOrDefault().Elevador);

            return lstElevadorMaisFrequentado;
        }

        public List<char> elevadorMenosFrequentado()
        {
            string[] arrElevadoresPesquisa = lstDadosPesquisa.Select(x => x.elevador).ToArray();

            var elevadoresAgrupados = arrElevadoresPesquisa
            .GroupBy(x => x)
            .Select(e => new
            {
              Elevador = e.Key,
              Quantidade = e.Count()
            })
            .ToArray();

            List<char> lstElevadorMenosFrequentado = new List<char>();

            int min = elevadoresAgrupados.Min(x => x.Quantidade);
            foreach (var item in elevadoresAgrupados.Where(x => x.Quantidade == min).ToList())
            {
                lstElevadorMenosFrequentado.AddRange(item.Elevador);
            }
            return lstElevadorMenosFrequentado;
        }

        public float percentualDeUsoElevadorA()
        {
            int totalElevadoresA = lstDadosPesquisa.Where(x => x.elevador == "A").Select(y => y.elevador).ToList().Count();
            return calculaPercentualUsoElevador(_totalElevadores, totalElevadoresA);
        }

        public float percentualDeUsoElevadorB()
        {
            int totalElevadoresB = lstDadosPesquisa.Where(x => x.elevador == "B").Select(y => y.elevador).ToList().Count();
            return calculaPercentualUsoElevador(_totalElevadores, totalElevadoresB);
        }

        public float percentualDeUsoElevadorC()
        {
            int totalElevadoresC = lstDadosPesquisa.Where(x => x.elevador == "C").Select(y => y.elevador).ToList().Count();
            return calculaPercentualUsoElevador(_totalElevadores, totalElevadoresC);
        }

        public float percentualDeUsoElevadorD()
        {
            int totalElevadoresD = lstDadosPesquisa.Where(x => x.elevador == "D").Select(y => y.elevador).ToList().Count();
            return calculaPercentualUsoElevador(_totalElevadores, totalElevadoresD);
        }

        public float percentualDeUsoElevadorE()
        {
            int totalElevadoresE = lstDadosPesquisa.Where(x => x.elevador == "E").Select(y => y.elevador).ToList().Count();
            return calculaPercentualUsoElevador(_totalElevadores, totalElevadoresE);
        }

        public List<char> periodoMaiorFluxoElevadorMaisFrequentado()
        {
            var lstElevadorMaisFrequentado = elevadorMaisFrequentado().Select(c => c.ToString()).ToList();

            List<PesquisaModel> elevadoresFiltrados = new List<PesquisaModel>();
            foreach (var elevador in lstElevadorMaisFrequentado)
            {
                elevadoresFiltrados.AddRange(lstDadosPesquisa.Where(x => x.elevador.Equals(elevador)).ToList());
            }

            string[] arrTurnosPesquisa = elevadoresFiltrados.Select(x => x.turno).ToArray();
            var turnosAgrupados = arrTurnosPesquisa
            .GroupBy(x => x)
            .Select(e => new
            {
                Turno = e.Key,
                Quantidade = e.Count()
            })
            .ToArray();

            List<char> lstPeriodoMaiorFluxo = new List<char>();
            lstPeriodoMaiorFluxo.AddRange(turnosAgrupados.FirstOrDefault().Turno);
            return lstPeriodoMaiorFluxo;
        }

        public List<char> periodoMaiorUtilizacaoConjuntoElevadores()
        {
            var turnosAgrupados = lstDadosPesquisa.Select(x => x.turno).ToArray().GroupBy(x => x)
            .Select(e => new
            {
                Turno = e.Key,
                Quantidade = e.Count()
            })
            .ToArray();

            List<char> lstTurnoMaiorUtilizacao = new List<char>();
            lstTurnoMaiorUtilizacao.AddRange(turnosAgrupados.OrderBy(i => i.Quantidade).LastOrDefault().Turno);
            return lstTurnoMaiorUtilizacao;
        }

        public List<char> periodoMenorFluxoElevadorMenosFrequentado()
        {
            List<PesquisaModel> elevadoresFiltrados = new List<PesquisaModel>();
            List<string> lstElevadorMenosFrequentado = elevadorMenosFrequentado().Select(c => c.ToString()).ToList();

            foreach (var elevador in lstElevadorMenosFrequentado)
                elevadoresFiltrados.AddRange(lstDadosPesquisa.Where(x => x.elevador.Equals(elevador)).ToList());

            string[] arrTurnosPesquisa = elevadoresFiltrados.Select(x => x.turno).ToArray();

            List<char> lstPeriodoMenorFluxo = new List<char>();

            foreach (var item in getPeriodoMenorFluxoElevadorMenosFrequentado(arrTurnosPesquisa).GroupBy(x => x)
            .Select(e => new
            {
                Turno = e.Key,
                Quantidade = e.Count()
            })
            .ToArray())
                lstPeriodoMenorFluxo.AddRange(item.Turno);

            return lstPeriodoMenorFluxo;
        }

        public List<string> getPeriodoMenorFluxoElevadorMenosFrequentado(string[] lstElevadores)
        {
            //M: Matutino; V: Vespertino; N: Noturno
            string[] turnos = new string[] { "M", "V", "N" };
            List<string> lstTurnosMenorFluxo = new List<string>();

            IEnumerable<string> turnosMenorFluxo = turnos.Except(lstElevadores);
            foreach (string turno in turnosMenorFluxo)
            {
                lstTurnosMenorFluxo.Add(turno);
            }
            return lstTurnosMenorFluxo;
        }

        private static List<PesquisaModel> getDadosPesquisa()
        {
            string json = File.ReadAllText("../../input.json");
            return JsonConvert.DeserializeObject<List<PesquisaModel>>(json);
        }

        private float calculaPercentualUsoElevador(int totalElevadores, int totalElevadorCategoria)
        {
            float percentual = totalElevadores / _100Porcento;
            float percentualUsoElevador = (percentual * totalElevadorCategoria) * _100Porcento;
            return percentualUsoElevador;
        }
    }
}
