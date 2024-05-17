namespace PolarisLite.Web;

public class InputFile : ComponentAdapter
{
    public virtual bool? IsRequired => string.IsNullOrEmpty(GetAttribute("required")) ? null : bool.Parse(GetAttribute("required"));

    public virtual void Upload(string filePath)
    {
        WrappedElement.SendKeys(filePath);
    }
}