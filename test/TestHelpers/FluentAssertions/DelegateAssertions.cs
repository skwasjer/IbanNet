using System.Reflection;
using FluentAssertions.Execution;
using FluentAssertions.Specialized;

namespace TestHelpers.FluentAssertions
{
    public class DelegateAssertions
        : DelegateAssertions<Delegate, DelegateAssertions>
    {
        public DelegateAssertions(Delegate @delegate, IExtractExceptions extractor) : base(@delegate, extractor)
        {
        }

        protected override string Identifier => GetType().Name;

        protected override void InvokeSubject()
        {
            Subject.DynamicInvoke();
        }

        /// <summary>
        /// Asserts that the current <see cref="T:System.Delegate" /> throws an exception of type <typeparamref name="TException" />.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="M:System.String.Format(System.String,System.Object[])" /> explaining why the assertion
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="!:because" />.
        /// </param>
        public ExceptionAssertions<TException> Throw<TException>
        (
            object[] args,
            string because = "",
            params object[] becauseArgs
        )
            where TException : Exception
        {
            Execute.Assertion
                .ForCondition(Subject is not null)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to throw {0}{reason}, but found <null>.", (object)typeof(TException));

            return ThrowInternal<TException>(
                InvokeSubjectWithInterception(args),
                because,
                becauseArgs
            );
        }

        /// <summary>
        /// Asserts that the current <see cref="Delegate" /> does not throw any exception.
        /// </summary>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="M:System.String.Format(System.String,System.Object[])" /> explaining why the assertion
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="!:because" />.
        /// </param>
        public new AndWhichConstraint<DelegateAssertions, object> NotThrow
        (
            string because = "",
            params object[] becauseArgs
        )
        {
            Execute.Assertion
                .ForCondition(Subject is not null)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} not to throw{reason}, but found <null>.");
            try
            {
                // ReSharper disable once PossibleNullReferenceException
                return new AndWhichConstraint<DelegateAssertions, object>(this, Subject.DynamicInvoke());
            }
            catch (TargetInvocationException ex) when (ex.InnerException is not null)
            {
                NotThrowV6(ex.InnerException, because, becauseArgs);
                return new AndWhichConstraint<DelegateAssertions, object>(this, default);
            }
            catch (Exception ex)
            {
                NotThrowV6(ex, because, becauseArgs);
                return new AndWhichConstraint<DelegateAssertions, object>(this, default);
            }
        }

        /// <summary>
        /// Asserts that the current <see cref="Delegate" /> does not throw any exception.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="M:System.String.Format(System.String,System.Object[])" /> explaining why the assertion
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="!:because" />.
        /// </param>
        public AndWhichConstraint<DelegateAssertions, object> NotThrow
        (
            object[] args,
            string because = "",
            params object[] becauseArgs
        )
        {
            Execute.Assertion
                .ForCondition(Subject is not null)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} not to throw{reason}, but found <null>.");
            try
            {
                // ReSharper disable once PossibleNullReferenceException
                return new AndWhichConstraint<DelegateAssertions, object>(this, Subject.DynamicInvoke(args));
            }
            catch (TargetInvocationException ex) when (ex.InnerException is not null)
            {
                NotThrowV6(ex.InnerException, because, becauseArgs);
                return new AndWhichConstraint<DelegateAssertions, object>(this, default);
            }
            catch (Exception ex)
            {
                NotThrowV6(ex, because, becauseArgs);
                return new AndWhichConstraint<DelegateAssertions, object>(this, default);
            }
        }

        private Exception InvokeSubjectWithInterception(object[] args)
        {
            Exception exception = null;
            try
            {
                Subject.DynamicInvoke(args);
            }
            catch (TargetInvocationException ex) when (ex.InnerException is not null)
            {
                exception = ex.InnerException;
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            return exception;
        }

        private void NotThrowV6(Exception ex, string because, object[] becauseArgs)
        {
            NotThrowInternal(ex, because, becauseArgs);
        }
    }
}
