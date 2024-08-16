using PolarisLite.Core;
using PolarisLite.Core.Utilities;
using System.Reflection;

namespace PolarisLite.API;

public class RetryFailedRequestsWorkflowPlugin : Plugin
{
    public override void OnAfterTestInitialize(MethodInfo memberInfo)
    {
        RetryFailedRequestsInfo retryFailedRequestsInfo = GetRetryFailedRequestsInfo(memberInfo);

        if (retryFailedRequestsInfo != null)
        {
            ApiClientAdapter.PauseBetweenFailures = TimeSpanConverter.Convert(retryFailedRequestsInfo.PauseBetweenFailures, retryFailedRequestsInfo.TimeUnit);
            ApiClientAdapter.MaxRetryAttempts = retryFailedRequestsInfo.MaxRetryAttempts;
        }
    }

    private RetryFailedRequestsInfo GetRetryFailedRequestsInfo(MemberInfo memberInfo)
    {
        RetryFailedRequestsInfo methodRetryFailedRequestsInfo = GetRetryFailedRequestsInfoByMethodInfo(memberInfo);
        RetryFailedRequestsInfo classRetryFailedRequestsInfo = GetRetryFailedRequestsInfoByType(memberInfo.DeclaringType);

        if (methodRetryFailedRequestsInfo != null)
        {
            return methodRetryFailedRequestsInfo;
        }
        else if (classRetryFailedRequestsInfo != null)
        {
            return classRetryFailedRequestsInfo;
        }

        return null;
    }

    private RetryFailedRequestsInfo GetRetryFailedRequestsInfoByType(Type currentType)
    {
        if (currentType == null)
        {
            throw new ArgumentNullException();
        }

        var retryFailedRequestsClassAttribute = currentType.GetCustomAttribute<RetryFailedRequestsAttribute>(true);
        return retryFailedRequestsClassAttribute?.RetryFailedRequestsInfo;
    }

    private RetryFailedRequestsInfo GetRetryFailedRequestsInfoByMethodInfo(MemberInfo memberInfo)
    {
        if (memberInfo == null)
        {
            throw new ArgumentNullException();
        }

        var retryFailedRequestsMethodAttribute = memberInfo.GetCustomAttribute<RetryFailedRequestsAttribute>(true);
        return retryFailedRequestsMethodAttribute?.RetryFailedRequestsInfo;
    }
}