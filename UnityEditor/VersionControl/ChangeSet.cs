﻿namespace UnityEditor.VersionControl
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    /// <summary>
    /// <para>Wrapper around a changeset description and ID.</para>
    /// </summary>
    public sealed class ChangeSet
    {
        /// <summary>
        /// <para>The ID of  the default changeset.</para>
        /// </summary>
        public static string defaultID = "-1";
        private IntPtr m_thisDummy;

        public ChangeSet()
        {
            this.InternalCreate();
        }

        public ChangeSet(string description)
        {
            this.InternalCreateFromString(description);
        }

        public ChangeSet(ChangeSet other)
        {
            this.InternalCopyConstruct(other);
        }

        public ChangeSet(string description, string revision)
        {
            this.InternalCreateFromStringString(description, revision);
        }

        [MethodImpl(MethodImplOptions.InternalCall), ThreadAndSerializationSafe]
        public extern void Dispose();
        ~ChangeSet()
        {
            this.Dispose();
        }

        [MethodImpl(MethodImplOptions.InternalCall), ThreadAndSerializationSafe]
        private extern void InternalCopyConstruct(ChangeSet other);
        [MethodImpl(MethodImplOptions.InternalCall), ThreadAndSerializationSafe]
        private extern void InternalCreate();
        [MethodImpl(MethodImplOptions.InternalCall), ThreadAndSerializationSafe]
        private extern void InternalCreateFromString(string description);
        [MethodImpl(MethodImplOptions.InternalCall), ThreadAndSerializationSafe]
        private extern void InternalCreateFromStringString(string description, string changeSetID);

        /// <summary>
        /// <para>Description of a changeset.</para>
        /// </summary>
        [ThreadAndSerializationSafe]
        public string description { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        /// <summary>
        /// <para>Version control specific ID of a changeset.</para>
        /// </summary>
        [ThreadAndSerializationSafe]
        public string id { [MethodImpl(MethodImplOptions.InternalCall)] get; }
    }
}

