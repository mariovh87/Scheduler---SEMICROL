using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Domain.Exceptions
{
    internal class CollectionEmptyException:DomainException
    {
        public CollectionEmptyException() : base("The collection is empty") { }
        public CollectionEmptyException(string message) : base(message) { }
    }
}
