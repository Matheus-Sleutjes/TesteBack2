using RestSharp;
using RotinaTimer.Model;
using RotinaTimer.Model.View;
using RotinaTimer.Service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TimerRotina.Model;

namespace RotinaTimer.Service
{
    public class ConverterService : IConverterService
    {
        private readonly string pathCotacao = "C:\\Users\\Public\\DadosCotacao.csv";
        private readonly string pathMoeda = "C:\\Users\\Public\\DadosMoeda.csv";
        public ConverterService()
        {

        }

        public async void Start()
        {
            InitializeTimer();

            //var listaCotacao = GetListCotacao(this.pathCotacao);
            //var listaMoeda = GetListMoeda(this.pathMoeda);

             //var retorno = await GetApi();

        }

        private static List<DadosCotacaoModel> GetListCotacao(string path)
        {
            var count = 0;
            var reader = new StreamReader(File.OpenRead(@path));
            var list = new List<DadosCotacaoModel>();
            
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                if (count != 0)
                {
                    var obj = new DadosCotacaoModel();
                    obj.ValorCotacao = Convert.ToDouble(values[0]);
                    obj.CodigoCotacao = Convert.ToInt32(values[1]);
                    obj.DataCotacao = Convert.ToDateTime(values[2]);

                    list.Add(obj);
                }
                count++;
            }
            return list;
        }

        private static List<DadosMoedaModelView> GetListMoeda(string path)
        {
            var reader = new StreamReader(File.OpenRead(@path));
            var list = new List<DadosMoedaModelView>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                var obj = new DadosMoedaModelView();
                obj.MoedaId = values[0];
                obj.DataRef = values[1];

                list.Add(obj);
            }
            list.RemoveAt(0);
            return list;
        }

        private static async Task<PesquisaModel> GetApi()
        {
            var client = new RestClient("https://localhost:44389/");
            
            var resource = "Moeda/GetItemFila";
            var cancelation = new CancellationToken(default);
            var response = await client.GetJsonAsync<PesquisaModel>(resource, cancelation);

            return response;
        }

        private static void Tick(Object stateInfo)
        {
            Console.WriteLine("Tick: {0}", DateTime.Now.ToString("h:mm:ss"));
            //getapi
            //fazer uma lista filtrando apartir do retorno api
            //pegar cotação 
        }

        private static void InitializeTimer()
        {
            var sair = false;
            TimerCallback callback = new TimerCallback(Tick);
            Console.WriteLine("Timer Iniciado");
            Console.WriteLine("Digite STOP para sair");
            Timer stateTimer = new Timer(callback, null, 0, 10000);
            Console.WriteLine("rodando");
            var console = Console.ReadLine().ToUpper();

            if (console == "STOP")
            {
                stateTimer.Dispose();
                Console.WriteLine("parou");
            }
        }
    }
}
