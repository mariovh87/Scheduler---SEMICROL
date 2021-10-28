using EnsureThat;
using Semicrol.Scheduler.Domain.Common;
using System;
using System.Collections.Generic;

namespace Semicrol.Scheduler.Domain.Entities
{
    public class Output
    {
        public IList<DateTime> ExecutionTime { get;  set; }
        public string Description { get; set; }

        public Output()
        {
            this.ExecutionTime = new List<DateTime>();
        }

        public void AddExecution(DateTime nextExecution)
        {
            nextExecution.EnsureIsValidDate();
            this.ExecutionTime.Add(nextExecution);
        }
    }
}
