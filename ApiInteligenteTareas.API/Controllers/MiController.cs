using ApiInteligenteTareas.API.ML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;

namespace ApiInteligenteTareas.API.Controllers;

[ApiController]
[Route("api/ml")]
public class MlController : ControllerBase
{
    [HttpPost("sentimiento")]
    public IActionResult AnalizarSentimiento([FromBody] SentimientoRequest request)
    {
        var mlContext = new MLContext();

        var data = mlContext.Data.LoadFromTextFile<SentimientoData>(
            "ML/sentimientos.csv",
            hasHeader: true,
            separatorChar: ',');

        var pipeline = mlContext.Transforms.Text.FeaturizeText(
                "Features",
                nameof(SentimientoData.Texto))
            .Append(
                mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(
                    labelColumnName: nameof(SentimientoData.Sentimiento),
                    featureColumnName: "Features"));

        var model = pipeline.Fit(data);

        var predictor = mlContext.Model.CreatePredictionEngine
            <SentimientoData, SentimientoPrediction>(model);

        var prediction = predictor.Predict(new SentimientoData
        {
            Texto = request.Comentario
        });

        return Ok(new
        {
            Comentario = request.Comentario,
            Sentimiento = prediction.Prediccion ? "Positivo" : "Negativo"
        });
    }
}

public class SentimientoRequest
{
    public string Comentario { get; set; } = string.Empty;
}