﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Semicrol.Scheduler.Application.UseCases;
using Semicrol.Scheduler.Domain.Entities;
using Xunit;
using static Semicrol.Scheduler.Domain.Common.SchedulerEnums;

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

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(10)]
        public void get_next_recurrence_start_date_should_add_every_multiplicated_by_7_days(int every)
        {
            DateTime startDate = new DateTime(2021, 01, 01);
            DateTime expected = startDate.AddDays(every * 7);
            WeeklyRecurrenceCalculator.AddEveryBy7DaysToCurrentDate(startDate, every).Should().Be(expected);
        }


        [Fact]
        public void add_days_to_recurrence_list_should_return_empty_list_if_no_days_checked()
        {
            DateTime currentdate = new DateTime(2021, 01, 01);
            WeeklyConfiguration config = new WeeklyConfiguration(2);

            IList<DateTime> expected = new List<DateTime>();

            WeeklyRecurrenceCalculator.AddDaysToRecurrenceList(currentdate, config.DaysOfWeek.Days).Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void get_next_recurrence_should_return_empty_list_if_no_days_checked()
        {
            DateTime currentdate = new DateTime(2021, 01, 01);
            WeeklyConfiguration config = new WeeklyConfiguration(2);

            IList<DateTime> expected = new List<DateTime>();

            WeeklyRecurrenceCalculator.GetNextRecurrence(currentdate, config).Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void get_next_recurrence_should_return_next_recurrence_of_days_checked_from_current_date_monday()
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

            WeeklyRecurrenceCalculator.GetNextRecurrence(currentdate, config).Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void get_next_recurrence_should_return_next_recurrence_of_days_checked_from_current_date_friday()
        {
            DateTime currentdate = new DateTime(2020, 12, 28);
            WeeklyConfiguration config = new WeeklyConfiguration(2);
            config.DaysOfWeek.CheckMonday(true);
            config.DaysOfWeek.CheckTuesday(true);
            config.DaysOfWeek.CheckWednesday(true);
            config.DaysOfWeek.CheckThursday(true);
            config.DaysOfWeek.CheckFriday(true);
            config.DaysOfWeek.CheckSaturday(true);
            config.DaysOfWeek.CheckSunday(true);

            IList<DateTime> expected = new List<DateTime>();
            expected.Add(new DateTime(2020, 12, 28));
            expected.Add(new DateTime(2020, 12, 29));
            expected.Add(new DateTime(2020, 12, 30));
            expected.Add(new DateTime(2020, 12, 31));
            expected.Add(new DateTime(2021, 01, 01));
            expected.Add(new DateTime(2021, 01, 02));
            expected.Add(new DateTime(2021, 01, 03));

            WeeklyRecurrenceCalculator.GetNextRecurrence(currentdate, config).Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void all_recurrences_should_return_empty_list_check_adding_weeks_to_last_date_is_later_than_limit_date()
        {
            DateTime currentdate = new DateTime(2021, 01, 01);
            WeeklyConfiguration config = new WeeklyConfiguration(2);
            DateTime limit = new DateTime(2021, 01, 10);
            IList<DateTime> expected = new List<DateTime>();

            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;

            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, occursEvery, every, starting, end);

            WeeklyRecurrenceCalculator.GetAllRecurrences(currentdate, config, dailyFrecuency, limit).Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void all_recurrences_should_return_list_with_recurrence_of_days_checked_every_week_before_limit()
        {
            DateTime currentdate = new DateTime(2021, 01, 01);
            WeeklyConfiguration config = new WeeklyConfiguration(2);
            DateTime limit = new DateTime(2021, 01, 15);

            config.DaysOfWeek.CheckMonday(true);
            config.DaysOfWeek.CheckTuesday(true);
            config.DaysOfWeek.CheckWednesday(true);
            config.DaysOfWeek.CheckThursday(true);
            config.DaysOfWeek.CheckFriday(true);
            config.DaysOfWeek.CheckSaturday(true);
            config.DaysOfWeek.CheckSunday(true);

            IList<DateTime> expected = new List<DateTime>();
            expected.Add(new DateTime(2021, 01, 01,01,30,00));
            expected.Add(new DateTime(2021, 01, 01, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 01, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 01, 07, 30, 00));
            expected.Add(new DateTime(2021, 01, 02, 01, 30, 00));

            expected.Add(new DateTime(2021, 01, 02, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 02, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 02, 07, 30, 00));
            expected.Add(new DateTime(2021, 01, 03, 01, 30, 00));

            expected.Add(new DateTime(2021, 01, 03, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 03, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 03, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 11, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 11, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 11, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 11, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 12, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 12, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 12, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 12, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 13, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 13, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 13, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 13, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 14, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 14, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 14, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 14, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 15, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 15, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 15, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 15, 07, 30, 00));

            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;

            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, occursEvery, every, starting, end);

            WeeklyRecurrenceCalculator.GetAllRecurrences(currentdate, config, dailyFrecuency, limit).Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void all_recurrences_should_return_list_with_recurrence_of_days_checked_every_week_before_limit_if_checked_from_friday()
        {
            DateTime currentdate = new DateTime(2021, 01, 01);
            WeeklyConfiguration config = new WeeklyConfiguration(2);
            DateTime limit = new DateTime(2021, 01, 15);
            config.DaysOfWeek.CheckFriday(true);
            config.DaysOfWeek.CheckSaturday(true);
            config.DaysOfWeek.CheckSunday(true);

            IList<DateTime> expected = new List<DateTime>();
            expected.Add(new DateTime(2021, 01, 01, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 01, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 01, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 01, 07, 30, 00));
            expected.Add(new DateTime(2021, 01, 02, 01, 30, 00));

            expected.Add(new DateTime(2021, 01, 02, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 02, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 02, 07, 30, 00));
            expected.Add(new DateTime(2021, 01, 03, 01, 30, 00));

            expected.Add(new DateTime(2021, 01, 03, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 03, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 03, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 15, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 15, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 15, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 15, 07, 30, 00));

            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;

            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, occursEvery, every, starting, end);

            WeeklyRecurrenceCalculator.GetAllRecurrences(currentdate, config, dailyFrecuency, limit).Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void all_recurrences_should_return_list_with_recurrence_of_days_checked_every_week_before_limit_if_checked_from_friday_with_limit_between_every()
        {
            DateTime currentdate = new DateTime(2021, 01, 01);
            WeeklyConfiguration config = new WeeklyConfiguration(2);
            DateTime limit = new DateTime(2021, 01, 22);

            config.DaysOfWeek.CheckFriday(true);
            config.DaysOfWeek.CheckSaturday(true);
            config.DaysOfWeek.CheckSunday(true);

            IList<DateTime> expected = new List<DateTime>();
            expected.Add(new DateTime(2021, 01, 01, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 01, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 01, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 01, 07, 30, 00));
            expected.Add(new DateTime(2021, 01, 02, 01, 30, 00));

            expected.Add(new DateTime(2021, 01, 02, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 02, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 02, 07, 30, 00));
            expected.Add(new DateTime(2021, 01, 03, 01, 30, 00));

            expected.Add(new DateTime(2021, 01, 03, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 03, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 03, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 15, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 15, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 15, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 15, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 16, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 16, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 16, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 16, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 17, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 17, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 17, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 17, 07, 30, 00));

            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;

            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, occursEvery, every, starting, end);

            WeeklyRecurrenceCalculator.GetAllRecurrences(currentdate, config, dailyFrecuency, limit).Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void all_recurrences_should_return_list_with_recurrence_of_days_checked_every_week_before_limit_if_checked_from_friday_with_limit_in_same_week()
        {
            DateTime currentdate = new DateTime(2021, 01, 01);
            WeeklyConfiguration config = new WeeklyConfiguration(2);
            DateTime limit = new DateTime(2021, 01,02);

            config.DaysOfWeek.CheckMonday(true);
            config.DaysOfWeek.CheckTuesday(true);
            config.DaysOfWeek.CheckWednesday(true);
            config.DaysOfWeek.CheckThursday(true);
            config.DaysOfWeek.CheckFriday(true);
            config.DaysOfWeek.CheckSaturday(true);
            config.DaysOfWeek.CheckSunday(true);

            IList<DateTime> expected = new List<DateTime>();
            expected.Add(new DateTime(2021, 01, 01, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 01, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 01, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 01, 07, 30, 00));
            
            expected.Add(new DateTime(2021, 01, 02, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 02, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 02, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 02, 07, 30, 00));
         
            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;

            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, occursEvery, every, starting, end);

            WeeklyRecurrenceCalculator.GetAllRecurrences(currentdate, config, dailyFrecuency, limit).Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void all_recurrences_should_return_list_with_recurrence_of_days_checked_every_week_before_limit_if_checked_from_monday_with_limit_in_same_week()
        {
            DateTime currentdate = new DateTime(2020, 12, 28);
            WeeklyConfiguration config = new WeeklyConfiguration(2);
            DateTime limit = new DateTime(2021, 01, 02);

            config.DaysOfWeek.CheckMonday(true);
            config.DaysOfWeek.CheckTuesday(true);
            config.DaysOfWeek.CheckWednesday(true);
            config.DaysOfWeek.CheckThursday(true);
            config.DaysOfWeek.CheckFriday(true);
            config.DaysOfWeek.CheckSaturday(true);
            config.DaysOfWeek.CheckSunday(true);

            IList<DateTime> expected = new List<DateTime>();
            expected.Add(new DateTime(2020, 12, 28, 01, 30, 00));
            expected.Add(new DateTime(2020, 12, 28, 03, 30, 00));
            expected.Add(new DateTime(2020, 12, 28, 05, 30, 00));
            expected.Add(new DateTime(2020, 12, 28, 07, 30, 00));

            expected.Add(new DateTime(2020, 12, 29, 01, 30, 00));
            expected.Add(new DateTime(2020, 12, 29, 03, 30, 00));
            expected.Add(new DateTime(2020, 12, 29, 05, 30, 00));
            expected.Add(new DateTime(2020, 12, 29, 07, 30, 00));

            expected.Add(new DateTime(2020, 12, 30, 01, 30, 00));
            expected.Add(new DateTime(2020, 12, 30, 03, 30, 00));
            expected.Add(new DateTime(2020, 12, 30, 05, 30, 00));
            expected.Add(new DateTime(2020, 12, 30, 07, 30, 00));

            expected.Add(new DateTime(2020, 12, 31, 01, 30, 00));
            expected.Add(new DateTime(2020, 12, 31, 03, 30, 00));
            expected.Add(new DateTime(2020, 12, 31, 05, 30, 00));
            expected.Add(new DateTime(2020, 12, 31, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 01, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 01, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 01, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 01, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 02, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 02, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 02, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 02, 07, 30, 00));

            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;

            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, occursEvery, every, starting, end);

            WeeklyRecurrenceCalculator.GetAllRecurrences(currentdate, config, dailyFrecuency, limit).Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void all_recurrences_should_return_list_with_recurrence_of_days_checked_every_week_before_limit_every_week_recurrence()
        {
            DateTime currentdate = new DateTime(2021, 01, 01);
            WeeklyConfiguration config = new WeeklyConfiguration(1);
            DateTime limit = new DateTime(2021, 01, 15);

            config.DaysOfWeek.CheckMonday(true);
            config.DaysOfWeek.CheckTuesday(true);
            config.DaysOfWeek.CheckWednesday(true);
            config.DaysOfWeek.CheckThursday(true);
            config.DaysOfWeek.CheckFriday(true);
            config.DaysOfWeek.CheckSaturday(true);
            config.DaysOfWeek.CheckSunday(true);

            IList<DateTime> expected = new List<DateTime>();
            expected.Add(new DateTime(2021, 01, 01, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 01, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 01, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 01, 07, 30, 00));
            
            expected.Add(new DateTime(2021, 01, 02, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 02, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 02, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 02, 07, 30, 00));
            
            expected.Add(new DateTime(2021, 01, 03, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 03, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 03, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 03, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 04, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 04, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 04, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 04, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 05, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 05, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 05, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 05, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 06, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 06, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 06, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 06, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 07, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 07, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 07, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 07, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 08, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 08, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 08, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 08, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 09, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 09, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 09, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 09, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 10, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 10, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 10, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 10, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 11, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 11, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 11, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 11, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 12, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 12, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 12, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 12, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 13, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 13, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 13, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 13, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 14, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 14, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 14, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 14, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 15, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 15, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 15, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 15, 07, 30, 00));

            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;

            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, occursEvery, every, starting, end);

            WeeklyRecurrenceCalculator.GetAllRecurrences(currentdate, config, dailyFrecuency, limit).Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void all_recurrences_should_return_list_with_recurrence_of_days_checked_alternated_before_limit_every_week_recurrence()
        {
            DateTime currentdate = new DateTime(2021, 01, 01);
            WeeklyConfiguration config = new WeeklyConfiguration(1);
            DateTime limit = new DateTime(2021, 02, 04);

            config.DaysOfWeek.CheckMonday(true);
            config.DaysOfWeek.CheckWednesday(true);

            IList<DateTime> expected = new List<DateTime>();

            expected.Add(new DateTime(2021, 01, 04, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 04, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 04, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 04, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 06, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 06, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 06, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 06, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 11, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 11, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 11, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 11, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 13, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 13, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 13, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 13, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 18, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 18, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 18, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 18, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 20, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 20, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 20, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 20, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 25, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 25, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 25, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 25, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 27, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 27, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 27, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 27, 07, 30, 00));

            expected.Add(new DateTime(2021, 02, 01, 01, 30, 00));
            expected.Add(new DateTime(2021, 02, 01, 03, 30, 00));
            expected.Add(new DateTime(2021, 02, 01, 05, 30, 00));
            expected.Add(new DateTime(2021, 02, 01, 07, 30, 00));

            expected.Add(new DateTime(2021, 02, 03, 01, 30, 00));
            expected.Add(new DateTime(2021, 02, 03, 03, 30, 00));
            expected.Add(new DateTime(2021, 02, 03, 05, 30, 00));
            expected.Add(new DateTime(2021, 02, 03, 07, 30, 00));


            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;

            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, occursEvery, every, starting, end);

            WeeklyRecurrenceCalculator.GetAllRecurrences(currentdate, config, dailyFrecuency, limit).Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void all_recurrences_should_return_list_with_recurrence_of_days_only_monday_checked_before_limit_every_week_recurrence()
        {
            DateTime currentdate = new DateTime(2021, 01, 01);
            WeeklyConfiguration config = new WeeklyConfiguration(1);
            DateTime limit = new DateTime(2021, 02, 04);

            config.DaysOfWeek.CheckMonday(true);

            IList<DateTime> expected = new List<DateTime>();


            expected.Add(new DateTime(2021, 01, 04, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 04, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 04, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 04, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 11, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 11, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 11, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 11, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 18, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 18, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 18, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 18, 07, 30, 00));

            expected.Add(new DateTime(2021, 01, 25, 01, 30, 00));
            expected.Add(new DateTime(2021, 01, 25, 03, 30, 00));
            expected.Add(new DateTime(2021, 01, 25, 05, 30, 00));
            expected.Add(new DateTime(2021, 01, 25, 07, 30, 00));

            expected.Add(new DateTime(2021, 02, 01, 01, 30, 00));
            expected.Add(new DateTime(2021, 02, 01, 03, 30, 00));
            expected.Add(new DateTime(2021, 02, 01, 05, 30, 00));
            expected.Add(new DateTime(2021, 02, 01, 07, 30, 00));


            TimeOnly starting = new TimeOnly(01, 30, 00);
            TimeOnly end = new TimeOnly(08, 30, 00);
            int occursEvery = 2;
            DailyRecurrence every = DailyRecurrence.Hours;

            DailyFrecuency dailyFrecuency = new DailyFrecuency(false, true, null, occursEvery, every, starting, end);

            WeeklyRecurrenceCalculator.GetAllRecurrences(currentdate, config, dailyFrecuency, limit).Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void get_day_of_week_consider_sunday_last_should_return_7_if_sunday_instead_of_0()
        {
            WeeklyRecurrenceCalculator.GetDayOfWeekIntConsideringSundayLast(DayOfWeek.Sunday).Should().Be(7);
        }

        [Fact]
        public void sublist_of_days_of_week_from_date_should_return_sublist_of_later_days_of_parameter_current_date_friday()
        {
            DateTime currenDate = new DateTime(2021, 01, 01);
            WeeklyConfiguration config = new WeeklyConfiguration(2);
            IList<ConfigDay> expected = new List<ConfigDay>();
            expected.Add(new ConfigDay(DayOfWeek.Friday, false));
            expected.Add(new ConfigDay(DayOfWeek.Saturday, false));
            expected.Add(new ConfigDay(DayOfWeek.Sunday, false));


            WeeklyRecurrenceCalculator.SublistDaysOfWeekFromDate(currenDate, config.DaysOfWeek.Days).Should().BeEquivalentTo(expected);
        }


        [Fact]
        public void sublist_of_days_of_week_from_date_should_return_sublist_of_later_days_of_parameter_current_date_tuesday()
        {
            DateTime currenDate = new DateTime(2020, 12, 29);
            WeeklyConfiguration config = new WeeklyConfiguration(2);
            IList<ConfigDay> expected = new List<ConfigDay>();
            expected.Add(new ConfigDay(DayOfWeek.Tuesday, false));
            expected.Add(new ConfigDay(DayOfWeek.Wednesday, false));
            expected.Add(new ConfigDay(DayOfWeek.Thursday, false));
            expected.Add(new ConfigDay(DayOfWeek.Friday, false));
            expected.Add(new ConfigDay(DayOfWeek.Saturday, false));
            expected.Add(new ConfigDay(DayOfWeek.Sunday, false));


            WeeklyRecurrenceCalculator.SublistDaysOfWeekFromDate(currenDate, config.DaysOfWeek.Days).Should().BeEquivalentTo(expected);
        }


        [Fact]
        public void check_date_is_before_limit_date_should_return_true_if_current_date_is_before_limit()
        {
            DateTime currentDate = new DateTime(2021, 01, 01);
            DateTime limit = new DateTime(2021, 01, 02);
            WeeklyRecurrenceCalculator.CheckDateIsBeforeLimitDate(currentDate, limit).Should().BeTrue();
        }

        [Fact]
        public void check_date_is_before_limit_date_should_return_false_if_current_date_is_after_limit()
        {
            DateTime currentDate = new DateTime(2021, 01, 02);
            DateTime limit = new DateTime(2021, 01, 01);
            WeeklyRecurrenceCalculator.CheckDateIsBeforeLimitDate(currentDate, limit).Should().BeFalse();

        }

        [Fact]
        public void check_date_is_before_limit_date_should_return_false_if_current_date_is_equal_limit()
        {
            DateTime currentDate = new DateTime(2021, 01, 01);
            DateTime limit = new DateTime(2021, 01, 01);
            WeeklyRecurrenceCalculator.CheckDateIsBeforeLimitDate(currentDate, limit).Should().BeTrue();

        }

        [Fact]
        public void get_recurrence_next_start_date_should_add_every_by_7_days_to_current_monday_week_day_starting_on_friday()
        {
            DateTime currentDate = new DateTime(2021, 01, 01);
            int every = 2;
            DateTime expected = new DateTime(2021, 01, 11);
            WeeklyRecurrenceCalculator.GetRecurrenceNextStartDate(currentDate, every).Should().Be(expected);

        }

        [Fact]
        public void get_recurrence_next_start_date_should_add_every_by_7_days_to_current_monday_week_day_starting_on_monday()
        {
            DateTime currentDate = new DateTime(2020, 12, 28);
            int every = 3;
            DateTime expected = new DateTime(2021, 01, 18);
            WeeklyRecurrenceCalculator.GetRecurrenceNextStartDate(currentDate, every).Should().Be(expected);

        }

        [Fact]
        public void get_current_week_monday_should_be_monday_date_of_current_week_if_current_day_is_monday()
        {
            DateTime currentDate = new DateTime(2020, 12, 28);
            DateTime expected = new DateTime(2020, 12, 28);
            WeeklyRecurrenceCalculator.GetCurrentWeekMonday(currentDate).Should().Be(expected);
        }

        [Fact]
        public void get_current_week_monday_should_be_monday_date_of_current_week_if_current_day_is_friday()
        {
            DateTime currentDate = new DateTime(2021, 01, 01);
            DateTime expected = new DateTime(2020, 12, 28);
            WeeklyRecurrenceCalculator.GetCurrentWeekMonday(currentDate).Should().Be(expected);
        }

    }
}