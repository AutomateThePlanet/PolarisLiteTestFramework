//using Bellatrix.Core.Plugins;
//using PolarisLite.Core;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.InteropServices.JavaScript;
//using System.Text;
//using System.Threading.Tasks;

//namespace PolarisLite.Web.Plugins;
//public class WebScreenshotPlugin : ScreenshotPlugin
//{
//    public WebScreenshotPlugin()
//        : base(ConfigurationService.GetSection<WebSettings>().ScreenshotsOnFailEnabled)
//    {
//    }

//    protected override void TakeScreenshot(string screenshotSaveDir, string filename)
//    {
//        var screenshot = new AShot()
//            .ShootingStrategy(ShootingStrategies.ViewportPasting(100))
//            .TakeScreenshot(DriverService.GetWrappedDriver());
//        var destFile = new FileInfo(Path.Combine(screenshotSaveDir, filename));
//        screenshot.Image.Save(destFile.FullName, ImageFormat.Png);
//    }

//    protected override string GetOutputFolder()
//    {
//        var saveLocation = ConfigurationService.Get<WebSettings>().ScreenshotsSaveLocation;
//        saveLocation = PathNormalizer.NormalizePath(saveLocation);

//        var directory = new DirectoryInfo(saveLocation);
//        if (!directory.Exists)
//        {
//            directory.Create();
//        }

//        return saveLocation;
//    }

//    protected override string GetUniqueFileName(string testName)
//    {
//        return $"{testName}_{Guid.NewGuid()}.png";
//    }
//}
