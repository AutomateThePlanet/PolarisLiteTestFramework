using NUnit.Framework;

namespace PolarisLite.Core.Layout;

public static partial class AssertionsExtensions
{
    public static void AssertHeight(this ILayoutComponent layoutComponent, double expected)
    {
        Assert.That(expected, Is.EqualTo(layoutComponent.Size.Height), $"The height of {layoutComponent} was not {expected} px, but {layoutComponent.Size.Height} px.");
    }

    public static void AssertHeightBetween(this ILayoutComponent layoutComponent, double from, double to)
    {
        Assert.IsTrue(layoutComponent.Size.Height >= from && layoutComponent.Size.Height <= to, $"The height of {layoutComponent} was not between {from} and {to} px, but {layoutComponent.Size.Height} px.");
    }

    public static void AssertHeightGreaterThan(this ILayoutComponent layoutComponent, double expected)
    {
        Assert.IsTrue(layoutComponent.Size.Height > expected, $"The height of {layoutComponent} was not > {expected} px, but {layoutComponent.Size.Height} px.");
    }

    public static void AssertHeightGreaterThanOrEqual(this ILayoutComponent layoutComponent, double expected)
    {
        Assert.IsTrue(layoutComponent.Size.Height >= expected, $"The height of {layoutComponent} was not >= {expected} px, but {layoutComponent.Size.Height} px.");
    }

    public static void AssertHeightLessThan(this ILayoutComponent layoutComponent, double expected)
    {
        Assert.IsTrue(layoutComponent.Size.Height < expected, $"The height of {layoutComponent} was not < {expected} px, but {layoutComponent.Size.Height} px.");
    }

    public static void AssertHeightLessThanOrEqual(this ILayoutComponent layoutComponent, double expected)
    {
        Assert.IsTrue(layoutComponent.Size.Height <= expected, $"The height of {layoutComponent} was not <= {expected} px, but {layoutComponent.Size.Height} px.");
    }

    public static void AssertHeightApproximate(this ILayoutComponent layoutComponent, ILayoutComponent secondElement, double expectedPercentDifference)
    {
        var actualPercentDifference = CalculatePercentDifference(layoutComponent.Size.Height, secondElement.Size.Height);
        Assert.IsTrue(actualPercentDifference <= expectedPercentDifference, $"The height % difference between {layoutComponent} and {secondElement} was greater than {expectedPercentDifference}%, it was {actualPercentDifference} px.");
    }
}
