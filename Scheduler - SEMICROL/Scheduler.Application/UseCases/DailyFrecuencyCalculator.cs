﻿using System;
using System.Collections.Generic;
using System.Text;
using EnsureThat;
using Semicrol.Scheduler.Domain.Common;
using Semicrol.Scheduler.Domain.Entities;
using static Semicrol.Scheduler.Domain.Common.SchedulerEnums;

namespace Semicrol.Scheduler.Application.UseCases
{
    public static class DailyFrecuencyCalculator
    {
        public static Output CalculateOutput(DailyFrecuency dailyFrecuency, Input input)
        {
            return dailyFrecuency.occursOnce
                ? CalculateOccursOnce(dailyFrecuency, input)
                : CalculateDailyFrecuencyExecutions(dailyFrecuency, input);
        }

        public static Output CalculateOccursOnce(DailyFrecuency dailyFrecuency, Input input)
        {
            ValidateCalculateOccursOnce(dailyFrecuency);
            Output output = new Output();
            output.AddExecution(SetOccursOnceTimeToDate(input.CurrentDate,dailyFrecuency.OccursOnceAt.Value));
            return output;
        }

        public static Output CalculateDailyFrecuencyExecutions(DailyFrecuency dailyFrecuency, Input input)
        {
            ValidateCalculateDailyFrecuency(dailyFrecuency);
            Output output = new Output();          
            foreach (var EachDate in GetExecutions(input.CurrentDate, dailyFrecuency.StartingAt.Value, dailyFrecuency.EndsAt.Value, dailyFrecuency.Every, dailyFrecuency.Frecuency))
            {
                output.AddExecution(EachDate);
            }
            return output;
        }

        public static void ValidateCalculateDailyFrecuency(DailyFrecuency dailyFrecuency)
        {
            Ensure.That(dailyFrecuency.occursOnce).IsFalse();
            Ensure.That(dailyFrecuency.occursEvery).IsTrue();
            Ensure.That(dailyFrecuency.StartingAt, nameof(dailyFrecuency.StartingAt)).IsNotNull();
            Ensure.That(dailyFrecuency.EndsAt, nameof(dailyFrecuency.EndsAt)).IsNotNull();
        }

        public static void ValidateCalculateOccursOnce(DailyFrecuency dailyFrecuency)
        {
            Ensure.That(dailyFrecuency.OccursOnceAt, nameof(dailyFrecuency.OccursOnceAt)).IsNotNull();
        }

        public static DateTime AddTime(DateTime executionDate, DailyRecurrence every, int occursEvery)
        {
            switch (every)
            {
                case DailyRecurrence.Seconds: return executionDate.AddSeconds(occursEvery);
                case DailyRecurrence.Minutes: return executionDate.AddMinutes(occursEvery);
                default: return executionDate.AddHours(occursEvery);                                
            }
        }

        public static DateTime SetOccursOnceTimeToDate(DateTime executionDate, TimeOnly occursOnce)
        {
            return new DateTime(executionDate.Year, executionDate.Month, executionDate.Day, occursOnce.Hour, occursOnce.Minute, occursOnce.Second);
        }

        public static IList<DateTime> GetExecutions(DateTime executionDate, TimeOnly startingAt, TimeOnly endsAt, DailyRecurrence every, int occursEvery)
        {
            DateTime addingTime = new DateTime(executionDate.Year, executionDate.Month, executionDate.Day, startingAt.Hour, startingAt.Minute, startingAt.Second);
            DateTime ends = new DateTime(executionDate.Year, executionDate.Month, executionDate.Day, endsAt.Hour, endsAt.Minute, endsAt.Second);
            IList<DateTime> executions = new List<DateTime>();
            while (addingTime <= ends)
            {                        
                executions.Add(addingTime);
                addingTime = AddTime(addingTime, every, occursEvery);
            }
            return executions;
        }
    }
}
