using EnsureThat;
using System;
using System.Collections.Generic;
using Scheduler.Domain.Common;

namespace Scheduler.Domain.Entities
{
    public class ExecutionTimes
    {
        private Queue<DateTime> _executionTimes;

        public ExecutionTimes()
        {
           _executionTimes = new Queue<DateTime>();
        }

        public DateTime GetNextExecution()
        {
            _executionTimes.HasItems();
            return this._executionTimes.Peek();
        }

        public DateTime DequeueNextExecution()
        {
            _executionTimes.HasItems();
            return this._executionTimes.Dequeue();
        }

        public void QueueExecution(DateTime execution)
        {
            _executionTimes.Enqueue(execution);
        }
    }
}
