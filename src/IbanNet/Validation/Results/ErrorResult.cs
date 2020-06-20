using System;

namespace IbanNet.Validation.Results
{
	/// <summary>
	/// Describes the error that occurred for a validation rule. Custom validation errors should derive from this class.
	/// </summary>
	public class ErrorResult : ValidationRuleResult
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ErrorResult"/> class using specified <paramref name="errorMessage"/>.
		/// </summary>
		/// <param name="errorMessage">The error message.</param>
		public ErrorResult(string errorMessage)
		{
			ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
		}

		/// <summary>
		/// Gets the error message.
		/// </summary>
		public string ErrorMessage { get; }
	}
}
