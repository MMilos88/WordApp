using WordApp.BusinessModel;
using WordApp.CommonInterface;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace WordApp.BusinessLogic
{
    /// <summary>
    /// Base class for BusinessLogic classes, containing validation logic. 
    /// </summary>
    public class BaseBusinessLogic
    {
        #region Private fields
        private readonly ILogger _logger;
        private readonly ISharedLocalizer _sharedLocalizer;
        #endregion

        #region Public constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseBusinessLogic"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="sharedLocalizer">The shared localizer.</param>
        public BaseBusinessLogic(ILogger logger, ISharedLocalizer sharedLocalizer)
        {
            _logger = logger;
            _sharedLocalizer = sharedLocalizer;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Determines whether [is request valid] [the specified request].
        /// </summary>
        /// <typeparam name="TRaquest">The type of the raquest.</typeparam>
        /// <typeparam name="TValidator">The type of the validator.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="request">The request.</param>
        /// <param name="response">The response.</param>
        /// <returns>
        ///   <c>true</c> if [is request valid] [the specified request]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsRequestValid<TRequest, TValidator, TResponse>(TRequest request, ref Result<TResponse> response) where TValidator : class, new()
                                                                                                                       where TResponse : class
        {
            Result result = response as Result;
            return IsRequestValid<TRequest, TValidator>(request, ref result);
        }

        /// <summary>
        /// Determines whether [is request valid] [the specified request].
        /// </summary>
        /// <typeparam name="TRaquest">The type of the request.</typeparam>
        /// <typeparam name="TValidator">The type of the validator.</typeparam>
        /// <param name="request">The request.</param>
        /// <param name="response">The response.</param>
        /// <returns>
        ///   <c>true</c> if [is request valid] [the specified request]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsRequestValid<TRequest, TValidator>(TRequest request, ref Result response) where TValidator : class, new()
        {
            bool result = true;
            var validator = new TValidator() as AbstractValidator<TRequest>;
            try
            {
                var validationResult = validator.Validate(request);
                result = validationResult.IsValid;

                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        response.ErrorMessages.Add(new Error { ErrorType = ErrorType.ValidationError, ErrorMessage = _sharedLocalizer[error.ErrorMessage] });
                    }
                    response.ErrorMessage = _sharedLocalizer["RequestModelNotValid"];
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Validation exception");
                response.ErrorMessage = _sharedLocalizer["RequestModelNotValid"];
                result = false;
            }

            return result;
        }

        #endregion
    }
}
