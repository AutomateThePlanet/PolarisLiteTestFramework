namespace PolarisLite.Core.Layout.Second;

public static class LayoutComponentValidationsBuilder
{
    public static LayoutPreciseValidationBuilder Above(this ILayoutComponent firstElement, ILayoutComponent secondElement)
    {
        return new LayoutPreciseValidationBuilder(
                () => CalculateAboveOfDistance(firstElement, secondElement),
                () => BuildNotificationValidationMessage(firstElement, secondElement, LayoutOptions.Above),
                () => BuildFailedValidationMessage(firstElement, secondElement, LayoutOptions.Above));
    }

    public static LayoutPreciseValidationBuilder Right(this ILayoutComponent firstElement, ILayoutComponent secondElement)
    {
        return new LayoutPreciseValidationBuilder(
                () => CalculateRightOfDistance(firstElement, secondElement),
                () => BuildNotificationValidationMessage(firstElement, secondElement, LayoutOptions.Right),
                () => BuildFailedValidationMessage(firstElement, secondElement, LayoutOptions.Right));
    }

    public static LayoutPreciseValidationBuilder Left(this ILayoutComponent firstElement, ILayoutComponent secondElement)
    {
        return new LayoutPreciseValidationBuilder(
                () => CalculateLeftOfDistance(firstElement, secondElement),
                () => BuildNotificationValidationMessage(firstElement, secondElement, LayoutOptions.Left),
                () => BuildFailedValidationMessage(firstElement, secondElement, LayoutOptions.Left));
    }

    public static LayoutPreciseValidationBuilder Below(this ILayoutComponent firstElement, ILayoutComponent secondElement)
    {
        return new LayoutPreciseValidationBuilder(
                () => CalculateBelowOfDistance(firstElement, secondElement),
                () => BuildNotificationValidationMessage(firstElement, secondElement, LayoutOptions.Below),
                () => BuildFailedValidationMessage(firstElement, secondElement, LayoutOptions.Below));
    }

    public static LayoutPreciseValidationBuilder TopInside(this ILayoutComponent firstElement, ILayoutComponent secondElement)
    {
        return new LayoutPreciseValidationBuilder(
                () => CalculateTopInsideOfDistance(firstElement, secondElement),
                () => BuildNotificationValidationMessage(firstElement, secondElement, LayoutOptions.TopInside),
                () => BuildFailedValidationMessage(firstElement, secondElement, LayoutOptions.TopInside));
    }

    public static LayoutPreciseValidationBuilder Inside(this ILayoutComponent firstElement, ILayoutComponent secondElement)
    {
        return new LayoutPreciseValidationBuilder(
                () => CalculateTopInsideOfDistance(firstElement, secondElement),
                () => BuildNotificationValidationMessage(firstElement, secondElement, LayoutOptions.Inside),
                () => BuildFailedValidationMessage(firstElement, secondElement, LayoutOptions.Inside));
    }

    public static LayoutPreciseValidationBuilder BottomInside(this ILayoutComponent firstElement, ILayoutComponent secondElement)
    {
        return new LayoutPreciseValidationBuilder(
                () => CalculateBottomInsideOfDistance(firstElement, secondElement),
                () => BuildNotificationValidationMessage(firstElement, secondElement, LayoutOptions.BottomInside),
                () => BuildFailedValidationMessage(firstElement, secondElement, LayoutOptions.BottomInside));
    }

    public static LayoutPreciseValidationBuilder LeftInside(this ILayoutComponent firstElement, ILayoutComponent secondElement)
    {
        return new LayoutPreciseValidationBuilder(
                () => CalculateLeftInsideOfDistance(firstElement, secondElement),
                () => BuildNotificationValidationMessage(firstElement, secondElement, LayoutOptions.LeftInside),
                () => BuildFailedValidationMessage(firstElement, secondElement, LayoutOptions.LeftInside));
    }

