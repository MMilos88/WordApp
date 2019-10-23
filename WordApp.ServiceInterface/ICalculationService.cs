using System.Threading.Tasks;
using WordApp.DataModel.Requests;
using WordApp.DataModel.Responses;

namespace WordApp.ServiceInterface
{
    public interface ICalculationService
    {
        Task<CalculateWordCountResponse> CalculateWordCount(CalculateWordCountRequest request);
    }
}
