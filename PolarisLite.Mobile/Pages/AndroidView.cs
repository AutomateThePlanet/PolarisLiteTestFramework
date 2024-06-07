namespace PolarisLite.Mobile;
public abstract class AndroidView
{
    public AndroidView()
    {
        App = new App();
    }

    public App App { get; set; }
}
