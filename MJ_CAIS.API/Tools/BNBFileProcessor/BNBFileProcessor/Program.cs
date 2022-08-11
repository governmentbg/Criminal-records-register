using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MJ_CAIS.DIContainer;
using MJ_CAIS.DataAccess;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using System.Text.RegularExpressions;
using MJ_CAIS.DataAccess.Entities;

namespace BNBFileProcessor
{
    public class Program
    {
        private static Logger logger;
        private static string regex1Str;
        private static string regex2Str;
        static async Task Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

            try
            {
                logger.Info($"Execution started.");
       

                IHost host = Host.CreateDefaultBuilder()
                    .ConfigureServices(services => ContainerExtension.Initialize(services, config))
                    //.ConfigureServices(services => services.AddSingleton(typeofExecutor))
                    //.ConfigureServices(services => services.AddAutoMapper(typeof(ApplicationProfile).Assembly))
                    //.ConfigureServices(services => services.AddSingleton<IUserContext>(new UserContext()
                    //{
                    //    UserId = config.GetValue<string>("ContextUser:UserId"),
                    //    UserName = config.GetValue<string>("ContextUser:UserName")
                    //}))

                    .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);

                    })
                    .UseNLog()

                    .Build();


                regex1Str = config.GetValue<string>("BNBPayments:regex1");
                regex2Str = config.GetValue<string>("BNBPayments:regex1");

                using (host)
                {
                    //todo: add file monitoring and pass files one by one
                    var paymentFilePath = $"C:\\Projects\\MJ-CAIS\\MJ_CAIS.API\\Tools\\BNBFileProcessor\\BNBFileProcessor\\Files\\IZ_220620_1737_1737.txt";
                    var dbContext = host.Services.GetService<CaisDbContext>();
                    string bnbIban = (await dbContext.GSystemParameters
                        .FirstOrDefaultAsync(p => p.Code == SystemParametersConstants.SystemParametersNames.MJ_IBAN_BNB))?.ValueString;
                    if (string.IsNullOrEmpty(bnbIban))
                    {
                        throw new Exception($"Parameter {SystemParametersConstants.SystemParametersNames.MJ_IBAN_BNB} not set.");
                    }
                    await ProcessFile(paymentFilePath, dbContext, bnbIban);

                }

            }
            catch (Exception ex)

            {
                logger.Error(ex, ex.Message, ex.Data);

            }
            finally
            {
                logger.Info($"Execution ended.");
                NLog.LogManager.Flush();
                NLog.LogManager.Shutdown();
            }




        }

        private static async Task ProcessFile(string paymentFilePath, CaisDbContext? dbContext, string bnbIban)
        {
            var payment = new PaymentFileReader(paymentFilePath, 1251, bnbIban, true);

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
                        var bnbAmount = row.Amount;
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
                                            ep.ModifiedProperties = new List<string>() { nameof(ep.PaymentStatus) , nameof(ep.BnbPayId) };
                                            ep.EntityState = MJ_CAIS.Common.Enums.EntityStateEnum.Modified;
                                        }

                                        dbContext.ApplyChanges(epayments.ToList());
                                        dbContext.ApplyChanges(row);
                                    }
                                    else
                                    {
                                        //todo: какво да правим в случай на разлика?
                                        logger.Warn($"{paymentCode}: sum of amounts in EPayment is different from  the amount in file.");
                                    }
                                }
                                else
                                {
                                    //todo: какво да правим в случай на разлика?
                                    logger.Warn($"{paymentCode}: no EPayments for this paymaent code.");

                                }


                            }
                            else
                            {
                                //todo: какво да правим в случай на разлика?
                                logger.Warn($"{paymentCode}: no APayments for this paymaent code.");
                            }

                        }
                        else
                        {
                            logger.Warn($"{paymentCode}: corresponding WApplication not found.");
                        }
                    }
               


                }
            }
            await dbContext.SaveChangesAsync();
            dbContext.ChangeTracker.Clear();
            payment.Dispose();
        }

        public static string GetPaymentCodeFromPaymentReason(string paymentReason)
        {
            var regex1 = new Regex(regex1Str, RegexOptions.Compiled);
            var regex2 = new Regex(regex2Str, RegexOptions.Compiled);
            if (string.IsNullOrWhiteSpace(paymentReason) || paymentReason.Length < 11)
                return null;

            var num = string.Empty;
            if (regex1.IsMatch(paymentReason))
                num = regex1.Match(paymentReason).Groups["code"].Value;
            else if (regex2.IsMatch(paymentReason))
                num = regex2.Match(paymentReason).Groups["code"].Value.Substring(0, 11);
            if (!string.IsNullOrWhiteSpace(num)) return num;
            //if (!string.IsNullOrWhiteSpace(num) && VerhoeffChecksum.ValidateVerhoeffDigit(num))
            //    return num;

            return null;
        }


    }
}


