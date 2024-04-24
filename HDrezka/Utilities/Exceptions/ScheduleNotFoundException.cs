namespace HDrezka.Utilities.Exceptions
{
    public class ScheduleNotFoundException : Exception
    {
        public ScheduleNotFoundException()
        {
        }

        public ScheduleNotFoundException(string message)
            : base(message)
        {
        }
    }
}