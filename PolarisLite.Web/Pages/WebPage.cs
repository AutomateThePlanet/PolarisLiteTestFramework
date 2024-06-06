namespace PolarisLite.Web;
public abstract class WebPage
{
    public WebPage()
    {
        App = new App();
    }

    public App App { get; set; }
}
