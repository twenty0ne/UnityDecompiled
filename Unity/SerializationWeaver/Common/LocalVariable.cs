﻿namespace Unity.SerializationWeaver.Common
{
    using Mono.Cecil;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class LocalVariable
    {
        private readonly int _index;
        private readonly Action _loadGenerator;
        private readonly Action _storeGenerator;
        private readonly TypeReference _type;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <IsUsed>k__BackingField;

        public LocalVariable(int index, TypeReference type, Action loadGenerator, Action storeGenerator)
        {
            this._index = index;
            this._type = type;
            this._loadGenerator = loadGenerator;
            this._storeGenerator = storeGenerator;
        }

        public void EmitLoad()
        {
            this.IsUsed = true;
            this._loadGenerator.Invoke();
        }

        public void EmitStore()
        {
            this.IsUsed = true;
            this._storeGenerator.Invoke();
        }

        public int Index
        {
            get
            {
                return this._index;
            }
        }

        public bool IsUsed { get; private set; }

        public TypeReference Type
        {
            get
            {
                return this._type;
            }
        }
    }
}
