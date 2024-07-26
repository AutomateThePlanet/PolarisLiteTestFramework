namespace PolarisLite.Web.Services.Contracts;
public interface IDevToolsService
{
    DevToolsSessionDomains DevToolsSessionDomains { get; set; }
    DevToolsSession DevToolsSession { get; set; }
  
    Task ListenConsoleLogs(EventHandler<MessageAddedEventArgs> messageAddedHandler);
    Task ListenJavaScriptConsoleLogs(EventHandler<JavaScriptConsoleApiCalledEventArgs> javaScriptConsoleApiCalled);
    Task ListenJavaScriptExceptionsThrown(EventHandler<JavaScriptExceptionThrownEventArgs> javaScriptExceptionThrown);
    Task AddInitializationScript(string name, string script);
}
