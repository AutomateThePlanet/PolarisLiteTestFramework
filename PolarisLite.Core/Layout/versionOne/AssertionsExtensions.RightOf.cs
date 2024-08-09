using NUnit.Framework;

namespace PolarisLite.Core.Layout;

public static partial class AssertionsExtensions
{
    public static void AssertRightOf(this ILayoutComponent element, ILayoutComponent secondElement)
    {
        var actualDistance = CalculateRightOfDistance(element, secondElement);
        Assert.That(actualDistance, Is.GreaterThan(0), $"{element} should be right from {secondElement} but was {actualDistance} px.");
    }

    public static void AssertRightOf(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateRightOfDistance(element, secondElement);
        Assert.That(actualDistance, Is.EqualTo(expected), $"{element} should be {expected} px right from {secondElement} but was {actualDistance} px.");
    }

    public static void AssertRightOfBetween(this ILayoutComponent element, ILayoutComponent secondElement, double from, double to)
    {
        var actualDistance = CalculateRightOfDistance(element, secondElement);
        Assert.That(actualDistance, Is.InRange(from, to), $"{element} should be between {from}-{to} px right from {secondElement}, but was {actualDistance} px.");
    }

    public static void AssertRightOfGreaterThan(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateRightOfDistance(element, secondElement);
        Assert.That(actualDistance, Is.GreaterThan(expected), $"{element} should be > {expected} px right from {secondElement} but was {actualDistance} px.");
    }

    public static void AssertRightOfGreaterThanOrEqual(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateRightOfDistance(element, secondElement);
        Assert.That(actualDistance, Is.GreaterThanOrEqualTo(expected), $"{element} should be >= {expected} px right from {secondElement} but was {actualDistance} px.");
    }

    public static void AssertRightOfLessThan(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateRightOfDistance(element, secondElement);
        Assert.That(actualDistance, Is.LessThan(expected), $"{element} should be < {expected} px right from {secondElement} but was {actualDistance} px.");
    }

    public static void AssertRightOfLessThanOrEqual(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateRightOfDistance(element, secondElement);
        Assert.That(actualDistance, Is.LessThanOrEqualTo(expected), $"{element} should be <= {expected} px right from {secondElement} but was {actualDistance} px.");
    }

    public static void AssertRightOfApproximate(this ILayoutComponent element, ILayoutComponent secondElement, double expected, double percent)
    {
        var actualDistance = CalculateRightOfDistance(element, secondElement);
        var actualPercentDifference = CalculatePercentDifference(expected, actualDistance);
        Assert.That(actualPercentDifference, Is.LessThanOrEqualTo(percent), $"{element} should be <= {percent}% of {expected} px right from {secondElement} but was {actualDistance} px.");
    }
}
