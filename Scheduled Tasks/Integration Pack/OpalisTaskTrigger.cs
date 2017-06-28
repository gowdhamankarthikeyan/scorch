using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace ScheduledTasksIntegrationPack
{
    [ActivityData("Opalis Scheduled Task Trigger")]
    class OpalisTaskTrigger
    {
        private String Type = String.Empty;
        private String BeginDay = String.Empty;
        private String BeginMonth = String.Empty;
        private String BeginYear = String.Empty;
        private String EndDay = String.Empty;
        private String EndMonth = String.Empty;
        private String EndYear = String.Empty;
        private String flags = String.Empty;
        private String MinutesDuration = String.Empty;
        private String MinutesInterval = String.Empty;
        private String RandomMinutesInterval = String.Empty;
        private String StartHour = String.Empty;
        private String StartMinute = String.Empty;
        private String TriggerSize = String.Empty;
        private String Bound = String.Empty;
        private String Disabled = String.Empty;
        private String HasEndDate = String.Empty;
        private String KillAtDurationEnd = String.Empty;

        private String DaysInterval = String.Empty;
        private String MonthlyDateDays = String.Empty;
        private String MonthlyDateMonths = String.Empty;
        private String MonthlyDOWDaysOfTheWeek = String.Empty;
        private String MonthlyDOWMonths = String.Empty;
        private String MonthlyDOWWhichWeek = String.Empty;
        private String weeklyDaysOfTheWeek = String.Empty;
        private String weeklyWeeksInterval = String.Empty;

        private String DisplayString = String.Empty;

        internal OpalisTaskTrigger(String Type, String BeginDay, String BeginMonth, String BeginYear, String EndDay,
                                    String EndMonth, String EndYear, String flags, String MinutesDuration, String MinutesInterval,
                                    String RandomMinutesInterval, String StartHour, String StartMinute, String TriggerSize, String Bound,
                                    String Disabled, String HasEndDate, String KillAtDurationEnd, String DaysInterval, String MonthlyDateDays,
                                    String MonthlyDateMonths, String MonthlyDOWDaysOfTheWeek, String MonthlyDOWMonths, String MonthlyDOWWhichWeek,
                                    String weeklyDaysOfTheWeek, String weeklyWeeksInterval, String DisplayString)
        {
            this.Type = Type;
            this.BeginDay = BeginDay;
            this.BeginMonth = BeginMonth;
            this.BeginYear = BeginYear;
            this.EndDay = EndDay;
            this.EndMonth = EndMonth;
            this.EndYear = EndYear;
            this.flags = flags;
            this.MinutesDuration = MinutesDuration;
            this.MinutesInterval = MinutesInterval;
            this.RandomMinutesInterval = RandomMinutesInterval;
            this.StartHour = StartHour;
            this.StartMinute = StartMinute;
            this.TriggerSize = TriggerSize;
            this.Bound = Bound;
            this.Disabled = Disabled;
            this.HasEndDate = HasEndDate;
            this.KillAtDurationEnd = KillAtDurationEnd;
            this.DaysInterval = DaysInterval;
            this.MonthlyDateDays = MonthlyDateDays;
            this.MonthlyDateMonths = MonthlyDateMonths;
            this.MonthlyDOWDaysOfTheWeek = MonthlyDOWDaysOfTheWeek;
            this.MonthlyDOWMonths = MonthlyDOWMonths;
            this.MonthlyDOWWhichWeek = MonthlyDOWWhichWeek;
            this.weeklyDaysOfTheWeek = weeklyDaysOfTheWeek;
            this.weeklyWeeksInterval = weeklyWeeksInterval;
            this.DisplayString = DisplayString;
        }

        [ActivityOutput, ActivityFilter]
        public String type
        {
            get { return Type; }
        }
        [ActivityOutput, ActivityFilter]
        public String Begin_Day
        {
            get { return BeginDay; }
        }
        [ActivityOutput, ActivityFilter]
        public String Begin_Month
        {
            get { return BeginMonth; }
        }
        [ActivityOutput, ActivityFilter]
        public String Begin_Year
        {
            get { return BeginYear; }
        }
        [ActivityOutput, ActivityFilter]
        public String End_Day
        {
            get { return EndDay; }
        }
        [ActivityOutput, ActivityFilter]
        public String End_Month
        {
            get { return EndMonth; }
        }
        [ActivityOutput, ActivityFilter]
        public String End_Year
        {
            get { return EndYear; }
        }
        [ActivityOutput, ActivityFilter]
        public String Flags
        {
            get { return flags; }
        }
        [ActivityOutput, ActivityFilter]
        public String Minutes_Duration
        {
            get { return MinutesDuration; }
        }
        [ActivityOutput, ActivityFilter]
        public String Minutes_Interval
        {
            get { return MinutesInterval; }
        }
        [ActivityOutput, ActivityFilter]
        public String Random_Minutes_Interval
        {
            get { return RandomMinutesInterval; }
        }
        [ActivityOutput, ActivityFilter]
        public String Start_Hour
        {
            get { return StartHour; }
        }
        [ActivityOutput, ActivityFilter]
        public String Start_Minute
        {
            get { return StartMinute; }
        }
        [ActivityOutput, ActivityFilter]
        public String Trigger_Size
        {
            get { return TriggerSize; }
        }
        [ActivityOutput, ActivityFilter]
        public String bound
        {
            get { return Bound; }
        }
        [ActivityOutput, ActivityFilter]
        public String disabled
        {
            get { return Disabled; }
        }
        [ActivityOutput, ActivityFilter]
        public String Has_End_Date
        {
            get { return HasEndDate; }
        }
        [ActivityOutput, ActivityFilter]
        public String Kill_At_Duration_End
        {
            get { return KillAtDurationEnd; }
        }
        [ActivityOutput, ActivityFilter]
        public String Days_Interval
        {
            get { return DaysInterval; }
        }
        [ActivityOutput, ActivityFilter]
        public String Monthly_Date_Days
        {
            get { return MonthlyDateDays; }
        }
        [ActivityOutput, ActivityFilter]
        public String Monthly_Date_Months
        {
            get { return MonthlyDateMonths; }
        }
        [ActivityOutput, ActivityFilter]
        public String Monthly_DOW_Days_Of_The_Week
        {
            get { return MonthlyDOWDaysOfTheWeek; }
        }
        [ActivityOutput, ActivityFilter]
        public String Monthly_DOW_Months
        {
            get { return MonthlyDOWMonths; }
        }
        [ActivityOutput, ActivityFilter]
        public String Monthly_DOW_Which_Week
        {
            get { return MonthlyDOWWhichWeek; }
        }
        [ActivityOutput, ActivityFilter]
        public String weekly_Days_Of_The_Week
        {
            get { return weeklyDaysOfTheWeek; }
        }
        [ActivityOutput, ActivityFilter]
        public String weekly_Weeks_Interval
        {
            get { return weeklyWeeksInterval; }
        }
        [ActivityOutput, ActivityFilter]
        public String Display_String
        {
            get { return DisplayString; }
        }
    }
}

