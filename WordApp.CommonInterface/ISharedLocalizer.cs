namespace WordApp.CommonInterface
{
    /// <summary>
    /// Interface for representing centralized point of access to resources
    /// </summary>
    public interface ISharedLocalizer
    {
        /// <summary>
        /// Gets the <see cref="System.String"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="System.String"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        string this[string index]
        {
            get;
        }
    }
}
