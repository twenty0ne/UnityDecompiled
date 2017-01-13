﻿namespace Unity.BindingsGenerator.Core.Attributes
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Struct)]
    internal class NativeStructAttribute : Attribute
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private NativeStructGenerateOption <GenerateMarshallingType>k__BackingField;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private string <Header>k__BackingField;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private string <Name>k__BackingField;

        public NativeStructGenerateOption GenerateMarshallingType { get; set; }

        public string Header { get; set; }

        public string Name { get; set; }
    }
}
