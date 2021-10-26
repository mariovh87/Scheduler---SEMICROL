using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Semicrol.Scheduler.Application.UseCases;
using Semicrol.Scheduler.Domain.Entities;
using Xunit;

namespace Semicrol.Scheduler.Application.Test.UseCases
{
    public class WeeklyRecurrenceCalculatorTest
    {
        [Fact]
        public void get_next_week_day_should_return_next_monday_when_day_of_week_monday()
        {
            DateTime currentDate = new DateTime(2021, 01, 01);
            DateTime expected = new DateTime(2021, 01, 04);
            WeeklyRecurrenceCalculator.GetNextWeekday(currentDate, DayOfWeek.Monday).Should().Be(expected);
        }

        [Fact]
        public void get_next_week_day_should_return_next_tuesday_when_day_of_week_tuesday()
        {
            DateTime currentDate = new DateTime(2021, 01, 01);
            DateTime expected = new DateTime(2021, 01, 05);
            WeeklyRecurrenceCalculator.GetNextWeekday(currentDate, DayOfWeek.Tuesday).Should().Be(expected);
        }

        [Fact]
        public void get_next_week_day_should_return_next_wednesday_when_day_of_week_wednesday()
        {
            DateTime currentDate = new DateTime(2021, 01, 01);
            DateTime expected = new DateTime(2021, 01, 06);
            WeeklyRecurrenceCalculator.GetNextWeekday(currentDate, DayOfWeek.Wednesday).Should().Be(expected);
        }
        [Fact]
        public void get_next_week_day_should_return_next_thursday_when_day_of_week_thursday()
        {
            DateTime currentDate = new DateTime(2021, 01, 01);
            DateTime expected = new DateTime(2021, 01, 07);
            WeeklyRecurrenceCalculator.GetNextWeekday(currentDate, DayOfWeek.Thursday).Should().Be(expected);
        }
        [Fact]
        public void get_next_week_day_should_return_next_friday_when_day_of_week_friday()
        {
            DateTime currentDate = new DateTime(2021, 01, 01);
            DateTime expected = new DateTime(2021, 01, 01);
            WeeklyRecurrenceCalculator.GetNextWeekday(currentDate, DayOfWeek.Friday).Should().Be(expected);
        }
        [Fact]
        public void get_next_week_day_should_return_next_saturday_when_day_of_week_saturday()
        {
            DateTime currentDate = new DateTime(2021, 01, 01);
            DateTime expected = new DateTime(2021, 01, 02);
            WeeklyRecurrenceCalculator.GetNextWeekday(currentDate, DayOfWeek.Saturday).Should().Be(expected);
        }
        [Fact]
        public void get_next_week_day_should_return_next_sunday_when_day_of_week_sunday()
        {
            DateTime currentDate = new DateTime(2021, 01, 01);
            DateTime expected = new DateTime(2021, 01, 03);
            WeeklyRecurrenceCalculator.GetNextWeekday(currentDate, DayOfWeek.Sunday).Should().Be(expected);
        }

        [Fact]
        public void check_adding_weeks_is_before_limit_date_should_return_true_if_adding_every_weeks_to_start_date_is_before_limit_date()
        {
            DateTime currentDate = new DateTime(2021, 01, 01);
            DateTime limit = new DateTime(2021, 01, 20);
            int every = 2;
            WeeklyRecurrenceCalculator.CheckAddingWeeksIsBeforeLimitDate(currentDate, every, limit).Should().BeTrue();
        }

        [Fact]
        public void check_adding_weeks_is_before_limit_date_should_return_false_if_adding_every_weeks_to_start_date_is_after_limit_date()
        {
            DateTime currentDate = new DateTime(2021, 01, 01);
            DateTime limit = new DateTime(2021, 01, 12);
            int every = 2;
            WeeklyRecurrenceCalculator.CheckAddingWeeksIsBeforeLimitDate(currentDate, every, limit).Should().BeFalse();
        }

        [Fact]
        public void check_adding_weeks_is_before_limit_date_should_return_false_if_adding_days_to_last_date_of_list_is_after_limit_date()
        {
            DateTime currentDate = new DateTime(2021, 01, 01);
            DateTime limit = new DateTime(2021, 01, 20);
            int every = 2;
            IList<DateTime> recurrenceDays = new List<DateTime>();
            recurrenceDays.Add(new DateTime(2021, 01, 01));
            recurrenceDays.Add(new DateTime(2021, 01, 02));
            recurrenceDays.Add(new DateTime(2021, 01, 03));
            recurrenceDays.Add(new DateTime(2021, 01, 04));
            recurrenceDays.Add(new DateTime(2021, 01, 05));
            recurrenceDays.Add(new DateTime(2021, 01, 06));
            recurrenceDays.Add(new DateTime(2021, 01, 07));
            recurrenceDays.Add(new DateTime(2021, 01, 08));
            recurrenceDays.Add(new DateTime(2021, 01, 09));
            recurrenceDays.Add(new DateTime(2021, 01, 10));
            recurrenceDays.Add(new DateTime(2021, 01, 11));
            recurrenceDays.Add(new DateTime(2021, 01, 12));
            recurrenceDays.Add(new DateTime(2021, 01, 13));
            recurrenceDays.Add(new DateTime(2021, 01, 14));
            WeeklyRecurrenceCalculator.CheckAddingWeeksIsBeforeLimitDate(recurrenceDays, every, limit).Should().BeFalse();
        }

