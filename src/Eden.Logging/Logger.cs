using log4net;
using System.Reflection;


namespace Eden.Logging
{
    public class Logger : Application.Interfaces.ILogger
    {
        #region ===[ Private Members ]=============================================================

        private static readonly ILog MainLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);
        private static readonly Lazy<Logger> LoggerInstance = new(() => new Logger());

        private const string ExceptionName = "Exception";
        private const string InnerExceptionName = "Inner Exception";
        private const string ExceptionMessageWithoutInnerException = "{0}{1}: {2}Message: {3}{4}StackTrace: {5}.";
        private const string ExceptionMessageWithInnerException = "{0}{1}{2}";

        #endregion

        #region ===[ Properties ]==================================================================

        /// <summary>
        /// Gets the Logger instance.
        /// </summary>
        public static Logger Instance
        {
            get { return LoggerInstance.Value; }
        }

        #endregion

        #region ===[ ILogger Members ]=============================================================

        /// <summary>
        /// Logs a message object with the log4net.Core.Level.Debug level.
        /// </summary>
        /// <param name="message"></param>
        public void Debug(object message)
        {
            if (MainLogger.IsDebugEnabled)
                MainLogger.Debug(message);
        }

        /// <summary>
        /// Logs a message object with the log4net.Core.Level.Info level.
        /// </summary>
        /// <param name="message"></param>
        public void Info(object message)
        {
            if (MainLogger.IsInfoEnabled)
                MainLogger.Info(message);
        }

        /// <summary>
        /// Logs a message object with the log4net.Core.Level.Info Warning.
        /// </summary>
        /// <param name="message"></param>
        public void Warn(object message)
        {
            if (MainLogger.IsWarnEnabled)
                MainLogger.Warn(message);
        }

        /// <summary>
        /// Logs a message object with the log4net.Core.Level.Error level.
        /// </summary>
        /// <param name="message"></param>
        public void Error(object message)
        {
            MainLogger.Error(message);
        }

        /// <summary>
        /// Logs a message object with the log4net.Core.Level.Fatal level.
        /// </summary>
        /// <param name="message"></param>
        public void Fatal(object message)
        {
            MainLogger.Fatal(message);
        }

        /// <summary>
        /// Logs a message object with the log4net.Core.Level.Debug level including the exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Debug(object message, Exception exception)
        {
            if (MainLogger.IsDebugEnabled)
                MainLogger.Debug(message, exception);
        }

        /// <summary>
        /// Logs a message object with the log4net.Core.Level.Info level including the exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Info(object message, Exception exception)
        {
            if (MainLogger.IsInfoEnabled)
                MainLogger.Info(message, exception);
        }

        /// <summary>
        /// Logs a message object with the log4net.Core.Level.Warn level including the exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Warn(object message, Exception exception)
        {
            if (MainLogger.IsWarnEnabled)
                MainLogger.Info(message, exception);
        }

        /// <summary>
        /// Logs a message object with the log4net.Core.Level.Error level including the exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Error(object message, Exception exception)
        {
            MainLogger.Error(message, exception);
        }

        /// <summary>
        /// Logs a message object with the log4net.Core.Level.Fatal level including the exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Fatal(object message, Exception exception)
        {
            MainLogger.Fatal(message, exception);
        }

        /// <summary>
        /// Log an exception with the log4net.Core.Level.Error level including the stack trace of the System.Exception passed as a parameter.
        /// </summary>
        /// <param name="exception"></param>
        public void Error(Exception exception)
        {
            MainLogger.Error(SerializeException(exception, ExceptionName));
        }

        /// <summary>
        /// Log an exception with the log4net.Core.Level.Fatal level including the stack trace of the System.Exception passed as a parameter.
        /// </summary>
        /// <param name="exception"></param>
        public void Fatal(Exception exception)
        {
            MainLogger.Fatal(SerializeException(exception, ExceptionName));
        }

        #endregion

        #region ===[ Public Methods ]==============================================================

        /// <summary>
        /// Serialize Exception to get the complete message and stack trace.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static string SerializeException(Exception exception)
        {
            return SerializeException(exception, string.Empty);
        }

        #endregion

        #region ===[ Private Methods ]=============================================================

        /// <summary>
        /// Serialize Exception to get the complete message and stack trace.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="exceptionMessage"></param>
        /// <returns></returns>
        private static string SerializeException(Exception ex, string exceptionMessage)
        {
            var mesgAndStackTrace = string.Format(ExceptionMessageWithoutInnerException, Environment.NewLine,
                exceptionMessage, Environment.NewLine, ex.Message, Environment.NewLine, ex.StackTrace);

            if (ex.InnerException != null)
            {
                mesgAndStackTrace = string.Format(ExceptionMessageWithInnerException, mesgAndStackTrace,
                    Environment.NewLine,
                    SerializeException(ex.InnerException, InnerExceptionName));
            }

            return mesgAndStackTrace + Environment.NewLine;
        }

        #endregion
    }
}
