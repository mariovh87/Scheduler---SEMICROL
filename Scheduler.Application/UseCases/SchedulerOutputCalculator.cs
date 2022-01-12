using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnsureThat;
using Semicrol.Scheduler.Domain.Common;
using Semicrol.Scheduler.Domain.Entities;
using Semicrol.Scheduler.Domain.Entities.MonthlyConfiguration;

namespace Semicrol.Scheduler.Application.UseCases
{
    public static class SchedulerOutputCalculator
    {
        public static Output CalculateWeeklyOutput(Input input, Configuration config, DailyFrecuency dailyFrecuency, WeeklyConfiguration weeklyConfig, Limits limits)
        {
            Output output = new();
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

        public static Output CalculateMonthlyOutput(Input input, DailyFrecuency dailyFrecuency, MonthlyConfiguration monthlyConfig, Limits limits)
        {
            return new Output 
            {
                ExecutionTime = CalculateMonthlyWithDailyFrecuency(input, dailyFrecuency, monthlyConfig, limits)
            };
        }

        public static IList<DateTime> CalculateMonthlyWithDailyFrecuency(Input input, DailyFrecuency dailyFrecuency, MonthlyConfiguration monthlyConfig, Limits limits)
        {
            List<DateTime> output = new();
            foreach(DateTime date in MonthlyRecurrenceCalculator.GetRecurrences(input.CurrentDate, limits, monthlyConfig))
            {
                output.AddRange(DailyFrecuencyCalculator.GetExecutions(date, dailyFrecuency));
            }

            return output.ToList();

        }
    }
}
