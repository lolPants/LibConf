using System;

namespace LibConf.Exceptions
{
    /// <summary>
    /// Thrown when reading a different type than expected
    /// </summary>
    [Serializable]
    public sealed class IncorrectTypeException : Exception
    {
        /// <summary>
        /// Create a new IncorrectTypeException
        /// </summary>
        public IncorrectTypeException() : base() { }

        /// <summary>
        /// Create a new IncorrectTypeException with a custom message
        /// </summary>
        /// <param name="message">Error message</param>
        public IncorrectTypeException(string message) : base(message) { }
    }
}
