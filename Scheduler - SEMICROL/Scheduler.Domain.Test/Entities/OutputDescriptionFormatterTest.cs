using FluentAssertions;
using Semicrol.Scheduler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;
using static Semicrol.Scheduler.Domain.Common.SchedulerEnums;

namespace Semicrol.Scheduler.Domain.Test.Entities
{
    public class OutputDescriptionFormatterTest
    {
        [Fact]
        public void get_days_of_week_string_should_return_string_join_separated_by_comas_and_last_of_checked_days()
        {
            ConfigDaysOfWeek days = new ConfigDaysOfWeek();
            days.CheckMonday(true);
            days.CheckTuesday(true);
            days.CheckWednesday(true);
            days.CheckThursday(true);
            days.CheckFriday(true);
            days.CheckSaturday(true);
            days.CheckSunday(true);

            var result = OutputDescriptionFormatter.GetDaysOfWeekString(days.Days);
            
            result.Should().BeEquivalentTo("Monday,Tuesday,Wednesday,Thursday,Friday,Saturday and Sunday");
        }

        [Fact]
        public void get_days_of_week_string_should_return_string_day_if_only_one_checked()
        {
            ConfigDaysOfWeek days = new ConfigDaysOfWeek();
            days.CheckMonday(true);

            var result = OutputDescriptionFormatter.GetDaysOfWeekString(days.Days);
                
            result.Should().BeEquivalentTo("Monday");
        }

        [Fact]
        public void get_days_of_week_string_should_return_string_day_and_last_if_2_days_checked()
        {
            ConfigDaysOfWeek days = new ConfigDaysOfWeek();
            days.CheckMonday(true);
            days.CheckFriday(true);
            var result = OutputDescriptionFormatter.GetDaysOfWeekString(days.Days);
            
            result.Should().BeEquivalentTo("Monday and Friday");
        }

        [Fact]
        public void get_weekly_configuration_description_should_return_formated_string()
        {
            ConfigDaysOfWeek days = new ConfigDaysOfWeek();
            days.CheckMonday(true);
            days.CheckThursday(true);
            days.CheckFriday(true);
            int every = 2;

            var result = OutputDescriptionFormatter.GetWeeklyConfigurationDescription(every, days.Days);
            
            result.Should().Be("Occurs every 2 weeks on Monday,Thursday and Friday");
        }

        [Fact]
        public void get_daily_recurrence_description_should_return_formated_string()
        {
            TimeOnly start = new TimeOnly(04,00,00);
            TimeOnly end = new TimeOnly(08, 00, 00);
            DailyRecurrence recurrence = DailyRecurrence.Hours;
            int every = 2;

            var result = OutputDescriptionFormatter.GetDailyRecurrenceDescription(every, recurrence, start, end);
            
            result.Should().Be(" every 2 Hours between 4:00:00 and 8:00:00");
        }

        [Fact]
        public void description_should_return_concat_of_weekly_and_daily_string_if_recurring()
        {
            Input input = new Input(new DateTime(2020, 01, 01));
            Configuration config = new Configuration(true, null, ConfigurationType.Recurring, RecurringType.Weekly);
            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, 2, DailyRecurrence.Hours, new TimeOnly(04, 00, 00), new TimeOnly(08, 00, 00));
            WeeklyConfiguration weeklyConfiguration = new WeeklyConfiguration(2);
            weeklyConfiguration.DaysOfWeek.CheckMonday(true);
            weeklyConfiguration.DaysOfWeek.CheckWednesday(true);
            weeklyConfiguration.DaysOfWeek.CheckSaturday(true);

            var result = OutputDescriptionFormatter.Description(input, config, dailyFrecuency, weeklyConfiguration);
            
            result.Should().Be("Occurs every 2 weeks on Monday,Wednesday and Saturday every 2 Hours between 4:00:00 and 8:00:00");
        }

        [Fact]
        public void description_should_return_once_if_recurring_type_once()
        {
            Input input = new Input(new DateTime(2020, 01, 01));
            Configuration config = new Configuration(true, new DateTime(2021,01,01), ConfigurationType.Once, RecurringType.Weekly);
            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, 2, DailyRecurrence.Hours, new TimeOnly(04, 00, 00), new TimeOnly(08, 00, 00));
            WeeklyConfiguration weeklyConfiguration = new WeeklyConfiguration(2);
            weeklyConfiguration.DaysOfWeek.CheckMonday(true);
            weeklyConfiguration.DaysOfWeek.CheckWednesday(true);
            weeklyConfiguration.DaysOfWeek.CheckSaturday(true);

            var result = OutputDescriptionFormatter.Description(input, config, dailyFrecuency, weeklyConfiguration);
            
            result.Should().Be("Occurs only once at 01/01/2021");
        }
    }
}
