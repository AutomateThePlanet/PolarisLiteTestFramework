using NUnit.Framework;

namespace PolarisLite.Core.Layout;

public static partial class AssertionsExtensions
{
    public static void AssertLeftOf(this ILayoutComponent element, ILayoutComponent secondElement)
    {
        var actualDistance = CalculateLeftOfDistance(element, secondElement);
        Assert.That(actualDistance, Is.GreaterThan(0), $"{element} should be left from {secondElement} but was {actualDistance} px.");
    }

    public static void AssertLeftOf(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateLeftOfDistance(element, secondElement);
        Assert.That(actualDistance, Is.EqualTo(expected), $"{element} should be {expected} px left from {secondElement} but was {actualDistance} px.");
    }

    public static void AssertLeftOfBetween(this ILayoutComponent element, ILayoutComponent secondElement, double from, double to)
    {
        var actualDistance = CalculateLeftOfDistance(element, secondElement);
        Assert.That(actualDistance, Is.InRange(from, to), $"{element} should be between {from}-{to} px left from {secondElement}, but was {actualDistance}.");
    }

    public static void AssertLeftOfGreaterThan(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateLeftOfDistance(element, secondElement);
        Assert.That(actualDistance, Is.GreaterThan(expected), $"{element} should be > {expected} px left from {secondElement} but was {actualDistance} px.");
    }

    public static void AssertLeftOfGreaterThanOrEqual(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateLeftOfDistance(element, secondElement);
        Assert.That(actualDistance, Is.GreaterThanOrEqualTo(expected), $"{element} should be >= {expected} px left from {secondElement} but was {actualDistance} px.");
    }

    public static void AssertLeftOfLessThan(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateLeftOfDistance(element, secondElement);
        Assert.That(actualDistance, Is.LessThan(expected), $"{element} should be < {expected} px left from {secondElement} but was {actualDistance} px.");
    }

    public static void AssertLeftOfLessThanOrEqual(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateLeftOfDistance(element, secondElement);
        Assert.That(actualDistance, Is.LessThanOrEqualTo(expected), $"{element} should be <= {expected} px left from {secondElement} but was {actualDistance} px.");
    }

    public static void AssertLeftOfApproximate(this ILayoutComponent element, ILayoutComponent secondElement, double expected, double percent)
    {
        var actualDistance = CalculateLeftOfDistance(element, secondElement);
        var actualPercentDifference = CalculatePercentDifference(expected, actualDistance);
        Assert.That(actualPercentDifference, Is.LessThanOrEqualTo(percent), $"{element} should be <= {percent}% of {expected} px left from {secondElement} but was {actualDistance} px.");
    }
}
