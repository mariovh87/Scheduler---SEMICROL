using FluentAssertions;
using Semicrol.Scheduler.Application.UseCases;
using Semicrol.Scheduler.Domain.Entities;
using Semicrol.Scheduler.Domain.Entities.MonthlyConfiguration;
using Semicrol.Scheduler.Domain.Exceptions;
using System;
using System.Linq;
using Xunit;
using static Semicrol.Scheduler.Domain.Common.SchedulerEnums;

namespace Semicrol.Scheduler.Application.Test.UseCases
{
    public class MonthlyRecurrenceCalculatorTest
    {
        [Fact]
        public void CheckIfConfigDayIsAfterEqualsCurrentStartDate_Should_Throw_Exception_If_Date_Is_Not_Valid()
        {
            DateTime current = DateTime.MinValue;
            int every = 1;

            Action act = () => MonthlyRecurrenceCalculator.CheckIfConfigDayIsAfterEqualsCurrentStartDate(current, every);

            act.Should().Throw<DomainException>();
        }

        [Fact]
        public void CheckIfConfigDayIsAfterEqualsCurrentStartDate_Should_Throw_Exception_If_Every_Is_Not_Positive()
        {
            DateTime current = new DateTime(2021, 01, 01);
            int every = -1;

            Action act = () => MonthlyRecurrenceCalculator.CheckIfConfigDayIsAfterEqualsCurrentStartDate(current, every);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void CheckIfConfigDayIsAfterEqualsCurrentStartDate_Should_Throw_Exception_If_Every_Is_Greather_Than_31()
        {
            DateTime current = new DateTime(2021, 01, 01);
            int every = 32;

            Action act = () => MonthlyRecurrenceCalculator.CheckIfConfigDayIsAfterEqualsCurrentStartDate(current, every);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void CheckIfConfigDayIsAfterEqualsCurrentStartDate_Should_Not_Throw_Exception_If_Every_Is_Day_Range()
        {
            DateTime current = new(2021, 01, 01);
            int every = 31;

            Action act = () => MonthlyRecurrenceCalculator.CheckIfConfigDayIsAfterEqualsCurrentStartDate(current, every);

            act.Should().NotThrow();
        }

        [Fact]
        public void CheckIfConfigDayIsAfterEqualsCurrentStartDate_Should_Be_True_If_Every_Is_Gte_Current_Day()
        {
            DateTime current = new(2021, 01, 10);
            int every = 31;

            Action act = () => MonthlyRecurrenceCalculator.CheckIfConfigDayIsAfterEqualsCurrentStartDate(current, every);

            act.Should().NotThrow();
        }


        [Fact]
        public void CheckIfMonthHaveDay_Should_Return_True_If_Last_Month_Day_Is_Bigger_Than_Every_Day()
        {
            DateTime current = new(2021, 01, 10);
            int every = 31;

            var result = MonthlyRecurrenceCalculator.CheckIfMonthHaveDay(current, every);

            result.Should().BeTrue();
        }
        [Fact]
        public void CheckIfMonthHaveDay_Should_Return_False_If_Last_Month_Day_Is_Lesser_Than_Every_Day()
        {
            DateTime current = new(2021, 04, 10);
            int every = 31;

            var result = MonthlyRecurrenceCalculator.CheckIfMonthHaveDay(current, every);

            result.Should().BeFalse();
        }

        [Fact]
        public void CheckIfMonthHaveDay_Should_Return_False_If_Month_February_And_Day_30()
        {
            DateTime current = new(2021, 02, 10);
            int every = 30;

            var result = MonthlyRecurrenceCalculator.CheckIfMonthHaveDay(current, every);

            result.Should().BeFalse();
        }

        [Fact]
        public void GetFirstDay_Should_Return_Next_Date_With_Months_Added_If_Day_Is_Before_Current_Day()
        {
            DateTime current = new(2021, 01, 30);
            int every = 20;
            int everyMonths = 2;
            DateTime limitDate = new(2022, 01, 01);

            var result = MonthlyRecurrenceCalculator.GetFirstDate(current, every, everyMonths, limitDate);

            result.Should().BeSameDateAs(new DateTime(2021,03,20));
        }

        [Fact]
        public void GetFirstDay_Should_Return_Max_Value_If_Not_Exist_Date_With_Added_Months()
        {
            DateTime current = new(2021, 01, 30);
            int every = 20;
            int everyMonths = 5;
            DateTime limitDate = new(2021, 04, 01);

            var result = MonthlyRecurrenceCalculator.GetFirstDate(current, every, everyMonths, limitDate);

            result.Should().BeSameDateAs(DateTime.MaxValue);
        }

        [Fact]
        public void GetFirstDay_Should_Return_Add_Months_To_Current_Date_With_Last_Day_Month()
        {
            DateTime current = new(2021, 01, 31);
            int every = 30;
            int everyMonths = 1;
            DateTime limitDate = new(2021, 04, 01);

            var result = MonthlyRecurrenceCalculator.GetFirstDate(current, every, everyMonths, limitDate);

            result.Should().BeSameDateAs(new DateTime(2021, 02, 28));
        }

        [Fact]
        public void GetFirstDay_Should_Return_Date_In_Same_Month_If_Every_Day_Is_After_Current_Date_Day()
        {
            DateTime current = new(2021, 01, 10);
            int every = 30;
            int everyMonths = 1;
            DateTime limitDate = new(2021, 01, 01);

            var result = MonthlyRecurrenceCalculator.GetFirstDate(current, every, everyMonths, limitDate);

            result.Should().BeSameDateAs(new DateTime(2021, 01, 30));
        }

        [Fact]
        public void GetDayRecurrences_Should_Return_Dates_With_Every_Month_Added() 
        { 
            DateTime current = new(2021, 01, 01);
            DateTime limitStart = new(2021, 01, 01);
            DateTime limitEnd = new(2022, 01, 01);
            Limits limits = new (limitStart, limitEnd);
            int everyDay = 10;
            int everyMonths = 1;
            MonthlyConfiguration config = new(true, everyDay, everyMonths);

            var result = MonthlyRecurrenceCalculator.GetDayRecurrences(current, limits, config);

            result[0].Should().BeSameDateAs(new DateTime(2021, 01, 10));
            result[1].Should().BeSameDateAs(new DateTime(2021, 02, 10));
            result[2].Should().BeSameDateAs(new DateTime(2021, 03, 10));
            result[3].Should().BeSameDateAs(new DateTime(2021, 04, 10));
            result[4].Should().BeSameDateAs(new DateTime(2021, 05, 10));
            result[5].Should().BeSameDateAs(new DateTime(2021, 06, 10));
            result[6].Should().BeSameDateAs(new DateTime(2021, 07, 10));
            result[7].Should().BeSameDateAs(new DateTime(2021, 08, 10));
            result[8].Should().BeSameDateAs(new DateTime(2021, 09, 10));
            result[9].Should().BeSameDateAs(new DateTime(2021, 10, 10));
            result[10].Should().BeSameDateAs(new DateTime(2021, 11, 10));
            result[11].Should().BeSameDateAs(new DateTime(2021, 12, 10));
        }

        [Fact]
        public void GetDayRecurrences_Should_Return_Dates_With_Every_3_Month_Added()
        {
            DateTime current = new(2021, 01, 01);
            DateTime limitStart = new(2021, 01, 01);
            DateTime limitEnd = new(2022, 01, 01);
            Limits limits = new(limitStart, limitEnd);
            int everyDay = 10;
            int everyMonths = 3;
            MonthlyConfiguration config = new(true, everyDay, everyMonths);

            var result = MonthlyRecurrenceCalculator.GetDayRecurrences(current, limits, config);

            result.Count.Should().Be(4);
            result[0].Should().BeSameDateAs(new DateTime(2021, 01, 10));
            result[1].Should().BeSameDateAs(new DateTime(2021, 04, 10));
            result[2].Should().BeSameDateAs(new DateTime(2021, 07, 10));
            result[3].Should().BeSameDateAs(new DateTime(2021, 10, 10));
        }

        [Fact]
        public void GetDayRecurrences_Should_Return_Dates_Month_Last_Day()
        {
            DateTime current = new(2021, 01, 01);
            DateTime limitStart = new(2021, 01, 01);
            DateTime limitEnd = new(2022, 01, 01);
            Limits limits = new(limitStart, limitEnd);
            int everyDay = 31;
            int everyMonths = 1;
            MonthlyConfiguration config = new(true, everyDay, everyMonths);

            var result = MonthlyRecurrenceCalculator.GetDayRecurrences(current, limits, config);

            result.Count.Should().Be(12);
            result[0].Should().BeSameDateAs(new DateTime(2021, 01, 31));
            result[1].Should().BeSameDateAs(new DateTime(2021, 02, 28));
            result[2].Should().BeSameDateAs(new DateTime(2021, 03, 31));
            result[3].Should().BeSameDateAs(new DateTime(2021, 04, 30));
            result[4].Should().BeSameDateAs(new DateTime(2021, 05, 31));
            result[5].Should().BeSameDateAs(new DateTime(2021, 06, 30));
            result[6].Should().BeSameDateAs(new DateTime(2021, 07, 31));
            result[7].Should().BeSameDateAs(new DateTime(2021, 08, 31));
            result[8].Should().BeSameDateAs(new DateTime(2021, 09, 30));
            result[9].Should().BeSameDateAs(new DateTime(2021, 10, 31));
            result[10].Should().BeSameDateAs(new DateTime(2021, 11, 30));
            result[11].Should().BeSameDateAs(new DateTime(2021, 12, 31));
        }

        [Fact]
        public void GetDayRecurrences_Should_Not_Return_Current_Date_Month_If_Every_Is_Before_Current_Date_Day()
        {
            DateTime current = new(2021, 01, 31);
            DateTime limitStart = new(2021, 01, 01);
            DateTime limitEnd = new(2022, 01, 01);
            Limits limits = new(limitStart, limitEnd);
            int everyDay = 20;
            int everyMonths = 1;
            MonthlyConfiguration config = new(true, everyDay, everyMonths);

            var result = MonthlyRecurrenceCalculator.GetDayRecurrences(current, limits, config);

            result.Count.Should().Be(11);
            result[0].Should().BeSameDateAs(new DateTime(2021, 02, 20));
            result[1].Should().BeSameDateAs(new DateTime(2021, 03, 20));
            result[2].Should().BeSameDateAs(new DateTime(2021, 04, 20));
            result[3].Should().BeSameDateAs(new DateTime(2021, 05, 20));
            result[4].Should().BeSameDateAs(new DateTime(2021, 06, 20));
            result[5].Should().BeSameDateAs(new DateTime(2021, 07, 20));
            result[6].Should().BeSameDateAs(new DateTime(2021, 08, 20));
            result[7].Should().BeSameDateAs(new DateTime(2021, 09, 20));
            result[8].Should().BeSameDateAs(new DateTime(2021, 10, 20));
            result[9].Should().BeSameDateAs(new DateTime(2021, 11, 20));
            result[10].Should().BeSameDateAs(new DateTime(2021, 12, 20));
        }

        [Fact]
        public void GetDayRecurrences_Should_Return_Current_Date_Month_If_Every_Is_After_Current_Date_Day()
        {
            DateTime current = new(2021, 01, 1);
            DateTime limitStart = new(2021, 01, 01);
            DateTime limitEnd = new(2022, 01, 01);
            Limits limits = new(limitStart, limitEnd);
            int everyDay = 20;
            int everyMonths = 1;
            MonthlyConfiguration config = new(true, everyDay, everyMonths);

            var result = MonthlyRecurrenceCalculator.GetDayRecurrences(current, limits, config);

            result.Count.Should().Be(12);
            result[0].Should().BeSameDateAs(new DateTime(2021, 01, 20));
            result[1].Should().BeSameDateAs(new DateTime(2021, 02, 20));
            result[2].Should().BeSameDateAs(new DateTime(2021, 03, 20));
            result[3].Should().BeSameDateAs(new DateTime(2021, 04, 20));
            result[4].Should().BeSameDateAs(new DateTime(2021, 05, 20));
            result[5].Should().BeSameDateAs(new DateTime(2021, 06, 20));
            result[6].Should().BeSameDateAs(new DateTime(2021, 07, 20));
            result[7].Should().BeSameDateAs(new DateTime(2021, 08, 20));
            result[8].Should().BeSameDateAs(new DateTime(2021, 09, 20));
            result[9].Should().BeSameDateAs(new DateTime(2021, 10, 20));
            result[10].Should().BeSameDateAs(new DateTime(2021, 11, 20));
            result[11].Should().BeSameDateAs(new DateTime(2021, 12, 20));
        }

        [Fact]
        public void GetDayRecurrences_Should_Return_Last_Day_Of_Month_If_Month_Does_Not_Contain_Day()
        {
            DateTime current = new(2021, 01, 1);
            DateTime limitStart = new(2021, 01, 01);
            DateTime limitEnd = new(2022, 01, 01);
            Limits limits = new(limitStart, limitEnd);
            int everyDay = 31;
            int everyMonths = 1;
            MonthlyConfiguration config = new(true, everyDay, everyMonths);

            var result = MonthlyRecurrenceCalculator.GetDayRecurrences(current, limits, config);

            result.Count.Should().Be(12);
            result[0].Should().BeSameDateAs(new DateTime(2021, 01, 31));
            result[1].Should().BeSameDateAs(new DateTime(2021, 02, 28));
            result[2].Should().BeSameDateAs(new DateTime(2021, 03, 31));
            result[3].Should().BeSameDateAs(new DateTime(2021, 04, 30));
            result[4].Should().BeSameDateAs(new DateTime(2021, 05, 31));
            result[5].Should().BeSameDateAs(new DateTime(2021, 06, 30));
            result[6].Should().BeSameDateAs(new DateTime(2021, 07, 31));
            result[7].Should().BeSameDateAs(new DateTime(2021, 08, 31));
            result[8].Should().BeSameDateAs(new DateTime(2021, 09, 30));
            result[9].Should().BeSameDateAs(new DateTime(2021, 10, 31));
            result[10].Should().BeSameDateAs(new DateTime(2021, 11, 30));
            result[11].Should().BeSameDateAs(new DateTime(2021, 12, 31));
        }

        [Fact]
        public void GetDayRecurrences_Should_Return_Dates_If_Adding_Months_Change_Year()
        {
            DateTime current = new(2021, 10, 31);
            DateTime limitStart = new(2021, 01, 01);
            DateTime limitEnd = new(2022, 04, 01);
            Limits limits = new(limitStart, limitEnd);
            int everyDay = 20;
            int everyMonths = 1;
            MonthlyConfiguration config = new(true, everyDay, everyMonths);

            var result = MonthlyRecurrenceCalculator.GetDayRecurrences(current, limits, config);

            result.Count.Should().Be(5);
            result[0].Should().BeSameDateAs(new DateTime(2021, 11, 20));
            result[1].Should().BeSameDateAs(new DateTime(2021, 12, 20));
            result[2].Should().BeSameDateAs(new DateTime(2022, 01, 20));
            result[3].Should().BeSameDateAs(new DateTime(2022, 02, 20));
            result[4].Should().BeSameDateAs(new DateTime(2022, 03, 20));
        }


        [Fact]
        public void GetDaysOfWeekInMonth_Should_Return_Ienumerable_With_Dates_Of_DayOfWeek_After_Current_Date()
        {
            DateTime current = new(2021, 10, 01);

            var result = MonthlyRecurrenceCalculator.GetDaysOfWeekForMonth(current, DayOfWeek.Wednesday);

            result.Count().Should().Be(4);
            result.ElementAt(0).Should().BeSameDateAs(new DateTime(2021, 10, 06));
            result.ElementAt(1).Should().BeSameDateAs(new DateTime(2021, 10, 13));
            result.ElementAt(2).Should().BeSameDateAs(new DateTime(2021, 10, 20));
            result.ElementAt(3).Should().BeSameDateAs(new DateTime(2021, 10, 27));
        }

        [Fact]
        public void GetDaysOfWeekInMonth_Should_Return_Ienumerable_With_Dates_Of_DayOfWeek_After_Current_Date_Day()
        {
            DateTime current = new(2021, 10, 10);

            var result = MonthlyRecurrenceCalculator.GetDaysOfWeekForMonth(current, DayOfWeek.Monday);

            result.Count().Should().Be(3);
            result.ElementAt(0).Should().BeSameDateAs(new DateTime(2021, 10, 11));
            result.ElementAt(1).Should().BeSameDateAs(new DateTime(2021, 10, 18));
            result.ElementAt(2).Should().BeSameDateAs(new DateTime(2021, 10, 25));
        }

        [Fact]
        public void GetDayForMonth_Should_Return_Datetime_Of_MonthlyFrecuency_First()
        {
            DateTime current = new(2021, 10, 10);

            var result = MonthlyRecurrenceCalculator.GetDayForMonth(current, MonthlyFrecuency.First);

            result.Should().BeSameDateAs(new DateTime(2021, 10, 1));
        }

        [Fact]
        public void GetDayForMonth_Should_Return_Datetime_Of_MonthlyFrecuency_Second()
        {
            DateTime current = new(2021, 10, 10);

            var result = MonthlyRecurrenceCalculator.GetDayForMonth(current, MonthlyFrecuency.Second);

            result.Should().BeSameDateAs(new DateTime(2021, 10, 2));
        }

        [Fact]
        public void GetDayForMonth_Should_Return_Datetime_Of_MonthlyFrecuency_Third()
        {
            DateTime current = new(2021, 10, 10);

            var result = MonthlyRecurrenceCalculator.GetDayForMonth(current, MonthlyFrecuency.Third);

            result.Should().BeSameDateAs(new DateTime(2021, 10, 3));
        }

        [Fact]
        public void GetDayForMonth_Should_Return_Datetime_Of_MonthlyFrecuency_Fourth()
        {
            DateTime current = new(2021, 10, 10);

            var result = MonthlyRecurrenceCalculator.GetDayForMonth(current, MonthlyFrecuency.Fourth);

            result.Should().BeSameDateAs(new DateTime(2021, 10, 4));
        }

        [Fact]
        public void GetDayForMonth_Should_Return_Datetime_Of_MonthlyFrecuency_Last()
        {
            DateTime current = new(2021, 10, 10);

            var result = MonthlyRecurrenceCalculator.GetDayForMonth(current, MonthlyFrecuency.Last);

            result.Should().BeSameDateAs(new DateTime(2021, 10, 31));
        }

        [Fact]
        public void GetWeekDayForMonth_Should_Return_Ienumerable_With_Dates_Of_DayOfWeek_After_Current_Date_Day()
        {
            DateTime current = new(2021, 10, 10);

            var result = MonthlyRecurrenceCalculator.GetWeekDayForMonth(current, MonthlyFrecuency.First);

            result.Count().Should().Be(3);
            result.ElementAt(0).Should().BeSameDateAs(new DateTime(2021, 10, 11));
            result.ElementAt(1).Should().BeSameDateAs(new DateTime(2021, 10, 18));
            result.ElementAt(2).Should().BeSameDateAs(new DateTime(2021, 10, 25));
        }

        [Fact]
        public void GetWeekDayForMonth_Should_Return_LastWeekDay_Ienumerable_With_Dates_Of_DayOfWeek_After_Current_Date_Day()
        {
            DateTime current = new(2021, 10, 1);

            var result = MonthlyRecurrenceCalculator.GetWeekDayForMonth(current, MonthlyFrecuency.Last);

            result.Count().Should().Be(5);
            result.ElementAt(0).Should().BeSameDateAs(new DateTime(2021, 10, 3));
            result.ElementAt(1).Should().BeSameDateAs(new DateTime(2021, 10, 10));
            result.ElementAt(2).Should().BeSameDateAs(new DateTime(2021, 10, 17));
            result.ElementAt(3).Should().BeSameDateAs(new DateTime(2021, 10, 24));
        }

        [Fact]
        public void GetWeekendDayForMonth_Should_Throw_Exception_If_Frecuency_Third()
        {
            DateTime current = new(2021, 10, 1);

            Action getWeekendDay = () =>
            {
                MonthlyRecurrenceCalculator.GetWeekendDayForMonth(current, MonthlyFrecuency.Third);
            };

            getWeekendDay.Should().Throw<ArgumentException>();

        }

        [Fact]
        public void GetWeekendDayForMonth_Should_Throw_Exception_If_Frecuency_Fourth()
        {
            DateTime current = new(2021, 10, 1);

            Action getWeekendDay = () =>
            {
                MonthlyRecurrenceCalculator.GetWeekendDayForMonth(current, MonthlyFrecuency.Fourth);
            };

            getWeekendDay.Should().Throw<ArgumentException>();

        }

        [Fact]
        public void GetWeekendDayForMonth_Should_Not_Throw_Exception_If_Frecuency_IsCorrect()
        {
            DateTime current = new(2021, 10, 1);

            Action getWeekendDay = () =>
            {
                MonthlyRecurrenceCalculator.GetWeekendDayForMonth(current, MonthlyFrecuency.Last);
            };

            getWeekendDay.Should().NotThrow<ArgumentException>();

        }

        [Fact]
        public void GetWeekendDayForMonth_Should_Return_List_Of_Month_Saturdays_If_Frecuency_First()
        {
            DateTime current = new(2021, 10, 1);

            var result = MonthlyRecurrenceCalculator.GetWeekendDayForMonth(current, MonthlyFrecuency.First);

            result.Count().Should().Be(5);
            result.ElementAt(0).Should().BeSameDateAs(new DateTime(2021, 10, 2));
            result.ElementAt(1).Should().BeSameDateAs(new DateTime(2021, 10, 9));
            result.ElementAt(2).Should().BeSameDateAs(new DateTime(2021, 10, 16));
            result.ElementAt(3).Should().BeSameDateAs(new DateTime(2021, 10, 23));
            result.ElementAt(4).Should().BeSameDateAs(new DateTime(2021, 10, 30));
        }

        [Fact]
        public void GetWeekendDayForMonth_Should_Return_List_Of_Month_Sunday_If_Frecuency_Second()
        {
            DateTime current = new(2021, 10, 1);

            var result = MonthlyRecurrenceCalculator.GetWeekendDayForMonth(current, MonthlyFrecuency.Second);

            result.Count().Should().Be(5);
            result.ElementAt(0).Should().BeSameDateAs(new DateTime(2021, 10, 3));
            result.ElementAt(1).Should().BeSameDateAs(new DateTime(2021, 10, 10));
            result.ElementAt(2).Should().BeSameDateAs(new DateTime(2021, 10, 17));
            result.ElementAt(3).Should().BeSameDateAs(new DateTime(2021, 10, 24));
            result.ElementAt(4).Should().BeSameDateAs(new DateTime(2021, 10, 31));
        }

        [Fact]
        public void GetWeekendDayForMonth_Should_Return_List_Of_Month_Sunday_If_Frecuency_Last()
        {
            DateTime current = new(2021, 10, 10);

            var result = MonthlyRecurrenceCalculator.GetWeekendDayForMonth(current, MonthlyFrecuency.Last);

            result.Count().Should().Be(4);
            result.ElementAt(0).Should().BeSameDateAs(new DateTime(2021, 10, 10));
            result.ElementAt(1).Should().BeSameDateAs(new DateTime(2021, 10, 17));
            result.ElementAt(2).Should().BeSameDateAs(new DateTime(2021, 10, 24));
            result.ElementAt(3).Should().BeSameDateAs(new DateTime(2021, 10, 31));
        }

        [Fact]
        public void GetWeekendDayForMonth_Should_Return_List_Of_Month_Sunday_If_Frecuency_Last_Starting_On_10()
        {
            DateTime current = new(2021, 10, 10);

            var result = MonthlyRecurrenceCalculator.GetWeekendDayForMonth(current, MonthlyFrecuency.Last);

            result.Count().Should().Be(4);
            result.ElementAt(0).Should().BeSameDateAs(new DateTime(2021, 10, 10));
            result.ElementAt(1).Should().BeSameDateAs(new DateTime(2021, 10, 17));
            result.ElementAt(2).Should().BeSameDateAs(new DateTime(2021, 10, 24));
            result.ElementAt(3).Should().BeSameDateAs(new DateTime(2021, 10, 31));
        }

        [Fact]
        public void GetWeekendDayForMonth_Should_Return_List_Of_Month_Sunday_If_Frecuency_Last_Starting_On_10_11()
        {
            DateTime current = new(2021, 11, 10);

            var result = MonthlyRecurrenceCalculator.GetWeekendDayForMonth(current, MonthlyFrecuency.Last);

            result.Count().Should().Be(3);
            result.ElementAt(0).Should().BeSameDateAs(new DateTime(2021, 11, 14));
            result.ElementAt(1).Should().BeSameDateAs(new DateTime(2021, 11, 21));
            result.ElementAt(2).Should().BeSameDateAs(new DateTime(2021, 11, 28));
        }

        [Fact]
        public void GetRecurrences_Should_Return_List_Of_Recurrences_After_CurrentDate_If_The_IsTrue()
        {
            DateTime current = new(2021, 01, 15);
            Limits limits = new(current, new DateTime(2022, 01, 01));
            MonthlyConfiguration config = new(true, MonthlyFrecuency.Second, MonthlyDayType.Tuesday, 2);
            var result = MonthlyRecurrenceCalculator.GetTheRecurrences(current, limits.EndDate.Value, config.DayType.Value, config.Frecuency.Value, config.EveryMonths.Value);

            result.Count.Should().Be(5);
            result.ElementAt(0).Should().BeSameDateAs(new DateTime(2021, 03, 09));
            result.ElementAt(1).Should().BeSameDateAs(new DateTime(2021, 05, 11));
            result.ElementAt(2).Should().BeSameDateAs(new DateTime(2021, 07, 13));
            result.ElementAt(3).Should().BeSameDateAs(new DateTime(2021, 09, 14));
            result.ElementAt(4).Should().BeSameDateAs(new DateTime(2021, 11, 09));
        }

        [Fact]
        public void GetRecurrences_Should_Return_List_Of_Recurrences_After_CurrentDate_If_The_IsTrue_In_2Different_Years()
        {
            DateTime current = new(2021, 01, 01);
            Limits limits = new(current, new DateTime(2022, 06, 01));
            MonthlyConfiguration config = new(true, MonthlyFrecuency.Last, MonthlyDayType.Sunday, 4);
            var result = MonthlyRecurrenceCalculator.GetTheRecurrences(current, limits.EndDate.Value, config.DayType.Value, config.Frecuency.Value, config.EveryMonths.Value);

            result.Count.Should().Be(5);
            result.ElementAt(0).Should().BeSameDateAs(new DateTime(2021, 01, 31));
            result.ElementAt(1).Should().BeSameDateAs(new DateTime(2021, 05, 30));
            result.ElementAt(2).Should().BeSameDateAs(new DateTime(2021, 09, 26));
            result.ElementAt(3).Should().BeSameDateAs(new DateTime(2022, 01, 30));
            result.ElementAt(4).Should().BeSameDateAs(new DateTime(2022, 05, 29));
        }

        [Fact]
        public void GetRecurrences_Should_Return_List_Of_Recurrences_After_CurrentDate_If_MonthlyDayType_Day()
        {
            DateTime current = new(2021, 01, 01);
            Limits limits = new(current, new DateTime(2022, 06, 01));
            MonthlyConfiguration config = new(true, MonthlyFrecuency.Last, MonthlyDayType.Day, 3);
            var result = MonthlyRecurrenceCalculator.GetTheRecurrences(current, limits.EndDate.Value, config.DayType.Value, config.Frecuency.Value, config.EveryMonths.Value);

            result.Count.Should().Be(6);
            result.ElementAt(0).Should().BeSameDateAs(new DateTime(2021, 01, 31));
            result.ElementAt(1).Should().BeSameDateAs(new DateTime(2021, 04, 30));
            result.ElementAt(2).Should().BeSameDateAs(new DateTime(2021, 07, 31));
            result.ElementAt(3).Should().BeSameDateAs(new DateTime(2021, 10, 31));
            result.ElementAt(4).Should().BeSameDateAs(new DateTime(2022, 01, 31));
            result.ElementAt(5).Should().BeSameDateAs(new DateTime(2022, 04, 30));
        }

        [Fact]
        public void GetRecurrences_Should_Return_List_Of_Recurrences_After_CurrentDate_If_MonthlyDayType_WeekendDay()
        {
            DateTime current = new(2021, 01, 01);
            Limits limits = new(current, new DateTime(2022, 06, 01));
            MonthlyConfiguration config = new(true, MonthlyFrecuency.Last, MonthlyDayType.WeekendDay, 5);
            var result = MonthlyRecurrenceCalculator.GetTheRecurrences(current, limits.EndDate.Value, config.DayType.Value, config.Frecuency.Value, config.EveryMonths.Value);

            result.Count.Should().Be(4);
            result.ElementAt(0).Should().BeSameDateAs(new DateTime(2021, 01, 31));
            result.ElementAt(1).Should().BeSameDateAs(new DateTime(2021, 06, 27));
            result.ElementAt(2).Should().BeSameDateAs(new DateTime(2021, 11, 28));
            result.ElementAt(3).Should().BeSameDateAs(new DateTime(2022, 04, 24));

        }
    }
}
