using NUnit.Framework;

namespace PolarisLite.Core.Layout;

public static partial class AssertionsExtensions
{
    public static void AssertWidth(this ILayoutComponent layoutComponent, double expected)
    {
        Assert.That(layoutComponent.Size.Width, Is.EqualTo(expected), $"The width of {layoutComponent} was not {expected} px, but {layoutComponent.Size.Width} px.");
    }

    public static void AssertWidthBetween(this ILayoutComponent layoutComponent, double from, double to)
    {
        Assert.That(layoutComponent.Size.Width, Is.InRange(from, to), $"The width of {layoutComponent} was not between {from} and {to} px, but {layoutComponent.Size.Width} px.");
    }

    public static void AssertWidthGreaterThan(this ILayoutComponent layoutComponent, double expected)
    {
        Assert.That(layoutComponent.Size.Width, Is.GreaterThan(expected), $"The width of {layoutComponent} was not > {expected} px, but {layoutComponent.Size.Width} px.");
    }

    public static void AssertWidthGreaterThanOrEqual(this ILayoutComponent layoutComponent, double expected)
    {
        Assert.That(layoutComponent.Size.Width, Is.GreaterThanOrEqualTo(expected), $"The width of {layoutComponent} was not >= {expected} px, but {layoutComponent.Size.Width} px.");
    }

    public static void AssertWidthLessThan(this ILayoutComponent layoutComponent, double expected)
    {
        Assert.That(layoutComponent.Size.Width, Is.LessThan(expected), $"The width of {layoutComponent} was not < {expected} px, but {layoutComponent.Size.Width} px.");
    }

    public static void AssertWidthLessThanOrEqual(this ILayoutComponent layoutComponent, double expected)
    {
        Assert.That(layoutComponent.Size.Width, Is.LessThanOrEqualTo(expected), $"The width of {layoutComponent} was not <= {expected} px, but {layoutComponent.Size.Width} px.");
    }

    public static void AssertWidthApproximate(this ILayoutComponent layoutComponent, ILayoutComponent secondElement, double expectedPercentDifference)
    {
        var actualPercentDifference = CalculatePercentDifference(layoutComponent.Size.Width, secondElement.Size.Width);
        Assert.That(actualPercentDifference, Is.LessThanOrEqualTo(expectedPercentDifference), $"The width % difference between {layoutComponent} and {secondElement} was greater than {expectedPercentDifference}%, it was {actualPercentDifference}%.");
    }
}
