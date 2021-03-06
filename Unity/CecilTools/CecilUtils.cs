﻿namespace Unity.CecilTools
{
    using Mono.Cecil;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using Unity.CecilTools.Extensions;

    public static class CecilUtils
    {
        [CompilerGenerated]
        private static Func<TypeDefinition, IEnumerable<InterfaceImplementation>> <>f__am$cache0;
        [CompilerGenerated]
        private static Func<InterfaceImplementation, TypeDefinition> <>f__am$cache1;

        public static IEnumerable<TypeDefinition> AllInterfacesImplementedBy(TypeDefinition typeDefinition)
        {
            if (<>f__am$cache0 == null)
            {
                <>f__am$cache0 = t => t.Interfaces;
            }
            if (<>f__am$cache1 == null)
            {
                <>f__am$cache1 = i => i.InterfaceType.CheckedResolve();
            }
            return Enumerable.Select<InterfaceImplementation, TypeDefinition>(Enumerable.SelectMany<TypeDefinition, InterfaceImplementation>(TypeAndBaseTypesOf(typeDefinition), <>f__am$cache0), <>f__am$cache1).Distinct<TypeDefinition>();
        }

        public static IEnumerable<TypeDefinition> BaseTypesOf(TypeReference typeReference) => 
            TypeAndBaseTypesOf(typeReference).Skip<TypeDefinition>(1);

        public static TypeReference ElementTypeOfCollection(TypeReference type)
        {
            ArrayType type2 = type as ArrayType;
            if (type2 != null)
            {
                return type2.ElementType;
            }
            if (!IsGenericList(type))
            {
                throw new ArgumentException();
            }
            return ((GenericInstanceType) type).GenericArguments.Single<TypeReference>();
        }

        public static MethodDefinition FindInTypeExplicitImplementationFor(MethodDefinition interfaceMethod, TypeDefinition typeDefinition)
        {
            <FindInTypeExplicitImplementationFor>c__AnonStorey1 storey = new <FindInTypeExplicitImplementationFor>c__AnonStorey1 {
                interfaceMethod = interfaceMethod
            };
            return Enumerable.SingleOrDefault<MethodDefinition>(typeDefinition.Methods, new Func<MethodDefinition, bool>(storey.<>m__0));
        }

        public static bool IsGenericDictionary(TypeReference type)
        {
            if (type is GenericInstanceType)
            {
                type = ((GenericInstanceType) type).ElementType;
            }
            return ((type.Name == "Dictionary`2") && (type.SafeNamespace() == "System.Collections.Generic"));
        }

        public static bool IsGenericList(TypeReference type) => 
            ((type.Name == "List`1") && (type.SafeNamespace() == "System.Collections.Generic"));

        [DebuggerHidden]
        public static IEnumerable<TypeDefinition> TypeAndBaseTypesOf(TypeReference typeReference) => 
            new <TypeAndBaseTypesOf>c__Iterator0 { 
                typeReference = typeReference,
                <$>typeReference = typeReference,
                $PC = -2
            };

        [CompilerGenerated]
        private sealed class <FindInTypeExplicitImplementationFor>c__AnonStorey1
        {
            internal MethodDefinition interfaceMethod;

            internal bool <>m__0(MethodDefinition m) => 
                Enumerable.Any<MethodReference>(m.Overrides, (Func<MethodReference, bool>) (o => o.CheckedResolve().SameAs(this.interfaceMethod)));

            internal bool <>m__1(MethodReference o) => 
                o.CheckedResolve().SameAs(this.interfaceMethod);
        }

        [CompilerGenerated]
        private sealed class <TypeAndBaseTypesOf>c__Iterator0 : IEnumerable, IEnumerable<TypeDefinition>, IEnumerator, IDisposable, IEnumerator<TypeDefinition>
        {
            internal TypeDefinition $current;
            internal bool $disposing;
            internal int $PC;
            internal TypeReference <$>typeReference;
            internal TypeDefinition <typeDefinition>__0;
            internal TypeReference typeReference;

            [DebuggerHidden]
            public void Dispose()
            {
                this.$disposing = true;
                this.$PC = -1;
            }

            public bool MoveNext()
            {
                uint num = (uint) this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        break;

                    case 1:
                        this.typeReference = this.<typeDefinition>__0.BaseType;
                        break;

                    default:
                        goto Label_007D;
                }
                if (this.typeReference != null)
                {
                    this.<typeDefinition>__0 = this.typeReference.CheckedResolve();
                    this.$current = this.<typeDefinition>__0;
                    if (!this.$disposing)
                    {
                        this.$PC = 1;
                    }
                    return true;
                }
                this.$PC = -1;
            Label_007D:
                return false;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            IEnumerator<TypeDefinition> IEnumerable<TypeDefinition>.GetEnumerator()
            {
                if (Interlocked.CompareExchange(ref this.$PC, 0, -2) == -2)
                {
                    return this;
                }
                return new CecilUtils.<TypeAndBaseTypesOf>c__Iterator0 { typeReference = this.<$>typeReference };
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<Mono.Cecil.TypeDefinition>.GetEnumerator();

            TypeDefinition IEnumerator<TypeDefinition>.Current =>
                this.$current;

            object IEnumerator.Current =>
                this.$current;
        }
    }
}