    public static LayoutPreciseValidationBuilder RightInside(this ILayoutComponent firstElement, ILayoutComponent secondElement)
    {
        return new LayoutPreciseValidationBuilder(
                () => CalculateRightInsideOfDistance(firstElement, secondElement),
                () => BuildNotificationValidationMessage(firstElement, secondElement, LayoutOptions.RightInside),
                () => BuildFailedValidationMessage(firstElement, secondElement, LayoutOptions.RightInside));
    }

    public static FinishValidationBuilder AlignedVerticallyAll(this ILayoutComponent firstElement, params ILayoutComponent[] elements)
    {
        int baseLineRightY = firstElement.Location.X + firstElement.Size.Width / 2;
        int baseLineLeftY = firstElement.Location.X;
        var comparingComponentsNames = GetElementsNames(elements);
        Func<bool> combinedPredicate = () => true;
        elements.ToList().ForEach(c =>
        {
            var rightX = c.Location.X + c.Size.Width / 2;
            var leftX = c.Location.X;
            combinedPredicate = () => combinedPredicate() && baseLineRightY.Equals(rightX) && baseLineLeftY.Equals(leftX);
        });

        return new FinishValidationBuilder(combinedPredicate,
                () => BuildNotificationAlignValidationMessage(firstElement, comparingComponentsNames, baseLineRightY, LayoutOptions.AlignedVerticallyRight) +
                        BuildNotificationAlignValidationMessage(firstElement, comparingComponentsNames, baseLineLeftY, LayoutOptions.AlignedVerticallyLeft),
                () => BuildFailedAlignValidationMessage(firstElement, comparingComponentsNames, baseLineRightY, LayoutOptions.AlignedVerticallyRight) +
                        BuildFailedAlignValidationMessage(firstElement, comparingComponentsNames, baseLineLeftY, LayoutOptions.AlignedVerticallyLeft));
    }

    public static FinishValidationBuilder AlignedVerticallyCentered(this ILayoutComponent firstElement, params ILayoutComponent[] elements)
    {
        int baseLineRightY = firstElement.Location.X + firstElement.Size.Width / 2;
        var comparingComponentsNames = GetElementsNames(elements);
        Func<bool> combinedPredicate = () => true;
        elements.ToList().ForEach(c =>
        {
            var rightX = c.Location.X + c.Size.Width / 2;
            combinedPredicate = () => combinedPredicate() && baseLineRightY.Equals(rightX);
        });

        return new FinishValidationBuilder(combinedPredicate,
                () => BuildNotificationAlignValidationMessage(firstElement, comparingComponentsNames, baseLineRightY, LayoutOptions.AlignedVerticallyCentered),
                () => BuildFailedAlignValidationMessage(firstElement, comparingComponentsNames, baseLineRightY, LayoutOptions.AlignedVerticallyCentered));
    }

    public static FinishValidationBuilder AlignedVerticallyRight(this ILayoutComponent firstElement, params ILayoutComponent[] elements)
    {
        int baseLineRightY = firstElement.Location.X + firstElement.Size.Width;
        var comparingComponentsNames = GetElementsNames(elements);
        Func<bool> combinedPredicate = () => true;
        elements.ToList().ForEach(c =>
        {
            var rightX = c.Location.X + c.Size.Width;
            combinedPredicate = () => combinedPredicate() && baseLineRightY.Equals(rightX);
        });

        return new FinishValidationBuilder(combinedPredicate,
                () => BuildNotificationAlignValidationMessage(firstElement, comparingComponentsNames, baseLineRightY, LayoutOptions.AlignedVerticallyRight),
                () => BuildFailedAlignValidationMessage(firstElement, comparingComponentsNames, baseLineRightY, LayoutOptions.AlignedVerticallyRight));
    }

