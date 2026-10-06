namespace Kusanagi.Communication.Responses
{
    public class PayloadResponse
    {
        public string Status { get; init; } = string.Empty;
        public List<string> Messages { get; init; } = [];
    }

    public class PayloadResponse<T> : PayloadResponse
    {
        public T? Data { get; init; }
    }
}
