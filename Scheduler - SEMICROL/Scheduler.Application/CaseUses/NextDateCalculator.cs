using EnsureThat;
using Scheduler.Domain.Entities;

namespace Scheduler.Application.CaseUses
{
    internal class NextDateCalculator
    {
        private readonly Input input;
        private readonly Configuration config;
        private readonly Limits limits;

        public NextDateCalculator(Input input, Configuration config, Limits limits)
        {
            Ensure.That<Input>(input).IsNotNull();
            Ensure.That<Configuration>(config).IsNotNull();
            Ensure.That<Limits>(limits).IsNotNull();
            this.input = input;
            this.config = config;
            this.limits = limits; 
        }

        public Output CalculateNextDate()
        {
            return null;
        }
    }
}
