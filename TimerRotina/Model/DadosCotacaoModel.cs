using CsvHelper.Configuration.Attributes;
using System;
using TesteBack.Enum;

namespace RotinaTimer.Model
{
    public class DadosCotacaoModel
    {
        public double ValorCotacao { get; set; }
        public int CodigoCotacao { get; set; }
        public DateTime DataCotacao { get; set; }
        public MoedaEnum Moeda { get; set; }
    }
}