        [Fact]
        public void check_adding_weeks_is_before_limit_date_should_return_true_if_adding_days_to_last_date_of_list_is_equals_limit_date()
        {
            DateTime limit = new DateTime(2021, 01, 28);
            int every = 2;
            IList<DateTime> recurrenceDays = new List<DateTime>();
            recurrenceDays.Add(new DateTime(2021, 01, 01));
            recurrenceDays.Add(new DateTime(2021, 01, 02));
            recurrenceDays.Add(new DateTime(2021, 01, 03));
            recurrenceDays.Add(new DateTime(2021, 01, 04));
            recurrenceDays.Add(new DateTime(2021, 01, 05));
            recurrenceDays.Add(new DateTime(2021, 01, 06));
            recurrenceDays.Add(new DateTime(2021, 01, 07));
            recurrenceDays.Add(new DateTime(2021, 01, 08));
            recurrenceDays.Add(new DateTime(2021, 01, 09));
            recurrenceDays.Add(new DateTime(2021, 01, 10));
            recurrenceDays.Add(new DateTime(2021, 01, 11));
            recurrenceDays.Add(new DateTime(2021, 01, 12));
            recurrenceDays.Add(new DateTime(2021, 01, 13));
            recurrenceDays.Add(new DateTime(2021, 01, 14));


            WeeklyRecurrenceCalculator.CheckAddingWeeksIsBeforeLimitDate(recurrenceDays, every, limit).Should().BeTrue();
        }

        [Fact]
        public void check_adding_weeks_is_before_limit_date_should_return_true_if_adding_days_to_last_date_of_list_is_after_limit_date()
        {
            DateTime limit = new DateTime(2021, 01, 30);
            int every = 2;
            IList<DateTime> recurrenceDays = new List<DateTime>();
            recurrenceDays.Add(new DateTime(2021, 01, 01));
            recurrenceDays.Add(new DateTime(2021, 01, 02));
            recurrenceDays.Add(new DateTime(2021, 01, 03));
            recurrenceDays.Add(new DateTime(2021, 01, 04));
            recurrenceDays.Add(new DateTime(2021, 01, 05));
            recurrenceDays.Add(new DateTime(2021, 01, 06));
            recurrenceDays.Add(new DateTime(2021, 01, 07));
            recurrenceDays.Add(new DateTime(2021, 01, 08));
            recurrenceDays.Add(new DateTime(2021, 01, 09));
            recurrenceDays.Add(new DateTime(2021, 01, 10));
            recurrenceDays.Add(new DateTime(2021, 01, 11));
            recurrenceDays.Add(new DateTime(2021, 01, 12));
            recurrenceDays.Add(new DateTime(2021, 01, 13));
            recurrenceDays.Add(new DateTime(2021, 01, 14));


            WeeklyRecurrenceCalculator.CheckAddingWeeksIsBeforeLimitDate(recurrenceDays, every, limit).Should().BeTrue();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(10)]
        public void get_next_recurrence_start_date_should_add_every_multiplicated_by_7_days(int every)
        {
            DateTime startDate = new DateTime(2021, 01, 01);
            DateTime expected = startDate.AddDays(every * 7);
            WeeklyRecurrenceCalculator.GetNextRecurrenceStartDate(startDate, every).Should().Be(expected);
        }

        [Fact]
        public void add_days_to_recurrence_list_should_return_list_with_next_day_of_week_if_checked()
        {
            DateTime currentdate = new DateTime(2021, 01, 01);
            WeeklyConfiguration config = new WeeklyConfiguration(2);
            config.DaysOfWeek.CheckMonday(true);
            config.DaysOfWeek.CheckWednesday(true);
            config.DaysOfWeek.CheckFriday(true);
            config.DaysOfWeek.CheckSunday(true);

            IList<DateTime> expected = new List<DateTime>();

            expected.Add(new DateTime(2021, 01, 01));
            expected.Add(new DateTime(2021,01,03));
            expected.Add(new DateTime(2021, 01, 04));
            expected.Add(new DateTime(2021, 01, 06));

            WeeklyRecurrenceCalculator.AddDaysToRecurrenceList(currentdate, config.DaysOfWeek.Days).Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void add_days_to_recurrence_list_should_return_all_days_if_all_checked()
        {
            DateTime currentdate = new DateTime(2021, 01, 01);
            WeeklyConfiguration config = new WeeklyConfiguration(2);
            config.DaysOfWeek.CheckMonday(true);
            config.DaysOfWeek.CheckTuesday(true);
            config.DaysOfWeek.CheckWednesday(true);
            config.DaysOfWeek.CheckThursday(true);
            config.DaysOfWeek.CheckFriday(true);
            config.DaysOfWeek.CheckSaturday(true);
            config.DaysOfWeek.CheckSunday(true);

            IList<DateTime> expected = new List<DateTime>();
            expected.Add(new DateTime(2021, 01, 01));
            expected.Add(new DateTime(2021, 01, 02));
            expected.Add(new DateTime(2021, 01, 03));
            expected.Add(new DateTime(2021, 01, 05));
            expected.Add(new DateTime(2021, 01, 04));
            expected.Add(new DateTime(2021, 01, 06));
            expected.Add(new DateTime(2021, 01, 07));

            WeeklyRecurrenceCalculator.AddDaysToRecurrenceList(currentdate, config.DaysOfWeek.Days).Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void add_days_to_recurrence_list_should_return_empty_list_if_no_days_checked()
        {
            DateTime currentdate = new DateTime(2021, 01, 01);
            WeeklyConfiguration config = new WeeklyConfiguration(2);

            IList<DateTime> expected = new List<DateTime>();

            WeeklyRecurrenceCalculator.AddDaysToRecurrenceList(currentdate, config.DaysOfWeek.Days).Should().BeEquivalentTo(expected);
        }
    }
}
