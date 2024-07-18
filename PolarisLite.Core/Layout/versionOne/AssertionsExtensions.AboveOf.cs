using NUnit.Framework;

namespace PolarisLite.Core.Layout;

public static partial class AssertionsExtensions
{
    public static void AssertAboveOf(this ILayoutComponent element, ILayoutComponent secondElement)
    {
        double actualDistance = CalculateAboveOfDistance(element, secondElement);
        Assert.IsTrue(actualDistance >= -1, $"{element} should be above of {secondElement} but was {actualDistance} px.");
    }

    public static void AssertAboveOf(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        double actualDistance = CalculateAboveOfDistance(element, secondElement);
        Assert.That(actualDistance, Is.EqualTo(expected), $"{element} should be {expected} px above of {secondElement} but was {actualDistance} px.");
    }

    public static void AssertAboveOfBetween(this ILayoutComponent element, ILayoutComponent secondElement, double from, double to)
    {
        var actualDistance = CalculateAboveOfDistance(element, secondElement);
        Assert.IsTrue(actualDistance >= from && actualDistance <= to, $"{element} should be between {from}-{to} px above of {secondElement}, but {actualDistance}.");
    }

    public static void AssertAboveOfGreaterThan(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateAboveOfDistance(element, secondElement);
        Assert.IsTrue(actualDistance > expected, $"{element} should be > {expected} px above of {secondElement} but was {actualDistance} px.");
    }

    public static void AssertAboveOfGreaterThanOrEqual(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateAboveOfDistance(element, secondElement);
        Assert.IsTrue(actualDistance >= expected, $"{element} should be >= {expected} px above of {secondElement} but was {actualDistance} px.");
    }

    public static void AssertAboveOfLessThan(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateAboveOfDistance(element, secondElement);
        Assert.IsTrue(actualDistance < expected, $"{element} should be < {expected} px above of {secondElement} but was {actualDistance} px.");
    }

    public static void AssertAboveOfLessThanOrEqual(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateAboveOfDistance(element, secondElement);
        Assert.IsTrue(actualDistance <= expected, $"{element} should be <= {expected} px above of {secondElement} but was {actualDistance} px.");
    }

    public static void AssertAboveOfApproximate(this ILayoutComponent element, ILayoutComponent secondElement, double expected, double percent)
    {
        var actualDistance = CalculateAboveOfDistance(element, secondElement);
        var actualPercentDifference = CalculatePercentDifference(expected, actualDistance);
        Assert.IsTrue(actualPercentDifference <= percent, $"{element} should be <= {percent}% of {expected} px above of {secondElement} but was {actualDistance} px.");
    }
}
