﻿namespace JetBrains.Annotations
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple=true, Inherited=true)]
    public sealed class ContractAnnotationAttribute : Attribute
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private string <Contract>k__BackingField;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private bool <ForceFullStates>k__BackingField;

        public ContractAnnotationAttribute([NotNull] string contract) : this(contract, false)
        {
        }

        public ContractAnnotationAttribute([NotNull] string contract, bool forceFullStates)
        {
            this.Contract = contract;
            this.ForceFullStates = forceFullStates;
        }

        public string Contract { get; private set; }

        public bool ForceFullStates { get; private set; }
    }
}

