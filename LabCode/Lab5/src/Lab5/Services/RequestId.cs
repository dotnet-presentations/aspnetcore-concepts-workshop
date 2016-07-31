using System.Threading;

public interface IRequestIdFactory
{
    string MakeRequestId();
}

public class RequestIdFactory : IRequestIdFactory
{
    private int _requestId;

    public string MakeRequestId() => Interlocked.Increment(ref _requestId).ToString();
}

public interface IRequestId
{
    string Id { get; }
}

public class RequestId : IRequestId
{
    private readonly string _requestId;

    public RequestId(IRequestIdFactory requestIdFactory)
    {
        _requestId = requestIdFactory.MakeRequestId();
    }

    public string Id => _requestId;
}