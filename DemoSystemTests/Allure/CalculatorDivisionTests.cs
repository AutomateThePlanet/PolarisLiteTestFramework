using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;

namespace Examples.Tests;

[TestFixture]
[AllureNUnit]
[AllureSuite("CalculatorTests")]
[AllureDisplayIgnored]
internal class CalculatorDivisionTests
{
    [Test(Description = "Multiply two integers and returns the result")]
    [AllureTag("CI")]
    [AllureOwner("Anton")]
    [AllureSubSuite("Multiply")]
    public void Return4_WhenMultiply2And2()
    {
        var calculator = new Calculator();

        int actualResult = calculator.Multiply(2, 2);

        Assert.That(actualResult, Is.EqualTo(4));
    }

    [Test(Description = "Multiply two integers and returns the result")]
    [AllureTag("CI")]
    [AllureSeverity(SeverityLevel.critical)]
    [AllureSubSuite("Multiply")]
    public void Return0_WhenMultiply0And0()
    {
        var calculator = new Calculator();

        int actualResult = calculator.Multiply(0, 0);

        Assert.That(actualResult, Is.EqualTo(0));
    }

    [Test(Description = "Multiply two integers and returns the result")]
    [AllureTag("CI")]
    [AllureSeverity(SeverityLevel.critical)]
    [AllureSubSuite("Multiply")]
    public void ReturnMinus5_WhenMultiply5AndMinus1()
    {
        var calculator = new Calculator();

        int actualResult = calculator.Multiply(5, -1);

        Assert.That(actualResult, Is.EqualTo(0));
    }
}
