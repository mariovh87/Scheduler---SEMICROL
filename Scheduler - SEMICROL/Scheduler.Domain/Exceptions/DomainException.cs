using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Domain.Exceptions
{
    internal class DomainException:Exception
    {
        public DomainException(string message) : base(message) { }
    }
}
