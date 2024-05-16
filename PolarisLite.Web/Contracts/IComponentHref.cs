using OpenQA.Selenium;

namespace PolarisLite.Web.Contracts;

public interface IComponentHref : IComponent
{
    public string Href => GetAttribute("href");
}