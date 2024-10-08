﻿using System.Drawing;

namespace PolarisLite.Web.Plugins;
public class BrowserConfiguration
{
    public BrowserConfiguration()
    {
    }

    public BrowserConfiguration(
        BrowserType browser,
        Lifecycle lifecycle)
    {
        Browser = browser;
        Lifecycle = lifecycle;
        ExecutionType = ExecutionType.Local;
    }

    public BrowserConfiguration(
    BrowserType browser,
    Lifecycle lifecycle,
    ExecutionType executionType,
    string browserVersion = "latest")
    {
        Browser = browser;
        Lifecycle = lifecycle;
        ExecutionType = executionType;
        BrowserVersion = browserVersion;
    }

    public BrowserConfiguration(
        string browser,
        string lifecycle,
        string executionType,
        string browserVersion = "latest")
    {
        Browser = (BrowserType)Enum.Parse(typeof(BrowserType), browser);
        Lifecycle = (Lifecycle)Enum.Parse(typeof(Lifecycle), lifecycle);
        ExecutionType = (ExecutionType)Enum.Parse(typeof(ExecutionType), executionType);
        BrowserVersion = browserVersion;
    }

    public BrowserType Browser { get; set; }
    public string BrowserVersion { get; set; }
    public Lifecycle Lifecycle { get; set; }
    public ExecutionType ExecutionType { get; set; }

    public Size Size { get; set; }
    public double PixelRation { get; set; }
    public string DeviceName { get; set; }
    public string UserAgent { get; set; }
    public bool MobileEmulation { get; set; }
}
