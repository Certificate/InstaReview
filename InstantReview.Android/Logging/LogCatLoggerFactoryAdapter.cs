using Common.Logging;
using Common.Logging.Configuration;
using Common.Logging.Simple;

namespace InstantReview.Droid.Logging
{
    /// <summary>
    /// Factory for creating <see cref="ILog" /> instances that write data using Android LogCat" />.
    /// </summary>
    /// <seealso cref="AbstractSimpleLoggerFactoryAdapter"/>
    public class LogCatFactoryAdapter : AbstractSimpleLoggerFactoryAdapter
    {
        public string LogName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogCatFactoryAdapter"/> class using default 
        /// settings.
        /// </summary>
        public LogCatFactoryAdapter()
            : base(null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogCatFactoryAdapter"/> class.
        /// </summary>
        /// <remarks>
        /// Looks for level, showDateTime, showLogName, dateTimeFormat items from 
        /// <paramref key="properties" /> for use when the GetLogger methods are called.
        /// <see cref="System.Configuration.ConfigurationManager"/> for more information on how to use the 
        /// standard .NET application configuraiton file (App.config/Web.config) 
        /// to configure this adapter.
        /// </remarks>
        /// <param name="properties">The key value collection, typically specified by the user in 
        /// a configuration section named common/logging.</param>
        public LogCatFactoryAdapter(NameValueCollection properties)
            : base(properties)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractSimpleLoggerFactoryAdapter"/> class with 
        /// default settings for the loggers created by this factory.
        /// </summary>
        public LogCatFactoryAdapter(string logName, LogLevel level, bool showDateTime, bool showLogName, bool showLevel, string dateTimeFormat)
            : base(level, showDateTime, showLogName, showLevel, dateTimeFormat)
        {
            LogName = logName;
        }

        /// <summary>
        /// Creates a new <see cref="LogCatLogger"/> instance.
        /// </summary>
        protected override ILog CreateLogger(string name, LogLevel level, bool showLevel, bool showDateTime, bool showLogName, string dateTimeFormat)
        {
            ILog log = new LogCatLogger(LogName, name, level, showLevel, showDateTime, showLogName, dateTimeFormat);
            return log;
        }
    }
}