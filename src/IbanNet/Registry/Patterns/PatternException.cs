using System;

namespace IbanNet.Registry.Patterns
{
    /// <summary>
    /// The exception that is thrown when a pattern is invalid.
    /// </summary>
#if SERIALIZABLE
    [Serializable]
#endif
    public class PatternException : FormatException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PatternException" />.
        /// </summary>
        public PatternException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatternException" /> class using specified message.
        /// </summary>
        /// <param name="message">The error message.</param>
        public PatternException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatternException" /> class using specified message and inner exception.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public PatternException(string message, Exception? innerException)
            : base(message, innerException)
        {
        }

#if SERIALIZABLE
        /// <summary>
        /// Initializes a new instance of the <see cref="PatternException" /> with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected PatternException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
            // Note: Result property info is lost since it is not serializable.
        }
#endif
    }
}
