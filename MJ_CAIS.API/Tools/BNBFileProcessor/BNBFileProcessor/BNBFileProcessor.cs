using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BNBFileProcessor
{
    public class BNBFileProcessor : IBNBEventProcessor

    {
        private CaisDbContext _dbContext;
        private readonly ILogger<BNBFileProcessor> _logger;
        private readonly IConfiguration _config;
     
        private  string regex2Str => _config.GetValue<string>("BNBPayments:regex1");
        private  string regex1Str => _config.GetValue<string>("BNBPayments:regex2");
        private int sizeOfRegNumber => _config.GetValue<int>("BNBPayments:sizeOfRegNumber");
        public BNBFileProcessor(CaisDbContext dbContext, ILogger<BNBFileProcessor> logger, IConfiguration config)
        {
            _dbContext = dbContext;
            _logger = logger;
            _config = config;
        }
        public async Task Process(string fullPath)
        {
                string bnbIban = (await _dbContext.GSystemParameters
                .FirstOrDefaultAsync(p => p.Code == SystemParametersConstants.SystemParametersNames.MJ_IBAN_BNB))?.ValueString;
            if (string.IsNullOrEmpty(bnbIban))
            {
                throw new Exception($"Parameter {SystemParametersConstants.SystemParametersNames.MJ_IBAN_BNB} not set.");
            }
            await ProcessFile(fullPath, _dbContext, bnbIban);

        }

        private  async Task ProcessFile(string paymentFilePath, CaisDbContext? dbContext, string bnbIban)
        {
            var payment = new PaymentFileReader(paymentFilePath, 1251, bnbIban, true);
            try
            {
                while (!payment.EndOfFile)
                {
                    var row = payment.ReadLine();

                    if (row != null)
                    {
                        row.Id = BaseEntity.GenerateNewId();
                        row.EntityState = MJ_CAIS.Common.Enums.EntityStateEnum.Added;
                        var paymentCode = GetPaymentCodeFromPaymentReason(row.PaymentReason);

                        if (paymentCode != null)
                        {
                            var webAppl = await dbContext.WApplications.AsNoTracking()
                                 .Include(w => w.APayments)
                                 .ThenInclude(p => p.EPayment).AsNoTracking()
                                 .FirstOrDefaultAsync(w => w.RegistrationNumber == paymentCode);
                            var bnbAmount = row.SentAmount;
                            if (webAppl != null)
                            {
                                if (webAppl.APayments.Count > 0)
                                {
                                    var epayments = webAppl.APayments.Select(p => p.EPayment);
                                    if (epayments.Count() > 0)
                                    {
                                        if (epayments.Sum(p => p.Amount) == bnbAmount)
                                        {
                                            foreach (var ep in epayments)
                                            {   //todo: add fk from ep to row???
                                                ep.PaymentStatus = PaymentConstants.PaymentStatuses.Payed;
                                                ep.BnbPayId = row.Id;
                                                ep.ModifiedProperties = new List<string>() { nameof(ep.PaymentStatus), nameof(ep.BnbPayId) };
                                                ep.EntityState = MJ_CAIS.Common.Enums.EntityStateEnum.Modified;
                                            }

                                            dbContext.ApplyChanges(epayments.ToList());
                                            dbContext.ApplyChanges(row);
                                        }
                                        else
                                        {
                                            //todo: какво да правим в случай на разлика?
                                            _logger.LogWarning($"{paymentCode}: sum of amounts in EPayment is different from  the amount in file.");
                                        }
                                    }
                                    else
                                    {
                                        //todo: какво да правим в случай на разлика?
                                        _logger.LogWarning($"{paymentCode}: no EPayments for this paymaent code.");

                                    }


                                }
                                else
                                {
                                    //todo: какво да правим в случай на разлика?
                                    _logger.LogWarning($"{paymentCode}: no APayments for this paymaent code.");
                                }

                            }
                            else
                            {
                                _logger.LogWarning($"{paymentCode}: corresponding WApplication not found.");
                            }
                        }



                    }
                }
                await dbContext.SaveChangesAsync();
                dbContext.ChangeTracker.Clear();
            }
            finally
            {
                payment.Dispose();
            }

        }

        private string GetPaymentCodeFromPaymentReason(string paymentReason)
        {
            var regex1 = new Regex(regex1Str, RegexOptions.Compiled);
            var regex2 = new Regex(regex2Str, RegexOptions.Compiled);
            if (string.IsNullOrWhiteSpace(paymentReason) || paymentReason.Length < sizeOfRegNumber)
                return null;

            var num = string.Empty;
            if (regex1.IsMatch(paymentReason))
                num = regex1.Match(paymentReason).Groups["code"].Value;
            else if (regex2.IsMatch(paymentReason))
                num = regex2.Match(paymentReason).Groups["code"].Value.Substring(0, sizeOfRegNumber);
            if (!string.IsNullOrWhiteSpace(num)) return num;
            //if (!string.IsNullOrWhiteSpace(num) && VerhoeffChecksum.ValidateVerhoeffDigit(num))
            //    return num;

            return null;
        }

    }
}
