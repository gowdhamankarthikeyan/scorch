using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Exchange.WebServices.Autodiscover;
using System.IO;

namespace ExchangeWebService
{
    [Cmdlet(VerbsCommunications.Read, "EWSEmail")]
    public class Read_Email : PSCmdlet
    {
        #region Parameters
        private ExchangeService _exchangeService;
        [Parameter(
           Position = 0,
           Mandatory = true,
           ValueFromPipeline = true
        )]
        public ExchangeService mailboxConnection
        {
            get { return _exchangeService; }
            set { _exchangeService = value; }
        }

        [Parameter(
           Position = 1,
           Mandatory = false
        )]
        public string FolderName
        {
            get { return _folderName; }
            set { _folderName = value; }
        }
        private string _folderName = "Inbox";

        [Parameter(
           Position = 2,
           Mandatory = false
        )]
        public int maxEmailCount
        {
            get { return _maxEmailCount; }
            set { _maxEmailCount = value; }
        }
        private int _maxEmailCount = 100;

        public enum readMailFilterOptions { All, Unread, Read }
        [Parameter(
           Position = 3,
           Mandatory = false
        )]
        public readMailFilterOptions? readMailFilter
        {
            get;
            set;
        }

        public enum SearchFieldOptions { Subject, From, Body }
        [Parameter(
           Position = 4,
           Mandatory = false,
           HelpMessage = "Field to search on. Default is NoFilter. Valid options\nNoFilter\nSubject\nBody\nFrom"
        )]
        public SearchFieldOptions SearchField
        {
            get;
            set;
        }
        
        [Parameter(
           Position = 5,
           Mandatory = false,
           HelpMessage = "String to search on. Based on Search Algorithm"
        )]
        public string SearchString
        {
            get;
            set;
        }

        public enum SearchAlgorithmOptions { Equals, ContainsString, DoesNotEqual }
        [Parameter(
           Position = 6,
           Mandatory = false,
           HelpMessage = "Algorith to use for searching. Valid Algorithms\nEquals\nContainsString\nDoesNotEqual"
        )]
        public SearchAlgorithmOptions? SearchAlgorithm
        {
            get;
            set;
        }
      
        [Parameter(
           Position = 7,
           Mandatory = false,
           HelpMessage = "Reqeusted body type"
        )]
        public BodyType? RequestedBodyType
        {
            get;
            set;
        }
        [Parameter(
           Position = 8,
           Mandatory = false
        )]
        public SwitchParameter doNotMarkRead
        {
            get;
            set;
        }
        #endregion
        protected override void ProcessRecord()
        {
            SearchFilter.SearchFilterCollection EmailfilterCollection = setupReadMailFilter();
            FolderId folderID = setSearchFolder();
            setupSchemaFiltering(EmailfilterCollection);

            FindItemsResults<Item> findResults = mailboxConnection.FindItems(folderID, EmailfilterCollection, new ItemView(maxEmailCount));

            PropertySet propSet = new PropertySet(BasePropertySet.FirstClassProperties);
            propSet.RequestedBodyType = BodyType.HTML;
            if (RequestedBodyType.HasValue) { propSet.RequestedBodyType = RequestedBodyType.Value; }

            foreach (Item result in findResults)
            {
                processResult(propSet, result);
            }
        }

        private void processResult(PropertySet propSet, Item result)
        {
            switch (result.GetType().ToString())
            {
                case "Microsoft.Exchange.WebServices.Data.EmailMessage":
                    EmailMessage message = EmailMessage.Bind(mailboxConnection, result.Id, propSet);

                    if (!doNotMarkRead)
                    {
                        message.IsRead = true;
                        message.Update(ConflictResolutionMode.AutoResolve);
                    }

                    WriteObject(message);
                    break;
                case "Microsoft.Exchange.WebServices.Data.Appointment":
                    Appointment appointment = Appointment.Bind(mailboxConnection, result.Id, propSet);

                    WriteObject(appointment);
                    break;
                default:
                    WriteObject(result);
                    break;
            }
        }

