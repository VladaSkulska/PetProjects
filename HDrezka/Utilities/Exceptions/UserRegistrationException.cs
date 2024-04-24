namespace HDrezka.Utilities.Exceptions
{
    public class UserRegistrationException : Exception
    {
        public UserRegistrationException() { }
        public UserRegistrationException(string message) : base(message) { }
    }
}