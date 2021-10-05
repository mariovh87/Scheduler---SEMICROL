using System;
using EnsureThat;
using Scheduler.Domain.Common;

namespace Scheduler.Domain.Entities
{
    public class Limits
    {
        private readonly DateTime startDate;
        private readonly DateTime? endDate;
        public Limits(DateTime startDate, DateTime? endDate)
        {
            startDate.EnsureIsValidDate();
            DateTimeExtensionMethods.ValidateRangeStartEnd(startDate, endDate); 

            this.startDate = startDate;
            this.endDate = endDate;
        }
      
        public DateTime StartDate()
        {
            return startDate;
        }

        public DateTime? EndDate()
        {
            return endDate;
        }
    }
}
