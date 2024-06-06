using PolarisLite.Web.ComponentsJS.CKEditor;
using PolarisLite.Web.Core.NUnit;
using PolarisLite.Web.Plugins;

namespace DemoSystemTests.Web;

[Browser(Browser.Chrome, Lifecycle.RestartEveryTime)]
public class CKEditorTests : WebTest
{
    protected override void TestInitialize()
    {
        App.Navigation.GoToUrl("https://ckeditor.com/ckeditor-5/demo/feature-rich/");
    }

    [Test]
    public void AllTextSelected_When_CallSelectAllMethodCkEditorComponent()
    {
        var editor = App.Elements.FindById<CkEditorComponent>("b-demo-editor");

        editor.SelectAll();
    }

    [Test]
    public void AllTextSelectedBolded_When_CallSelectAllMethod_And_SendBoldCommand()
    {
        var editor = App.Elements.FindById<CkEditorComponent>("b-demo-editor");

        editor.SelectAll();
        editor.ExecuteCommand(EditorCommands.Bold);
    }

    [Test]
    public void ReturnCorrectText_When_CallGetTextMethodCkEditorComponent()
    {
        var editor = App.Elements.FindById<CkEditorComponent>("b-demo-editor");

        var currentText = editor.GetText();

        StringAssert.Contains("Discover the riches of our editor", currentText);
    }

    [Test]
    public void ReturnCorrectHtml_When_CallGetHtmlMethodCkEditorComponent()
    {
        var editor = App.Elements.FindById<CkEditorComponent>("b-demo-editor");

        var currentText = editor.GetHtml();

        StringAssert.Contains("<p>Read on to better understand the functionalities you can test with this demo.</p>", currentText);
    }
}