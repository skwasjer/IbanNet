using System;

namespace IbanNet.Validation.Results
{
	/// <summary>
	/// Describes the error that occurred for a validation rule.
	/// </summary>
	[Obsolete("Remove when we refactor out the enum.")]
	internal class BuiltInErrorResult : ErrorResult
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BuiltInErrorResult"/> class using specified <paramref name="result"/>.
		/// </summary>
		/// <param name="result"></param>
		public BuiltInErrorResult(IbanValidationResult result)
		{
			Result = result;
		}

		/// <summary>
		/// Gets the built-in validation result.
		/// </summary>
		internal IbanValidationResult Result { get; }
	}
}
