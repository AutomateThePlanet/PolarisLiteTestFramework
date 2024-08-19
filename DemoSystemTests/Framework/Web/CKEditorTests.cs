using PolarisLite.Web.ComponentsJS.CKEditor;
using PolarisLite.Web.Core.NUnit;
using PolarisLite.Web;
using NUnit.Framework.Legacy;
using PolarisLite.Web.Plugins;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using Allure.Net.Commons;

namespace DemoSystemTests.Web;

//[Browser(Browser.Chrome, Lifecycle.RestartEveryTime)]
[LambdaTest]
//[LocalExecution(BrowserType.Chrome, Lifecycle.RestartEveryTime)]
[AllureNUnit]
[AllureParentSuite("Advanced Custom Components")]
[AllureSuite("CKEditor")]
[AllureSeverity(SeverityLevel.critical)]
[AllureOwner("Anton Angelov")]
public class CKEditorTests : WebTest
{
    protected override void TestInitialize()
    {
        App.Navigation.GoToUrl("https://ckeditor.com/ckeditor-5/demo/feature-rich/");
    }

    [Test]
    [Category(Categories.CUSTOM_CONTROLS)]
    [Category(Categories.CI)]
    public void AllTextSelected_When_CallSelectAllMethodCkEditorComponent()
    {
        var editor = App.Elements.FindById<CkEditorComponent>("demo").ToBeClickable();

        editor.SelectAll();
    }

    [Test]
    [Category(Categories.CUSTOM_CONTROLS)]
    [Category(Categories.CI)]
    public void AllTextSelectedBolded_When_CallSelectAllMethod_And_SendBoldCommand()
    {
        var editor = App.Elements.FindById<CkEditorComponent>("demo").ToBeClickable();

        editor.SelectAll();
        editor.ExecuteCommand(EditorCommands.Bold);
    }

    [Test]
    [Category(Categories.CUSTOM_CONTROLS)]
    [Category(Categories.CI)]
    public void ReturnCorrectText_When_CallGetTextMethodCkEditorComponent()
    {
        var editor = App.Elements.FindById<CkEditorComponent>("demo").ToBeClickable();

        var currentText = editor.GetText();

        StringAssert.Contains("Discover the riches of our editor", currentText);
    }

    [Test]
    [Category(Categories.CUSTOM_CONTROLS)]
    [Category(Categories.CI)]
    public void ReturnCorrectHtml_When_CallGetHtmlMethodCkEditorComponent()
    {
        var editor = App.Elements.FindById<CkEditorComponent>("demo").ToBeClickable();

        var currentText = editor.GetHtml();

        StringAssert.Contains("<p>Read on to better understand the functionalities you can test with this demo.</p>", currentText);
    }
}