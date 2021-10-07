using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Domain.Exceptions
{
    public class DateRangeException : DomainException
    {
        public DateRangeException(string message) : base(message) { }
    }
}
