using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Proiect_Zboruri_Cilibia_Malina.Models;

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
            var response = await _httpClient.PostAsJsonAsync("/predict", input);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<SatisfactionApiResponse>();

            return result?.predictedLabel;
        }

        private class SatisfactionApiResponse
        {
            public string predictedLabel { get; set; }
        }
    }
}