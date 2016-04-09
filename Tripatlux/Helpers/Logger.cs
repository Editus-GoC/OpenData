using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;
using System.Linq;

namespace Tripatlux
{
    /// <summary>
    /// Outils pour les logs
    /// </summary>
    public static class Logger
    {
        #region Members
        private static readonly NLog.Logger _logger;
        #endregion


        #region CTORs
        static Logger()
        {
            _logger = NLog.LogManager.GetLogger("Logger");
        }
        #endregion


        #region Public Methods
        public static void Trace(string message, params object[] args)
        {
            if(_logger.IsTraceEnabled)
                _logger.Trace(FormatMessage(message), args);
        }
        public static void Trace(Exception ex, string message, params object[] args)
        {
            if (_logger.IsTraceEnabled)
                _logger.Trace(string.Format(FormatMessage(message), args), ex);
        }

        public static void Debug(string message, params object[] args)
        {
            if (_logger.IsDebugEnabled)
                _logger.Debug(FormatMessage(message), args);
        }
        public static void Debug(Exception ex, string message, params object[] args)
        {
            if (_logger.IsDebugEnabled)
                _logger.Debug(string.Format(FormatMessage(message), args), ex);
        }

        public static void Info(string message, params object[] args)
        {
            if (_logger.IsInfoEnabled)
                _logger.Info(FormatMessage(message), args);
        }
        public static void Info(Exception ex, string message, params object[] args)
        {
            if (_logger.IsInfoEnabled)
                _logger.Info(string.Format(FormatMessage(message), args), ex);
        }

        public static void Warn(string message, params object[] args)
        {
            if (_logger.IsWarnEnabled)
                _logger.Warn(FormatMessage(message), args);
        }
        public static void Warn(Exception ex, string message, params object[] args)
        {
            if (_logger.IsWarnEnabled)
                _logger.Warn(string.Format(FormatMessage(message), args), ex);
        }
        public static void Warn(Exception ex)
        {
            if (_logger.IsWarnEnabled)
                _logger.Warn(ex.Message, ex);
        }

        public static void Error(string message, params object[] args)
        {
            if (_logger.IsErrorEnabled)
                _logger.Error(FormatMessage(message), args);
        }
        public static void Error(Exception ex, string message, params object[] args)
        {
            if (_logger.IsErrorEnabled)
                _logger.Error(string.Format(FormatMessage(message), args), ex);
        }
        public static void Error(Exception ex)
        {
            if (_logger.IsErrorEnabled)
                _logger.Error(ex.Message, ex);
        }

        public static void Fatal(string message, params object[] args)
        {
            if (_logger.IsFatalEnabled)
                _logger.Fatal(FormatMessage(message), args);
        }
        public static void Fatal(Exception ex, string message, params object[] args)
        {
            if (_logger.IsFatalEnabled)
                _logger.Fatal(string.Format(FormatMessage(message), args), ex);
        }
        public static void Fatal(Exception ex)
        {
            if (_logger.IsFatalEnabled)
                _logger.Fatal(ex.Message, ex);
        }

        /// <summary>
        /// Trace l'entrée dans la méthode accompagné de la valeur des paramètres si disponibles
        /// </summary>
        /// <param name="args">Tous les paramètres de la fonction dans l'ordre. Insérer null pour ne pas tracer une valeur sensible.</param>
        /// <example>
        /// void FunctionName (int param1, string paramSensible, TypeComplex param3)
        /// {
        ///     DebugMethodIn(param1,null,param3.ToFriendlyString());
        ///     ...
        /// }
        /// </example>
        /// <date>22/03/2013</date>
        public static void DebugMethodIn(params object[] args)
        {
            if (!_logger.IsDebugEnabled) return;
                

            var methodName = "Unknown Method";
            var parameters = new List<KeyValuePair<string,string>>();

            try
            {
                var stackFrame = (new System.Diagnostics.StackTrace()).GetFrame(1);
                var methodInfo = stackFrame.GetMethod();

                methodName = string.Format("{0}.{1}", methodInfo.ReflectedType.FullName, methodInfo.Name);

                int paramIndex = 0;
                foreach (ParameterInfo pInfo in methodInfo.GetParameters())
                {
                    var paramValue = paramIndex < args.Length ? args[paramIndex] : null;
                    parameters.Add(new KeyValuePair<string, string>(pInfo.ParameterType.Name + " " + pInfo.Name, paramValue == null ? "null" : paramValue.ToString()));
                    paramIndex++;
                }
            }
            catch{/*ignore exception in log statment*/}

            _logger.Debug(string.Format("Entering - {0} ({1})",methodName,string.Join(", ", parameters.Select(kvp => string.Format("[{0}: {1}]",kvp.Key,kvp.Value)))));
        }

        /// <summary>
        /// Trace la sortie d'une méthode accompagnée de la valeur de sortie
        /// </summary>
        /// <example>
        /// void FunctionName ()
        /// {
        ///     TypeComplex;
        ///     ...
        ///     DebugMethodOut();
        /// }
        /// </example>
        /// <date>22/03/2013</date>
        public static void DebugMethodOut()
        {
            if (!_logger.IsDebugEnabled) return;
            DebugMethodOutFrame2(null);
        }

        /// <summary>
        /// Trace la sortie d'une méthode accompagnée de la valeur de sortie
        /// </summary>
        /// <param name="result">La valeur de retour</param>
        /// <example>
        /// TypeComplex FunctionName ()
        /// {
        ///     TypeComplex result;
        ///     ...
        ///     DebugMethodOut(param3.ToFriendlyString());
        /// }
        /// </example>
        /// <date>22/03/2013</date>
        public static void DebugMethodOut(object result)
        {
            if (!_logger.IsDebugEnabled) return; 
            DebugMethodOutFrame2(result);
        }

        /// <summary>
        /// Trace la sortie d'une méthode accompagnée de la valeur de sortie
        /// </summary>
        /// <param name="result">La valeur de retour</param>
        /// <example>
        /// TypeComplex FunctionName ()
        /// {
        ///     TypeComplex result;
        ///     ...
        ///     DebugMethodOut(param3.ToFriendlyString());
        /// }
        /// </example>
        /// <date>22/03/2013</date>
        private static void DebugMethodOutFrame2(object result)
        {
            var methodName = "Unknown Method";
            var resultText = "Unknown";
            var parameters = new List<KeyValuePair<string, string>>();

            try
            {
                var stackFrame = (new System.Diagnostics.StackTrace()).GetFrame(2);
                var methodInfo = stackFrame.GetMethod() as MethodInfo;

                methodName = string.Format("{0}.{1}", methodInfo.ReflectedType.FullName, methodInfo.Name);

                resultText = methodInfo.ReturnType == typeof (void) ? "void" : string.Format("[{0}: {1}]", methodInfo.ReturnType.FullName, result);
            }
            catch {/*ignore exception in log statment*/}

            _logger.Debug(string.Format("Leaving - {0} - Result: {1}", methodName, resultText));
        }
        #endregion


        #region Private Methods
        /// <summary>
        /// Ajoute des informations additionnelles au message telle que le nom de la méthode appelante
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string FormatMessage(string message)
        {
            try
            {
                var stackFrame = (new System.Diagnostics.StackTrace()).GetFrame(2);
                var methodInfo = stackFrame.GetMethod();
                return string.Format("{0}.{1} - {2}",methodInfo.ReflectedType.FullName, methodInfo.Name, message);
            }
            catch (Exception)
            {
                return message;
            }
        }
        #endregion
    }
}
