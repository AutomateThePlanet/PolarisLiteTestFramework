using NUnit.Framework;
using PolarisLite.Web.Components;
using System.Text.RegularExpressions;

namespace PolarisLite.Web;

public static class StyleAssertions
{
    public static void AssertBackgroundColor(this WebComponent element, string expectedBackgroundColor)
    {
        var actualColor = element.WrappedElement.GetCssValue("background-color");
        Assert.That(actualColor, Is.EqualTo(expectedBackgroundColor));
    }

    public static void AssertBorderColor(this WebComponent element, string expectedBorderColor)
    {
        Assert.That(element.WrappedElement.GetCssValue("border-color"), Is.EqualTo(expectedBorderColor));
    }

    public static void AssertColor(this WebComponent element, string expectedColor)
    {
        Assert.That(element.WrappedElement.GetCssValue("color"), Is.EqualTo(expectedColor));
    }

    public static void AssertFontFamily(this WebComponent element, string expectedFontFamily)
    {
        Assert.That(element.WrappedElement.GetCssValue("font-family"), Is.EqualTo(expectedFontFamily));
    }

    public static void AssertFontWeight(this WebComponent element, string expectedFontWeight)
    {
        Assert.That(element.WrappedElement.GetCssValue("font-weight"), Is.EqualTo(expectedFontWeight));
    }

    public static void AssertFontSize(this WebComponent element, string expectedFontSize)
    {
        var elementCss = element.WrappedElement.GetCssValue("font-size");

        Assert.That(ExtractDoubleValue(elementCss), Is.EqualTo(ExtractDoubleValue(expectedFontSize)).Within(1));
        Assert.That(ExtractMeasureUnit(expectedFontSize), Is.EqualTo(ExtractMeasureUnit(elementCss)));
    }

    public static void AssertTextAlign(this WebComponent element, string expectedTextAlign)
    {
        Assert.That(element.WrappedElement.GetCssValue("text-align"), Is.EqualTo(expectedTextAlign));
    }

    public static void AssertVerticalAlign(this WebComponent element, string expectedVerticalAlign)
    {
        Assert.That(element.WrappedElement.GetCssValue("vertical-align"), Is.EqualTo(expectedVerticalAlign));
    }

    public static void AssertLineHeight(this WebComponent element, string expectedLineHeight)
    {
        var elementCss = element.WrappedElement.GetCssValue("line-height");

        Assert.That(ExtractDoubleValue(elementCss), Is.EqualTo(ExtractDoubleValue(expectedLineHeight)).Within(5));
        Assert.That(ExtractMeasureUnit(expectedLineHeight), Is.EqualTo(ExtractMeasureUnit(elementCss)));
    }

    public static void AssertLetterSpacing(this WebComponent element, string expectedLetterSpacing)
    {
        var elementCss = element.WrappedElement.GetCssValue("letter-spacing");

        Assert.That(ExtractDoubleValue(elementCss), Is.EqualTo(ExtractDoubleValue(expectedLetterSpacing)).Within(5));
        Assert.That(ExtractMeasureUnit(expectedLetterSpacing), Is.EqualTo(ExtractMeasureUnit(elementCss)));
    }

    public static void AssertMarginTop(this WebComponent element, string expectedMarginTop)
    {
        var elementCss = element.WrappedElement.GetCssValue("margin-top");

        Assert.That(ExtractDoubleValue(elementCss), Is.EqualTo(ExtractDoubleValue(expectedMarginTop)).Within(5));
        Assert.That(ExtractMeasureUnit(expectedMarginTop), Is.EqualTo(ExtractMeasureUnit(elementCss)));
    }

    public static void AssertMarginBottom(this WebComponent element, string expectedMarginBottom)
    {
        var elementCss = element.WrappedElement.GetCssValue("margin-bottom");

        Assert.That(ExtractDoubleValue(elementCss), Is.EqualTo(ExtractDoubleValue(expectedMarginBottom)).Within(5));
        Assert.That(ExtractMeasureUnit(expectedMarginBottom), Is.EqualTo(ExtractMeasureUnit(elementCss)));
    }

