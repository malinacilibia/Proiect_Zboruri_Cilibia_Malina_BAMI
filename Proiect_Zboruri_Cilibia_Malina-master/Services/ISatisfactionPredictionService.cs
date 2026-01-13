using System.Threading.Tasks;
using Proiect_Zboruri_Cilibia_Malina.Models;

// AICI ERA GRESEALA: Trebuie sa fie namespace-ul proiectului MVC, nu WebApi
namespace Proiect_Zboruri_Cilibia_Malina.Services
{
    public interface ISatisfactionPredictionService
    {
        Task<string> PredictSatisfactionAsync(SatisfactionInput input);
    }
}