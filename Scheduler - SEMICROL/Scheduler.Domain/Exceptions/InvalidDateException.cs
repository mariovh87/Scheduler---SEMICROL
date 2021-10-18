using System;
namespace Semicrol.Scheduler.Domain.Exceptions
{
    public class InvalidDateException:DomainException
    {
        public InvalidDateException()
            :base("Date should be greater than Datetime MinValue and lesser than Datetime MaxValue")
        {
        }
    }
}
