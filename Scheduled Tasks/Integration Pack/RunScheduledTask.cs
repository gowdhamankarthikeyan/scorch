using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace ScheduledTasksIntegrationPack
{
    [Activity("Run Scheduled Task")]
    class RunScheduledTask : IActivity
    {
        private String computerName = String.Empty;
        private String taskName = String.Empty;
        private String taskStatus = String.Empty;

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Computer Name");
            designer.AddInput("Task Name");
            designer.AddOutput("Task Status").AsString();
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            computerName = request.Inputs["Computer Name"].AsString();
            taskName = request.Inputs["Task Name"].AsString();

            ScheduledTasks st = new ScheduledTasks(@"\\" + computerName);

            Task t = st.OpenTask(taskName);

            t.Run();
            taskStatus = t.Status.ToString();
            t.Close();

            while (taskStatus.Equals("Ready"))
            {
                t = st.OpenTask(taskName);
                taskStatus = t.Status.ToString();
                t.Close();
            }

            st.Dispose();

            response.Publish("Task Status", taskStatus);
        }
    }
}

