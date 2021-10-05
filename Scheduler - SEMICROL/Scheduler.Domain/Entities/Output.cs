using EnsureThat;
using System;

namespace Scheduler.Domain.Entities
{
    public class Output
    {
        private ExecutionTimesQueue executionTimes;
        private string description;
        public Output(ExecutionTimesQueue executionTimes, string description)
        {
            Ensure.That(executionTimes).IsNotNull();
            Ensure.That(description).IsNotEmptyOrWhiteSpace();
            this.executionTimes = executionTimes;
            this.description = description;
        }

        public DateTime NextExecution()
        {
            return executionTimes.GetNextExecution();
        }

        public DateTime DequeueNextExecution()
        {
            return executionTimes.DequeueNextExecution();
        }

        public string Description()
        {
            return this.description;
        }
    }
}