        private void setupSchemaFiltering(SearchFilter.SearchFilterCollection EmailfilterCollection)
        {
            if (SearchAlgorithm.HasValue)
            {
                SearchFilter EmailFilter;
                switch (SearchAlgorithm)
                {
                    case SearchAlgorithmOptions.Equals:
                        switch (SearchField)
                        {
                            case SearchFieldOptions.Body:
                                EmailFilter = new SearchFilter.IsEqualTo(EmailMessageSchema.Body, SearchString);
                                break;
                            case SearchFieldOptions.From:
                                EmailFilter = new SearchFilter.IsEqualTo(EmailMessageSchema.From, SearchString);
                                break;
                            case SearchFieldOptions.Subject:
                            default:
                                EmailFilter = new SearchFilter.IsEqualTo(EmailMessageSchema.Subject, SearchString);
                                break;
                        }
                        break;
                    case SearchAlgorithmOptions.DoesNotEqual:
                        switch (SearchField)
                        {
                            case SearchFieldOptions.Body:
                                EmailFilter = new SearchFilter.IsNotEqualTo(EmailMessageSchema.Body, SearchString);
                                break;
                            case SearchFieldOptions.From:
                                EmailFilter = new SearchFilter.IsNotEqualTo(EmailMessageSchema.From, SearchString);
                                break;
                            case SearchFieldOptions.Subject:
                            default:
                                EmailFilter = new SearchFilter.IsNotEqualTo(EmailMessageSchema.Subject, SearchString);
                                break;
                        }
                        break;
                    case SearchAlgorithmOptions.ContainsString:
                    default:
                        switch (SearchField)
                        {
                            case SearchFieldOptions.Body:
                                EmailFilter = new SearchFilter.ContainsSubstring(EmailMessageSchema.Body, SearchString);
                                break;
                            case SearchFieldOptions.From:
                                EmailFilter = new SearchFilter.ContainsSubstring(EmailMessageSchema.From, SearchString);
                                break;
                            case SearchFieldOptions.Subject:
                            default:
                                EmailFilter = new SearchFilter.ContainsSubstring(EmailMessageSchema.Subject, SearchString);
                                break;
                        }
                        break;
                }
                EmailfilterCollection.Add(EmailFilter);
            }
        }

        private FolderId setSearchFolder()
        {
            FolderId folderID = new FolderId(WellKnownFolderName.Inbox);
            if (!FolderName.ToLower().Equals("inbox"))
            {
                SearchFilter folderFilter = new SearchFilter.IsEqualTo(FolderSchema.DisplayName, FolderName);
                FolderView view = new FolderView(int.MaxValue);
                view.PropertySet = new PropertySet(BasePropertySet.IdOnly);
                view.PropertySet.Add(FolderSchema.DisplayName);
                view.Traversal = FolderTraversal.Deep;
                FindFoldersResults results = mailboxConnection.FindFolders(WellKnownFolderName.MsgFolderRoot, folderFilter, view);

                foreach (Folder folder in results)
                {
                    folderID = folder.Id;
                    break;
                }
            }
            return folderID;
        }

        private SearchFilter.SearchFilterCollection setupReadMailFilter()
        {
            SearchFilter.SearchFilterCollection EmailfilterCollection = new SearchFilter.SearchFilterCollection(LogicalOperator.And);
            SearchFilter.SearchFilterCollection readMailFilterCollection = new SearchFilter.SearchFilterCollection(LogicalOperator.Or);
            if (readMailFilter.HasValue)
            {
                switch (readMailFilter.Value)
                {
                    case readMailFilterOptions.All:

                        readMailFilterCollection.Add(new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, true));
                        readMailFilterCollection.Add(new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, false));
                        break;
                    case readMailFilterOptions.Unread:
                    default:
                        readMailFilterCollection.Add(new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, false));
                        break;
                    case readMailFilterOptions.Read:
                        readMailFilterCollection.Add(new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, true));
                        break;
                }
            }
            else
            {
                readMailFilterCollection.Add(new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, false));
            }
            EmailfilterCollection.Add(readMailFilterCollection);
            return EmailfilterCollection;
        }
    }
}
