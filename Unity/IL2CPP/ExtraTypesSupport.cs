﻿namespace Unity.IL2CPP
{
    using Mono.Cecil;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using Unity.Cecil.Visitor;
    using Unity.IL2CPP.Common;
    using Unity.IL2CPP.GenericsCollection;

    [StructLayout(LayoutKind.Sequential)]
    public struct ExtraTypesSupport
    {
        private readonly GenericContextFreeVisitor _visitor;
        private readonly IEnumerable<AssemblyDefinition> _usedAssemblies;
        [CompilerGenerated]
        private static Func<string, string, string> <>f__am$cache0;
        [CompilerGenerated]
        private static Func<TypeDefinition, bool> <>f__am$cache1;
        public ExtraTypesSupport(InflatedCollectionCollector collectionCollector, IEnumerable<AssemblyDefinition> usedAssemblies)
        {
            this._usedAssemblies = usedAssemblies;
            this._visitor = new GenericContextFreeVisitor(collectionCollector);
        }

        public bool AddType(string typeName, TypeNameParseInfo typeNameInfo)
        {
            try
            {
                this.TypeReferenceFor(typeNameInfo);
                return true;
            }
            catch (TypeResolutionException)
            {
                return false;
            }
        }

        private TypeReference TypeReferenceFor(TypeNameParseInfo typeNameInfo)
        {
            TypeReference typeByName = this.GetTypeByName(CecilElementTypeNameFor(typeNameInfo), typeNameInfo.Assembly);
            if (typeByName == null)
            {
                throw new TypeResolutionException(typeNameInfo);
            }
            if (typeNameInfo.HasGenericArguments)
            {
                GenericInstanceType genericInstanceType = new GenericInstanceType(typeByName);
                foreach (TypeNameParseInfo info in typeNameInfo.TypeArguments)
                {
                    genericInstanceType.GenericArguments.Add(this.TypeReferenceFor(info));
                }
                Unity.Cecil.Visitor.Extensions.Accept(genericInstanceType, this._visitor);
                typeByName = genericInstanceType;
            }
            if (typeNameInfo.IsPointer)
            {
                throw new TypeResolutionException(typeNameInfo);
            }
            if (!typeNameInfo.IsArray)
            {
                return typeByName;
            }
            ArrayType type2 = new ArrayType(typeByName, typeNameInfo.Ranks[0]);
            for (int i = 1; i < typeNameInfo.Ranks.Length; i++)
            {
                type2 = new ArrayType(type2, typeNameInfo.Ranks[i]);
            }
            Unity.Cecil.Visitor.Extensions.Accept(type2, this._visitor);
            return type2;
        }

        private static string CecilElementTypeNameFor(TypeNameParseInfo typeNameInfo)
        {
            if (!typeNameInfo.IsNested)
            {
                return typeNameInfo.ElementTypeName;
            }
            string name = typeNameInfo.Name;
            if (!string.IsNullOrEmpty(typeNameInfo.Namespace))
            {
                name = typeNameInfo.Namespace + "." + name;
            }
            if (<>f__am$cache0 == null)
            {
                <>f__am$cache0 = new Func<string, string, string>(null, (IntPtr) <CecilElementTypeNameFor>m__0);
            }
            return Enumerable.Aggregate<string, string>(typeNameInfo.Nested, name, <>f__am$cache0);
        }

        private TypeReference GetTypeByName(string name, AssemblyNameParseInfo assembly)
        {
            <GetTypeByName>c__AnonStorey0 storey = new <GetTypeByName>c__AnonStorey0 {
                name = name,
                assembly = assembly
            };
            if ((storey.assembly == null) || string.IsNullOrEmpty(storey.assembly.Name))
            {
                if (<>f__am$cache1 == null)
                {
                    <>f__am$cache1 = new Func<TypeDefinition, bool>(null, (IntPtr) <GetTypeByName>m__1);
                }
                return Enumerable.FirstOrDefault<TypeDefinition>(Enumerable.Select<AssemblyDefinition, TypeDefinition>(this._usedAssemblies, new Func<AssemblyDefinition, TypeDefinition>(storey, (IntPtr) this.<>m__0)), <>f__am$cache1);
            }
            AssemblyDefinition definition = Enumerable.FirstOrDefault<AssemblyDefinition>(this._usedAssemblies, new Func<AssemblyDefinition, bool>(storey, (IntPtr) this.<>m__1));
            if (definition == null)
            {
                return null;
            }
            return definition.MainModule.GetType(storey.name);
        }
        [CompilerGenerated]
        private sealed class <GetTypeByName>c__AnonStorey0
        {
            internal AssemblyNameParseInfo assembly;
            internal string name;

            internal TypeDefinition <>m__0(AssemblyDefinition a)
            {
                return a.MainModule.GetType(this.name);
            }

            internal bool <>m__1(AssemblyDefinition a)
            {
                return (a.Name.Name == this.assembly.Name);
            }
        }
    }
}
