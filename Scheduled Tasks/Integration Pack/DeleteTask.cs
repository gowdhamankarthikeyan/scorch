using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace ScheduledTasksIntegrationPack
{
    [Activity("Delete Task")]
    class DeleteTask : IActivity
    {
        private String computerName = String.Empty;
        private String taskName = String.Empty;
        private int numTasks = 0;

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Computer Name");
            designer.AddInput("Task Name");
            designer.AddCorellatedData(typeof(opalisTask));
            designer.AddOutput("Number of Removed Tasks");
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            computerName = request.Inputs["Computer Name"].AsString();
            taskName = request.Inputs["Task Name"].AsString();

            response.WithFiltering().PublishRange(deleteTask());
            response.Publish("Number of Removed Tasks", numTasks);
        }

        private IEnumerable<opalisTask> deleteTask()
        {
            ScheduledTasks st = new ScheduledTasks(@"\\" + computerName);

            Task t = st.OpenTask(taskName);

            String AccountName = t.AccountName.ToString();
            String ApplicationName = t.ApplicationName.ToString();
            String Comment = t.Comment.ToString();
            String Creator = t.Creator.ToString();
            String ExitCode = t.ExitCode.ToString();
            String Flags = t.Flags.ToString();
            String Hidden = t.Hidden.ToString();
            String IdleWaitDeadlineMinutes = t.IdleWaitDeadlineMinutes.ToString();
            String IdleWaitMinutes = t.IdleWaitMinutes.ToString();
            String MaxRunTime = t.MaxRunTime.ToString();
            String MaxRunTimeLimited = t.MaxRunTimeLimited.ToString();
            String MostRecentRunTime = t.MostRecentRunTime.ToString();
            String Name = t.Name.ToString();
            String NextRunTime = t.NextRunTime.ToString();
            String Parameters = t.Parameters.ToString();
            String Priority = t.Priority.ToString();
            String Status = t.Status.ToString();
            String WorkingDirectory = t.WorkingDirectory.ToString();

            numTasks++;
            t.Close();
            st.DeleteTask(taskName);

            yield return new opalisTask(Name, AccountName, ApplicationName, Comment,
                                        Creator, ExitCode, Flags, Hidden, IdleWaitDeadlineMinutes,
                                        IdleWaitMinutes, MaxRunTime, MaxRunTimeLimited, MostRecentRunTime,
                                        NextRunTime, Parameters, Priority, Status, WorkingDirectory);

            st.Dispose();
        }
    }
}

