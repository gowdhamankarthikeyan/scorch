using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using OrchestratorInterop.Data_Class;
using OrchestratorInterop.SCOrchestrator;
using OrchestratorInterop;
using OrchestratorIP.ConfigurationObjects;
using OrchestratorIP.ReturnTypes;
using System.Globalization;
using System.Net;
using System.Data.Services.Client;
namespace OrchestratorIP.Activities
{
    [Activity("Get Events")]
    public class GetEvents : IActivity
    {
        private int numberOfEvents = 0;
        [ActivityConfiguration]
        public ConnectionCredentials Credentials
        {
            get;
            set;
        }

        public void Design(IActivityDesigner designer)
        {
            OrchestratorContext sco = new OrchestratorContext(new Uri(Credentials.OrchestratorServiceURL));

            sco.Credentials = new NetworkCredential(Credentials.UserName, Credentials.Password, Credentials.Domain);
            sco.MergeOption = MergeOption.OverwriteChanges;

            string[] SummaryString = SCOrch.getOrchestartorEventSummaries(sco);

            if (SummaryString.Length > 0) { designer.AddFilter("Summary").WithRelations(Relation.Contains | Relation.EqualTo).WithListBrowser(SummaryString); }
            else { designer.AddFilter("Summary").WithRelations(Relation.Contains | Relation.EqualTo); }

            designer.AddFilter("CreationTime").WithRelations(Relation.After | Relation.Before);

            designer.AddCorellatedData(typeof(EventInst));
            designer.AddOutput("Number of Events");
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            String Summary = String.Empty;
            String SummaryCriteria = String.Empty;
            DateTime MinCreationDate = DateTime.MinValue;
            DateTime MaxCreationDate = DateTime.MaxValue;

            foreach (IFilterCriteria filter in request.Filters)
            {
                switch (filter.Name)
                {
                    case "CreationTime":
                        switch (filter.Relation)
                        {
                            case Relation.After:
                                MinCreationDate = Convert.ToDateTime(filter.Value.AsString());
                                break;
                            case Relation.Before:
                                MaxCreationDate = Convert.ToDateTime(filter.Value.AsString());
                                break;
                        }
                        break;
                    case "Summary":
                        Summary = filter.Value.AsString();
                        switch (filter.Relation)
                        {
                            case Relation.Contains:
                                SummaryCriteria = "Contains";
                                break;
                            case Relation.EqualTo:
                                SummaryCriteria = "Equals";
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }



            OrchestratorContext sco = new OrchestratorContext(new Uri(Credentials.OrchestratorServiceURL));

            sco.Credentials = new NetworkCredential(Credentials.UserName, Credentials.Password, Credentials.Domain);
            sco.MergeOption = MergeOption.OverwriteChanges;

            Event[] eventArray = null;
            
            if (Summary.Equals(String.Empty))
            {
                eventArray = SCOrch.getOrchestartorEvents(sco, MinCreationDate, MaxCreationDate);
            }
            else
            {
                eventArray = SCOrch.getOrchestartorEvents(sco, MinCreationDate, MaxCreationDate, Summary, SummaryCriteria);
            }


            response.WithFiltering().PublishRange(parseResults(eventArray));
            response.Publish("Number of Events", numberOfEvents);
        }

        private IEnumerable<EventInst> parseResults(Event[] eventArray)
        {
            foreach (Event e in eventArray)
            {
                numberOfEvents = numberOfEvents + 1;
                yield return new EventInst(e);
            }
        }
    }
}
