namespace PolarisLite.API;

public abstract class AssertExtensionsEventHandlers
{
    public virtual void SubscribeToAll()
    {
        ApiAssertExtensions.AssertExecutionTimeUnderEvent += AssertExecutionTimeUnderEventHandler;
        ApiAssertExtensions.AssertContentContainsEvent += AssertContentContainsEventHandler;
        ApiAssertExtensions.AssertContentNotContainsEvent += AssertContentNotContainsEventHandler;
        ApiAssertExtensions.AssertContentEqualsEvent += AssertContentEqualsEventHandler;
        ApiAssertExtensions.AssertContentNotEqualsEvent += AssertContentNotEqualsEventHandler;
        ApiAssertExtensions.AssertResultEqualsEvent += AssertResultEqualsEventHandler;
        ApiAssertExtensions.AssertResultNotEqualsEvent += AssertResultNotEqualsEventHandler;
        ApiAssertExtensions.AssertSuccessStatusCodeEvent += AssertSuccessStatusCodeEventHandler;
        ApiAssertExtensions.AssertStatusCodeEvent += AssertStatusCodeEventHandler;
        ApiAssertExtensions.AssertResponseHeaderEvent += AssertResponseHeaderEventHandler;
        ApiAssertExtensions.AssertContentTypeEvent += AssertContentTypeEventHandler;
        ApiAssertExtensions.AssertContentEncodingEvent += AssertContentEncodingEventHandler;
        ApiAssertExtensions.AssertCookieExistsEvent += AssertCookieExistsEventHandler;
        ApiAssertExtensions.AssertCookieEvent += AssertCookieEventHandler;
        ApiAssertExtensions.AssertSchemaEvent += AssertSchemaEventHandler;
    }

    public virtual void UnsubscribeToAll()
    {
        ApiAssertExtensions.AssertExecutionTimeUnderEvent -= AssertExecutionTimeUnderEventHandler;
        ApiAssertExtensions.AssertContentContainsEvent -= AssertContentContainsEventHandler;
        ApiAssertExtensions.AssertContentNotContainsEvent -= AssertContentNotContainsEventHandler;
        ApiAssertExtensions.AssertContentEqualsEvent -= AssertContentEqualsEventHandler;
        ApiAssertExtensions.AssertContentNotEqualsEvent -= AssertContentNotEqualsEventHandler;
        ApiAssertExtensions.AssertResultEqualsEvent -= AssertResultEqualsEventHandler;
        ApiAssertExtensions.AssertResultNotEqualsEvent -= AssertResultNotEqualsEventHandler;
        ApiAssertExtensions.AssertSuccessStatusCodeEvent -= AssertSuccessStatusCodeEventHandler;
        ApiAssertExtensions.AssertStatusCodeEvent -= AssertStatusCodeEventHandler;
        ApiAssertExtensions.AssertResponseHeaderEvent -= AssertResponseHeaderEventHandler;
        ApiAssertExtensions.AssertContentTypeEvent -= AssertContentTypeEventHandler;
        ApiAssertExtensions.AssertContentEncodingEvent -= AssertContentEncodingEventHandler;
        ApiAssertExtensions.AssertCookieExistsEvent -= AssertCookieExistsEventHandler;
        ApiAssertExtensions.AssertCookieEvent -= AssertCookieEventHandler;
        ApiAssertExtensions.AssertSchemaEvent -= AssertSchemaEventHandler;
    }

    protected virtual void AssertExecutionTimeUnderEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }

    protected virtual void AssertContentContainsEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }

    protected virtual void AssertContentNotContainsEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }

    protected virtual void AssertContentEqualsEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }

    protected virtual void AssertContentNotEqualsEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }

    protected virtual void AssertResultEqualsEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }

    protected virtual void AssertResultNotEqualsEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }

    protected virtual void AssertSuccessStatusCodeEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }

    protected virtual void AssertStatusCodeEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }

    protected virtual void AssertResponseHeaderEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }

    protected virtual void AssertContentTypeEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }

    protected virtual void AssertContentEncodingEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }

    protected virtual void AssertCookieExistsEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }

    protected virtual void AssertCookieEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }

    protected virtual void AssertSchemaEventHandler(object sender, ApiAssertEventArgs arg)
    {
    }
}
