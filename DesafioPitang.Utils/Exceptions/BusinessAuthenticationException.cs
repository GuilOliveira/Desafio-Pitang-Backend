namespace DesafioPitang.Utils.Exceptions
{
    public class BusinessAuthenticationException : Exception
    {
        public BusinessAuthenticationException() { }
        public BusinessAuthenticationException(string message) : base(message) { }
        public BusinessAuthenticationException(string message, Exception exception) : base(message, exception) { }
        
    }
}
