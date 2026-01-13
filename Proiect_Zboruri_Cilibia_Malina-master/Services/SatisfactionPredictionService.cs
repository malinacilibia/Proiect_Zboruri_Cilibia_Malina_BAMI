using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Proiect_Zboruri_Cilibia_Malina.Models;
// Nu mai avem nevoie de "using SatisfactionModelWebApi.Services;" pentru ca acum sunt in acelasi namespace

namespace Proiect_Zboruri_Cilibia_Malina.Services
{
    public class SatisfactionPredictionService : ISatisfactionPredictionService
    {
        private readonly HttpClient _httpClient;

        public SatisfactionPredictionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> PredictSatisfactionAsync(SatisfactionInput input)
        {
            // POST către endpoint-ul /predict al API-ului
            var response = await _httpClient.PostAsJsonAsync("/predict", input);
            response.EnsureSuccessStatusCode();

            // Citim răspunsul JSON. 
            // API-ul tău returnează un câmp numit "predictedLabel"
            var result = await response.Content.ReadFromJsonAsync<SatisfactionApiResponse>();

            return result?.predictedLabel;
        }

        // Clasâ internă pentru maparea răspunsului JSON
        private class SatisfactionApiResponse
        {
            // Aici trebuie să fie exact numele din JSON-ul din Swagger
            public string predictedLabel { get; set; }
        }
    }
}