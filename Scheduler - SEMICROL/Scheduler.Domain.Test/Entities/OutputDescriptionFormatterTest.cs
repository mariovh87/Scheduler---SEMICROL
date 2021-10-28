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

            string expected = "Monday,Tuesday,Wednesday,Thursday,Friday,Saturday and Sunday";

            OutputDescriptionFormatter.GetDaysOfWeekString(days.Days).Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void get_days_of_week_string_should_return_string_day_if_only_one_checked()
        {
            ConfigDaysOfWeek days = new ConfigDaysOfWeek();
            days.CheckMonday(true);

            string expected = "Monday";

            OutputDescriptionFormatter.GetDaysOfWeekString(days.Days).Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void get_days_of_week_string_should_return_string_day_and_last_if_2_days_checked()
        {
            ConfigDaysOfWeek days = new ConfigDaysOfWeek();
            days.CheckMonday(true);
            days.CheckFriday(true);

            string expected = "Monday and Friday";

            OutputDescriptionFormatter.GetDaysOfWeekString(days.Days).Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void get_weekly_configuration_description_should_return_formated_string()
        {
            ConfigDaysOfWeek days = new ConfigDaysOfWeek();
            days.CheckMonday(true);
            days.CheckThursday(true);
            days.CheckFriday(true);
            int every = 2;

            string expected = "Occurs every 2 weeks Monday,Thursday and Friday";
            OutputDescriptionFormatter.GetWeeklyConfigurationDescription(every, days.Days).Should().Be(expected);
        }

        [Fact]
        public void get_daily_recurrence_description_should_return_formated_string()
        {
            TimeOnly start = new TimeOnly(04,00,00);
            TimeOnly end = new TimeOnly(08, 00, 00);
            DailyRecurrence recurrence = DailyRecurrence.Hours;
            int every = 2;

            string expected = " every 2 Hours between 4:00:00 and 8:00:00";
            OutputDescriptionFormatter.GetDailyRecurrenceDescription(every, recurrence, start, end).Should().Be(expected);
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
            string expected = "Occurs every 2 weeks on Monday,Wednesday and Saturday every 2 Hours between 4:00:00 and 8:00:00";
            OutputDescriptionFormatter.Description(input, config, dailyFrecuency, weeklyConfiguration).Should().Be(expected);
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
            string expected = "Occurs only once at 01/01/2021";
            OutputDescriptionFormatter.Description(input, config, dailyFrecuency, weeklyConfiguration).Should().Be(expected);
        }
    }
}