    public static void AssertMarginLeft(this WebComponent element, string expectedMarginLeft)
    {
        var elementCss = element.WrappedElement.GetCssValue("margin-left");

        Assert.That(ExtractDoubleValue(elementCss), Is.EqualTo(ExtractDoubleValue(expectedMarginLeft)).Within(5));
        Assert.That(ExtractMeasureUnit(expectedMarginLeft), Is.EqualTo(ExtractMeasureUnit(elementCss)));
    }

    public static void AssertMarginRight(this WebComponent element, string expectedMarginRight)
    {
        var elementCss = element.WrappedElement.GetCssValue("margin-right");

        Assert.That(ExtractDoubleValue(elementCss), Is.EqualTo(ExtractDoubleValue(expectedMarginRight)).Within(5));
        Assert.That(ExtractMeasureUnit(expectedMarginRight), Is.EqualTo(ExtractMeasureUnit(elementCss)));
    }

    public static void AssertPaddingTop(this WebComponent element, string expectedPaddingTop)
    {
        var elementCss = element.WrappedElement.GetCssValue("padding-top");

        Assert.That(ExtractDoubleValue(elementCss), Is.EqualTo(ExtractDoubleValue(expectedPaddingTop)).Within(5));
        Assert.That(ExtractMeasureUnit(expectedPaddingTop), Is.EqualTo(ExtractMeasureUnit(elementCss)));
    }

    public static void AssertPaddingBottom(this WebComponent element, string expectedPaddingBottom)
    {
        var elementCss = element.WrappedElement.GetCssValue("padding-bottom");

        Assert.That(ExtractDoubleValue(elementCss), Is.EqualTo(ExtractDoubleValue(expectedPaddingBottom)).Within(5));
        Assert.That(ExtractMeasureUnit(expectedPaddingBottom), Is.EqualTo(ExtractMeasureUnit(elementCss)));
    }

    public static void AssertPaddingLeft(this WebComponent element, string expectedPaddingLeft)
    {
        var elementCss = element.WrappedElement.GetCssValue("padding-left");

        Assert.That(ExtractDoubleValue(elementCss), Is.EqualTo(ExtractDoubleValue(expectedPaddingLeft)).Within(5));
        Assert.That(ExtractMeasureUnit(expectedPaddingLeft), Is.EqualTo(ExtractMeasureUnit(elementCss)));
    }

    public static void AssertPaddingRight(this WebComponent element, string expectedPaddingRight)
    {
        var elementCss = element.WrappedElement.GetCssValue("padding-right");

        Assert.That(ExtractDoubleValue(elementCss), Is.EqualTo(ExtractDoubleValue(expectedPaddingRight)).Within(5));
        Assert.That(ExtractMeasureUnit(expectedPaddingRight), Is.EqualTo(ExtractMeasureUnit(elementCss)));
    }

    public static void AssertPosition(this WebComponent element, string expectedPosition)
    {
        Assert.That(element.WrappedElement.GetCssValue("position"), Is.EqualTo(expectedPosition));
    }

    public static void AssertHeight(this WebComponent element, string expectedHeight)
    {
        var elementCss = element.WrappedElement.GetCssValue("height");

        Assert.That(ExtractDoubleValue(elementCss), Is.EqualTo(ExtractDoubleValue(expectedHeight)).Within(5));
        Assert.That(ExtractMeasureUnit(expectedHeight), Is.EqualTo(ExtractMeasureUnit(elementCss)));
    }

    public static void AssertWidth(this WebComponent element, string expectedWidth)
    {
        var elementCss = element.WrappedElement.GetCssValue("width");

        Assert.That(ExtractDoubleValue(elementCss), Is.EqualTo(ExtractDoubleValue(expectedWidth)).Within(30));
        Assert.That(ExtractMeasureUnit(expectedWidth), Is.EqualTo(ExtractMeasureUnit(elementCss)));
    }

    private static string ExtractMeasureUnit(string stringCssValue)
    {
        return stringCssValue.Substring(stringCssValue.IndexOf(stringCssValue.FirstOrDefault(x => char.IsLetter(x))));
    }

    private static double ExtractDoubleValue(string stringCssValue)
    {
        var regex = "([\\+\\-]?[0-9\\.]+)(%|px|rem|pt|em|in|cm|mm|ex|pc|vw)?";

        string doubleCssValue = Regex.Matches(stringCssValue, regex).FirstOrDefault().Groups[1].Value;

        return Convert.ToDouble(doubleCssValue);
    }
}