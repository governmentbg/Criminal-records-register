using MJ_CAIS.DataAccess;
using TL.EGovPayments.ControllerModels;
using TL.EGovPayments.Interfaces;
using TL.EGovPayments.JsonModels;

namespace MJ_CAIS.WebPortal.Public.Services
{
    public class EGovPaymentsService : IEGovIntegrationService
    {
        public EGovPaymentsService(CaisDbContext dbContext)
        { }

        public bool ChangePaymentStatusCallback(PaymentStatus paymentStatus)
        {
            
            //TODO: Insert
            return true;

            //try
            //{
            //    int applicationId = GetApplicationIdByPaymentRef(paymentStatus.Id);
            //    var statuses = GetApplicationStatus(applicationId);

            //    if ((paymentStatus.Status == PaymentRequestStatuses.PAID
            //        || paymentStatus.Status == PaymentRequestStatuses.AUTHORIZED
            //        || paymentStatus.Status == PaymentRequestStatuses.ORDERED)
            //       && statuses.Item1 != PaymentStatusesEnum.PaidOK)
            //    {
            //        MarkPaymentAsPaid(applicationId, paymentStatus.Id, paymentStatus.Status.ToString(), paymentStatus.ChangeTime);
            //    }
            //    else if ((paymentStatus.Status == PaymentRequestStatuses.EXPIRED
            //                || paymentStatus.Status == PaymentRequestStatuses.CANCELED
            //                || paymentStatus.Status == PaymentRequestStatuses.SUSPENDED)
            //             && statuses.Item2 != ApplicationStatusesEnum.PAYMENT_ANNUL)
            //    {
            //        MarkPaymentAsAnnuled(applicationId, paymentStatus.Status.ToString());
            //    }
            //    else // for PENDING payment status
            //    {
            //        string newStatusReason = string.Format(AppResources.statusInPayEGovBG, paymentStatus.Status.ToString());
            //        this.UpdateApplicationStatusReason(applicationId, newStatusReason);
            //    }

            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    logger.LogException(ex);
            //    return false;
            //}
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
            throw new NotImplementedException();
            //int applicationId = GetApplicationIdByPaymentRef(vPOSPaymentResponse.RequestId);
            //logger.LogWarning($"{nameof(PaymentResponseCallback)} ApplicationId: {applicationId} PaymentStatus: {vPOSPaymentResponse.Status}");

            ////MovePaymentStatus(applicationId, vPOSPaymentResponse.Status != VPOSPaymentStatus.SUCCESS);
        }

        public void SavePaymentId(string paymentRefNumber, string paymentId)
        {

            //TODO


            //int applicationId = int.Parse(paymentRefNumber);
            //ApplicationPayment applicationPayment = (from appl in Db.Applications
            //                                         join applPayment in Db.ApplicationPayments on appl.Id equals applPayment.ApplicationId
            //                                         where appl.Id == applicationId
            //                                         select applPayment).Single();

            //applicationPayment.PaymentRefNum = paymentId;

            //Db.SaveChanges();
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
