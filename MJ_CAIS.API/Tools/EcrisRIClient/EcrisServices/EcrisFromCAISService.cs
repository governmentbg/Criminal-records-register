using EcrisIntegrationServices;
using EcrisRIClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.XmlData;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EcrisService;
using MJ_CAIS.EcrisObjectsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace EcrisServices
{
    public class EcrisFromCAISService
    {
        private CaisDbContext _dbContext;
        private readonly ILogger<EcrisFromCAISService> _logger;
   
        public EcrisFromCAISService(CaisDbContext dbContext, ILogger<EcrisFromCAISService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

     
        public async Task SendMessagesToEcris(string username, string password, string folderName)
        {
            _logger.LogInformation($"SendMessagesToEcris started. Username: {username}; Folder: {folderName}.");
            string? sessionID = null;
            EcrisClient? client = null;
            try
            {
                var contents = await _dbContext.DDocContents.Where(cont => cont.DDocuments
                                    .Where(dd => dd.EcrisMsg != null && dd.EcrisMsg.EcrisMsgStatus == ECRISConstants.EcrisMessageStatuses.ForSending).Any()).ToListAsync();
                if (contents.Count > 0)
                {
                    client = new EcrisClient(username, password);
                    _logger.LogTrace($" EcrisClient created.");
                    sessionID = await client.GetActiveSessionId();
                    _logger.LogTrace($" EcrisClient logged in.");
                    var folderId = await client.GetInboxFolderIdentifier(sessionID, folderName);
                    _logger.LogTrace($" Folder {folderName} identified as {folderId}.");
                    var notificationTypeId = await CommonService.GetDocTypeCodeAsync(EcrisMessageTypeOrAliasMessageType.NOT, _dbContext);
                    var responseTypeId = await CommonService.GetDocTypeCodeAsync(EcrisMessageTypeOrAliasMessageType.RRS, _dbContext);
                    foreach (var content in contents)
                    {
                        var ecrisMsg = content.DDocuments.First().EcrisMsg;
                        var ecrisOutbox = ecrisMsg?.EEcrisOutboxes.FirstOrDefault();
                        bool isNew = false;
                        if (ecrisOutbox == null)
                        {
                            _logger.LogTrace($"EcrisMessage({ecrisMsg?.Id}) : New outbox created.");
                            ecrisOutbox = new EEcrisOutbox();
                            ecrisOutbox.Id = BaseEntity.GenerateNewId();
                            ecrisOutbox.CreatedOn = DateTime.UtcNow;
                            isNew = true;
                        }
                        if (content.Content == null)
                        {
                            throw new Exception("Empty content.");
                        }
                        string xml = Encoding.UTF8.GetString(content.Content);
                        ecrisOutbox.XmlObject = xml;
                        //todo:add constants
                        ecrisOutbox.Status = "PENDING";
                        ecrisOutbox.Error = null;
                        ecrisOutbox.HasError = false;
                        ecrisOutbox.Attempts = ecrisOutbox.Attempts == null ? 1 : ecrisOutbox.Attempts + 1;
                        ecrisOutbox.EcrisMsgId = ecrisMsg?.Id;
                        ecrisOutbox.StackTrace = null;
                        ecrisOutbox.ExecutionDate = DateTime.UtcNow;



                        AbstractMessageType msg = XmlUtils.DeserializeXml<AbstractMessageType>(xml);
                        //msg.MessageEcrisIdentifier = null;
                        //msg.MessageIdentifier = null;
                        AbstractMessageType? resp = null;

                        var msgTypeID = ecrisMsg?.MsgTypeId;
                        try
                        {
                            if (msgTypeID == notificationTypeId)
                            {
                                var not = (NotificationMessageType)msg;
                                ecrisOutbox.Operation = "STORE_NOTIFICATION";
                                if (isNew)
                                {

                                    _dbContext.EEcrisOutboxes.Add(ecrisOutbox);
                                }
                                else
                                {
                                    _dbContext.EEcrisOutboxes.Update(ecrisOutbox);
                                }
                                _logger.LogTrace($"EcrisMessage({ecrisMsg?.Id}) : Outbox for pending pre-save.");
                                await _dbContext.SaveChangesAsync();
                                _logger.LogTrace($"EcrisMessage({ecrisMsg?.Id}) : Outbox for pending saved.");
                                _logger.LogTrace($"EcrisMessage({ecrisMsg?.Id}) : Notification pre-insert.");
                                resp = await client.InsertNotification(not, sessionID, folderId);
                                _logger.LogTrace($"EcrisMessage({ecrisMsg?.Id}) : Notification post-insert.");
                            }
                            if (msgTypeID == responseTypeId)
                            {
                                var reqResp = (RequestResponseMessageType)msg;
                                ecrisOutbox.Operation = "STORE_RESPONSE";
                                if (isNew)
                                {
                                    _dbContext.EEcrisOutboxes.Add(ecrisOutbox);
                                }
                                else
                                {
                                    _dbContext.EEcrisOutboxes.Update(ecrisOutbox);
                                }
                                _logger.LogTrace($"EcrisMessage({ecrisMsg?.Id}) : Outbox for pending pre-save.");
                                await _dbContext.SaveChangesAsync();
                                _logger.LogTrace($"EcrisMessage({ecrisMsg?.Id}) : Outbox for pending saved.");
                                _logger.LogTrace($"EcrisMessage({ecrisMsg?.Id}) : Response pre-insert.");
                                resp = await client.InsertRequestResponse(reqResp, sessionID, folderId);
                                _logger.LogTrace($"EcrisMessage({ecrisMsg?.Id}) : Response post-insert.");
                            }

                            ecrisMsg.EcrisIdentifier = resp?.MessageEcrisIdentifier;
                            ecrisMsg.Identifier = resp?.MessageIdentifier;
                            ecrisMsg.EcrisMsgStatus = ECRISConstants.EcrisMessageStatuses.Sent;
                            _dbContext.EEcrisMessages.Update(ecrisMsg);
                            ecrisOutbox.Status = "SENT";
                            _dbContext.EEcrisOutboxes.Update(ecrisOutbox);
                            _logger.LogTrace($"EcrisMessage({ecrisMsg?.Id}) : Outbox and ecris message pre-update.");
                            await _dbContext.SaveChangesAsync();
                            _logger.LogTrace($"EcrisMessage({ecrisMsg?.Id}) : Outbox and ecris message post-update.");
                        }
                        catch (Exception ex)
                        {
                            //continue with next
                            _logger.LogError(ex.Message, ex.Data, ex);
                            ecrisOutbox.Status = "ERROR";
                            ecrisOutbox.HasError = true;
                            ecrisOutbox.StackTrace = ex.StackTrace;
                            ecrisOutbox.Error = ex.Message;
                            await _dbContext.SaveChangesAsync();

                        }



                    }

                }

            }
            catch (Exception ex)
            {
                //todo: транзакции и промени в базата, logout?!
                _logger.LogError(ex.Message, ex.Data, ex);
                throw ex;
            }
            finally
            {
                if (string.IsNullOrEmpty(sessionID))
                {
                    _logger.LogTrace($" EcrisClient logged out.");
                    client?.Logout(sessionID);
                }
            }

            _logger.LogInformation($"SendMessagesToEcris ended.");

        }

    }
}
