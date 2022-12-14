using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Certificate;
using MJ_CAIS.ExternalWebServices.Contracts;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL.EDelivery;

namespace MJ_CAIS.ExternalWebServices
{
    public class CAISEDeliveryService : BaseAsyncService<CertificateDTO, CertificateDTO, CertificateGridDTO, ACertificate, string, CaisDbContext>, ICAISEDeliveryService
    {
        private readonly ICertificateRepository _certificateRepository;
        private readonly IEDeliveryService _eDeliveryService;


        public CAISEDeliveryService(IMapper mapper, ICertificateRepository certificateRepository, IEDeliveryService eDeliveryService)
            : base(mapper, certificateRepository)
        {
            _certificateRepository = certificateRepository;
            _eDeliveryService = eDeliveryService;

        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SendCertificateToEDeliveryAsync(EEdeliveryMsg message)
        {

            var returnValue = -1;
            ACertificate certificate = await _certificateRepository.GetCertificateWithDocumentContent(message.CertificateId);
            if (certificate == null) {
                throw new Exception("Certificate does not exist.");
            }
            var fileContent = certificate.Doc?.DocContent?.Content;
            if (fileContent == null)
            {
                throw new Exception("File does not exist.");
            }
            try
            {
                var channel = _eDeliveryService.CreateChannel();

                //todo: add resources
                var result = await channel.SendElectronicDocumentAsync(subject: $"Свидетелство за съдимост {certificate.RegistrationNumber}",
                    docBytes: fileContent,
                    docNameWithExtension: $"Certificate_{certificate.RegistrationNumber}.pdf",
                    docRegNumber: certificate.RegistrationNumber,
                    receiverType: eProfileType.Person,
                    receiverUniqueIdentifier: certificate.Application.Egn,
                    receiverPhone: certificate.Application.AddrPhone,
                    receiverEmail: certificate.Application.Email,
                    serviceOID: null,
                    operatorEGN: null);
                message.Status = EdeliveryConstants.EdeliveryStatuses.Sent;
                message.ReferenceNumber = result.ToString();
                returnValue = result;
            }
            catch (Exception ex)
            {
                message.Status = EdeliveryConstants.EdeliveryStatuses.Failed;
                message.Error = ex.Message;
                message.StackTrace = ex.StackTrace;
                message.Attempts = (byte?)(message.Attempts + 1);
                message.HasError = true;

            }
            finally
            {
                message.SentDate = DateTime.Now;
                message.EntityState = Common.Enums.EntityStateEnum.Modified;
                message.ModifiedProperties.Add(nameof(message.SentDate));
                message.ModifiedProperties.Add(nameof(message.Status));
                message.ModifiedProperties.Add(nameof(message.Error));
                message.ModifiedProperties.Add(nameof(message.StackTrace));
                message.ModifiedProperties.Add(nameof(message.HasError));
                message.ModifiedProperties.Add(nameof(message.Attempts));
                message.ModifiedProperties.Add(nameof(message.ReferenceNumber));
                await _certificateRepository.SaveEntityAsync(message, false);
               // dbContext.EEdeliveryMsgs.Update(message);
               // dbContext.SaveChanges();
            }

            return returnValue;

        }

   
    }
}
