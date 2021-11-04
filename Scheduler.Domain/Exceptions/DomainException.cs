using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Semicrol.Scheduler.Domain.Exceptions
{
    public class DomainException:Exception, ISerializable
    {
        public DomainException(string message) : base(message) { }
    }
}
