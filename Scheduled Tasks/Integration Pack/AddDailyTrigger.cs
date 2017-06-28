using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace ScheduledTasksIntegrationPack
{
    [Activity("Add Daily Task Trigger")]
    class AddDailyTrigger : IActivity
    {
        private String computerName = String.Empty;
        private String taskName = String.Empty;
        private String dayInterval = String.Empty;
        private String hour = String.Empty;
        private String minute = String.Empty;

        private String accountName = String.Empty;
        private String password = String.Empty;

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Computer Name");
            designer.AddInput("Task Name");
            designer.AddInput("Day Interval");
            designer.AddInput("Hour");
            designer.AddInput("Minute");
            designer.AddInput("Account Name");
            designer.AddInput("Password");
            designer.AddCorellatedData(typeof(OpalisTaskTrigger));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            computerName = request.Inputs["Computer Name"].AsString();
            taskName = request.Inputs["Task Name"].AsString();
            dayInterval = request.Inputs["Day Interval"].AsString();
            hour = request.Inputs["Hour"].AsString();
            minute = request.Inputs["Minute"].AsString();

            accountName = request.Inputs["Account Name"].AsString();
            password = request.Inputs["Password"].AsString();

            response.WithFiltering().PublishRange(AddTrigger());
        }

        private IEnumerable<OpalisTaskTrigger> AddTrigger()
        {
            ScheduledTasks st = new ScheduledTasks(@"\\" + computerName);

            Task t = st.OpenTask(taskName);
            t.SetAccountInformation(accountName, password);
            DailyTrigger trigger = new DailyTrigger(Convert.ToInt16(hour), Convert.ToInt16(minute), Convert.ToInt16(dayInterval));

            t.Triggers.Add(trigger);
            t.Save();

            String Type = trigger.GetType().ToString().Split('.')[1];
            String BeginDay = trigger.taskTrigger.BeginDay.ToString();
            String BeginMonth = trigger.taskTrigger.BeginMonth.ToString();
            String BeginYear = trigger.taskTrigger.BeginYear.ToString();
            String EndDay = trigger.taskTrigger.EndDay.ToString();
            String EndMonth = trigger.taskTrigger.EndMonth.ToString();
            String EndYear = trigger.taskTrigger.EndYear.ToString();
            String flags = trigger.taskTrigger.Flags.ToString();
            String MinutesDuration = trigger.taskTrigger.MinutesDuration.ToString();
            String MinutesInterval = trigger.taskTrigger.MinutesInterval.ToString();
            String RandomMinutesInterval = trigger.taskTrigger.RandomMinutesInterval.ToString();
            String StartHour = trigger.taskTrigger.StartHour.ToString();
            String StartMinute = trigger.taskTrigger.StartMinute.ToString();
            String TriggerSize = trigger.taskTrigger.TriggerSize.ToString();
            String Bound = trigger.Bound.ToString();
            String Disabled = trigger.Disabled.ToString();
            String HasEndDate = trigger.HasEndDate.ToString();
            String KillAtDurationEnd = trigger.KillAtDurationEnd.ToString();

            String DaysInterval = trigger.taskTrigger.Data.daily.DaysInterval.ToString();
            String MonthlyDateDays = trigger.taskTrigger.Data.monthlyDate.Days.ToString();
            String MonthlyDateMonths = trigger.taskTrigger.Data.monthlyDate.Months.ToString();
            String MonthlyDOWDaysOfTheWeek = trigger.taskTrigger.Data.monthlyDOW.DaysOfTheWeek.ToString();
            String MonthlyDOWMonths = trigger.taskTrigger.Data.monthlyDOW.Months.ToString();
            String MonthlyDOWWhichWeek = trigger.taskTrigger.Data.monthlyDOW.WhichWeek.ToString();
            String weeklyDaysOfTheWeek = trigger.taskTrigger.Data.weekly.DaysOfTheWeek.ToString();
            String weeklyWeeksInterval = trigger.taskTrigger.Data.weekly.WeeksInterval.ToString();

            String DisplayString = trigger.ToString();

            yield return new OpalisTaskTrigger(Type, BeginDay, BeginMonth, BeginYear, EndDay, EndMonth, EndYear, flags,
                                               MinutesDuration, MinutesInterval, RandomMinutesInterval, StartHour,
                                               StartMinute, TriggerSize, Bound, Disabled, HasEndDate, KillAtDurationEnd,
                                               DaysInterval, MonthlyDateDays, MonthlyDateMonths, MonthlyDOWDaysOfTheWeek,
                                               MonthlyDOWMonths, MonthlyDOWWhichWeek, weeklyDaysOfTheWeek, weeklyWeeksInterval,
                                               DisplayString);
            t.Close();
            st.Dispose();
        }
    }
}

