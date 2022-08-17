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


        public async Task SendMessagesToEcris(string username, string password, string folderName, string endpointAuth, string endpointStorage, string endPointAddressSearch)
        {
            _logger.LogInformation($"SendMessagesToEcris started. Username: {username}; Folder: {folderName}.");
            string? sessionID = null;
            EcrisClient? client = null;
            try
            {
                var numAttempts = (await _dbContext.GSystemParameters.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Code == SystemParametersConstants.SystemParametersNames.ECRIS_MAX_NUMBER_OF_ATTEMPTS))?.ValueNumber;
                if (numAttempts != null)
                {
                    throw new Exception($"{SystemParametersConstants.SystemParametersNames.ECRIS_MAX_NUMBER_OF_ATTEMPTS} not set.");
                }
                var contents = await _dbContext.DDocContents.AsNoTracking()
                                        .Include(dd => dd.DDocuments).AsNoTracking()
                                        .Where(cont => cont.DDocuments.Where(dd => dd.EcrisMsg != null 
                                        && dd.EcrisMsg.EcrisMsgStatus == ECRISConstants.EcrisMessageStatuses.ForSending).Any()
                                            && cont.MimeType == "application/xml").ToListAsync();
                if (contents.Count > 0)
                {
                   
                    client = new EcrisClient(username, password, endpointAuth, endpointStorage, endPointAddressSearch);
                    _logger.LogTrace($" EcrisClient created.");
                    sessionID = await client.GetActiveSessionId();
                    _logger.LogTrace($" EcrisClient logged in.");
                    var folderIds = await client.GetInboxFolderIdentifier(sessionID, folderName, false);
                    if (folderIds.Count != 1)
                    {
                        throw new Exception($"Folder {folderName} does not exist.");
                    }
                    var folderId = folderIds[0];

                    _logger.LogTrace($" Folder {folderName} identified as {folderId}.");
                    var notificationTypeId = await ServiceHelper.GetDocTypeCodeAsync(EcrisMessageTypeOrAliasMessageType.NOT, _dbContext);
                    var responseTypeId = await ServiceHelper.GetDocTypeCodeAsync(EcrisMessageTypeOrAliasMessageType.RRS, _dbContext);
                    foreach (var content in contents)
                    {
                        if (content.Content == null)
                        {
                            throw new Exception("Empty content.");
                        }
                        foreach (var document in content.DDocuments.Where(d => d.EcrisMsgId != null))
                        {
                            var ecrisMsg = await _dbContext.EEcrisMessages.AsNoTracking()
                                                .Include(e => e.EEcrisOutboxes).AsNoTracking()
                                                .FirstOrDefaultAsync(ei => ei.Id == document.EcrisMsgId);

                            var ecrisOutbox = ecrisMsg?.EEcrisOutboxes.FirstOrDefault();
                            bool isNew = false;
                            if (ecrisOutbox == null)
                            {
                                _logger.LogTrace($"EcrisMessage({ecrisMsg?.Id}) : New outbox created.");
                                ecrisOutbox = new EEcrisOutbox();
                                ecrisOutbox.Id = BaseEntity.GenerateNewId();
                                ecrisOutbox.CreatedOn = DateTime.Now;
                                isNew = true;
                            }
                            ecrisOutbox.EcrisMsg = null;
                            string xml = Encoding.UTF8.GetString(content.Content);
                            ecrisOutbox.XmlObject = xml;
                          
                            ecrisOutbox.Status = ECRISConstants.EcrisOutboxStatuses.Pending;
                            ecrisOutbox.Error = null;
                            ecrisOutbox.HasError = false;
                            ecrisOutbox.Attempts = ecrisOutbox.Attempts == null ? 1 : ecrisOutbox.Attempts + 1;
                            ecrisOutbox.EcrisMsgId = ecrisMsg?.Id;
                            ecrisOutbox.StackTrace = null;
                            ecrisOutbox.ExecutionDate = DateTime.Now;

                            AbstractMessageType msg = XmlUtils.DeserializeXml<AbstractMessageType>(xml);
                          
                            AbstractMessageType? resp = null;

                            var msgTypeID = ecrisMsg?.MsgTypeId;
                      if(!notificationTypeId.Contains(msgTypeID) && !responseTypeId.Contains(msgTypeID))
                            {
                                throw new Exception($"Ecris msg {ecrisMsg.Id} is of type {msgTypeID} which is not suitable for sending to Ecris");
                            }
                            try
                            {
                                if (notificationTypeId.Contains(msgTypeID))
                                {
                                    var not = (NotificationMessageType)msg;
                                    ecrisOutbox.Operation = ECRISConstants.EcrisOutboxOperations.StoreResponce;
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
                                    _dbContext.ChangeTracker.Clear();
                                    _logger.LogTrace($"EcrisMessage({ecrisMsg?.Id}) : Outbox for pending saved.");
                                    _logger.LogTrace($"EcrisMessage({ecrisMsg?.Id}) : Notification pre-insert.");
                                    resp = await client.InsertNotification(not, sessionID, folderId);
                                    _logger.LogTrace($"EcrisMessage({ecrisMsg?.Id}) : Notification post-insert.");
                                }
                                if (responseTypeId.Contains(msgTypeID))
                                {
                                    var reqResp = (RequestResponseMessageType)msg;
                                    ecrisOutbox.Operation = ECRISConstants.EcrisOutboxOperations.StoreResponce;
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
                                    _dbContext.ChangeTracker.Clear();
                                    _logger.LogTrace($"EcrisMessage({ecrisMsg?.Id}) : Outbox for pending saved.");
                                    _logger.LogTrace($"EcrisMessage({ecrisMsg?.Id}) : Response pre-insert.");
                                     resp = await client.InsertRequestResponse(reqResp, sessionID, folderId);
                                    _logger.LogTrace($"EcrisMessage({ecrisMsg?.Id}) : Response post-insert.");
                                }

                                ecrisMsg.EcrisIdentifier = resp?.MessageEcrisIdentifier;
                                ecrisMsg.Identifier = resp?.MessageIdentifier;
                                ecrisMsg.EcrisMsgStatus = ECRISConstants.EcrisMessageStatuses.Sent;
                                ecrisOutbox.Status = ECRISConstants.EcrisOutboxStatuses.Sent;
                              
                                ecrisMsg.EEcrisOutboxes.Clear();
                        
                                _dbContext.EEcrisMessages.Update(ecrisMsg);

                                _dbContext.EEcrisOutboxes.Update(ecrisOutbox);
                                _logger.LogTrace($"EcrisMessage({ecrisMsg?.Id}) : Outbox and ecris message pre-update.");
                                await _dbContext.SaveChangesAsync();
                                _dbContext.ChangeTracker.Clear();
                                _logger.LogTrace($"EcrisMessage({ecrisMsg?.Id}) : Outbox and ecris message post-update.");
                            }
                            catch (Exception ex)
                            {
                                //continue with next
                                _logger.LogError(ex.Message, ex.Data, ex);
                                _dbContext.ChangeTracker.Clear();
                                try
                                {
                                    var dbOutbox = await _dbContext.EEcrisOutboxes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == ecrisOutbox.Id);

                                    if (dbOutbox != null)
                                    {
                                        ecrisOutbox = dbOutbox;
                                        ecrisOutbox.Version = null;
                                    }
                                    ecrisOutbox.Status = ECRISConstants.EcrisOutboxStatuses.Error;
                                    ecrisOutbox.HasError = true;
                                    ecrisOutbox.StackTrace = ex.StackTrace;
                                    ecrisOutbox.Error = ex.Message;
                                    ecrisOutbox.Attempts = ecrisOutbox.Attempts == null ? 1 : ecrisOutbox.Attempts + 1;
                                    if (ecrisOutbox.Attempts > numAttempts)
                                    {
                                        var emsg = await _dbContext.EEcrisMessages.AsNoTracking().FirstOrDefaultAsync(x => x.Id == ecrisOutbox.EcrisMsgId);
                                        emsg.EcrisMsgStatus = ECRISConstants.EcrisMessageStatuses.Error;
                                        _dbContext.Update(emsg);
                                    }
                                    _dbContext.Update(ecrisOutbox);
                                    await _dbContext.SaveChangesAsync();
                                    _dbContext.ChangeTracker.Clear();
                                }
                            
                            catch (Exception ex1)
                            {
                                _logger.LogError(ex1.Message, ex1.Data, ex1);
                                _dbContext.ChangeTracker.Clear();
                            }

                            }



                        }

                    }

                }
            }
            catch (Exception ex)
            
            {
                //todo: транзакции и промени в базата?!
                _logger.LogError(ex.Message, ex.Data, ex);
                throw ex;
            }
            finally
            {
                if (string.IsNullOrEmpty(sessionID))
                {
             
                    client?.Logout(sessionID);
                    _logger.LogTrace($" EcrisClient logged out.");
                }
            }

            _logger.LogInformation($"SendMessagesToEcris ended.");

        }

    }
}
