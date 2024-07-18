using NUnit.Framework;

namespace PolarisLite.Core.Layout;

public static partial class AssertionsExtensions
{
    public static void AssertWidth(this ILayoutComponent layoutComponent, double expected)
    {
        Assert.That(expected, Is.EqualTo(layoutComponent.Size.Width), $"The width of {layoutComponent} was not {expected} px, but {layoutComponent.Size.Width} px.");
    }

    public static void AssertWidthBetween(this ILayoutComponent layoutComponent, double from, double to)
    {
        Assert.IsTrue(layoutComponent.Size.Width >= from && layoutComponent.Size.Width <= to, $"The width of {layoutComponent} was not between {from} and {to} px, but {layoutComponent.Size.Width} px.");
    }

    public static void AssertWidthGreaterThan(this ILayoutComponent layoutComponent, double expected)
    {
        Assert.IsTrue(layoutComponent.Size.Width > expected, $"The width of {layoutComponent} was not > {expected} px, but {layoutComponent.Size.Width} px.");
    }

    public static void AssertWidthGreaterThanOrEqual(this ILayoutComponent layoutComponent, double expected)
    {
        Assert.IsTrue(layoutComponent.Size.Width >= expected, $"The width of {layoutComponent} was not >= {expected} px, but {layoutComponent.Size.Width} px.");
    }

    public static void AssertWidthLessThan(this ILayoutComponent layoutComponent, double expected)
    {
        Assert.IsTrue(layoutComponent.Size.Width < expected, $"The width of {layoutComponent} was not < {expected} px, but {layoutComponent.Size.Width} px.");
    }

    public static void AssertWidthLessThanOrEqual(this ILayoutComponent layoutComponent, double expected)
    {
        Assert.IsTrue(layoutComponent.Size.Width <= expected, $"The width of {layoutComponent} was not <= {expected} px, but {layoutComponent.Size.Width} px.");
    }

    public static void AssertWidthApproximate(this ILayoutComponent layoutComponent, ILayoutComponent secondElement, double expectedPercentDifference)
    {
        var actualPercentDifference = CalculatePercentDifference(layoutComponent.Size.Width, secondElement.Size.Width);
        Assert.IsTrue(actualPercentDifference <= expectedPercentDifference, $"The width % difference between {layoutComponent} and {secondElement} was greater than {expectedPercentDifference}%, it was {actualPercentDifference} px.");
    }
}
