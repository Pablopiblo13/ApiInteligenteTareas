using Microsoft.ML.Data;

namespace ApiInteligenteTareas.API.ML;

public class SentimientoPrediction
{
    [ColumnName("PredictedLabel")]
    public bool Prediccion { get; set; }
}