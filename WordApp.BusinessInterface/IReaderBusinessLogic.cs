using System.IO;
using System.Threading.Tasks;
using WordApp.BusinessModel;
using WordApp.BusinessModel.Requests;
using WordApp.BusinessModel.Responses;

namespace WordApp.BusinessInterface
{
    public interface IReaderBusinessLogic
    {
        /// <summary>
        /// Calculates the word count.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<Result<CalculateWordCountResponse>> CalculateWordCount(CalculateWordCountRequest request);

        /// <summary>
        /// Calculates the word count from database.
        /// </summary>
        /// <returns></returns>
        Task<Result<CalculateWordCountResponse>> CalculateWordCountFromDB();

        /// <summary>
        /// Calculates the word count from file.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<Result<CalculateWordCountResponse>> CalculateWordCountFromFile(Stream stream);
    }
}
