using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace ScheduledTasksIntegrationPack
{
    [Activity("Clear Task Triggers")]
    class ClearTaskTriggers : IActivity
    {
        private String computerName = String.Empty;
        private String taskName = String.Empty;
        private int numTriggers = 0;

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Computer Name");
            designer.AddInput("Task Name");            
            designer.AddCorellatedData(typeof(OpalisTaskTrigger));
            designer.AddOutput("Number of Triggers Removed");
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            computerName = request.Inputs["Computer Name"].AsString();
            taskName = request.Inputs["Task Name"].AsString();

            response.WithFiltering().PublishRange(ClearTriggers());
            response.Publish("Number of Triggers Removed", numTriggers);
        }

        private IEnumerable<OpalisTaskTrigger> ClearTriggers()
        {
            ScheduledTasks st = new ScheduledTasks(@"\\" + computerName);

            Task t = st.OpenTask(taskName);

            foreach (Trigger trigger in t.Triggers)
            {

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
                
                numTriggers++;

                yield return new OpalisTaskTrigger(Type, BeginDay, BeginMonth, BeginYear, EndDay, EndMonth, EndYear, flags,
                                                   MinutesDuration, MinutesInterval, RandomMinutesInterval, StartHour,
                                                   StartMinute, TriggerSize, Bound, Disabled, HasEndDate, KillAtDurationEnd,
                                                   DaysInterval, MonthlyDateDays, MonthlyDateMonths, MonthlyDOWDaysOfTheWeek,
                                                   MonthlyDOWMonths, MonthlyDOWWhichWeek, weeklyDaysOfTheWeek, weeklyWeeksInterval,
                                                   DisplayString);
            }
            t.Triggers.Clear();
            t.Save();
            t.Close();
            st.Dispose();
        }
    }
}

