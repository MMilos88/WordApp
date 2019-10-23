using System;
using System.Threading.Tasks;
using WordApp.DataModel.Requests;
using WordApp.DataModel.Responses;
using WordApp.ServiceInterface;

namespace WordApp.Service
{
    public class CalculationService : ICalculationService
    {
        #region Private fields

        #endregion

        #region Public constructors
        public CalculationService()
        {
        }
        #endregion

        #region Public methods
        public async Task<CalculateWordCountResponse> CalculateWordCount(CalculateWordCountRequest request)
        {
            var result = new CalculateWordCountResponse();
            var text = request.InputText;

            int count = 0;
            int i = 0;
            int ind = 0;
            while (i < text.Length)
            {
                if (text[i] == ' ' || text[i] == '\n' || text[i] == '\t')
                    ind = 0;
                else if (ind == 0)
                {
                    ind = 1;
                    count++;
                }
                i++;
            }
            result.TotalWordCount = count;
            return result;
        }
        #endregion
    }
}
