using System.Reflection;
using TestHelpers.FluentAssertions;

namespace TestHelpers
{
    public static class NullArgumentTest
    {
        [AssertionMethod]
        public static void Execute(params object[] testArgs)
        {
            if (testArgs is null)
            {
                throw new ArgumentNullException(nameof(testArgs));
            }

            string testCase = (string)testArgs[0];
            var func = (Delegate)testArgs[1];
            string expectedParamName = (string)testArgs[2];
            ParameterInfo param = func.GetMethodInfo().GetParameters().Single(p => p.Name == expectedParamName);
            object[] args = testArgs.Skip(3).ToArray();
            if (args.Length == 0)
            {
                // args can be empty for single param, so in that case provide array with 1 item.
                args = new object[1];
            }

            ArgumentException ex = param.ParameterType == typeof(string)
                ? func.Should().Throw<ArgumentException>(args).Which
                : func.Should().Throw<ArgumentNullException>(args).Which;
            string paramName = ex.ParamName;
            paramName.Should().Be(expectedParamName, "no null was provided for {1}", expectedParamName, testCase);
        }
    }
}
