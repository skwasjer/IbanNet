using System;
using System.Collections.Generic;
using System.ComponentModel;
using IbanNet.Registry;
using IbanNet.Validation.Methods;
using IbanNet.Validation.Rules;

namespace IbanNet.DependencyInjection
{
	/// <summary>
	/// Extensions for <see cref="IIbanNetOptionsBuilder"/>.
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class IbanNetOptionsBuilderExtensions
	{
		/// <summary>
		/// Registers a handler to configure the options when the builder executes.
		/// </summary>
		/// <param name="builder">The builder instance.</param>
		/// <param name="configure">The handler that is called when configuring the options.</param>
		public static IIbanNetOptionsBuilder Configure(this IIbanNetOptionsBuilder builder, Action<IbanValidatorOptions> configure)
		{
			if (builder is null)
			{
				throw new ArgumentNullException(nameof(builder));
			}

			if (configure is null)
			{
				throw new ArgumentNullException(nameof(configure));
			}

			return builder.Configure((_, options) => configure(options));
		}

		/// <summary>
		/// Configures the <see cref="IbanValidator"/> to use the specified registry.
		/// </summary>
		/// <param name="builder">The builder instance.</param>
		/// <param name="registry">The registry of IBAN countries.</param>
		/// <returns>The <see cref="IIbanNetOptionsBuilder"/> so that additional calls can be chained.</returns>
		public static IIbanNetOptionsBuilder UseRegistry(this IIbanNetOptionsBuilder builder, IEnumerable<IbanCountry> registry)
		{
			if (builder is null)
			{
				throw new ArgumentNullException(nameof(builder));
			}

			if (registry is null)
			{
				throw new ArgumentNullException(nameof(registry));
			}

			var ir = new IbanRegistry(registry);
			builder.Configure(options => options.Registry = ir);
			return builder;
		}

		/// <summary>
		/// Configures the <see cref="IbanValidator"/> to use strict validation.
		/// </summary>
		/// <param name="builder">The builder instance.</param>
		/// <returns>The <see cref="IIbanNetOptionsBuilder"/> so that additional calls can be chained.</returns>
		public static IIbanNetOptionsBuilder UseStrictValidation(this IIbanNetOptionsBuilder builder)
		{
			return builder.UseValidationMethod(new StrictValidation());
		}

		/// <summary>
		/// Configures the <see cref="IbanValidator"/> to use loose validation.
		/// </summary>
		/// <param name="builder">The builder instance.</param>
		/// <returns>The <see cref="IIbanNetOptionsBuilder"/> so that additional calls can be chained.</returns>
		public static IIbanNetOptionsBuilder UseLooseValidation(this IIbanNetOptionsBuilder builder)
		{
			return builder.UseValidationMethod(new LooseValidation());
		}

		private static IIbanNetOptionsBuilder UseValidationMethod(this IIbanNetOptionsBuilder builder, ValidationMethod validationMethod)
		{
			if (builder is null)
			{
				throw new ArgumentNullException(nameof(builder));
			}

			return builder.Configure(options => options.ValidationMethod = validationMethod);
		}


		/// <summary>
		/// Registers a custom validation rule that is executed after built-in validation has passed.
		/// </summary>
		/// <param name="builder">The builder instance.</param>
		/// <param name="implementationType">The type of the validation rule.</param>
		/// <returns>The <see cref="IIbanNetOptionsBuilder"/> so that additional calls can be chained.</returns>
		public static IIbanNetOptionsBuilder WithRule(this IIbanNetOptionsBuilder builder, Type implementationType)
		{
			if (builder is null)
			{
				throw new ArgumentNullException(nameof(builder));
			}

			if (implementationType is null)
			{
				throw new ArgumentNullException(nameof(implementationType));
			}

			return builder.Configure(
				(adapter, options) => options.Rules.Add((IIbanValidationRule)adapter.GetRequiredService(implementationType))
			);
		}

		/// <summary>
		/// Registers a custom validation rule that is executed after built-in validation has passed.
		/// </summary>
		/// <typeparam name="T">The type of the validation rule.</typeparam>
		/// <param name="builder">The builder instance.</param>
		/// <returns>The <see cref="IIbanNetOptionsBuilder"/> so that additional calls can be chained.</returns>
		public static IIbanNetOptionsBuilder WithRule<T>(this IIbanNetOptionsBuilder builder)
			where T : class, IIbanValidationRule
		{
			if (builder is null)
			{
				throw new ArgumentNullException(nameof(builder));
			}

			return builder.WithRule(typeof(T));
		}

		/// <summary>
		/// Registers a custom validation rule that is executed after built-in validation has passed.
		/// </summary>
		/// <typeparam name="T">The type of the validation rule.</typeparam>
		/// <param name="builder">The builder instance.</param>
		/// <param name="implementationFactory">The factory returning a new instance of the rule.</param>
		/// <returns>The <see cref="IIbanNetOptionsBuilder"/> so that additional calls can be chained.</returns>
		public static IIbanNetOptionsBuilder WithRule<T>(this IIbanNetOptionsBuilder builder, Func<T> implementationFactory)
			where T : class, IIbanValidationRule
		{
			if (builder is null)
			{
				throw new ArgumentNullException(nameof(builder));
			}

			if (implementationFactory is null)
			{
				throw new ArgumentNullException(nameof(implementationFactory));
			}

			return builder.Configure(
				options => options.Rules.Add(implementationFactory())
			);
		}
	}
}
