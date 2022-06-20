using CsvHelper.Configuration.Attributes;
using System;

namespace RotinaTimer.Model.View
{
    public class DadosCotacaoModelView
    {
        //[Name("vlr_cotacao")]
        public string ValorCotacao { get; set; }

        //[Name("cod_cotacao")]
        public string CodigoCotacao { get; set; }

        //[Name("dat_cotacao")]
        public string DataCotacao { get; set; }
    }
}
