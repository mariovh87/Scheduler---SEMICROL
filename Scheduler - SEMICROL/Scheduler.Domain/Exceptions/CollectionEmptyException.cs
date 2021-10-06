using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Domain.Exceptions
{
    internal class CollectionEmptyException:DomainException
    {
        public CollectionEmptyException() : base("La colección está vacía") { }
        public CollectionEmptyException(string message) : base(message) { }
    }
}
