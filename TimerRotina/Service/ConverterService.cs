using RestSharp;
using RotinaTimer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TesteBack.Enum;
using TimerRotina.Model;

namespace RotinaTimer.Service
{
    public class ConverterService 
    {
        public ConverterService() { }

        public void Start()
        {
            InitializeTimer();
        }

        private static void InitializeTimer()
        {
            var callback = new TimerCallback(Tick);
            Console.WriteLine("O servico foi iniciado!");
            Console.WriteLine("Digite STOP para sair");

            Timer stateTimer = new Timer(callback, null, 0, 120000);

            var console = Console.ReadLine().ToUpper();

            if (console == "STOP")
            {
                stateTimer.Dispose();
                Console.WriteLine("O servico foi parado!");
            }
        }
        private async static void Tick(Object stateInfo)
        {
            Console.WriteLine("Tick: {0}", DateTime.Now.ToString("HH:mm:ss"));
            var response = await GetApi();

            if (response.Moeda != null)
            {
                var data = DateTime.Now.ToLocalTime();
                Console.WriteLine($"Gerando Resultado_{data.ToString("yyyyMMdd")}_{data.ToString("HHmmss")}");

                var lista = GetListCsv(response);

                CreateCsv(lista, data);
            }
            else
            {
                Console.WriteLine("NADA FOI ENCONTRADO");
            }
        }

        private static async Task<PesquisaModel> GetApi()
        {
            var client = new RestClient("https://localhost:44389/");

            var resource = "Moeda/GetItemFila";
            var cancelation = new CancellationToken(default);

            return await client.GetJsonAsync<PesquisaModel>(resource, cancelation);
        }

        private static List<DadosCotacaoModel> GetListCsv(PesquisaModel pesquisa)
        {
            var count = 0;
            var reader = new StreamReader(File.OpenRead(@"C:\\Users\\Public\\DadosCotacao.csv"));
            var list = new List<DadosCotacaoModel>();
            
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                if (count != 0)
                {
                    var obj = new DadosCotacaoModel();
                    obj.ValorCotacao = Convert.ToDouble(values[0]);
                    obj.DataCotacao = Convert.ToDateTime(values[2]);
                    obj.Moeda = (MoedaEnum)Convert.ToInt32(values[1]);
                    
                    if (obj.Moeda.ToString() == pesquisa.Moeda && obj.DataCotacao > pesquisa.DataInicio && pesquisa.DataFim > obj.DataCotacao)
                        list.Add(obj);
                }
                count++;
            }
            return list;
        }

        private static void CreateCsv(List<DadosCotacaoModel> lista, DateTime data)
        {
            var path = $"C:\\Users\\Public\\Resultado_{data.ToString("yyyyMMdd")}_{data.ToString("HHmmss")}.csv";

            var sw = new StreamWriter(@path, true, Encoding.UTF8);

            sw.WriteLine("ID_MOEDA;DATA_REF;VL_COTACAO");
            foreach (var item in lista)
            {
                sw.WriteLine($"{item.Moeda};{item.DataCotacao.ToString("yyyy-MM-dd")};{item.ValorCotacao}");
            }
            sw.Close();
        }
    }
}
