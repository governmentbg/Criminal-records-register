using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  BNBFileProcessor.ExceptionHandlers
{
    public class EventListenerErrorLogger
    {
        private static Logger NLogger => LogManager.GetLogger(nameof(EventListenerErrorLogger));

        public static string LogConncectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["logConnectionString"].ConnectionString;
            }
        }

        public static async Task HandleExceptionAsync(Exception exception, EventLog appEventLog, string fileName = null)
        {
            LogInEventLog(exception, appEventLog, fileName);
            await LogInDbAsync(exception, fileName);
        }

        public static async Task HandleExceptionAsync(AggregateException aggregateException, EventLog appEventLog, string fileName = null)
        {
            LogInEventLog(aggregateException, appEventLog, fileName);
            await LogInDbAsync(aggregateException, fileName);
        }

        public static void LogInEventLog(Exception exception, EventLog AppEventLog, string fileName = null)
        {
            var exceptionMessage = string.IsNullOrEmpty(fileName) ? string.Empty : $"FileName: {fileName}{Environment.NewLine}";
            exceptionMessage += GetExceptionFullStackTrace(exception);

            AppEventLog.WriteEntry(exceptionMessage, EventLogEntryType.Error);
        }


        public static void LogInEventLog(string message, EventLog AppEventLog, string fileName = null)
        {
            fileName = string.IsNullOrEmpty(fileName) ? string.Empty : $"FileName: {fileName}";

            var messageToLog = $"{fileName}{Environment.NewLine}{message}";

            AppEventLog.WriteEntry(messageToLog, EventLogEntryType.Information);
        }

        public static void LogInEventLog(AggregateException aggregateException, EventLog AppEventLog, string fileName = null)
        {
            StringBuilder stringBuilder = new StringBuilder(string.IsNullOrEmpty(fileName) ? string.Empty : $"FileName: {fileName}{Environment.NewLine}");

            foreach (var exception in aggregateException.Flatten().InnerExceptions)
            {
                stringBuilder.Append(GetExceptionFullStackTrace(exception));
                stringBuilder.AppendLine();
            }

            AppEventLog.WriteEntry(stringBuilder.ToString(), EventLogEntryType.Error);
        }

        public static async Task LogInDbAsync(AggregateException aggregateException, string fileName = null)
        {
            foreach (var exception in aggregateException.Flatten().InnerExceptions)
            {
                await LogInDbAsync(exception, fileName);
            }
        }

        public static async Task LogInDbAsync(Exception exception, string fileName = null)
        {
            try
            {
              //  var insertQuery = "INSERT INTO SystemErrorLog (LogDateTime,ErrorType,ErrorMessage,ErrorStackTrace,UserName,ExternalSource)" +
              //"VALUES (@LogDateTime,@ErrorType,@ErrorMessage,@ErrorStackTrace,@UserName, @ExternalSource)";

              //  using (SqlConnection connection = new SqlConnection(LogConncectionString))
              //  {
              //      var innerExceptionMessage = FindInnerException(exception).Message;

              //      var errorMessage = string.IsNullOrEmpty(fileName) ? innerExceptionMessage : $"FileName: {fileName}{Environment.NewLine}{innerExceptionMessage}";

              //      SqlCommand command = new SqlCommand(insertQuery, connection);
              //      command.Parameters.AddWithValue("@LogDateTime", DateTime.Now);
              //      command.Parameters.AddWithValue("@ErrorType", exception.GetType().FullName);
              //      command.Parameters.AddWithValue("@ErrorMessage", errorMessage);
              //      command.Parameters.AddWithValue("@ErrorStackTrace", GetExceptionFullStackTrace(exception));
              //      command.Parameters.AddWithValue("@UserName", "windows-service-account");
              //      command.Parameters.AddWithValue("@ExternalSource", System.Diagnostics.Process.GetCurrentProcess().ProcessName);

              //      await connection.OpenAsync();
              //      await command.ExecuteNonQueryAsync();
              //  }
            }
            catch (Exception ex)
            {
                NLogger.Error("{@ExceptionMessage}{@Exception}", ex.Message, ex);
            }
        }

        private static Exception FindInnerException(Exception ex)
        {
            if (ex.InnerException != null)
            {
                return FindInnerException(ex.InnerException);
            }
            else { return ex; }
        }


        private static string GetExceptionFullStackTrace(Exception e)
        {
            string stackTrace = string.Format("{1}{0}{2}{0}{3}{0}{4}{0}",
                Environment.NewLine,
                e.GetType().ToString(),
                e.Source,
                e.Message,
                e.StackTrace);
            if (e.InnerException != null)
            {
                stackTrace = stackTrace + GetExceptionFullStackTrace(e.InnerException);
            }
            return stackTrace;
        }
    }
}
