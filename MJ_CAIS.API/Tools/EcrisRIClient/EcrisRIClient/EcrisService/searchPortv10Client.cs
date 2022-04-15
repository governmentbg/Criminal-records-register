using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcrisRIClient.EcrisService
{

    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ServiceModel.ServiceContractAttribute(Name = "searchPort-v1.0", Namespace = "http://ec.europa.eu/ECRIS-RI/contract", ConfigurationName = " searchPortv10")]
    public interface searchPortv10
    {

        [System.ServiceModel.OperationContractAttribute(Action = "http://ec.europa.eu/ECRIS-RI/search/performSearch", ReplyAction = "*")]
        [System.ServiceModel.FaultContractAttribute(typeof(ServerErrorFaultType), Action = "http://ec.europa.eu/ECRIS-RI/search/performSearch", Name = "ServerErrorFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(AuthenticationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/search/performSearch", Name = "AuthenticationFaultMessage")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StatisticsPeriodType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DateType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PositiveDecimalType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractRelationshipType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EntityType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UpdateEventsStateWSDataInputType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetEventsWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetEventsWSDataInputType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ManageEventTypesConnectionsToUserRolesWSDataInputType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MailServerConnectionStatusWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetBuildNumberWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SetSystemMaintenanceModeWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ValidateConfigurationFilesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ValidateConfigurationFilesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UploadConfigurationFilesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UploadConfigurationFilesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ResetPasswordWsInput))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PerformFunctionalValidationWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PerformFunctionalValidationWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SetSystemAvailabilityWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ListBackendLogsWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ListBackendLogsWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SystemAvailabilityWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MoveDownRuleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MoveUpRuleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DeleteRuleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreRuleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ListAssignedFolderRolesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ListAssignedFolderRolesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AssignFolderRolesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RuleListWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UserListWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RuleListWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UserListWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DisableUserWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EnableUserWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RemoveUserWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UserWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UserWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RemoveUserRoleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreRoleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveAvailableRolesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveAvailableRolesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MemberStateCodeContainingWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveMemberStateEndpointListWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetTransactionForMessageWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetTransactionForMessageWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CalculateNextExportDeadlineWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DownloadStatisticsWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DownloadStatisticsWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AdministrativeMessageWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveStoredQueriesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveStoredQueryWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreOrSendMessageWithCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SendMessageZipWithCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SendMessageWithCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreMessageZipWithCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreMessageWithCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DeleteMessageCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SetMessageCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetMessageCommentWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetMessageCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SearchWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractDateQueryParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SearchWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ExportReferenceTablesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ReadNistWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ReadMessageWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EcrisRiIdentifierContainingMessageWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreMessageWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreFolderWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RemoveMessagesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RemoveMessagesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DeleteFolderWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MoveMessagesToFolderWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetMessagesForFolderWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetMessagesForFolderWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(FindMessageInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractPeriodType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractNationalCategoriesSupportingEntityType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StringType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractNameType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractPersonType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(IdentifiableMessageType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(FindMessageByIdentifierWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(FindMessageByIdentifierWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetFoldersWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveAuditLogWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveAuditLogWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(LoginWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(LoginWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BaseEcrisRiWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractFaultType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractWSType))]
        System.Threading.Tasks.Task<performSearchResponse> performSearchAsync(performSearchRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ec.europa.eu/ECRIS-RI/search/storeQuery", ReplyAction = "*")]
        [System.ServiceModel.FaultContractAttribute(typeof(ServerErrorFaultType), Action = "http://ec.europa.eu/ECRIS-RI/search/storeQuery", Name = "ServerErrorFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(AuthenticationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/search/storeQuery", Name = "AuthenticationFaultMessage")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StatisticsPeriodType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DateType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PositiveDecimalType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractRelationshipType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EntityType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UpdateEventsStateWSDataInputType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetEventsWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetEventsWSDataInputType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ManageEventTypesConnectionsToUserRolesWSDataInputType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MailServerConnectionStatusWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetBuildNumberWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SetSystemMaintenanceModeWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ValidateConfigurationFilesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ValidateConfigurationFilesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UploadConfigurationFilesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UploadConfigurationFilesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ResetPasswordWsInput))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PerformFunctionalValidationWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PerformFunctionalValidationWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SetSystemAvailabilityWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ListBackendLogsWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ListBackendLogsWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SystemAvailabilityWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MoveDownRuleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MoveUpRuleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DeleteRuleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreRuleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ListAssignedFolderRolesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ListAssignedFolderRolesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AssignFolderRolesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RuleListWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UserListWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RuleListWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UserListWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DisableUserWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EnableUserWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RemoveUserWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UserWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UserWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RemoveUserRoleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreRoleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveAvailableRolesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveAvailableRolesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MemberStateCodeContainingWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveMemberStateEndpointListWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetTransactionForMessageWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetTransactionForMessageWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CalculateNextExportDeadlineWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DownloadStatisticsWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DownloadStatisticsWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AdministrativeMessageWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveStoredQueriesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveStoredQueryWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreOrSendMessageWithCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SendMessageZipWithCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SendMessageWithCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreMessageZipWithCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreMessageWithCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DeleteMessageCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SetMessageCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetMessageCommentWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetMessageCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SearchWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractDateQueryParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SearchWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ExportReferenceTablesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ReadNistWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ReadMessageWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EcrisRiIdentifierContainingMessageWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreMessageWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreFolderWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RemoveMessagesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RemoveMessagesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DeleteFolderWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MoveMessagesToFolderWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetMessagesForFolderWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetMessagesForFolderWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(FindMessageInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractPeriodType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractNationalCategoriesSupportingEntityType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StringType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractNameType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractPersonType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(IdentifiableMessageType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(FindMessageByIdentifierWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(FindMessageByIdentifierWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetFoldersWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveAuditLogWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveAuditLogWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(LoginWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(LoginWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BaseEcrisRiWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractFaultType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractWSType))]
        System.Threading.Tasks.Task<storeQueryResponse> storeQueryAsync(storeQueryRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ec.europa.eu/ECRIS-RI/search/retrieveStoredQueries", ReplyAction = "*")]
        [System.ServiceModel.FaultContractAttribute(typeof(ServerErrorFaultType), Action = "http://ec.europa.eu/ECRIS-RI/search/retrieveStoredQueries", Name = "ServerErrorFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(AuthenticationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/search/retrieveStoredQueries", Name = "AuthenticationFaultMessage")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StatisticsPeriodType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DateType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PositiveDecimalType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractRelationshipType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EntityType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UpdateEventsStateWSDataInputType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetEventsWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetEventsWSDataInputType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ManageEventTypesConnectionsToUserRolesWSDataInputType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MailServerConnectionStatusWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetBuildNumberWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SetSystemMaintenanceModeWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ValidateConfigurationFilesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ValidateConfigurationFilesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UploadConfigurationFilesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UploadConfigurationFilesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ResetPasswordWsInput))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PerformFunctionalValidationWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PerformFunctionalValidationWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SetSystemAvailabilityWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ListBackendLogsWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ListBackendLogsWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SystemAvailabilityWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MoveDownRuleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MoveUpRuleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DeleteRuleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreRuleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ListAssignedFolderRolesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ListAssignedFolderRolesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AssignFolderRolesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RuleListWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UserListWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RuleListWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UserListWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DisableUserWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EnableUserWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RemoveUserWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UserWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UserWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RemoveUserRoleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreRoleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveAvailableRolesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveAvailableRolesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MemberStateCodeContainingWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveMemberStateEndpointListWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetTransactionForMessageWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetTransactionForMessageWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CalculateNextExportDeadlineWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DownloadStatisticsWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DownloadStatisticsWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AdministrativeMessageWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveStoredQueriesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveStoredQueryWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreOrSendMessageWithCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SendMessageZipWithCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SendMessageWithCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreMessageZipWithCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreMessageWithCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DeleteMessageCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SetMessageCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetMessageCommentWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetMessageCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SearchWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractDateQueryParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SearchWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ExportReferenceTablesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ReadNistWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ReadMessageWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EcrisRiIdentifierContainingMessageWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreMessageWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreFolderWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RemoveMessagesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RemoveMessagesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DeleteFolderWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MoveMessagesToFolderWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetMessagesForFolderWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetMessagesForFolderWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(FindMessageInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractPeriodType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractNationalCategoriesSupportingEntityType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StringType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractNameType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractPersonType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(IdentifiableMessageType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(FindMessageByIdentifierWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(FindMessageByIdentifierWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetFoldersWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveAuditLogWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveAuditLogWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(LoginWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(LoginWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BaseEcrisRiWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractFaultType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractWSType))]
        System.Threading.Tasks.Task<retrieveStoredQueriesResponse> retrieveStoredQueriesAsync( retrieveStoredQueriesRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ec.europa.eu/ECRIS-RI/search/deleteStoredQueries", ReplyAction = "*")]
        [System.ServiceModel.FaultContractAttribute(typeof(ServerErrorFaultType), Action = "http://ec.europa.eu/ECRIS-RI/search/deleteStoredQueries", Name = "ServerErrorFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(AuthenticationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/search/deleteStoredQueries", Name = "AuthenticationFaultMessage")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StatisticsPeriodType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DateType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PositiveDecimalType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractRelationshipType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EntityType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UpdateEventsStateWSDataInputType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetEventsWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetEventsWSDataInputType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ManageEventTypesConnectionsToUserRolesWSDataInputType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MailServerConnectionStatusWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetBuildNumberWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SetSystemMaintenanceModeWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ValidateConfigurationFilesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ValidateConfigurationFilesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UploadConfigurationFilesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UploadConfigurationFilesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ResetPasswordWsInput))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PerformFunctionalValidationWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PerformFunctionalValidationWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SetSystemAvailabilityWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ListBackendLogsWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ListBackendLogsWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SystemAvailabilityWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MoveDownRuleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MoveUpRuleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DeleteRuleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreRuleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ListAssignedFolderRolesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ListAssignedFolderRolesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AssignFolderRolesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RuleListWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UserListWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RuleListWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UserListWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DisableUserWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EnableUserWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RemoveUserWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UserWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UserWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RemoveUserRoleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreRoleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveAvailableRolesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveAvailableRolesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MemberStateCodeContainingWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveMemberStateEndpointListWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetTransactionForMessageWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetTransactionForMessageWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CalculateNextExportDeadlineWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DownloadStatisticsWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DownloadStatisticsWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AdministrativeMessageWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveStoredQueriesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveStoredQueryWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreOrSendMessageWithCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SendMessageZipWithCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SendMessageWithCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreMessageZipWithCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreMessageWithCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DeleteMessageCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SetMessageCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetMessageCommentWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetMessageCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SearchWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractDateQueryParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SearchWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ExportReferenceTablesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ReadNistWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ReadMessageWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EcrisRiIdentifierContainingMessageWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreMessageWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreFolderWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RemoveMessagesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RemoveMessagesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DeleteFolderWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MoveMessagesToFolderWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetMessagesForFolderWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetMessagesForFolderWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(FindMessageInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractPeriodType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractNationalCategoriesSupportingEntityType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StringType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractNameType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractPersonType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(IdentifiableMessageType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(FindMessageByIdentifierWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(FindMessageByIdentifierWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetFoldersWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveAuditLogWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveAuditLogWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(LoginWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(LoginWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BaseEcrisRiWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractFaultType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractWSType))]
        System.Threading.Tasks.Task<storeQueryResponse> deleteStoredQueriesAsync(deleteStoredQueriesRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ec.europa.eu/ECRIS-RI/search/retrieveStoredQuery", ReplyAction = "*")]
        [System.ServiceModel.FaultContractAttribute(typeof(ServerErrorFaultType), Action = "http://ec.europa.eu/ECRIS-RI/search/retrieveStoredQuery", Name = "ServerErrorFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(AuthenticationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/search/retrieveStoredQuery", Name = "AuthenticationFaultMessage")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StatisticsPeriodType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DateType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PositiveDecimalType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractRelationshipType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EntityType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UpdateEventsStateWSDataInputType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetEventsWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetEventsWSDataInputType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ManageEventTypesConnectionsToUserRolesWSDataInputType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MailServerConnectionStatusWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetBuildNumberWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SetSystemMaintenanceModeWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ValidateConfigurationFilesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ValidateConfigurationFilesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UploadConfigurationFilesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UploadConfigurationFilesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ResetPasswordWsInput))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PerformFunctionalValidationWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PerformFunctionalValidationWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SetSystemAvailabilityWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ListBackendLogsWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ListBackendLogsWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SystemAvailabilityWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MoveDownRuleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MoveUpRuleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DeleteRuleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreRuleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ListAssignedFolderRolesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ListAssignedFolderRolesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AssignFolderRolesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RuleListWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UserListWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RuleListWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UserListWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DisableUserWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EnableUserWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RemoveUserWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UserWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UserWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RemoveUserRoleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreRoleWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveAvailableRolesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveAvailableRolesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MemberStateCodeContainingWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveMemberStateEndpointListWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetTransactionForMessageWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetTransactionForMessageWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CalculateNextExportDeadlineWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DownloadStatisticsWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DownloadStatisticsWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AdministrativeMessageWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveStoredQueriesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveStoredQueryWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreOrSendMessageWithCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SendMessageZipWithCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SendMessageWithCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreMessageZipWithCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreMessageWithCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DeleteMessageCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SetMessageCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetMessageCommentWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetMessageCommentWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SearchWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractDateQueryParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SearchWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ExportReferenceTablesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ReadNistWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ReadMessageWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EcrisRiIdentifierContainingMessageWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreMessageWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StoreFolderWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RemoveMessagesWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RemoveMessagesWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DeleteFolderWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MoveMessagesToFolderWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetMessagesForFolderWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetMessagesForFolderWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(FindMessageInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractPeriodType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractNationalCategoriesSupportingEntityType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StringType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractNameType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractPersonType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(IdentifiableMessageType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(FindMessageByIdentifierWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(FindMessageByIdentifierWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetFoldersWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveAuditLogWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RetrieveAuditLogWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(LoginWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(LoginWSInputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BaseEcrisRiWSOutputDataType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractFaultType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractWSType))]
        System.Threading.Tasks.Task<retrieveStoredQueriesResponse> retrieveStoredQueryAsync(retrieveStoredQueryRequest request);
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class performSearchRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/search-v1.0", Order = 0)]
        public SearchWSInputType PerformSearchWSInput;

        public performSearchRequest()
        {
        }

        public performSearchRequest(SearchWSInputType PerformSearchWSInput)
        {
            this.PerformSearchWSInput = PerformSearchWSInput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class performSearchResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/search-v1.0", Order = 0)]
        public SearchWSOutputType PerformSearchWSOutput;

        public performSearchResponse()
        {
        }

        public performSearchResponse(SearchWSOutputType PerformSearchWSOutput)
        {
            this.PerformSearchWSOutput = PerformSearchWSOutput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class storeQueryRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/search-v1.0", Order = 0)]
        public SearchWSInputType StoreQueryWSInput;

        public storeQueryRequest()
        {
        }

        public storeQueryRequest(SearchWSInputType StoreQueryWSInput)
        {
            this.StoreQueryWSInput = StoreQueryWSInput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class storeQueryResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/contract", Order = 0)]
        public BaseEcrisRiWSOutputType BaseEcrisRiWSOutput;

        public storeQueryResponse()
        {
        }

        public storeQueryResponse(BaseEcrisRiWSOutputType BaseEcrisRiWSOutput)
        {
            this.BaseEcrisRiWSOutput = BaseEcrisRiWSOutput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class retrieveStoredQueriesRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/contract", Order = 0)]
        public BaseEcrisRiWSInputType BaseEcrisRiWSInput;

        public retrieveStoredQueriesRequest()
        {
        }

        public retrieveStoredQueriesRequest(BaseEcrisRiWSInputType BaseEcrisRiWSInput)
        {
            this.BaseEcrisRiWSInput = BaseEcrisRiWSInput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class retrieveStoredQueriesResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/search-v1.0", Order = 0)]
        public RetrieveStoredQueriesWSOutputType RetrieveStoredQueriesWSOutput;

        public retrieveStoredQueriesResponse()
        {
        }

        public retrieveStoredQueriesResponse(RetrieveStoredQueriesWSOutputType RetrieveStoredQueriesWSOutput)
        {
            this.RetrieveStoredQueriesWSOutput = RetrieveStoredQueriesWSOutput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class deleteStoredQueriesRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/search-v1.0", Order = 0)]
        public DeleteStoredQueriesWSInputType DeleteStoredQueriesWSInput;

        public deleteStoredQueriesRequest()
        {
        }

        public deleteStoredQueriesRequest(DeleteStoredQueriesWSInputType DeleteStoredQueriesWSInput)
        {
            this.DeleteStoredQueriesWSInput = DeleteStoredQueriesWSInput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class retrieveStoredQueryRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/search-v1.0", Order = 0)]
        public RetrieveStoredQueryWSInputType RetrieveStoredQueryWSInput;

        public retrieveStoredQueryRequest()
        {
        }

        public retrieveStoredQueryRequest(RetrieveStoredQueryWSInputType RetrieveStoredQueryWSInput)
        {
            this.RetrieveStoredQueryWSInput = RetrieveStoredQueryWSInput;
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    public interface searchPortv10Channel : searchPortv10, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    public partial class searchPortv10Client : System.ServiceModel.ClientBase<searchPortv10>, searchPortv10
    {

        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);

        public searchPortv10Client() :
                base(searchPortv10Client.GetDefaultBinding(), searchPortv10Client.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.searchPort.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }

        public searchPortv10Client(EndpointConfiguration endpointConfiguration) :
                base(searchPortv10Client.GetBindingForEndpoint(endpointConfiguration), searchPortv10Client.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }

        public searchPortv10Client(EndpointConfiguration endpointConfiguration, string remoteAddress) :
                base(searchPortv10Client.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }

        public searchPortv10Client(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) :
                base(searchPortv10Client.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }

        public searchPortv10Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
                base(binding, remoteAddress)
        {
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task< performSearchResponse>  searchPortv10.performSearchAsync( performSearchRequest request)
        {
            return base.Channel.performSearchAsync(request);
        }

        public System.Threading.Tasks.Task< performSearchResponse> performSearchAsync( SearchWSInputType PerformSearchWSInput)
        {
             performSearchRequest inValue = new  performSearchRequest();
            inValue.PerformSearchWSInput = PerformSearchWSInput;
            return (( searchPortv10)(this)).performSearchAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task< storeQueryResponse>  searchPortv10.storeQueryAsync( storeQueryRequest request)
        {
            return base.Channel.storeQueryAsync(request);
        }

        public System.Threading.Tasks.Task< storeQueryResponse> storeQueryAsync( SearchWSInputType StoreQueryWSInput)
        {
             storeQueryRequest inValue = new  storeQueryRequest();
            inValue.StoreQueryWSInput = StoreQueryWSInput;
            return (( searchPortv10)(this)).storeQueryAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task< retrieveStoredQueriesResponse>  searchPortv10.retrieveStoredQueriesAsync( retrieveStoredQueriesRequest request)
        {
            return base.Channel.retrieveStoredQueriesAsync(request);
        }

        public System.Threading.Tasks.Task< retrieveStoredQueriesResponse> retrieveStoredQueriesAsync( BaseEcrisRiWSInputType BaseEcrisRiWSInput)
        {
             retrieveStoredQueriesRequest inValue = new  retrieveStoredQueriesRequest();
            inValue.BaseEcrisRiWSInput = BaseEcrisRiWSInput;
            return (( searchPortv10)(this)).retrieveStoredQueriesAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task< storeQueryResponse>  searchPortv10.deleteStoredQueriesAsync( deleteStoredQueriesRequest request)
        {
            return base.Channel.deleteStoredQueriesAsync(request);
        }

        public System.Threading.Tasks.Task< storeQueryResponse> deleteStoredQueriesAsync( DeleteStoredQueriesWSInputType DeleteStoredQueriesWSInput)
        {
             deleteStoredQueriesRequest inValue = new  deleteStoredQueriesRequest();
            inValue.DeleteStoredQueriesWSInput = DeleteStoredQueriesWSInput;
            return (( searchPortv10)(this)).deleteStoredQueriesAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task< retrieveStoredQueriesResponse>  searchPortv10.retrieveStoredQueryAsync( retrieveStoredQueryRequest request)
        {
            return base.Channel.retrieveStoredQueryAsync(request);
        }

        public System.Threading.Tasks.Task< retrieveStoredQueriesResponse> retrieveStoredQueryAsync( RetrieveStoredQueryWSInputType RetrieveStoredQueryWSInput)
        {
             retrieveStoredQueryRequest inValue = new  retrieveStoredQueryRequest();
            inValue.RetrieveStoredQueryWSInput = RetrieveStoredQueryWSInput;
            return (( searchPortv10)(this)).retrieveStoredQueryAsync(inValue);
        }

        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }

        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }

        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.searchPort))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpTransportBindingElement httpBindingElement = new System.ServiceModel.Channels.HttpTransportBindingElement();
                httpBindingElement.AllowCookies = true;
                httpBindingElement.MaxBufferSize = int.MaxValue;
                httpBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }

        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.searchPort))
            {
                return new System.ServiceModel.EndpointAddress("http://172.16.0.101/ecris-ri-backend/s/search");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }

        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return searchPortv10Client.GetBindingForEndpoint(EndpointConfiguration.searchPort);
        }

        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return searchPortv10Client.GetEndpointAddress(EndpointConfiguration.searchPort);
        }

        public enum EndpointConfiguration
        {

            searchPort,
        }
    }


}
