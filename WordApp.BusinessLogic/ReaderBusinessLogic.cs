using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using WordApp.BusinessInterface;
using WordApp.BusinessModel;
using WordApp.BusinessModel.Requests;
using WordApp.BusinessModel.Responses;
using WordApp.CommonInterface;
using WordApp.RepositoryInterface;
using WordApp.ServiceInterface;

namespace WordApp.BusinessLogic
{
    public class ReaderBusinessLogic : BaseBusinessLogic, IReaderBusinessLogic
    {
        #region Private fields
        private readonly ILogger _logger;
        private readonly ISharedLocalizer _sharedLocalizer;
        private readonly IMapper _mapper;
        private readonly ICalculationService _calculationService;
        private readonly ITextRepository _textRepository;
        #endregion

        #region Public constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderBusinessLogic"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="sharedLocalizer">The shared localizer.</param>
        public ReaderBusinessLogic(ILogger<ReaderBusinessLogic> logger, ISharedLocalizer sharedLocalizer, IMapper mapper, ICalculationService calculationService, ITextRepository textRepository)
            : base(logger, sharedLocalizer)
        {
            _logger = logger;
            _sharedLocalizer = sharedLocalizer;
            _mapper = mapper;
            _calculationService = calculationService;
            _textRepository = textRepository;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Calculates the word count.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<Result<CalculateWordCountResponse>> CalculateWordCount(CalculateWordCountRequest request)
        {
            _logger.LogTrace($"ReaderBusinessLogic: Entering CalculateWordCount method: {@request}", request);

            Result<CalculateWordCountResponse> response = new Result<CalculateWordCountResponse>();

            if (!IsRequestValid<CalculateWordCountRequest, CalculateWordCountRequestValidator, CalculateWordCountResponse>(request, ref response))
            {
                return response;
            }

            try
            {
                var dataRequest = _mapper.Map<CalculateWordCountRequest, DataModel.Requests.CalculateWordCountRequest>(request);
                var dataResponse = await _calculationService.CalculateWordCount(dataRequest);
                response.Data = _mapper.Map<DataModel.Responses.CalculateWordCountResponse, CalculateWordCountResponse>(dataResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ReaderBusinessLogic: Error in CalculateWordCount: {@request}", request);
                response.ErrorMessage = _sharedLocalizer["FrendlyErrorMessage"];
            }

            return response;
        }

        /// <summary>
        /// Calculates the word count from database.
        /// </summary>
        /// <returns></returns>
        public async Task<Result<CalculateWordCountResponse>> CalculateWordCountFromDB()
        {
            _logger.LogTrace($"ReaderBusinessLogic: Entering CalculateWordCountFromDB method");

            Result<CalculateWordCountResponse> response = new Result<CalculateWordCountResponse>();

            try
            {
                var textFromDB = await _textRepository.GetByIdAsync(1);
                var requestData = new CalculateWordCountRequest { InputText = textFromDB.TextData };

                return await CalculateWordCount(requestData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ReaderBusinessLogic: Error in CalculateWordCount");
                response.ErrorMessage = _sharedLocalizer["FrendlyErrorMessage"];
            }

            return response;
        }

        /// <summary>
        /// Calculates the word count from file.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<Result<CalculateWordCountResponse>> CalculateWordCountFromFile(Stream stream)
        {
            _logger.LogTrace($"ReaderBusinessLogic: Entering CalculateWordCountFromFile method: {@stream}", stream.ToString());

            Result<CalculateWordCountResponse> response = new Result<CalculateWordCountResponse>();

            try
            {
                string textDataFromFile = String.Empty;
                using (var streamreader = new StreamReader(stream))
                {
                    textDataFromFile = streamreader.ReadToEnd();
                }

                var requestData = new CalculateWordCountRequest { InputText = textDataFromFile };
            
                return await CalculateWordCount(requestData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ReaderBusinessLogic: Error in CalculateWordCount: {@stream}", stream.ToString());
                response.ErrorMessage = _sharedLocalizer["FrendlyErrorMessage"];
            }

            return response;
        }

        #endregion
    }
}
