﻿namespace JetBrains.Annotations
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple=false, Inherited=true)]
    public sealed class NotifyPropertyChangedInvocatorAttribute : Attribute
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private string <ParameterName>k__BackingField;

        public NotifyPropertyChangedInvocatorAttribute()
        {
        }

        public NotifyPropertyChangedInvocatorAttribute(string parameterName)
        {
            this.ParameterName = parameterName;
        }

        public string ParameterName { get; private set; }
    }
}

