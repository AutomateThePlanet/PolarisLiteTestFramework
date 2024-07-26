using Bellatrix.Core.Plugins;
using OpenQA.Selenium.Support.Extensions;
using PolarisLite.Core;
using PolarisLite.Web.Configuration.StaticImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace PolarisLite.Web.Plugins;
public class WebScreenshotPlugin : ScreenshotPlugin
{
    public WebScreenshotPlugin()
        : base(WebSettings.ScreenshotsOnFailure)
    {
    }

    protected override void TakeScreenshot(string screenshotSaveDir, string filename)
    {
        IWebDriver driver = DriverFactory.WrappedDriver;
        var screenshot = driver.TakeScreenshot();
        var destFile = new FileInfo(Path.Combine(screenshotSaveDir, filename));
        File.WriteAllBytes(destFile.FullName, screenshot.AsByteArray);
    }

    protected override string GetOutputFolder()
    {
        var saveLocation = WebSettings.ScreenshotsSaveLocation;

        var directory = new DirectoryInfo(saveLocation);
        if (!directory.Exists)
        {
            directory.Create();
        }

        return saveLocation;
    }

    protected override string GetUniqueFileName(string testName)
    {
        return $"{testName}_{Guid.NewGuid()}.png";
    }
}
