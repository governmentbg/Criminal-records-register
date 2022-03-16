namespace EcrisRIClient.EcrisService
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ServiceModel.ServiceContractAttribute(Name = "storagePort-v1.0", Namespace = "http://ec.europa.eu/ECRIS-RI/contract", ConfigurationName = "EcrisRiMessageStorageService.storagePortv10")]
    public interface storagePortv10
    {
        [System.ServiceModel.OperationContractAttribute(Action = "http://ec.europa.eu/ECRIS-RI/message-storage/getFolders", ReplyAction = "*")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.ServerErrorFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/getFolders", Name = "ServerErrorFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.AuthenticationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/getFolders", Name = "AuthenticationFaultMessage")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EntityType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StatisticsPeriodType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DateType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PositiveDecimalType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractRelationshipType1))]
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
        System.Threading.Tasks.Task<EcrisService.getFoldersResponse> getFoldersAsync(EcrisService.getFoldersRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ec.europa.eu/ECRIS-RI/message-storage/findMessageByIdentifier", ReplyAction = "*")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.ServerErrorFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/findMessageByIdentifier", Name = "ServerErrorFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.AuthenticationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/findMessageByIdentifier", Name = "AuthenticationFaultMessage")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EntityType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StatisticsPeriodType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DateType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PositiveDecimalType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractRelationshipType1))]
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
        System.Threading.Tasks.Task<EcrisService.findMessageByIdentifierResponse> findMessageByIdentifierAsync(EcrisService.findMessageByIdentifierRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ec.europa.eu/ECRIS-RI/message-storage/findMessageByEcrisIdentifier", ReplyAction = "*")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.ServerErrorFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/findMessageByEcrisIdentifier", Name = "ServerErrorFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.AuthenticationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/findMessageByEcrisIdentifier", Name = "AuthenticationFaultMessage")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EntityType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StatisticsPeriodType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DateType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PositiveDecimalType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractRelationshipType1))]
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
        System.Threading.Tasks.Task<EcrisService.findMessageByEcrisIdentifierResponse> findMessageByEcrisIdentifierAsync(EcrisService.findMessageByEcrisIdentifierRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ec.europa.eu/ECRIS-RI/message-storage/findAccessibleMessageByIdentifier", ReplyAction = "*")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.ServerErrorFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/findAccessibleMessageByIdentifier", Name = "ServerErrorFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.AuthenticationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/findAccessibleMessageByIdentifier", Name = "AuthenticationFaultMessage")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EntityType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StatisticsPeriodType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DateType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PositiveDecimalType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractRelationshipType1))]
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
        System.Threading.Tasks.Task<EcrisService.findAccessibleMessageByIdentifierResponse> findAccessibleMessageByIdentifierAsync(EcrisService.findAccessibleMessageByIdentifierRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ec.europa.eu/ECRIS-RI/message-storage/getMessagesForFolder", ReplyAction = "*")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.ServerErrorFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/getMessagesForFolder", Name = "ServerErrorFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.AuthenticationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/getMessagesForFolder", Name = "AuthenticationFaultMessage")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EntityType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StatisticsPeriodType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DateType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PositiveDecimalType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractRelationshipType1))]
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
        System.Threading.Tasks.Task<EcrisService.getMessagesForFolderResponse> getMessagesForFolderAsync(EcrisService.getMessagesForFolderRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ec.europa.eu/ECRIS-RI/message-storage/moveMessagesToFolder", ReplyAction = "*")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.ServerErrorFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/moveMessagesToFolder", Name = "ServerErrorFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.OperationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/moveMessagesToFolder", Name = "OperationFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.AuthenticationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/moveMessagesToFolder", Name = "AuthenticationFaultMessage")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EntityType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StatisticsPeriodType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DateType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PositiveDecimalType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractRelationshipType1))]
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
        System.Threading.Tasks.Task<EcrisService.moveMessagesToFolderResponse> moveMessagesToFolderAsync(EcrisService.moveMessagesToFolderRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ec.europa.eu/ECRIS-RI/message-storage/deleteFolder", ReplyAction = "*")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.ServerErrorFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/deleteFolder", Name = "ServerErrorFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.OperationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/deleteFolder", Name = "OperationFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.AuthenticationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/deleteFolder", Name = "AuthenticationFaultMessage")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EntityType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StatisticsPeriodType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DateType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PositiveDecimalType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractRelationshipType1))]
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
        System.Threading.Tasks.Task<EcrisService.moveMessagesToFolderResponse> deleteFolderAsync(EcrisService.deleteFolderRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ec.europa.eu/ECRIS-RI/message-storage/removeMessages", ReplyAction = "*")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.ServerErrorFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/removeMessages", Name = "ServerErrorFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.AuthenticationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/removeMessages", Name = "AuthenticationFaultMessage")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EntityType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StatisticsPeriodType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DateType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PositiveDecimalType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractRelationshipType1))]
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
        System.Threading.Tasks.Task<EcrisService.removeMessagesResponse> removeMessagesAsync(EcrisService.removeMessagesRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ec.europa.eu/ECRIS-RI/message-storage/storeFolder", ReplyAction = "*")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.ServerErrorFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/storeFolder", Name = "ServerErrorFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.OperationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/storeFolder", Name = "OperationFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.AuthenticationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/storeFolder", Name = "AuthenticationFaultMessage")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EntityType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StatisticsPeriodType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DateType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PositiveDecimalType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractRelationshipType1))]
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
        System.Threading.Tasks.Task<EcrisService.moveMessagesToFolderResponse> storeFolderAsync(EcrisService.storeFolderRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ec.europa.eu/ECRIS-RI/message-storage/storeMessage", ReplyAction = "*")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.ServerErrorFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/storeMessage", Name = "ServerErrorFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.OperationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/storeMessage", Name = "OperationFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.AuthenticationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/storeMessage", Name = "AuthenticationFaultMessage")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EntityType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StatisticsPeriodType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DateType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PositiveDecimalType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractRelationshipType1))]
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
        System.Threading.Tasks.Task<EcrisService.storeMessageResponse> storeMessageAsync(EcrisService.storeMessageRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ec.europa.eu/ECRIS-RI/message-storage/storeMessageZip", ReplyAction = "*")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.ServerErrorFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/storeMessageZip", Name = "ServerErrorFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.OperationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/storeMessageZip", Name = "OperationFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.AuthenticationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/storeMessageZip", Name = "AuthenticationFaultMessage")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EntityType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StatisticsPeriodType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DateType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PositiveDecimalType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractRelationshipType1))]
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
        System.Threading.Tasks.Task<EcrisService.storeMessageResponse> storeMessageZipAsync(EcrisService.storeMessageZipRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ec.europa.eu/ECRIS-RI/message-storage/readMessage", ReplyAction = "*")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.ServerErrorFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/readMessage", Name = "ServerErrorFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.OperationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/readMessage", Name = "OperationFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.AuthenticationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/readMessage", Name = "AuthenticationFaultMessage")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EntityType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StatisticsPeriodType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DateType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PositiveDecimalType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractRelationshipType1))]
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
        System.Threading.Tasks.Task<EcrisService.readMessageResponse> readMessageAsync(EcrisService.readMessageRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ec.europa.eu/ECRIS-RI/message-storage/readNist", ReplyAction = "*")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.ServerErrorFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/readNist", Name = "ServerErrorFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.AuthenticationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/readNist", Name = "AuthenticationFaultMessage")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EntityType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StatisticsPeriodType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DateType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PositiveDecimalType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractRelationshipType1))]
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
        System.Threading.Tasks.Task<EcrisService.readNistResponse> readNistAsync(EcrisService.readNistRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ec.europa.eu/ECRIS-RI/message-storage/deleteNist", ReplyAction = "*")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.ServerErrorFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/deleteNist", Name = "ServerErrorFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.AuthenticationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/message-storage/deleteNist", Name = "AuthenticationFaultMessage")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EntityType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StatisticsPeriodType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DateType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PositiveDecimalType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractRelationshipType1))]
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
        System.Threading.Tasks.Task<EcrisService.deleteNistResponse> deleteNistAsync(EcrisService.deleteNistRequest request);
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class getFoldersRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/contract", Order = 0)]
        public EcrisService.BaseEcrisRiWSInputType BaseEcrisRiWSInput;

        public getFoldersRequest()
        {
        }

        public getFoldersRequest(EcrisService.BaseEcrisRiWSInputType BaseEcrisRiWSInput)
        {
            this.BaseEcrisRiWSInput = BaseEcrisRiWSInput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class getFoldersResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/message-storage-v1.0", Order = 0)]
        public EcrisService.GetFoldersWSOutputType GetFoldersWSOutput;

        public getFoldersResponse()
        {
        }

        public getFoldersResponse(EcrisService.GetFoldersWSOutputType GetFoldersWSOutput)
        {
            this.GetFoldersWSOutput = GetFoldersWSOutput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class findMessageByIdentifierRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/message-storage-v1.0", Order = 0)]
        public EcrisService.FindMessageByIdentifierWSInputType FindMessageByIdentifierWSInput;

        public findMessageByIdentifierRequest()
        {
        }

        public findMessageByIdentifierRequest(EcrisService.FindMessageByIdentifierWSInputType FindMessageByIdentifierWSInput)
        {
            this.FindMessageByIdentifierWSInput = FindMessageByIdentifierWSInput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class findMessageByIdentifierResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/message-storage-v1.0", Order = 0)]
        public EcrisService.FindMessageByIdentifierWSOutputType FindMessageByIdentifierWSOutput;

        public findMessageByIdentifierResponse()
        {
        }

        public findMessageByIdentifierResponse(EcrisService.FindMessageByIdentifierWSOutputType FindMessageByIdentifierWSOutput)
        {
            this.FindMessageByIdentifierWSOutput = FindMessageByIdentifierWSOutput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class findMessageByEcrisIdentifierRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/message-storage-v1.0", Order = 0)]
        public EcrisService.FindMessageByEcrisIdentifierWSInputType FindMessageByEcrisIdentifierWSInput;

        public findMessageByEcrisIdentifierRequest()
        {
        }

        public findMessageByEcrisIdentifierRequest(EcrisService.FindMessageByEcrisIdentifierWSInputType FindMessageByEcrisIdentifierWSInput)
        {
            this.FindMessageByEcrisIdentifierWSInput = FindMessageByEcrisIdentifierWSInput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class findMessageByEcrisIdentifierResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/message-storage-v1.0", Order = 0)]
        public EcrisService.FindMessageByEcrisIdentifierWSOutputType FindMessageByEcrisIdentifierWSOutput;

        public findMessageByEcrisIdentifierResponse()
        {
        }

        public findMessageByEcrisIdentifierResponse(EcrisService.FindMessageByEcrisIdentifierWSOutputType FindMessageByEcrisIdentifierWSOutput)
        {
            this.FindMessageByEcrisIdentifierWSOutput = FindMessageByEcrisIdentifierWSOutput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class findAccessibleMessageByIdentifierRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/message-storage-v1.0", Order = 0)]
        public EcrisService.FindAccessibleMessageByIdentifierWSInputType FindAccessibleMessageByIdentifierWSInput;

        public findAccessibleMessageByIdentifierRequest()
        {
        }

        public findAccessibleMessageByIdentifierRequest(EcrisService.FindAccessibleMessageByIdentifierWSInputType FindAccessibleMessageByIdentifierWSInput)
        {
            this.FindAccessibleMessageByIdentifierWSInput = FindAccessibleMessageByIdentifierWSInput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class findAccessibleMessageByIdentifierResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/message-storage-v1.0", Order = 0)]
        public EcrisService.FindAccessibleMessageByIdentifierWSOutputType FindAccessibleMessageByIdentifierWSOutput;

        public findAccessibleMessageByIdentifierResponse()
        {
        }

        public findAccessibleMessageByIdentifierResponse(EcrisService.FindAccessibleMessageByIdentifierWSOutputType FindAccessibleMessageByIdentifierWSOutput)
        {
            this.FindAccessibleMessageByIdentifierWSOutput = FindAccessibleMessageByIdentifierWSOutput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class getMessagesForFolderRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/message-storage-v1.0", Order = 0)]
        public EcrisService.GetMessagesForFolderWSInputType GetMessagesForFolderWSInput;

        public getMessagesForFolderRequest()
        {
        }

        public getMessagesForFolderRequest(EcrisService.GetMessagesForFolderWSInputType GetMessagesForFolderWSInput)
        {
            this.GetMessagesForFolderWSInput = GetMessagesForFolderWSInput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class getMessagesForFolderResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/message-storage-v1.0", Order = 0)]
        public EcrisService.GetMessagesForFolderWSOutputType GetMessagesForFolderWSOutput;

        public getMessagesForFolderResponse()
        {
        }

        public getMessagesForFolderResponse(EcrisService.GetMessagesForFolderWSOutputType GetMessagesForFolderWSOutput)
        {
            this.GetMessagesForFolderWSOutput = GetMessagesForFolderWSOutput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class moveMessagesToFolderRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/message-storage-v1.0", Order = 0)]
        public EcrisService.MoveMessagesToFolderWSInputType MoveMessagesToFolderWSInput;

        public moveMessagesToFolderRequest()
        {
        }

        public moveMessagesToFolderRequest(EcrisService.MoveMessagesToFolderWSInputType MoveMessagesToFolderWSInput)
        {
            this.MoveMessagesToFolderWSInput = MoveMessagesToFolderWSInput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class moveMessagesToFolderResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/contract", Order = 0)]
        public EcrisService.BaseEcrisRiWSOutputType BaseEcrisRiWSOutput;

        public moveMessagesToFolderResponse()
        {
        }

        public moveMessagesToFolderResponse(EcrisService.BaseEcrisRiWSOutputType BaseEcrisRiWSOutput)
        {
            this.BaseEcrisRiWSOutput = BaseEcrisRiWSOutput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class deleteFolderRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/message-storage-v1.0", Order = 0)]
        public EcrisService.DeleteFolderWSInputType DeleteFolderWSInput;

        public deleteFolderRequest()
        {
        }

        public deleteFolderRequest(EcrisService.DeleteFolderWSInputType DeleteFolderWSInput)
        {
            this.DeleteFolderWSInput = DeleteFolderWSInput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class removeMessagesRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/message-storage-v1.0", Order = 0)]
        public EcrisService.RemoveMessagesWSInputType RemoveMessagesWSInput;

        public removeMessagesRequest()
        {
        }

        public removeMessagesRequest(EcrisService.RemoveMessagesWSInputType RemoveMessagesWSInput)
        {
            this.RemoveMessagesWSInput = RemoveMessagesWSInput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class removeMessagesResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/message-storage-v1.0", Order = 0)]
        public EcrisService.RemoveMessagesWSOutputType RemoveMessagesWSOutput;

        public removeMessagesResponse()
        {
        }

        public removeMessagesResponse(EcrisService.RemoveMessagesWSOutputType RemoveMessagesWSOutput)
        {
            this.RemoveMessagesWSOutput = RemoveMessagesWSOutput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class storeFolderRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/message-storage-v1.0", Order = 0)]
        public EcrisService.StoreFolderWSInputType StoreFolderWSInput;

        public storeFolderRequest()
        {
        }

        public storeFolderRequest(EcrisService.StoreFolderWSInputType StoreFolderWSInput)
        {
            this.StoreFolderWSInput = StoreFolderWSInput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class storeMessageRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/message-storage-v1.0", Order = 0)]
        public EcrisService.StoreMessageWSInputType StoreMessageWSInput;

        public storeMessageRequest()
        {
        }

        public storeMessageRequest(EcrisService.StoreMessageWSInputType StoreMessageWSInput)
        {
            this.StoreMessageWSInput = StoreMessageWSInput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class storeMessageResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/message-storage-v1.0", Order = 0)]
        public EcrisService.StoreMessageWSOutputType StoreMessageWSOutput;

        public storeMessageResponse()
        {
        }

        public storeMessageResponse(EcrisService.StoreMessageWSOutputType StoreMessageWSOutput)
        {
            this.StoreMessageWSOutput = StoreMessageWSOutput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class storeMessageZipRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/message-storage-v1.0", Order = 0)]
        public EcrisService.StoreMessageZipWSInputType StoreMessageZipWSInput;

        public storeMessageZipRequest()
        {
        }

        public storeMessageZipRequest(EcrisService.StoreMessageZipWSInputType StoreMessageZipWSInput)
        {
            this.StoreMessageZipWSInput = StoreMessageZipWSInput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class readMessageRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/message-storage-v1.0", Order = 0)]
        public EcrisService.EcrisRiIdentifierContainingWSInputType ReadMessageWSInput;

        public readMessageRequest()
        {
        }

        public readMessageRequest(EcrisService.EcrisRiIdentifierContainingWSInputType ReadMessageWSInput)
        {
            this.ReadMessageWSInput = ReadMessageWSInput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class readMessageResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/message-storage-v1.0", Order = 0)]
        public EcrisService.ReadMessageWSOutputType ReadMessageWSOutput;

        public readMessageResponse()
        {
        }

        public readMessageResponse(EcrisService.ReadMessageWSOutputType ReadMessageWSOutput)
        {
            this.ReadMessageWSOutput = ReadMessageWSOutput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class readNistRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/message-storage-v1.0", Order = 0)]
        public EcrisService.EcrisRiIdentifierContainingWSInputType ReadNistWSInput;

        public readNistRequest()
        {
        }

        public readNistRequest(EcrisService.EcrisRiIdentifierContainingWSInputType ReadNistWSInput)
        {
            this.ReadNistWSInput = ReadNistWSInput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class readNistResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/message-storage-v1.0", Order = 0)]
        public EcrisService.ReadNistWSOutputType ReadNistWSOutput;

        public readNistResponse()
        {
        }

        public readNistResponse(EcrisService.ReadNistWSOutputType ReadNistWSOutput)
        {
            this.ReadNistWSOutput = ReadNistWSOutput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class deleteNistRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/message-storage-v1.0", Order = 0)]
        public EcrisService.EcrisRiIdentifierContainingWSInputType DeleteNistWSInput;

        public deleteNistRequest()
        {
        }

        public deleteNistRequest(EcrisService.EcrisRiIdentifierContainingWSInputType DeleteNistWSInput)
        {
            this.DeleteNistWSInput = DeleteNistWSInput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class deleteNistResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/message-storage-v1.0", Order = 0)]
        public EcrisService.BaseEcrisRiWSOutputType DeleteNistWSOutput;

        public deleteNistResponse()
        {
        }

        public deleteNistResponse(EcrisService.BaseEcrisRiWSOutputType DeleteNistWSOutput)
        {
            this.DeleteNistWSOutput = DeleteNistWSOutput;
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    public interface storagePortv10Channel : EcrisService.storagePortv10, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    public partial class storagePortv10Client : System.ServiceModel.ClientBase<EcrisService.storagePortv10>, EcrisService.storagePortv10
    {

        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);

        public storagePortv10Client() :
                base(storagePortv10Client.GetDefaultBinding(), storagePortv10Client.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.storageServicePort.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }

        public storagePortv10Client(EndpointConfiguration endpointConfiguration) :
                base(storagePortv10Client.GetBindingForEndpoint(endpointConfiguration), storagePortv10Client.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }

        public storagePortv10Client(EndpointConfiguration endpointConfiguration, string remoteAddress) :
                base(storagePortv10Client.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }

        public storagePortv10Client(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) :
                base(storagePortv10Client.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }

        public storagePortv10Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
                base(binding, remoteAddress)
        {
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EcrisService.getFoldersResponse> EcrisService.storagePortv10.getFoldersAsync(EcrisService.getFoldersRequest request)
        {
            return base.Channel.getFoldersAsync(request);
        }

        public System.Threading.Tasks.Task<EcrisService.getFoldersResponse> getFoldersAsync(EcrisService.BaseEcrisRiWSInputType BaseEcrisRiWSInput)
        {
            EcrisService.getFoldersRequest inValue = new EcrisService.getFoldersRequest();
            inValue.BaseEcrisRiWSInput = BaseEcrisRiWSInput;
            return ((EcrisService.storagePortv10)(this)).getFoldersAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EcrisService.findMessageByIdentifierResponse> EcrisService.storagePortv10.findMessageByIdentifierAsync(EcrisService.findMessageByIdentifierRequest request)
        {
            return base.Channel.findMessageByIdentifierAsync(request);
        }

        public System.Threading.Tasks.Task<EcrisService.findMessageByIdentifierResponse> findMessageByIdentifierAsync(EcrisService.FindMessageByIdentifierWSInputType FindMessageByIdentifierWSInput)
        {
            EcrisService.findMessageByIdentifierRequest inValue = new EcrisService.findMessageByIdentifierRequest();
            inValue.FindMessageByIdentifierWSInput = FindMessageByIdentifierWSInput;
            return ((EcrisService.storagePortv10)(this)).findMessageByIdentifierAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EcrisService.findMessageByEcrisIdentifierResponse> EcrisService.storagePortv10.findMessageByEcrisIdentifierAsync(EcrisService.findMessageByEcrisIdentifierRequest request)
        {
            return base.Channel.findMessageByEcrisIdentifierAsync(request);
        }

        public System.Threading.Tasks.Task<EcrisService.findMessageByEcrisIdentifierResponse> findMessageByEcrisIdentifierAsync(EcrisService.FindMessageByEcrisIdentifierWSInputType FindMessageByEcrisIdentifierWSInput)
        {
            EcrisService.findMessageByEcrisIdentifierRequest inValue = new EcrisService.findMessageByEcrisIdentifierRequest();
            inValue.FindMessageByEcrisIdentifierWSInput = FindMessageByEcrisIdentifierWSInput;
            return ((EcrisService.storagePortv10)(this)).findMessageByEcrisIdentifierAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EcrisService.findAccessibleMessageByIdentifierResponse> EcrisService.storagePortv10.findAccessibleMessageByIdentifierAsync(EcrisService.findAccessibleMessageByIdentifierRequest request)
        {
            return base.Channel.findAccessibleMessageByIdentifierAsync(request);
        }

        public System.Threading.Tasks.Task<EcrisService.findAccessibleMessageByIdentifierResponse> findAccessibleMessageByIdentifierAsync(EcrisService.FindAccessibleMessageByIdentifierWSInputType FindAccessibleMessageByIdentifierWSInput)
        {
            EcrisService.findAccessibleMessageByIdentifierRequest inValue = new EcrisService.findAccessibleMessageByIdentifierRequest();
            inValue.FindAccessibleMessageByIdentifierWSInput = FindAccessibleMessageByIdentifierWSInput;
            return ((EcrisService.storagePortv10)(this)).findAccessibleMessageByIdentifierAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EcrisService.getMessagesForFolderResponse> EcrisService.storagePortv10.getMessagesForFolderAsync(EcrisService.getMessagesForFolderRequest request)
        {
            return base.Channel.getMessagesForFolderAsync(request);
        }

        public System.Threading.Tasks.Task<EcrisService.getMessagesForFolderResponse> getMessagesForFolderAsync(EcrisService.GetMessagesForFolderWSInputType GetMessagesForFolderWSInput)
        {
            EcrisService.getMessagesForFolderRequest inValue = new EcrisService.getMessagesForFolderRequest();
            inValue.GetMessagesForFolderWSInput = GetMessagesForFolderWSInput;
            return ((EcrisService.storagePortv10)(this)).getMessagesForFolderAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EcrisService.moveMessagesToFolderResponse> EcrisService.storagePortv10.moveMessagesToFolderAsync(EcrisService.moveMessagesToFolderRequest request)
        {
            return base.Channel.moveMessagesToFolderAsync(request);
        }

        public System.Threading.Tasks.Task<EcrisService.moveMessagesToFolderResponse> moveMessagesToFolderAsync(EcrisService.MoveMessagesToFolderWSInputType MoveMessagesToFolderWSInput)
        {
            EcrisService.moveMessagesToFolderRequest inValue = new EcrisService.moveMessagesToFolderRequest();
            inValue.MoveMessagesToFolderWSInput = MoveMessagesToFolderWSInput;
            return ((EcrisService.storagePortv10)(this)).moveMessagesToFolderAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EcrisService.moveMessagesToFolderResponse> EcrisService.storagePortv10.deleteFolderAsync(EcrisService.deleteFolderRequest request)
        {
            return base.Channel.deleteFolderAsync(request);
        }

        public System.Threading.Tasks.Task<EcrisService.moveMessagesToFolderResponse> deleteFolderAsync(EcrisService.DeleteFolderWSInputType DeleteFolderWSInput)
        {
            EcrisService.deleteFolderRequest inValue = new EcrisService.deleteFolderRequest();
            inValue.DeleteFolderWSInput = DeleteFolderWSInput;
            return ((EcrisService.storagePortv10)(this)).deleteFolderAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EcrisService.removeMessagesResponse> EcrisService.storagePortv10.removeMessagesAsync(EcrisService.removeMessagesRequest request)
        {
            return base.Channel.removeMessagesAsync(request);
        }

        public System.Threading.Tasks.Task<EcrisService.removeMessagesResponse> removeMessagesAsync(EcrisService.RemoveMessagesWSInputType RemoveMessagesWSInput)
        {
            EcrisService.removeMessagesRequest inValue = new EcrisService.removeMessagesRequest();
            inValue.RemoveMessagesWSInput = RemoveMessagesWSInput;
            return ((EcrisService.storagePortv10)(this)).removeMessagesAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EcrisService.moveMessagesToFolderResponse> EcrisService.storagePortv10.storeFolderAsync(EcrisService.storeFolderRequest request)
        {
            return base.Channel.storeFolderAsync(request);
        }

        public System.Threading.Tasks.Task<EcrisService.moveMessagesToFolderResponse> storeFolderAsync(EcrisService.StoreFolderWSInputType StoreFolderWSInput)
        {
            EcrisService.storeFolderRequest inValue = new EcrisService.storeFolderRequest();
            inValue.StoreFolderWSInput = StoreFolderWSInput;
            return ((EcrisService.storagePortv10)(this)).storeFolderAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EcrisService.storeMessageResponse> EcrisService.storagePortv10.storeMessageAsync(EcrisService.storeMessageRequest request)
        {
            return base.Channel.storeMessageAsync(request);
        }

        public System.Threading.Tasks.Task<EcrisService.storeMessageResponse> storeMessageAsync(EcrisService.StoreMessageWSInputType StoreMessageWSInput)
        {
            EcrisService.storeMessageRequest inValue = new EcrisService.storeMessageRequest();
            inValue.StoreMessageWSInput = StoreMessageWSInput;
            return ((EcrisService.storagePortv10)(this)).storeMessageAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EcrisService.storeMessageResponse> EcrisService.storagePortv10.storeMessageZipAsync(EcrisService.storeMessageZipRequest request)
        {
            return base.Channel.storeMessageZipAsync(request);
        }

        public System.Threading.Tasks.Task<EcrisService.storeMessageResponse> storeMessageZipAsync(EcrisService.StoreMessageZipWSInputType StoreMessageZipWSInput)
        {
            EcrisService.storeMessageZipRequest inValue = new EcrisService.storeMessageZipRequest();
            inValue.StoreMessageZipWSInput = StoreMessageZipWSInput;
            return ((EcrisService.storagePortv10)(this)).storeMessageZipAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EcrisService.readMessageResponse> EcrisService.storagePortv10.readMessageAsync(EcrisService.readMessageRequest request)
        {
            return base.Channel.readMessageAsync(request);
        }

        public System.Threading.Tasks.Task<EcrisService.readMessageResponse> readMessageAsync(EcrisService.EcrisRiIdentifierContainingWSInputType ReadMessageWSInput)
        {
            EcrisService.readMessageRequest inValue = new EcrisService.readMessageRequest();
            inValue.ReadMessageWSInput = ReadMessageWSInput;
            return ((EcrisService.storagePortv10)(this)).readMessageAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EcrisService.readNistResponse> EcrisService.storagePortv10.readNistAsync(EcrisService.readNistRequest request)
        {
            return base.Channel.readNistAsync(request);
        }

        public System.Threading.Tasks.Task<EcrisService.readNistResponse> readNistAsync(EcrisService.EcrisRiIdentifierContainingWSInputType ReadNistWSInput)
        {
            EcrisService.readNistRequest inValue = new EcrisService.readNistRequest();
            inValue.ReadNistWSInput = ReadNistWSInput;
            return ((EcrisService.storagePortv10)(this)).readNistAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EcrisService.deleteNistResponse> EcrisService.storagePortv10.deleteNistAsync(EcrisService.deleteNistRequest request)
        {
            return base.Channel.deleteNistAsync(request);
        }

        public System.Threading.Tasks.Task<EcrisService.deleteNistResponse> deleteNistAsync(EcrisService.EcrisRiIdentifierContainingWSInputType DeleteNistWSInput)
        {
            EcrisService.deleteNistRequest inValue = new EcrisService.deleteNistRequest();
            inValue.DeleteNistWSInput = DeleteNistWSInput;
            return ((EcrisService.storagePortv10)(this)).deleteNistAsync(inValue);
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
            if ((endpointConfiguration == EndpointConfiguration.storageServicePort))
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
            if ((endpointConfiguration == EndpointConfiguration.storageServicePort))
            {
                return new System.ServiceModel.EndpointAddress("http://172.16.0.101/ecris-ri-backend//s/message-storage");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }

        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return storagePortv10Client.GetBindingForEndpoint(EndpointConfiguration.storageServicePort);
        }

        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return storagePortv10Client.GetEndpointAddress(EndpointConfiguration.storageServicePort);
        }

        public enum EndpointConfiguration
        {

            storageServicePort,
        }
    }
}
