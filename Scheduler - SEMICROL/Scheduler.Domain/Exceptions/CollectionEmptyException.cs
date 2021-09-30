using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Domain.Exceptions
{
    internal class CollectionEmptyException:Exception
    {
        public CollectionEmptyException(string message) : base(message) { }
    }
}
