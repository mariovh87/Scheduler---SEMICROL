using EnsureThat;
using Scheduler.Domain.Common;
using System;

namespace Scheduler.Domain.Entities
{
    public class Output
    {
        private readonly DateTime executionTime;
        private readonly string description;
        public Output(DateTime executionTime, string description)
        {
            executionTime.EnsureIsValidDate();
            Ensure.That(description).IsNotEmptyOrWhiteSpace();
            this.executionTime = executionTime;
            this.description = description;
        }

        public DateTime NextExecution()
        {
            return executionTime;
        }

        public string Description()
        {
            return this.description;
        }
    }
}
