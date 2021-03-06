﻿namespace Unity.CecilTools.Extensions
{
    using Mono.Cecil;
    using System;
    using System.Runtime.CompilerServices;

    public static class ResolutionExtensions
    {
        [CompilerGenerated]
        private static Func<TypeReference, TypeDefinition> <>f__am$cache0;
        [CompilerGenerated]
        private static Func<MethodReference, MethodDefinition> <>f__am$cache1;

        public static MethodDefinition CheckedResolve(this MethodReference method)
        {
            if (<>f__am$cache1 == null)
            {
                <>f__am$cache1 = reference => reference.Resolve();
            }
            return Resolve<MethodReference, MethodDefinition>(method, <>f__am$cache1);
        }

        public static TypeDefinition CheckedResolve(this TypeReference type)
        {
            if (<>f__am$cache0 == null)
            {
                <>f__am$cache0 = reference => reference.Resolve();
            }
            return Resolve<TypeReference, TypeDefinition>(type, <>f__am$cache0);
        }

        private static TDefinition Resolve<TReference, TDefinition>(TReference reference, Func<TReference, TDefinition> resolve) where TReference: MemberReference where TDefinition: class, IMemberDefinition
        {
            if (reference.Module == null)
            {
                throw new ResolutionException(reference);
            }
            TDefinition local = resolve(reference);
            if (local == null)
            {
                throw new ResolutionException(reference);
            }
            return local;
        }
    }
}

