﻿using NUnit.Allure.Attributes;
using NUnit.Allure.Core;

namespace Examples.Tests;

[TestFixture]
[AllureNUnit]
[AllureSuite("CalculatorTests")]
[AllureDisplayIgnored]
internal class CalculatorMultipleTests
{
    [Test(Description = "Performing Division on two float variables. ")]
    [AllureTag("CI")]
    [AllureOwner("Anton")]
    [AllureSubSuite("Division")]
    public void ThrowException_When_DivisionOn0()
    {
        var calculator = new Calculator();

        float actualResult = calculator.Division(2, 0);

        Assert.That(actualResult, Is.EqualTo(4));
    }

    [Test(Description = "Performing Division on two float variables. ")]
    [AllureTag("CI")]
    [AllureOwner("Anton")]
    [AllureSubSuite("Division")]
    public void Return2_When_Division2On1()
    {
        var calculator = new Calculator();

        float actualResult = calculator.Division(2, 1);

        Assert.That(actualResult, Is.EqualTo(4));
    }
}