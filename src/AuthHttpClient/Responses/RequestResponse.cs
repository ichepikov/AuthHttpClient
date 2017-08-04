namespace AuthHttpClient.Responses
{
    public class RequestResponse<T>
    {
        public RequestResponse(T t) : this(t, RequestResponseErrorTypes.None)
        {
        }

        public RequestResponse(T t, RequestResponseErrorTypes errorCode)
        {
            Data = t;
            ErrorCode = errorCode;
        }

        public T Data { get; }
        public RequestResponseErrorTypes ErrorCode { get; }

        public bool HasError
        {
            get { return Data == null || ErrorCode != RequestResponseErrorTypes.None; }
        }
    }
}
