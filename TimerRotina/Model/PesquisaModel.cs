using System;

namespace TimerRotina.Model
{
    public class PesquisaModel
    {
        public string Moeda { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public bool Excluido { get; set; }
        public DateTime DataPesquisa { get; set; }
    }
}
