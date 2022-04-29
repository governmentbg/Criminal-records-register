using System.Data;
using System.Data.Common;

namespace MJ_CAIS.Common.Exceptions
{
    public static class ExceptionUtils
    {
        private static readonly string LinesSeparator = new string('=', 27);
        private const string ErrorMessageTemplate = "{0}\n{1}\n{2}{3}";

        public static string GetFormatedLastError(Exception exception)
        {
            var lastException = exception;
            while (lastException.InnerException != null)
            {
                lastException = lastException.InnerException;
            }

            string exceptionMessage = GetFullExceptionDetails(exception, false);
            string userFriendlyMessage = GetUserFriendlyMessage(lastException, ref exceptionMessage);
            string result = $"{userFriendlyMessage}\n{exceptionMessage}";
            return result;
        }

        public static string GetFormatedErrorMessage(Exception exception, bool beautifyNewLine = true)
        {
            string exceptionMessage = GetFullExceptionDetails(exception, true);
            string userFriendlyMessage = GetUserFriendlyMessage(exception, ref exceptionMessage);
            
            var exeptionName = exception.GetType().FullName;
            var time = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            var message = string.Format(ErrorMessageTemplate, userFriendlyMessage, time, exceptionMessage, exeptionName);

            if (beautifyNewLine)
            {
                return HtmlBeautifyNewLine(message);
            }

            return message;
        }

        public static string HtmlBeautifyNewLine(string text)
        {
            var result = text;
            if (!string.IsNullOrEmpty(text))
            {
                result = text.Replace("\r", "").Replace("\n", "<br>");
                result = result.Replace(" ", "&nbsp;");
            }

            return result;
        }

        public static string GetFullExceptionDetails(Exception e, bool includeStackTrace = true)
        {
            string newLine = Environment.NewLine;
            string message = e.Message;
            string exceptionType = e.GetType().ToString();

            string stack = "";
            if (includeStackTrace && !string.IsNullOrEmpty(e.StackTrace))
            {
                stack = e.StackTrace + newLine;
            }

            string result = string.Format("{0}{1}{2}{1}{3}{1}{4}",
                LinesSeparator,
                newLine,
                exceptionType,
                message,
                stack);
            if (e.InnerException != null)
            {
                result += GetFullExceptionDetails(e.InnerException, includeStackTrace);
            }

            return result;
        }

        private static string GetUserFriendlyMessage(Exception exception, ref string exceptionMessage)
        {
            string userFriendlyMessage;
            if (exception is DbException || exception is DataException)
            {
                userFriendlyMessage = CommonResources.MsgDataBaseError;
            }
            else if (exception.GetType() == typeof(ArgumentException))
            {
                userFriendlyMessage = exception.Message;
                if (exception.InnerException != null)
                {
                    userFriendlyMessage += " " + exception.InnerException.Message;
                }
            }
            else
            {
                userFriendlyMessage = CommonResources.MsgOperationException;
            }

            return userFriendlyMessage;
        }

        private static Exception FindLastInnerException(Exception ex)
        {
            if (ex.InnerException != null)
            {
                return FindLastInnerException(ex.InnerException);
            }

            return ex;
        }
    }
}
