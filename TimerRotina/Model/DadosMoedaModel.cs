using CsvHelper.Configuration.Attributes;
using System;

namespace RotinaTimer.Model
{
    public class DadosMoedaModel
    {
        [Name("ID_MOEDA")]
        public string MoedaId { get; set; }

        [Name("DATA_REF")]
        public DateTime DataRef { get; set; }
    }
}
