namespace PolarisLite.Core.Layout;

public static partial class AssertionsExtensions
{
    internal static double CalculatePercentDifference(double num1, double num2)
    {
        var percentDifference = (num1 - num2) / ((num1 + num2) / 2) * 100;
        var actualPercentDifference = Math.Abs(percentDifference);

        return actualPercentDifference;
    }

    internal static double CalculateLeftOfDistance(ILayoutComponent element, ILayoutComponent secondElement)
    {
        return secondElement.Location.X - (element.Location.X + element.Size.Width);
    }

    internal static double CalculateRightOfDistance(ILayoutComponent element, ILayoutComponent secondElement)
    {
        return element.Location.X - (secondElement.Location.X + secondElement.Size.Width);
    }

    internal static double CalculateAboveOfDistance(ILayoutComponent element, ILayoutComponent secondElement)
    {
        return secondElement.Location.Y - (element.Location.Y + element.Size.Height);
    }

    internal static double CalculateBelowOfDistance(ILayoutComponent element, ILayoutComponent secondElement)
    {
        return element.Location.Y - (secondElement.Location.Y + secondElement.Size.Height);
    }

    internal static double CalculateTopInsideOfDistance(ILayoutComponent innerElement, ILayoutComponent outerElement)
    {
        return innerElement.Location.Y - outerElement.Location.Y;
    }

    internal static double CalculateBottomInsideOfDistance(ILayoutComponent innerElement, ILayoutComponent outerElement)
    {
        return (outerElement.Location.Y + outerElement.Size.Height) - (innerElement.Location.Y + innerElement.Size.Height);
    }

    internal static double CalculateLeftInsideOfDistance(ILayoutComponent innerElement, ILayoutComponent outerElement)
    {
        return innerElement.Location.X - outerElement.Location.X;
    }

    internal static double CalculateRightInsideOfDistance(ILayoutComponent innerElement, ILayoutComponent outerElement)
    {
        return (outerElement.Location.X + outerElement.Size.Width) - (innerElement.Location.X + innerElement.Size.Width);
    }
}