    public static FinishValidationBuilder AlignedVerticallyLeft(this ILayoutComponent firstElement, params ILayoutComponent[] elements)
    {
        int baseLineLeftY = firstElement.Location.X;
        var comparingComponentsNames = GetElementsNames(elements);
        Func<bool> combinedPredicate = () => true;
        elements.ToList().ForEach(c => combinedPredicate = () => combinedPredicate() && baseLineLeftY.Equals(c.Location.X));

        return new FinishValidationBuilder(combinedPredicate,
                () => BuildNotificationAlignValidationMessage(firstElement, comparingComponentsNames, baseLineLeftY, LayoutOptions.AlignedVerticallyLeft),
                () => BuildFailedAlignValidationMessage(firstElement, comparingComponentsNames, baseLineLeftY, LayoutOptions.AlignedVerticallyLeft));
    }

    public static FinishValidationBuilder AlignedHorizontallyAll(this ILayoutComponent firstElement, params ILayoutComponent[] elements)
    {
        int baseLineTopY = firstElement.Location.Y;
        int baseLineBottomY = firstElement.Location.Y + firstElement.Size.Height;
        var comparingComponentsNames = GetElementsNames(elements);
        Func<bool> combinedPredicate = () => true;
        elements.ToList().ForEach(c =>
        {
            var topY = c.Location.Y;
            var bottomY = c.Location.Y + c.Size.Height;
            combinedPredicate = () => combinedPredicate() && baseLineTopY.Equals(topY) && baseLineBottomY.Equals(bottomY);
        });

        return new FinishValidationBuilder(combinedPredicate,
                () => BuildNotificationAlignValidationMessage(firstElement, comparingComponentsNames, baseLineTopY, LayoutOptions.AlignedHorizontallyTop) +
                        BuildNotificationAlignValidationMessage(firstElement, comparingComponentsNames, baseLineBottomY, LayoutOptions.AlignedHorizontallyBottom),
                () => BuildFailedAlignValidationMessage(firstElement, comparingComponentsNames, baseLineTopY, LayoutOptions.AlignedHorizontallyTop) +
                        BuildFailedAlignValidationMessage(firstElement, comparingComponentsNames, baseLineBottomY, LayoutOptions.AlignedHorizontallyBottom));
    }

    public static FinishValidationBuilder AlignedHorizontallyCentered(this ILayoutComponent firstElement, params ILayoutComponent[] elements)
    {
        int baseLineTopY = firstElement.Location.Y + firstElement.Size.Height / 2;
        var comparingComponentsNames = GetElementsNames(elements);
        Func<bool> combinedPredicate = () => true;
        elements.ToList().ForEach(c =>
         {
             var bottomY = c.Location.Y + c.Size.Height / 2;
             combinedPredicate = () => combinedPredicate() && baseLineTopY.Equals(bottomY);
         });

        return new FinishValidationBuilder(combinedPredicate,
                () => BuildNotificationAlignValidationMessage(firstElement, comparingComponentsNames, baseLineTopY, LayoutOptions.AlignedHorizontallyCentered),
                () => BuildFailedAlignValidationMessage(firstElement, comparingComponentsNames, baseLineTopY, LayoutOptions.AlignedHorizontallyCentered));
    }

    public static FinishValidationBuilder AlignedHorizontallyTop(this ILayoutComponent firstElement, params ILayoutComponent[] elements)
    {
        int baseLineTopY = firstElement.Location.Y;
        var comparingComponentsNames = GetElementsNames(elements);
        Func<bool> combinedPredicate = () => true;
        elements.ToList().ForEach(c => combinedPredicate = () => combinedPredicate() && baseLineTopY.Equals(c.Location.Y));

        return new FinishValidationBuilder(combinedPredicate,
                () => BuildNotificationAlignValidationMessage(firstElement, comparingComponentsNames, baseLineTopY, LayoutOptions.AlignedHorizontallyTop),
                () => BuildFailedAlignValidationMessage(firstElement, comparingComponentsNames, baseLineTopY, LayoutOptions.AlignedHorizontallyTop));
    }

