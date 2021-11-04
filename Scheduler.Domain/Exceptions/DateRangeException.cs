using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Semicrol.Scheduler.Domain.Exceptions
{
    public class DateRangeException : DomainException, ISerializable
    {
        public DateRangeException(string message) : base(message) { }
    }
}
