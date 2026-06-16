using Microsoft.ML.Data;

namespace ApiInteligenteTareas.API.ML;

public class SentimientoData
{
    [LoadColumn(0)]
    public string Texto { get; set; } = string.Empty;

    [LoadColumn(1)]
    public bool Sentimiento { get; set; }
}