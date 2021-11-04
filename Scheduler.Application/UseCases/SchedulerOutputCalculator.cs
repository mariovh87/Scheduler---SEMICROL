using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnsureThat;
using Semicrol.Scheduler.Domain.Common;
using Semicrol.Scheduler.Domain.Entities;

namespace Semicrol.Scheduler.Application.UseCases
{
    public static class SchedulerOutputCalculator
    {
        public static Output CalculateOutput(Input input, Configuration config, DailyFrecuency dailyFrecuency, WeeklyConfiguration weeklyConfig, Limits limits)
        {
            Output output = new Output();
            if (config.Type == SchedulerEnums.ConfigurationType.Once)
            {
                config.OnceTimeAt.EnsureIsValidDate();
                DateTimeExtensionMethods.EnsureDateIsInRange(config.OnceTimeAt.Value, limits.StartDate, limits.EndDate);
                output.AddExecution(config.OnceTimeAt.Value);               
            }
            else
            {
                limits.EndDate.EnsureIsValidDate();
                IList<DateTime> executions = WeeklyRecurrenceCalculator.GetAllRecurrences(input.CurrentDate, weeklyConfig, dailyFrecuency, limits.EndDate.Value);
                output.ExecutionTime = executions;
            }
            output.Description = OutputDescriptionFormatter.Description(input,config,dailyFrecuency,weeklyConfig);
            return output;
        }

    }
}
