using OpenQA.Selenium;

namespace PolarisLite.Web.Contracts;

public interface IComponentInnerHtml : IComponent
{
    public string InnerHtml => GetAttribute("innerHTML");
}