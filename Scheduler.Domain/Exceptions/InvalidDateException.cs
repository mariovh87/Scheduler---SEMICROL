using System;
using System.Runtime.Serialization;

namespace Semicrol.Scheduler.Domain.Exceptions
{
    public class InvalidDateException:DomainException, ISerializable
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
