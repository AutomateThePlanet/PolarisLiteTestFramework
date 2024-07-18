using NUnit.Framework;

namespace PolarisLite.Core.Layout;

public static class LayoutAssert
{
    public static void AssertAlignedVerticallyAll(params ILayoutComponent[] layoutComponents)
    {
        ValidateLayoutComponentsCount(layoutComponents);

        var baseLineLeftY = layoutComponents.First().Location.X;
        var baseLineRightY = layoutComponents.First().Location.X + layoutComponents.First().Size.Width;

        for (int i = 1; i < layoutComponents.Length; i++)
        {
            var leftX = layoutComponents[i].Location.X;
            var rightX = layoutComponents[i].Location.X + layoutComponents[i].Size.Width;
            Assert.That(leftX, Is.EqualTo(baseLineLeftY), $"{layoutComponents.First()} should be aligned left vertically {layoutComponents[i]} Y = {baseLineLeftY} px but was {leftX} px.");
            Assert.That(rightX, Is.EqualTo(baseLineRightY), $"{layoutComponents.First()} should be aligned right vertically {layoutComponents[i]} Y = {baseLineRightY} px but was {rightX} px.");
        }
    }

    public static void AssertAlignedVerticallyCentered(params ILayoutComponent[] layoutComponents)
    {
        ValidateLayoutComponentsCount(layoutComponents);

        var baseLineRightY = layoutComponents.First().Location.X + layoutComponents.First().Size.Width / 2;

        for (int i = 1; i < layoutComponents.Length; i++)
        {
            var rightX = layoutComponents[i].Location.X + layoutComponents[i].Size.Width / 2;
            Assert.That(rightX, Is.EqualTo(baseLineRightY), $"{layoutComponents.First()} should be aligned centered vertically {layoutComponents[i]} Y = {baseLineRightY} px but was {rightX} px.");
        }
    }

    public static void AssertAlignedVerticallyRight(params ILayoutComponent[] layoutComponents)
    {
        ValidateLayoutComponentsCount(layoutComponents);

        var baseLineRightY = layoutComponents.First().Location.X + layoutComponents.First().Size.Width;

        for (int i = 1; i < layoutComponents.Length; i++)
        {
            var rightX = layoutComponents[i].Location.X + layoutComponents[i].Size.Width;
            Assert.That(rightX, Is.EqualTo(baseLineRightY), $"{layoutComponents.First()} should be aligned right vertically {layoutComponents[i]} Y = {baseLineRightY} px but was {rightX} px.");
        }
    }

    public static void AssertAlignedVerticallyLeft(params ILayoutComponent[] layoutComponents)
    {
        ValidateLayoutComponentsCount(layoutComponents);

        var baseLineLeftY = layoutComponents.First().Location.X;

        for (int i = 1; i < layoutComponents.Length; i++)
        {
            var leftX = layoutComponents[i].Location.X;
            Assert.That(leftX, Is.EqualTo(baseLineLeftY), $"{layoutComponents.First()} should be aligned left vertically {layoutComponents[i]} Y = {baseLineLeftY} px but was {leftX} px.");
        }
    }

    public static void AssertAlignedHorizontallyAll(params ILayoutComponent[] layoutComponents)
    {
        ValidateLayoutComponentsCount(layoutComponents);

        var baseLineTopY = layoutComponents.First().Location.Y;
        var baseLineBottomY = layoutComponents.First().Location.Y + layoutComponents.First().Size.Height;

        for (int i = 1; i < layoutComponents.Length; i++)
        {
            var topY = layoutComponents[i].Location.Y;
            var bottomY = layoutComponents[i].Location.Y + layoutComponents[i].Size.Height;
            Assert.That(topY, Is.EqualTo(baseLineTopY), $"{layoutComponents.First()} should be aligned top horizontally {layoutComponents[i]} Y = {baseLineTopY} px but was {topY} px.");
            Assert.That(bottomY, Is.EqualTo(baseLineBottomY), $"{layoutComponents.First()} should be aligned bottom horizontally {layoutComponents[i]} Y = {baseLineBottomY} px but was {bottomY} px.");
        }
    }

    public static void AssertAlignedHorizontallyCentered(params ILayoutComponent[] layoutComponents)
    {
        ValidateLayoutComponentsCount(layoutComponents);

        var baseLineTopY = layoutComponents.First().Location.Y + layoutComponents.First().Size.Height / 2;

        for (int i = 1; i < layoutComponents.Length; i++)
        {
            var centeredY = layoutComponents[i].Location.Y + layoutComponents[i].Size.Height / 2;
            Assert.That(centeredY, Is.EqualTo(baseLineTopY), $"{layoutComponents.First()} should be aligned centered horizontally {layoutComponents[i]} Y = {baseLineTopY} px but was {centeredY} px.");
        }
    }

    public static void AssertAlignedHorizontallyTop(params ILayoutComponent[] layoutComponents)
    {
        ValidateLayoutComponentsCount(layoutComponents);

        var baseLineTopY = layoutComponents.First().Location.Y;

        for (int i = 1; i < layoutComponents.Length; i++)
        {
            var topY = layoutComponents[i].Location.Y;
            Assert.That(topY, Is.EqualTo(baseLineTopY), $"{layoutComponents.First()} should be aligned top horizontally {layoutComponents[i]} Y = {baseLineTopY} px but was {topY} px.");
        }
    }

    public static void AssertAlignedHorizontallyBottom(params ILayoutComponent[] layoutComponents)
    {
        ValidateLayoutComponentsCount(layoutComponents);

        var baseLineBottomY = layoutComponents.First().Location.Y + layoutComponents.First().Size.Height;

        for (int i = 1; i < layoutComponents.Length; i++)
        {
            var bottomY = layoutComponents[i].Location.Y + layoutComponents[i].Size.Height;
            Assert.That(bottomY, Is.EqualTo(baseLineBottomY), $"{layoutComponents.First()} should be aligned bottom horizontally {layoutComponents[i]} Y = {baseLineBottomY} px but was {bottomY} px.");
        }
    }

    private static void ValidateLayoutComponentsCount(params ILayoutComponent[] layoutComponents)
    {
        if (layoutComponents.Length <= 1)
        {
            throw new ArgumentException("You need to pass at least two elements.");
        }
    }
}
