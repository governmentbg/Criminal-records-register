using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DIContainer;
using NLog;
using NLog.Web;
using System.Net;
using System.Net.Mail;

namespace EmailSender
{
    public class Program
    {
        private static string HostUrl;
        private static string MailAddress;
        private static bool IsBodyHtml;
        private static int SmtpServerPort;
        private static bool SmtpServerEnableSsl;
        private static string NetworkUserName;
        private static string NetworkPassword;

        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

            try
            {
                logger.Info($"Execution started.");
                IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices(services => ContainerExtension.Initialize(services, config))
                  .ConfigureServices(services => services.AddSingleton<IUserContext>(new UserContext()
                  {
                      UserId = config.GetValue<string>("ContextUser:UserId"),
                      UserName = config.GetValue<string>("ContextUser:UserName")
                  }))

                    .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);

                    })
                    .UseNLog()
                .Build();

                using (host)
                {
                    FillConfigValues(config, logger);

                    var dbContext = host.Services.GetService<CaisDbContext>();

                    //try
                    //{
                    SendAllMails(dbContext, logger);
                    //}
                    //catch (Exception ex)
                    //{
                    //    // TODO:
                    //    //_logger.Error(ex);
                    //    throw;
                    //}
                    //}
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

        private static void FillConfigValues(IConfiguration config, Logger logger)
        {
            var section = config.GetSection("EmailSettings");
            HostUrl = section.GetValue<string>("HostUrl");
            MailAddress = section.GetValue<string>("MailAddress");
            IsBodyHtml = section.GetValue<bool>("IsBodyHtml");
            SmtpServerPort = section.GetValue<int>("SmtpServerPort");
            SmtpServerEnableSsl = section.GetValue<bool>("SmtpServerEnableSsl");
            NetworkUserName = section.GetValue<string>("NetworkUserName");
            NetworkPassword = section.GetValue<string>("NetworkPassword");
        }

        private static void SendAllMails(CaisDbContext dbContext, Logger logger)
        {
            var numberOfAttempts = dbContext.GSystemParameters.AsNoTracking().FirstOrDefault(x => x.Code == "EMAIL_NUMBER_OF_ATTEMPTS")?.ValueNumber;
            if (numberOfAttempts == null)
            {
                throw new Exception("Parameter EMAIL_NUMBER_OF_ATTEMPTS is not set.");
            }
            var emailsToSend = dbContext.EEmailEvents
                .Where(e => e.EmailStatus == EmailStatusConstants.Pending || (e.EmailStatus == EmailStatusConstants.Rejected && e.Attempts < numberOfAttempts))
                .Where(x => !string.IsNullOrEmpty(x.EmailAddress))
                //.Where(x => x.EmailAddress.EndsWith("technologica.com")) // TODO: remove later
                .ToList();

            logger.Info("Брой мейли за изпращане: " + emailsToSend.Count);

            foreach (var entity in emailsToSend)
            {
                bool success = false;
                try
                {
                    //Правим го, защото ако гръмне catch или записването на успеха няма да отбележим, че сме изпратили мейла и може да го пратим повторно
                    entity.EmailStatus = EmailStatusConstants.InProgress;
                    dbContext.SaveChanges();

                    SendEmailMessage(entity.EmailAddress, entity.Subject, entity.Body);
                    success = true;

                    logger.Info("Успешно изпратен мейл до \"" + entity.EmailAddress + "\"");
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Грешка при изпращане на мейл до \"" + entity.EmailAddress + "\"");

                    entity.HasError = true;
                    entity.Error = ex.GetType().FullName + ": " + ex.Message;
                    entity.StackTrace = ex.StackTrace;
                    entity.Attempts = (byte)(entity.Attempts == 0 ? 1 : entity.Attempts + 1);
                    entity.SentDate = DateTime.Now;
                    entity.EmailStatus = EmailStatusConstants.Rejected;

                    dbContext.SaveChanges();
                }

                if (success)
                {
                    entity.Attempts = (byte)(entity.Attempts == 0 ? 1 : entity.Attempts + 1);
                    entity.SentDate = DateTime.Now;
                    entity.EmailStatus = EmailStatusConstants.Accepted;

                    dbContext.SaveChanges();
                }
            }
        }

        static void SendEmailMessage(string email, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(HostUrl);

            mail.From = new MailAddress(MailAddress);
            mail.To.Add(email);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = IsBodyHtml;

            SmtpServer.Port = SmtpServerPort;
            SmtpServer.EnableSsl = SmtpServerEnableSsl;

            if (!string.IsNullOrEmpty(NetworkUserName))
            {
                SmtpServer.Credentials = new NetworkCredential(NetworkUserName, NetworkPassword);
            }

            SmtpServer.Send(mail);
        }
    }
}