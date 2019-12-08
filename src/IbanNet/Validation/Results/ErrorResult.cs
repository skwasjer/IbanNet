using System;

namespace IbanNet.Validation.Results
{
	/// <summary>
	/// Describes the error that occurred for a validation rule.
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

		/// <summary>Determines whether the specified object is equal to the current object.</summary>
		/// <param name="other">The object to compare with the current object.</param>
		/// <returns>
		/// <see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.</returns>
		// ReSharper disable once MemberCanBePrivate.Global
		protected bool Equals(ErrorResult other)
		{
			return ErrorMessage == other.ErrorMessage;
		}

		/// <summary>Determines whether the specified object is equal to the current object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		/// <see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.</returns>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}

			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			return obj.GetType() == GetType() && Equals((ErrorResult)obj);
		}

		/// <summary>Serves as the default hash function.</summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			return (ErrorMessage != null ? ErrorMessage.GetHashCode() : 0);
		}
	}
}
