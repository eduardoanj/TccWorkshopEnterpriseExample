namespace Registration.UserRegistrationEnterpriseExample.Infrastructure.Exceptions;

public class RequeueException : Exception
{
    public RequeueException()
    {
    }
    
    public RequeueException(string message) 
        : base(message)
    {
    }
    
    public RequeueException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
    
    public RequeueException(string message, Exception innerException, TimeSpan? delay = null, 
        bool ignoreMaxRequeuesLimit = false) 
        : base(message, innerException)
    {
        if (delay != null) Delay = delay.Value;
        IgnoreMaxRequeuesLimit = ignoreMaxRequeuesLimit;
    }
    
    public TimeSpan Delay { get; }
    public bool IgnoreMaxRequeuesLimit { get; }
}