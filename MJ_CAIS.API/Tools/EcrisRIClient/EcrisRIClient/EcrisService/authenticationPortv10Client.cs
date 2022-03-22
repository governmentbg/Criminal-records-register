namespace EcrisRIClient.EcrisService
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ServiceModel.ServiceContractAttribute(Name = "authenticationPort-v1.0", Namespace = "http://ec.europa.eu/ECRIS-RI/contract", ConfigurationName = "EcrisService.authenticationPortv10")]
    public interface authenticationPortv10
    {
        [System.ServiceModel.OperationContractAttribute(Action = "http://ec.europa.eu/ECRIS-RI/authentication/login", ReplyAction = "*")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.ServerErrorFaultType), Action = "http://ec.europa.eu/ECRIS-RI/authentication/login", Name = "ServerErrorFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.AuthenticationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/authentication/login", Name = "AuthenticationFaultMessage")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DateType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PositiveDecimalType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractRelationshipType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StatisticsPeriodType))]
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
        System.Threading.Tasks.Task<EcrisService.loginResponse> loginAsync(EcrisService.loginRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ec.europa.eu/ECRIS-RI/authentication/logout", ReplyAction = "*")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.ServerErrorFaultType), Action = "http://ec.europa.eu/ECRIS-RI/authentication/logout", Name = "ServerErrorFaultMessage")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DateType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PositiveDecimalType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractRelationshipType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StatisticsPeriodType))]
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
        System.Threading.Tasks.Task<EcrisService.logoutResponse> logoutAsync(EcrisService.logoutRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ec.europa.eu/ECRIS-RI/authentication/retrieveAuditLog", ReplyAction = "*")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.ServerErrorFaultType), Action = "http://ec.europa.eu/ECRIS-RI/authentication/retrieveAuditLog", Name = "ServerErrorFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.AuthenticationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/authentication/retrieveAuditLog", Name = "AuthenticationFaultMessage")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DateType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PositiveDecimalType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractRelationshipType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StatisticsPeriodType))]
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
        System.Threading.Tasks.Task<EcrisService.retrieveAuditLogResponse> retrieveAuditLogAsync(EcrisService.retrieveAuditLogRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://ec.europa.eu/ECRIS-RI/authentication/forceResetPassword", ReplyAction = "*")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.ServerErrorFaultType), Action = "http://ec.europa.eu/ECRIS-RI/authentication/forceResetPassword", Name = "ServerErrorFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.OperationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/authentication/forceResetPassword", Name = "OperationFaultMessage")]
        [System.ServiceModel.FaultContractAttribute(typeof(EcrisService.AuthenticationFaultType), Action = "http://ec.europa.eu/ECRIS-RI/authentication/forceResetPassword", Name = "AuthenticationFaultMessage")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DateType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PositiveDecimalType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AbstractRelationshipType1))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StatisticsPeriodType))]
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
        System.Threading.Tasks.Task<EcrisService.forceResetPasswordResponse> forceResetPasswordAsync(EcrisService.forceResetPasswordRequest request);
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class loginRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/authentication-v1.0", Order = 0)]
        public EcrisService.LoginWSInputType LoginWSInput;

        public loginRequest()
        {
        }

        public loginRequest(EcrisService.LoginWSInputType LoginWSInput)
        {
            this.LoginWSInput = LoginWSInput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class loginResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/authentication-v1.0", Order = 0)]
        public EcrisService.LoginWSOutputType LoginWSOutput;

        public loginResponse()
        {
        }

        public loginResponse(EcrisService.LoginWSOutputType LoginWSOutput)
        {
            this.LoginWSOutput = LoginWSOutput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class logoutRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/authentication-v1.0", Order = 0)]
        public EcrisService.LogoutWSInputType LogoutWSInput;

        public logoutRequest()
        {
        }

        public logoutRequest(EcrisService.LogoutWSInputType LogoutWSInput)
        {
            this.LogoutWSInput = LogoutWSInput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class logoutResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/authentication-v1.0", Order = 0)]
        public EcrisService.LogoutWSOutputType LogoutWSOutput;

        public logoutResponse()
        {
        }

        public logoutResponse(EcrisService.LogoutWSOutputType LogoutWSOutput)
        {
            this.LogoutWSOutput = LogoutWSOutput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class retrieveAuditLogRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/authentication-v1.0", Order = 0)]
        public EcrisService.RetrieveAuditLogWSInputType RetrieveAuditLogWSInput;

        public retrieveAuditLogRequest()
        {
        }

        public retrieveAuditLogRequest(EcrisService.RetrieveAuditLogWSInputType RetrieveAuditLogWSInput)
        {
            this.RetrieveAuditLogWSInput = RetrieveAuditLogWSInput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class retrieveAuditLogResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/authentication-v1.0", Order = 0)]
        public EcrisService.RetrieveAuditLogWSOutputType RetrieveAuditLogWSOutput;

        public retrieveAuditLogResponse()
        {
        }

        public retrieveAuditLogResponse(EcrisService.RetrieveAuditLogWSOutputType RetrieveAuditLogWSOutput)
        {
            this.RetrieveAuditLogWSOutput = RetrieveAuditLogWSOutput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class forceResetPasswordRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/authentication-v1.0", Order = 0)]
        public EcrisService.ResetPasswordWsInput ResetPasswordWsInput;

        public forceResetPasswordRequest()
        {
        }

        public forceResetPasswordRequest(EcrisService.ResetPasswordWsInput ResetPasswordWsInput)
        {
            this.ResetPasswordWsInput = ResetPasswordWsInput;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class forceResetPasswordResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://ec.europa.eu/ECRIS-RI/contract", Order = 0)]
        public EcrisService.BaseEcrisRiWSOutputType BaseEcrisRiWSOutput;

        public forceResetPasswordResponse()
        {
        }

        public forceResetPasswordResponse(EcrisService.BaseEcrisRiWSOutputType BaseEcrisRiWSOutput)
        {
            this.BaseEcrisRiWSOutput = BaseEcrisRiWSOutput;
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    public interface authenticationPortv10Channel : EcrisService.authenticationPortv10, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3-preview3.21351.2")]
    public partial class authenticationPortv10Client : System.ServiceModel.ClientBase<EcrisService.authenticationPortv10>, EcrisService.authenticationPortv10
    {

        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);

        public authenticationPortv10Client() :
                base(authenticationPortv10Client.GetDefaultBinding(), authenticationPortv10Client.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.authenticationPort.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }

        public authenticationPortv10Client(EndpointConfiguration endpointConfiguration) :
                base(authenticationPortv10Client.GetBindingForEndpoint(endpointConfiguration), authenticationPortv10Client.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }

        public authenticationPortv10Client(EndpointConfiguration endpointConfiguration, string remoteAddress) :
                base(authenticationPortv10Client.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }

        public authenticationPortv10Client(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) :
                base(authenticationPortv10Client.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }

        public authenticationPortv10Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
                base(binding, remoteAddress)
        {
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EcrisService.loginResponse> EcrisService.authenticationPortv10.loginAsync(EcrisService.loginRequest request)
        {
            return base.Channel.loginAsync(request);
        }

        public System.Threading.Tasks.Task<EcrisService.loginResponse> loginAsync(EcrisService.LoginWSInputType LoginWSInput)
        {
            EcrisService.loginRequest inValue = new EcrisService.loginRequest();
            inValue.LoginWSInput = LoginWSInput;
            return ((EcrisService.authenticationPortv10)(this)).loginAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EcrisService.logoutResponse> EcrisService.authenticationPortv10.logoutAsync(EcrisService.logoutRequest request)
        {
            return base.Channel.logoutAsync(request);
        }

        public System.Threading.Tasks.Task<EcrisService.logoutResponse> logoutAsync(EcrisService.LogoutWSInputType LogoutWSInput)
        {
            EcrisService.logoutRequest inValue = new EcrisService.logoutRequest();
            inValue.LogoutWSInput = LogoutWSInput;
            return ((EcrisService.authenticationPortv10)(this)).logoutAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EcrisService.retrieveAuditLogResponse> EcrisService.authenticationPortv10.retrieveAuditLogAsync(EcrisService.retrieveAuditLogRequest request)
        {
            return base.Channel.retrieveAuditLogAsync(request);
        }

        public System.Threading.Tasks.Task<EcrisService.retrieveAuditLogResponse> retrieveAuditLogAsync(EcrisService.RetrieveAuditLogWSInputType RetrieveAuditLogWSInput)
        {
            EcrisService.retrieveAuditLogRequest inValue = new EcrisService.retrieveAuditLogRequest();
            inValue.RetrieveAuditLogWSInput = RetrieveAuditLogWSInput;
            return ((EcrisService.authenticationPortv10)(this)).retrieveAuditLogAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EcrisService.forceResetPasswordResponse> EcrisService.authenticationPortv10.forceResetPasswordAsync(EcrisService.forceResetPasswordRequest request)
        {
            return base.Channel.forceResetPasswordAsync(request);
        }

        public System.Threading.Tasks.Task<EcrisService.forceResetPasswordResponse> forceResetPasswordAsync(EcrisService.ResetPasswordWsInput ResetPasswordWsInput)
        {
            EcrisService.forceResetPasswordRequest inValue = new EcrisService.forceResetPasswordRequest();
            inValue.ResetPasswordWsInput = ResetPasswordWsInput;
            return ((EcrisService.authenticationPortv10)(this)).forceResetPasswordAsync(inValue);
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
            if ((endpointConfiguration == EndpointConfiguration.authenticationPort))
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
            if ((endpointConfiguration == EndpointConfiguration.authenticationPort))
            {
                return new System.ServiceModel.EndpointAddress("http://172.16.0.101/ecris-ri-backend/s/authentication");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }

        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return authenticationPortv10Client.GetBindingForEndpoint(EndpointConfiguration.authenticationPort);
        }

        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return authenticationPortv10Client.GetEndpointAddress(EndpointConfiguration.authenticationPort);
        }

        public enum EndpointConfiguration
        {

            authenticationPort,
        }
    }
}
