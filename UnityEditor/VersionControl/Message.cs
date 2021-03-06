﻿namespace UnityEditor.VersionControl
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    /// <summary>
    /// <para>Messages from the version control system.</para>
    /// </summary>
    public sealed class Message
    {
        private IntPtr m_thisDummy;

        internal Message()
        {
        }

        [MethodImpl(MethodImplOptions.InternalCall), ThreadAndSerializationSafe]
        public extern void Dispose();
        ~Message()
        {
            this.Dispose();
        }

        private static void Info(string message)
        {
            Debug.Log("Version control:\n" + message);
        }

        /// <summary>
        /// <para>Write the message to the console.</para>
        /// </summary>
        public void Show()
        {
            Info(this.message);
        }

        /// <summary>
        /// <para>The message text.</para>
        /// </summary>
        [ThreadAndSerializationSafe]
        public string message { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        /// <summary>
        /// <para>The severity of the message.</para>
        /// </summary>
        [ThreadAndSerializationSafe]
        public Severity severity { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        /// <summary>
        /// <para>Severity of a version control message.</para>
        /// </summary>
        [Flags]
        public enum Severity
        {
            Data,
            Verbose,
            Info,
            Warning,
            Error
        }
    }
}

