namespace AuthHttpClient.Responses
{
    public enum RequestErrorTypes
    {
        None,
        Connection,
        Server,
        Parse,
        Authentication,
        Timeout,
        Unknown,
    }
}
