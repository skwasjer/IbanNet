using System.Reflection;

namespace TestHelpers;

public class DelegateTestCase : List<object>
{
    public string Name { get; }
    public Delegate Delegate { get; }

    private DelegateTestCase(Delegate @delegate)
    {
        Name = @delegate.GetMethodInfo().Name;
        Delegate = @delegate;
        Add(Name);
        Add(@delegate);
    }

    public IEnumerable<object[]> GetNullArgumentTestCases(bool withoutName = false)
    {
        const int paramOffset = 2;
        MethodInfo methodInfo = Delegate.GetMethodInfo();
        ParameterInfo[] parameters = methodInfo.GetParameters();
        for (int i = paramOffset; i < Count; i++)
        {
            ParameterInfo parameter = parameters[i - paramOffset];

            // Ignore value types.
            if (parameter.ParameterType.GetTypeInfo().IsValueType)
            {
                continue;
            }

            if (this[i] is null)
            {
                // If value is null, then null is allowed and we ignore case.
                continue;
            }

            // Set value for current case to null.
            var testCase = new List<object>(this)
            {
                [i] = null
            };

            // Insert current parameter name.
            testCase.Insert(paramOffset, parameter.Name!);

            yield return testCase.Skip(withoutName ? 1 : 0).ToArray();
        }
    }

    public object[] WithoutName()
    {
        return this.Skip(1).ToArray();
    }

    public static DelegateTestCase Create<TOut>(Func<TOut> @delegate)
    {
        return new(@delegate);
    }

    public static DelegateTestCase Create<T1, TOut>(Func<T1, TOut> @delegate, T1 arg1)
    {
        return new(@delegate)
        {
            arg1,
        };
    }

    public static DelegateTestCase Create<T1, T2, TOut>(Func<T1, T2, TOut> @delegate, T1 arg1, T2 arg2)
    {
        return new(@delegate)
        {
            arg1, arg2
        };
    }

    public static DelegateTestCase Create<T1, T2, T3, TOut>(Func<T1, T2, T3, TOut> @delegate, T1 arg1, T2 arg2, T3 arg3)
    {
        return new(@delegate)
        {
            arg1, arg2, arg3
        };
    }

    public static DelegateTestCase Create<T1, T2, T3, T4, TOut>(Func<T1, T2, T3, T4, TOut> @delegate, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        return new(@delegate)
        {
            arg1, arg2, arg3, arg4
        };
    }

    public static DelegateTestCase Create<T1, T2, T3, T4, T5, TOut>(Func<T1, T2, T3, T4, T5, TOut> @delegate, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        return new(@delegate)
        {
            arg1, arg2, arg3, arg4, arg5
        };
    }
}