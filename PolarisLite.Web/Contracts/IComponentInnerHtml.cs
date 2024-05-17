using OpenQA.Selenium;

namespace PolarisLite.Web.Contracts;

public interface IComponentInnerHtml : IComponent
{
    string InnerHtml { get; }
}