using NUnit.Framework;

namespace PolarisLite.Core.Layout;

public static partial class AssertionsExtensions
{
    public static void AssertBelowOf(this ILayoutComponent element, ILayoutComponent secondElement)
    {
        double actualDistance = CalculateBelowOfDistance(element, secondElement);
        Assert.IsTrue(actualDistance <= 0, $"{element} should be below of {secondElement} but was {actualDistance} px.");
    }

    public static void AssertBelowOf(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        double actualDistance = CalculateBelowOfDistance(element, secondElement);
        Assert.That(actualDistance, Is.EqualTo(expected), $"{element} should be {expected} px below of {secondElement} but was {actualDistance} px.");
    }

    public static void AssertBelowOfBetween(this ILayoutComponent element, ILayoutComponent secondElement, double from, double to)
    {
        var actualDistance = CalculateBelowOfDistance(element, secondElement);
        Assert.IsTrue(actualDistance >= from && actualDistance <= to, $"{element} should be between {from}-{to} px below of {secondElement}, but {actualDistance}.");
    }

    public static void AssertBelowOfGreaterThan(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateBelowOfDistance(element, secondElement);
        Assert.IsTrue(actualDistance > expected, $"{element} should be > {expected} px below of {secondElement} but was {actualDistance} px.");
    }

    public static void AssertBelowOfGreaterThanOrEqual(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateBelowOfDistance(element, secondElement);
        Assert.IsTrue(actualDistance >= expected, $"{element} should be >= {expected} px below of {secondElement} but was {actualDistance} px.");
    }

    public static void AssertBelowOfLessThan(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateBelowOfDistance(element, secondElement);
        Assert.IsTrue(actualDistance < expected, $"{element} should be < {expected} px below of {secondElement} but was {actualDistance} px.");
    }

    public static void AssertBelowOfLessThanOrEqual(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateBelowOfDistance(element, secondElement);
        Assert.IsTrue(actualDistance <= expected, $"{element} should be <= {expected} px below of {secondElement} but was {actualDistance} px.");
    }

    public static void AssertBelowOfApproximate(this ILayoutComponent element, ILayoutComponent secondElement, double expected, double percent)
    {
        var actualDistance = CalculateBelowOfDistance(element, secondElement);
        var actualPercentDifference = CalculatePercentDifference(expected, actualDistance);
        Assert.IsTrue(actualPercentDifference <= percent, $"{element} should be <= {percent}% of {expected} px below of {secondElement} but was {actualDistance} px.");
    }
}
