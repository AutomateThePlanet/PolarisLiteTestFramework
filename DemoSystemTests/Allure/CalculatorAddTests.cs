using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;


namespace Examples.Tests;

[TestFixture]
[AllureNUnit]
[AllureSuite("CalculatorTests")]
[AllureDisplayIgnored]
internal class CalculatorAddTests
{
    [Test(Description = "Add two integers and returns the sum")]
    [AllureTag("CI")]
    [AllureSeverity(SeverityLevel.critical)]
    [AllureIssue("8911")]
    [AllureTms("532")]
    [AllureOwner("Anton")]
    [AllureSubSuite("Add")]
    public void Return4_WhenAdd2And2()
    {
        var calculator = new Calculator();

        int actualResult = calculator.Add(2, 2);

        Assert.That(actualResult, Is.EqualTo(4));
    }

    [Test(Description = "Add two integers and returns the sum")]
    [AllureTag("CI")]
    [AllureSeverity(SeverityLevel.critical)]
    [AllureSubSuite("Add")]
    public void Return0_WhenAdd0And0()
    {
        var calculator = new Calculator();

        int actualResult = calculator.Add(0, 0);

        Assert.That(actualResult, Is.EqualTo(0));
    }

    [Test(Description = "Add two integers and returns the sum")]
    [AllureTag("CI")]
    [AllureSeverity(SeverityLevel.critical)]
    [AllureSubSuite("Add")]
    public void ReturnMinus5_WhenAddMinus3AndMinus2()
    {
        var calculator = new Calculator();

        int actualResult = calculator.Add(0, 0);

        Assert.That(actualResult, Is.EqualTo(1));
    }
}
