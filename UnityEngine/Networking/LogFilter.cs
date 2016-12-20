﻿namespace UnityEngine.Networking
{
    using System;

    /// <summary>
    /// <para>FilterLog is a utility class that controls the level of logging generated by UNET clients and servers.</para>
    /// </summary>
    public class LogFilter
    {
        /// <summary>
        /// <para>The current logging level that UNET is running with.</para>
        /// </summary>
        public static FilterLevel current = FilterLevel.Info;
        /// <summary>
        /// <para>Setting LogFilter.currentLogLevel to this will enable verbose debug logging.</para>
        /// </summary>
        public const int Debug = 1;
        internal const int Developer = 0;
        /// <summary>
        /// <para>Setting LogFilter.currentLogLevel to this will error and above messages.</para>
        /// </summary>
        public const int Error = 4;
        public const int Fatal = 5;
        /// <summary>
        /// <para>Setting LogFilter.currentLogLevel to this will log only info and above messages. This is the default level.</para>
        /// </summary>
        public const int Info = 2;
        private static int s_CurrentLogLevel = 2;
        /// <summary>
        /// <para>Setting LogFilter.currentLogLevel to this will log wanring and above messages.</para>
        /// </summary>
        public const int Warn = 3;

        /// <summary>
        /// <para>The current logging level that UNET is running with.</para>
        /// </summary>
        public static int currentLogLevel
        {
            get
            {
                return s_CurrentLogLevel;
            }
            set
            {
                s_CurrentLogLevel = value;
            }
        }

        /// <summary>
        /// <para>Checks if debug logging is enabled.</para>
        /// </summary>
        public static bool logDebug
        {
            get
            {
                return (s_CurrentLogLevel <= 1);
            }
        }

        internal static bool logDev
        {
            get
            {
                return (s_CurrentLogLevel <= 0);
            }
        }

        /// <summary>
        /// <para>Checks if error logging is enabled.</para>
        /// </summary>
        public static bool logError
        {
            get
            {
                return (s_CurrentLogLevel <= 4);
            }
        }

        public static bool logFatal
        {
            get
            {
                return (s_CurrentLogLevel <= 5);
            }
        }

        /// <summary>
        /// <para>Checks if info level logging is enabled.</para>
        /// </summary>
        public static bool logInfo
        {
            get
            {
                return (s_CurrentLogLevel <= 2);
            }
        }

        /// <summary>
        /// <para>Checks if wanring level logging is enabled.</para>
        /// </summary>
        public static bool logWarn
        {
            get
            {
                return (s_CurrentLogLevel <= 3);
            }
        }

        /// <summary>
        /// <para>Control how verbose the network log messages are.</para>
        /// </summary>
        public enum FilterLevel
        {
            Developer,
            Debug,
            Info,
            Warn,
            Error,
            Fatal
        }
    }
}
