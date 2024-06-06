using PolarisLite.Locators;
using PolarisLite.Web.Components;

namespace PolarisLite.Web.ComponentsJS.CKEditor;
public class CkEditorComponent : Component
{
    public Component TextArea => FindComponent<Component>(new XPathFindStrategy(".//div[contains(@class, 'ck-content ck-editor__editable')]"));

    public void ExecuteCommand(EditorCommands command, string arg)
    {
        var commandText = $"arguments[0].ckeditorInstance.commands.get('{command.GetValue()}').execute({arg});";
        JavaScriptExecutor.ExecuteScript(commandText, TextArea.WrappedElement);
    }

    public void ExecuteCommand(EditorCommands command)
    {
        var commandText = $"arguments[0].ckeditorInstance.commands.get('{command.GetValue()}').execute();";
        JavaScriptExecutor.ExecuteScript(commandText, TextArea.WrappedElement);
    }

    public CkEditorComponent SetText(string text)
    {
        TextArea.WrappedElement.SendKeys(text);
        return this;
    }

    public CkEditorComponent SetText(string[] textAsArray)
    {
        for (int i = 0; i < textAsArray.Length; i++)
        {
            SetText(textAsArray[i]);
            if (i < textAsArray.Length - 1)
            {
                Enter();
            }
        }
        return this;
    }

    public void Clear()
    {
        SetText("");
    }

    public string GetHtml()
    {
        string command = GetEditorInstanceCommand("getData");
        var result = JavaScriptExecutor.ExecuteScript($"return {command}", TextArea.WrappedElement);
        return result?.ToString();
    }

    public string GetText()
    {
        return TextArea.WrappedElement.Text;
    }

    public CkEditorComponent SelectAll()
    {
        TextArea.WrappedElement.SendKeys(Keys.Control + "a");
        return this;
    }

    public void ExecuteAgainstEditorInstance(string command)
    {
        var commandText = $"arguments[0].ckeditorInstance.{command}";
        JavaScriptExecutor.ExecuteScript(commandText, WrappedElement);
    }

    public string GetEditorInstanceCommand(string command)
    {
        return $"arguments[0].ckeditorInstance.{command}();";
    }

    public CkEditorComponent Enter()
    {
        TextArea.WrappedElement.SendKeys(Keys.Enter);
        return this;
    }

    public CkEditorComponent MoveCursorInText(int timesOfMovement)
    {
        for (int i = 0; i < timesOfMovement; i++)
        {
            TextArea.WrappedElement.SendKeys(Keys.Control + Keys.ArrowLeft);
        }
        return this;
    }

    private void ClickToolbarButton(ToolbarButton toolbarButton)
    {
        string selector = $"//span[text()='{toolbarButton.GetValue()}']//ancestor::button[contains(@class, 'ck-button')]";
        var element = WrappedElement.FindElement(By.XPath(selector));
        Button button = FindComponent<Button>(new XPathFindStrategy(selector));
        // wait to be clickable
        button.Click();
    }
}

