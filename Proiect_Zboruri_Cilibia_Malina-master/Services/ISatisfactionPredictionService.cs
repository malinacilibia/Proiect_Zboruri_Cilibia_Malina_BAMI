using System.Threading.Tasks;
using Proiect_Zboruri_Cilibia_Malina.Models;

namespace Proiect_Zboruri_Cilibia_Malina.Services
{
    public interface ISatisfactionPredictionService
    {
        Task<string> PredictSatisfactionAsync(SatisfactionInput input);
    }
}