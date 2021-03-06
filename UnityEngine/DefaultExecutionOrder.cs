﻿namespace UnityEngine
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine.Scripting;

    [AttributeUsage(AttributeTargets.Class), UsedByNativeCode]
    public class DefaultExecutionOrder : Attribute
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private int <order>k__BackingField;

        public DefaultExecutionOrder(int order)
        {
            this.order = order;
        }

        public int order { get; private set; }
    }
}

