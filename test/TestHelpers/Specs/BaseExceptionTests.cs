using System;
using System.Reflection;
using FluentAssertions;
using FluentAssertions.Common;
using TestHelpers.FluentAssertions;
using Xunit;

namespace TestHelpers.Specs
{
	/// <summary>
	/// Base class to test exceptions. Asserts all 'required' ctors are available and pass parameters as expected.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class BaseExceptionTests<T>
		where T : Exception
	{
		[Fact]
		public void It_should_have_default_ctor()
		{
			// Act
			Func<Exception> act = Activator.CreateInstance<T>;

			// Assert
			DelegateExtensions.Should(act).NotThrow("a default constructor is expected");
		}

		[Fact]
		public void It_should_have_ctor_accepting_message()
		{
			const string message = "exception message";

			// Act
			ConstructorInfo ctor = typeof(T).GetConstructor(new[] { typeof(string) });

			// Assert
			ctor.Should().NotBeNull();
			ctor.Should().HaveAccessModifier(CSharpAccessModifier.Public);

			// Invoke
			Func<Exception> act = () => ctor.Invoke(new object[] { message }) as Exception;

			// Assert
			act.Should()
				.NotThrow("a default constructor is expected")
				.Which.Message.Should()
				.Be(message);
		}

		[Fact]
		public void It_should_have_ctor_accepting_message_and_inner_exception()
		{
			const string message = "exception message";
			var innerException = new Exception();

			// Act
			ConstructorInfo ctor = typeof(T).GetConstructor(new[] { typeof(string), typeof(Exception) });

			// Assert
			ctor.Should().NotBeNull();
			ctor.Should().HaveAccessModifier(CSharpAccessModifier.Public);

			// Invoke
			Func<Exception> act = () => ctor.Invoke(new object[] { message, innerException }) as Exception;

			// Assert
			Exception actual = act.Should()
				.NotThrow("a default constructor is expected")
				.Which;
			actual.Message.Should().Be(message);
			actual.InnerException.Should().BeSameAs(innerException);
		}
	}
}
