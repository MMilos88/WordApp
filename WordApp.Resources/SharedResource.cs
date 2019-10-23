using Microsoft.Extensions.Localization;
using WordApp.CommonInterface;

namespace WordApp.Resources
{
    /// <summary>
    /// Class for representing centralized point of access to resources
    /// </summary>
    /// <seealso cref="EntityCloner.CommonInterface.ISharedLocalizer" />
    public class SharedResource : ISharedLocalizer
    {
        #region Private fields
        /// <summary>
        /// The localizer
        /// </summary>
        private readonly IStringLocalizer _localizer;
        #endregion

        #region Public constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SharedResource"/> class.
        /// </summary>
        /// <param name="localizer">The localizer.</param>
        public SharedResource(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }
        #endregion

        /// <summary>
        /// Gets the <see cref="System.String"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="System.String"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public string this[string index]
        {
            get
            {
                return _localizer[index];
            }
        }
    }
}
