namespace AuthHttpClient.Responses
{
    public class RequestResponce<T>
    {
        public RequestResponce(T t) : this(t, RequestErrorTypes.None)
        {
        }

        public RequestResponce(T t, RequestErrorTypes errorCode)
        {
            Data = t;
            ErrorCode = errorCode;
        }

        public T Data { get; }
        public RequestErrorTypes ErrorCode { get; }

        public bool HasError
        {
            get { return Data == null || ErrorCode != RequestErrorTypes.None; }
        }
    }
}
