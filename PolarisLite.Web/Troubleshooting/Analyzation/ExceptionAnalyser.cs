using PolarisLite.Web.Services;

namespace PolarisLite.Web.Troubshoting;

public static class ExceptionAnalyser
{
    private static readonly List<ExceptionAnalysationHandler> _exceptionAnalysationHandlers = new List<ExceptionAnalysationHandler>();
    private static readonly DriverAdapter _driverAdapter = new DriverAdapter();

    public static void AddExceptionAnalysationHandler<TExceptionAnalysationHandler>(TExceptionAnalysationHandler exceptionAnalysationHandler)
      where TExceptionAnalysationHandler : ExceptionAnalysationHandler, new()
    {
        _exceptionAnalysationHandlers.Add(exceptionAnalysationHandler);
    }

    public static void AddExceptionAnalysationHandler<TExceptionAnalysationHandler>()
        where TExceptionAnalysationHandler : ExceptionAnalysationHandler, new()
    {
        _exceptionAnalysationHandlers.Add(new TExceptionAnalysationHandler());
    }

    public static void Analyse(Exception ex = null, params object[] context)
    {
        foreach (var exceptionHandler in _exceptionAnalysationHandlers)
        {
            if (exceptionHandler.IsApplicable(_driverAdapter, ex, context))
            {
                throw new AnalyzedTestException(exceptionHandler.DetailedIssueExplanation);
            }
        }
    }
}