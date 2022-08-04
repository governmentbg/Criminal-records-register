using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Newtonsoft.Json;
using TL.EGovPayments.ControllerModels;
using TL.EGovPayments.Interfaces;
using TL.EGovPayments.JsonEnums;
using TL.EGovPayments.JsonModels;

namespace MJ_CAIS.WebPortal.Public.Services
{
    public class EGovPaymentsService : IEGovIntegrationService
    {
        private CaisDbContext _dbContext;
        public EGovPaymentsService(CaisDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool ChangePaymentStatusCallback(PaymentStatus paymentStatus)
        {
            try
            {
                var payment = _dbContext.EPayments.Where(ep => ep.InvoiceNumber == paymentStatus.Id).FirstOrDefault();

                if (payment != null)
                {
                    var paymentNotification = new EPaymentNotification();
                    paymentNotification.Id = BaseEntity.GenerateNewId();
                    paymentNotification.DecodedText = JsonConvert.SerializeObject(paymentStatus);
                    paymentNotification.PaymentId = payment.Id;
                    paymentNotification.LogDate = paymentStatus.ChangeTime.ToString("dd.MM.yyyy hh:mm:ss");
                    _dbContext.EPaymentNotifications.Add(paymentNotification);

                    var statuses = payment.PaymentStatus;
                    if (paymentStatus.Status == PaymentRequestStatuses.PAID
                        || paymentStatus.Status == PaymentRequestStatuses.AUTHORIZED
                        || paymentStatus.Status == PaymentRequestStatuses.ORDERED
                       )
                    {
                        payment.PaymentStatus = PaymentConstants.PaymentStatuses.Payed;
                    }
                    else if (paymentStatus.Status == PaymentRequestStatuses.EXPIRED
                                || paymentStatus.Status == PaymentRequestStatuses.CANCELED
                                || paymentStatus.Status == PaymentRequestStatuses.SUSPENDED
                             )
                    {
                        payment.PaymentStatus = PaymentConstants.PaymentStatuses.Canceled;
                    }
                    else // for PENDING payment status
                    {
                        //string newStatusReason = string.Format(AppResources.statusInPayEGovBG, paymentStatus.Status.ToString());
                        //this.UpdateApplicationStatusReason(applicationId, newStatusReason);
                    }
                    _dbContext.SaveChanges();
                }
                else
                {
                    return false;
                }


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string GeneratePaymentNumber(PaymentRequest paymentRequest)
        {
            return paymentRequest.PaymentReferenceNumber;
        }

        public int GetApplicationIdByPaymentRef(string paymentRefNumber)
        {
            throw new NotImplementedException();
            //return (from application in Db.Applications
            //        join payment in Db.ApplicationPayments on application.Id equals payment.ApplicationId
            //        where payment.PaymentRefNum == paymentRefNumber
            //        select application.Id).FirstOrDefault();
        }

        public EGovPaymentRequestModel GetPaymentData(string paymentRefNumber)
        {
            throw new NotImplementedException();
            //DateTime now = DateTime.Now;

            //var userInfo = GetCurrentUserInfo();

            //int applicationId = int.Parse(paymentRefNumber);
            //var paymentDetails = GetPaymentDetails(applicationId);

            //EGovPaymentRequestModel result = new EGovPaymentRequestModel
            //{
            //    ApplicantIdentifier = userInfo.EgnLnch,
            //    ApplicantName = userInfo.Name,
            //    PaymentAmount = (float)paymentDetails.Amount,
            //    PaymentReason = paymentDetails.Description,
            //    PaymentRefDate = now,
            //    PaymentRefNumber = paymentRefNumber
            //};

            //if (userInfo.IdentifierType == IdentifierTypeEnum.EGN)
            //{
            //    result.ApplicantType = ApplicantTypes.EGN;
            //}
            //else if (userInfo.IdentifierType == IdentifierTypeEnum.LNC)
            //{
            //    result.ApplicantType = ApplicantTypes.LNCH;
            //}
            //else
            //{
            //    throw new ArgumentException("Unknown applicant type", nameof(result.ApplicantType));
            //}

            //return result;
        }

        public void PaymentResponseCallback(VPOSPaymentResponse vPOSPaymentResponse)
        {
            //throw new NotImplementedException();
            //int applicationId = GetApplicationIdByPaymentRef(vPOSPaymentResponse.RequestId);
            //logger.LogWarning($"{nameof(PaymentResponseCallback)} ApplicationId: {applicationId} PaymentStatus: {vPOSPaymentResponse.Status}");

            ////MovePaymentStatus(applicationId, vPOSPaymentResponse.Status != VPOSPaymentStatus.SUCCESS);
        }

        public void SavePaymentId(string paymentRefNumber, string paymentId)
        {
            var wApplication = 
                _dbContext
                .WApplications
                .Include("ApplicationType")
                .Where(wa => wa.RegistrationNumber == paymentRefNumber)
                .FirstOrDefault();

            if (wApplication != null)
            {

                APayment payment = new APayment();
                payment.Id = BaseEntity.GenerateNewId();
                payment.WApplicationId = wApplication.WApplicationId;

                EPayment ePayment = new EPayment();
                ePayment.Id = BaseEntity.GenerateNewId();

                ePayment.Amount = wApplication.ApplicationType.Price;
                ePayment.PaymentStatus = PaymentConstants.PaymentStatuses.Pending;
                ePayment.InvoiceNumber = paymentId;
                payment.EPaymentId = ePayment.Id;
                payment.EPayment = ePayment;
                wApplication.APayments.Add(payment);

                _dbContext.EPayments.Add(ePayment);
                _dbContext.APayments.Add(payment);
                _dbContext.SaveChanges();
            }
        }

        private void UpdateApplicationStatusReason(int applicationId, string statusReason)
        {
            throw new NotImplementedException();
            //DateTime now = DateTime.Now;
            //Application application = (from appl in Db.Applications.AsSplitQuery().Include(x => x.ApplicationChangeHistories)
            //                           where appl.Id == applicationId
            //                           select appl).Single();

            //application.StatusReason = statusReason;

            //Db.AddApplicationChangeHistory(application, now);
            //Db.SaveChanges();
        }
    }
}
