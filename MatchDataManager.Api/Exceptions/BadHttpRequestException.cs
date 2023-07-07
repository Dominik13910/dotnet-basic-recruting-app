namespace MatchDataManager.Api.Exceptions
{
    public class BadHttpRequestException : Exception
    {
        public BadHttpRequestException(string message) : base(message)
        {
        }
    }
}
