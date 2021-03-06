﻿namespace UnityEditor.VersionControl
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    /// <summary>
    /// <para>This class describes the.</para>
    /// </summary>
    public sealed class ConfigField
    {
        private string m_guid;
        private IntPtr m_thisDummy;

        internal ConfigField()
        {
        }

        [MethodImpl(MethodImplOptions.InternalCall), ThreadAndSerializationSafe]
        public extern void Dispose();
        ~ConfigField()
        {
            this.Dispose();
        }

        /// <summary>
        /// <para>Descrition of the configuration field.</para>
        /// </summary>
        public string description { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        /// <summary>
        /// <para>This is true if the configuration field is a password field.</para>
        /// </summary>
        public bool isPassword { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        /// <summary>
        /// <para>This is true if the configuration field is required for the version control plugin to function correctly.</para>
        /// </summary>
        public bool isRequired { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        /// <summary>
        /// <para>Label that is displayed next to the configuration field in the editor.</para>
        /// </summary>
        public string label { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        /// <summary>
        /// <para>Name of the configuration field.</para>
        /// </summary>
        public string name { [MethodImpl(MethodImplOptions.InternalCall)] get; }
    }
}

