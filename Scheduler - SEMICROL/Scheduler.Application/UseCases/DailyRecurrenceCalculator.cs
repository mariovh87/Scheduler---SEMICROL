using EnsureThat;
using Semicrol.Scheduler.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semicrol.Scheduler.Application.UseCases
{
    public static class DailyRecurrenceCalculator
    {
        public static IList<DateTime> GetAllRecurrences(DateTime currentDate, DateTime limitStartDate, DateTime limitEndDate, int every)
        {
            DateTimeExtensionMethods.EnsureDateIsInRange(currentDate, limitStartDate, limitEndDate);
            Ensure.That(every).IsGt(0);
            List<DateTime> recurrenceDays = new List<DateTime>();
            DateTime startDate = currentDate;

            while (startDate <= limitEndDate)
            {
                recurrenceDays.Add(startDate);
                startDate = startDate.AddDays(every);
            }
            return recurrenceDays;
        }
    }
}
