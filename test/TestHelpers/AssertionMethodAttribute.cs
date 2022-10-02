namespace TestHelpers;

/// <summary>
/// Marks a method as an assertion method as a hint for SonarQube: S2699
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class AssertionMethodAttribute : Attribute { }
