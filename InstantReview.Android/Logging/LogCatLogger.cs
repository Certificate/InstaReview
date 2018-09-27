using System;
using System.Text;
using Android.Util;
using Common.Logging;
using Common.Logging.Simple;

namespace InstantReview.Droid.Logging
{
    /// <summary>
    /// Sends log messages to Android LogCat />.
    /// </summary>
    public class LogCatLogger : AbstractSimpleLogger
    {
        private string LogName { get; }

        /// <summary>
        /// Creates and initializes a logger that writes messages to Android LogCat />.
        /// </summary>
        /// <param name="logName">The key, usually type key of the calling class, of the logger.</param>
        /// <param name="logLevel">The current logging threshold. Messages recieved that are beneath this threshold will not be logged.</param>
        /// <param name="showLevel">Include the current log level in the log message.</param>
        /// <param name="showDateTime">Include the current time in the log message.</param>
        /// <param name="showLogName">Include the instance key in the log message.</param>
        /// <param name="dateTimeFormat">The date and time format to use in the log message.</param>
        public LogCatLogger(string logName, string packageName, LogLevel logLevel, bool showLevel, bool showDateTime, bool showLogName, string dateTimeFormat)
            : base(packageName, logLevel, showLevel, showDateTime, showLogName, dateTimeFormat)
        {
            LogName = logName;
        }

        /// <summary>
        /// Do the actual logging by constructing the log message using a <see cref="StringBuilder" /> then
        /// sending the output to LogCat />.
        /// </summary>
        /// <param name="level">The <see cref="LogLevel" /> of the message.</param>
        /// <param name="message">The log message.</param>
        /// <param name="e">An optional <see cref="Exception" /> associated with the message.</param>
        protected override void WriteInternal(LogLevel level, object message, Exception e)
        {
            // Use a StringBuilder for better performance
            StringBuilder sb = new StringBuilder();
            FormatOutput(sb, level, message, e);

            string msg = sb.ToString();

            // Print to the appropriate destination
            switch (level)
            {
                case LogLevel.Fatal:
                    Log.Wtf(LogName, msg);
                    break;
                case LogLevel.Error:
                    Log.Error(LogName, msg);
                    break;
                case LogLevel.Warn:
                    Log.Warn(LogName, msg);
                    break;
                case LogLevel.Info:
                    Log.Info(LogName, msg);
                    break;
                case LogLevel.Debug:
                    Log.Debug(LogName, msg);
                    break;
                case LogLevel.Trace:
                    Log.Verbose(LogName, msg);
                    break;
            }
        }
    }
}