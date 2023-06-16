namespace Weather.Functions.ExceptionExtensions
{
    /// <summary>
    /// Tries to add some semantic meaning to exceptions.
    /// </summary>
    public abstract class SemanticExceptionBase : Exception
    {
        protected SemanticExceptionBase(string message, string code) : base(message)
        {
            Code = code;
        }

        protected SemanticExceptionBase(string message, string code, Exception cause) : base(message, cause)
        {
            Code = code;
        }

        /// <summary>
        /// An actual trust-worthy machine readable error code
        /// </summary>
        public virtual string Code { get; set; } = "Unknown";
    }
}
