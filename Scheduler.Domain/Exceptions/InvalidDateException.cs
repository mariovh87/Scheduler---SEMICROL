using System;
namespace Semicrol.Scheduler.Domain.Exceptions
{
    public class InvalidDateException:DomainException
    {
        public InvalidDateException()
            :base("Invalid date")
        {
        }

        public InvalidDateException(string message)
            : base(message)
        {
        }
    }
}
