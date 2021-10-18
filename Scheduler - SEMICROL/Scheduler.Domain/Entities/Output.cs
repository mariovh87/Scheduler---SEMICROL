using EnsureThat;
using Semicrol.Scheduler.Domain.Common;
using System;

namespace Semicrol.Scheduler.Domain.Entities
{
    public class Output
    {
        public DateTime ExecutionTime { get; private set; }
        public string Description { get; private set; }

        public Output(DateTime executionTime, string description)
        {
            executionTime.EnsureIsValidDate();
            Ensure.That(description).IsNotEmptyOrWhiteSpace();
            this.ExecutionTime = executionTime;
            this.Description = description;
        }
    }
}
