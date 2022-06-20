using CsvHelper.Configuration.Attributes;
using System;

namespace RotinaTimer.Model
{
    public class DadosCotacaoModel
    {
        //[Name("vlr_cotacao")]
        public double ValorCotacao { get; set; }

        //[Name("cod_cotacao")]
        public int CodigoCotacao { get; set; }

        //[Name("dat_cotacao")]
        public DateTime DataCotacao { get; set; }
    }
}
