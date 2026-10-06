using System.Net;

namespace Kusanagi.Exception.ExceptionsBase;

public abstract class BaseException : System.Exception
{
    public abstract HttpStatusCode GetStatusCode();
    public abstract List<string> GetErrorMessages();
}
