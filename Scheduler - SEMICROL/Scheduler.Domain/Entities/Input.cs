using EnsureThat;
using System;

namespace Scheduler.Domain.Entities
{
    public class Input
    {
        internal DateTime currentDate;
        public Input(DateTime currentDate)
        {
            this.ValidateDate();
            this.currentDate = currentDate;
        }
        private void ValidateDate()
        {
            Ensure.That(currentDate).HasValue();
        }

    }
}
