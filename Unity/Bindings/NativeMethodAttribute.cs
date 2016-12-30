﻿namespace Unity.Bindings
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Method)]
    internal class NativeMethodAttribute : Attribute
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <IsFreeFunction>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <IsThreadSafe>k__BackingField;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private string <Name>k__BackingField;

        public bool IsFreeFunction { get; set; }

        public bool IsThreadSafe { get; set; }

        public string Name { get; set; }
    }
}
