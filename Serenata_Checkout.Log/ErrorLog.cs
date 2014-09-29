using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace Serenata_Checkout.Log
{
    public class ErrorLog
    {
        #region Property and data member

        private static log4net.ILog _logger = null;

        // Get logger information from config file
        private static ILog Logger
        {
            get
            {
                if (_logger == null)
                {
                    _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                }

                return _logger;
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Logged the debug error with appropriate user message in log file
        /// </summary>
        /// <param name="message"></param>
        public static void Debug(object message)
        {
            if (Logger.IsDebugEnabled == true)
            {
                Logger.Logger.Log(typeof(ErrorLog), log4net.Core.Level.Debug, message, null);
            }

        }

        /// <summary>
        /// Logged the debug error in log file with user message and exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Debug(object message, Exception exception)
        {
            if (Logger.IsDebugEnabled == true)
            {
                Logger.Logger.Log(typeof(ErrorLog), log4net.Core.Level.Debug, message, exception);
            }

        }

        /// <summary>
        /// Logged the error with appropriate user message in log file
        /// </summary>
        /// <param name="message"></param>
        public static void Error(object message)
        {
            if (Logger.IsErrorEnabled == true)
            {
                Logger.Logger.Log(typeof(ErrorLog), log4net.Core.Level.Error, message, null);
            }

        }

        /// <summary>
        /// Logged the error in log file with user message and exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Error(object message, Exception exception)
        {
            if (Logger.IsErrorEnabled == true)
            {
                Logger.Logger.Log(typeof(ErrorLog), log4net.Core.Level.Error, message, exception);
            }

        }

        /// <summary>
        /// Logged the fatal error with appropriate user message in log file
        /// </summary>
        /// <param name="message"></param>
        public static void Fatal(object message)
        {
            if (Logger.IsFatalEnabled == true)
            {
                Logger.Logger.Log(typeof(ErrorLog), log4net.Core.Level.Fatal, message, null);
            }

        }

        /// <summary>
        /// Logged the fatal error in log file with user message and exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Fatal(object message, Exception exception)
        {
            if (Logger.IsFatalEnabled == true)
            {
                Logger.Logger.Log(typeof(ErrorLog), log4net.Core.Level.Fatal, message, exception);
            }

        }

        /// <summary>
        /// Loged the info with appropriate user message
        /// </summary>
        /// <param name="message"></param>
        public static void Info(object message)
        {
            if (Logger.IsInfoEnabled == true)
            {
                Logger.Logger.Log(typeof(ErrorLog), log4net.Core.Level.Info, message, null);

            }

        }

        /// <summary>
        /// Loged the info message with user message and exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Info(object message, Exception exception)
        {
            if (Logger.IsInfoEnabled == true)
            {
                Logger.Logger.Log(typeof(ErrorLog), log4net.Core.Level.Info, message, exception);
            }

        }

        /// <summary>
        /// Loged the warning with user message
        /// </summary>
        /// <param name="message"></param>
        public static void Warn(object message)
        {
            if (Logger.IsWarnEnabled == true)
            {
                Logger.Logger.Log(typeof(ErrorLog), log4net.Core.Level.Warn, message, null);
            }

        }

        /// <summary>
        /// Logged the warning with user messege and exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Warn(object message, Exception exception)
        {
            if (Logger.IsWarnEnabled == true)
            {
                Logger.Logger.Log(typeof(ErrorLog), log4net.Core.Level.Warn, message, exception);
            }

        }

        /// <summary>
        /// Set the hierarchy lavel for log4net repository.
        /// </summary>
        /// <param name="loggerName"></param>
        /// <param name="levelName"></param>
        public static void SetLevel(string loggerName, string levelName)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(loggerName);
            log4net.Repository.Hierarchy.Logger l = (log4net.Repository.Hierarchy.Logger)log.Logger;
            l.Level = l.Hierarchy.LevelMap[levelName];
        }

        /// <summary>
        /// To shut down the logger repository
        /// </summary>
        public static void ShutdownLog()
        {
            if (_logger != null)
                Logger.Logger.Repository.Shutdown();
        }

        #endregion
    }
}
