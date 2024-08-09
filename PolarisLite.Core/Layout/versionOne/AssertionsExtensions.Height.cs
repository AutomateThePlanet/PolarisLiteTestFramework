using NUnit.Framework;

namespace PolarisLite.Core.Layout;

public static partial class AssertionsExtensions
{
    public static void AssertHeight(this ILayoutComponent layoutComponent, double expected)
    {
        Assert.That(layoutComponent.Size.Height, Is.EqualTo(expected), $"The height of {layoutComponent} was not {expected} px, but {layoutComponent.Size.Height} px.");
    }

    public static void AssertHeightBetween(this ILayoutComponent layoutComponent, double from, double to)
    {
        Assert.That(layoutComponent.Size.Height, Is.InRange(from, to), $"The height of {layoutComponent} was not between {from} and {to} px, but {layoutComponent.Size.Height} px.");
    }

    public static void AssertHeightGreaterThan(this ILayoutComponent layoutComponent, double expected)
    {
        Assert.That(layoutComponent.Size.Height, Is.GreaterThan(expected), $"The height of {layoutComponent} was not > {expected} px, but {layoutComponent.Size.Height} px.");
    }

    public static void AssertHeightGreaterThanOrEqual(this ILayoutComponent layoutComponent, double expected)
    {
        Assert.That(layoutComponent.Size.Height, Is.GreaterThanOrEqualTo(expected), $"The height of {layoutComponent} was not >= {expected} px, but {layoutComponent.Size.Height} px.");
    }

    public static void AssertHeightLessThan(this ILayoutComponent layoutComponent, double expected)
    {
        Assert.That(layoutComponent.Size.Height, Is.LessThan(expected), $"The height of {layoutComponent} was not < {expected} px, but {layoutComponent.Size.Height} px.");
    }

    public static void AssertHeightLessThanOrEqual(this ILayoutComponent layoutComponent, double expected)
    {
        Assert.That(layoutComponent.Size.Height, Is.LessThanOrEqualTo(expected), $"The height of {layoutComponent} was not <= {expected} px, but {layoutComponent.Size.Height} px.");
    }

    public static void AssertHeightApproximate(this ILayoutComponent layoutComponent, ILayoutComponent secondElement, double expectedPercentDifference)
    {
        var actualPercentDifference = CalculatePercentDifference(layoutComponent.Size.Height, secondElement.Size.Height);
        Assert.That(actualPercentDifference, Is.LessThanOrEqualTo(expectedPercentDifference), $"The height % difference between {layoutComponent} and {secondElement} was greater than {expectedPercentDifference}%, it was {actualPercentDifference}%.");
    }
}
