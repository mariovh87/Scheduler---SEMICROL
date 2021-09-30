using EnsureThat;
using System;
using System.Collections.Generic;
using Scheduler.Domain.Common;

namespace Scheduler.Domain.Entities
{
    internal class ExecutionTimes
    {
        private Queue<DateTime> _executionTimes;

        public ExecutionTimes()
        {
           _executionTimes = new Queue<DateTime>();
        }

        public DateTime GetNextExecutionTime()
        {
            _executionTimes.HasItems();
            return this._executionTimes.Peek();
        }

        public DateTime DequeueNextExecutionTime()
        {
            _executionTimes.HasItems();
            return this._executionTimes.Dequeue();
        }
    }
}
