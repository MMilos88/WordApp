using System.Collections.Generic;
using System.Linq;

namespace WordApp.BusinessModel
{
    /// <summary>
    /// Base class for modeling result of some operation.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        public Result()
        {
            ErrorMessages = new List<Error>();
        }
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// Gets or sets the error messages.
        /// </summary>
        /// <value>
        /// The error messages.
        /// </value>
        public List<Error> ErrorMessages { get; set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="Result"/> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        public bool Success
        {
            get
            {
                return string.IsNullOrEmpty(ErrorMessage) && ErrorMessages.Where(it => it.ErrorType == ErrorType.Error).Count() == 0;
            }
        }
    }

    /// <summary>
    /// Class for modeling result of some operation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T> : Result where T : class
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public T Data { get; set; }
    }
}