    public static FinishValidationBuilder AlignedHorizontallyBottom(this ILayoutComponent firstElement, params ILayoutComponent[] elements)
    {
        int baseLineBottomY = firstElement.Location.Y + firstElement.Size.Height;
        var comparingComponentsNames = GetElementsNames(elements);
        Func<bool> combinedPredicate = () => true;
        elements.ToList().ForEach(c =>
        {
            var bottomY = c.Location.Y + c.Size.Height;
            combinedPredicate = () => combinedPredicate() && baseLineBottomY.Equals(bottomY);
        });

        return new FinishValidationBuilder(combinedPredicate,
                () => BuildNotificationAlignValidationMessage(firstElement, comparingComponentsNames, baseLineBottomY, LayoutOptions.AlignedHorizontallyBottom),
                () => BuildFailedAlignValidationMessage(firstElement, comparingComponentsNames, baseLineBottomY, LayoutOptions.AlignedHorizontallyBottom));
    }

    public static LayoutPreciseValidationBuilder Height(this ILayoutComponent firstElement)
    {
        return new LayoutPreciseValidationBuilder(firstElement.Size.Height);
    }

    public static LayoutPreciseValidationBuilder Width(this ILayoutComponent firstElement)
    {
        return new LayoutPreciseValidationBuilder(firstElement.Size.Height);
    }

    private static string GetElementsNames(params ILayoutComponent[] elements)
    {
        var comparingComponentsNames = new List<string>();
        comparingComponentsNames.AddRange(elements.ToList().Select(x => x.ToString()));
        return string.Join(',', comparingComponentsNames);
    }

    private static string BuildNotificationAlignValidationMessage(ILayoutComponent firstElement, string componentNames, int expected, LayoutOptions validationType)
    {
        return $"validate {firstElement} is {validationType.GetStringValue()} {componentNames} {expected} px ";
    }

    private static string BuildFailedAlignValidationMessage(ILayoutComponent firstElement, string componentNames, int expected, LayoutOptions validationType)
    {
        return $"{firstElement} should be {validationType.GetStringValue()} {componentNames} {expected} px but was not.";
    }

    private static string BuildNotificationValidationMessage(ILayoutComponent firstElement, ILayoutComponent secondElement, LayoutOptions validationType)
    {
        return $"validate {firstElement} is {validationType.GetStringValue()} of {secondElement}";
    }

    private static string BuildFailedValidationMessage(ILayoutComponent firstElement, ILayoutComponent secondElement, LayoutOptions validationType)
    {
        return $"{firstElement} should be {validationType.GetStringValue()} of {secondElement}";
    }

    private static double CalculateLeftOfDistance(ILayoutComponent component, ILayoutComponent secondComponent)
    {
        return secondComponent.Location.X - (component.Location.X + component.Size.Width);
    }

    private static double CalculateRightOfDistance(ILayoutComponent component, ILayoutComponent secondComponent)
    {
        return component.Location.X - (secondComponent.Location.X + secondComponent.Size.Width);
    }

    private static double CalculateAboveOfDistance(ILayoutComponent component, ILayoutComponent secondComponent)
    {
        return secondComponent.Location.Y - (component.Location.Y + component.Size.Height);
    }

    private static double CalculateBelowOfDistance(ILayoutComponent component, ILayoutComponent secondComponent)
    {
        return component.Location.Y - (secondComponent.Location.Y + secondComponent.Size.Height);
    }

    private static double CalculateTopInsideOfDistance(ILayoutComponent innerComponent, ILayoutComponent outerComponent)
    {
        return innerComponent.Location.Y - outerComponent.Location.Y;
    }

    private static double CalculateBottomInsideOfDistance(ILayoutComponent innerComponent, ILayoutComponent outerComponent)
    {
        return (outerComponent.Location.Y + outerComponent.Size.Height) - (innerComponent.Location.Y + innerComponent.Size.Height);
    }

    private static double CalculateLeftInsideOfDistance(ILayoutComponent innerComponent, ILayoutComponent outerComponent)
    {
        return innerComponent.Location.X - outerComponent.Location.X;
    }

    private static double CalculateRightInsideOfDistance(ILayoutComponent innerComponent, ILayoutComponent outerComponent)
    {
        return (outerComponent.Location.X + outerComponent.Size.Width) - (innerComponent.Location.X + innerComponent.Size.Width);
    }
}
