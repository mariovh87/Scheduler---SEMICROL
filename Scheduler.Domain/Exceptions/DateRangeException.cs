using System;
using System.Collections.Generic;
using System.Text;

namespace Semicrol.Scheduler.Domain.Exceptions
{
    public class DateRangeException : DomainException
    {
        public DateRangeException(string message) : base(message) { }
    }
}
