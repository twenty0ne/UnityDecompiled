﻿namespace Unity.IL2CPP
{
    using Mono.Cecil;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text;
    using Unity.IL2CPP.ILPreProcessor;
    using Unity.IL2CPP.IoC;
    using Unity.IL2CPP.IoCServices;
    using Unity.IL2CPP.Marshaling;
    using Unity.IL2CPP.Marshaling.MarshalInfoWriters;

    public class ComInterfaceWriter
    {
        private readonly CppCodeWriter _writer;
        [Inject]
        public static INamingService Naming;

        public ComInterfaceWriter(CppCodeWriter writer)
        {
            this._writer = writer;
        }

        internal static string BuildMethodParameterList(MethodReference interopMethod, MethodReference interfaceMethod, Unity.IL2CPP.ILPreProcessor.TypeResolver typeResolver, MarshalType marshalType, bool includeTypeNames)
        {
            List<string> elements = new List<string>();
            int num = 0;
            foreach (ParameterDefinition definition in interopMethod.Parameters)
            {
                MarshalInfo marshalInfo = interfaceMethod.Parameters[num].MarshalInfo;
                DefaultMarshalInfoWriter writer = MarshalDataCollector.MarshalInfoWriterFor(typeResolver.Resolve(definition.ParameterType), marshalType, marshalInfo, true, false, false, null);
                foreach (MarshaledType type in writer.MarshaledTypes)
                {
                    elements.Add(string.Format(!includeTypeNames ? "{1}" : "{0} {1}", type.DecoratedName, Naming.ForParameterName(definition) + type.VariableName));
                }
                num++;
            }
            TypeReference reference2 = typeResolver.Resolve(interopMethod.ReturnType);
            if (reference2.MetadataType != MetadataType.Void)
            {
                MarshalInfo info2 = interfaceMethod.MethodReturnType.MarshalInfo;
                MarshaledType[] marshaledTypes = MarshalDataCollector.MarshalInfoWriterFor(reference2, marshalType, info2, true, false, false, null).MarshaledTypes;
                for (int i = 0; i < (marshaledTypes.Length - 1); i++)
                {
                    elements.Add(string.Format(!includeTypeNames ? "{1}" : "{0}* {1}", marshaledTypes[i].DecoratedName, Naming.ForComInterfaceReturnParameterName() + marshaledTypes[i].VariableName));
                }
                elements.Add(string.Format(!includeTypeNames ? "{1}" : "{0}* {1}", marshaledTypes[marshaledTypes.Length - 1].DecoratedName, Naming.ForComInterfaceReturnParameterName()));
            }
            return elements.AggregateWithComma();
        }

        public static string GetSignature(MethodReference method, MethodReference interfaceMethod, Unity.IL2CPP.ILPreProcessor.TypeResolver typeResolver, string typeName = null)
        {
            StringBuilder builder = new StringBuilder();
            MarshalType marshalType = !interfaceMethod.DeclaringType.Resolve().IsWindowsRuntime ? MarshalType.COM : MarshalType.WindowsRuntime;
            if (string.IsNullOrEmpty(typeName))
            {
                builder.Append("virtual il2cpp_hresult_t STDCALL ");
            }
            else
            {
                builder.Append("il2cpp_hresult_t ");
                builder.Append(typeName);
                builder.Append("::");
            }
            builder.Append(Naming.ForMethodNameOnly(interfaceMethod));
            builder.Append('(');
            builder.Append(BuildMethodParameterList(method, interfaceMethod, typeResolver, marshalType, true));
            builder.Append(')');
            return builder.ToString();
        }

        public void WriteComInterfaceFor(TypeReference type)
        {
            this._writer.WriteCommentedLine(type.FullName);
            this.WriteForwardDeclarations(type);
            string str = !type.Resolve().IsWindowsRuntime ? "Il2CppIUnknown" : "Il2CppIInspectable";
            object[] args = new object[] { Naming.ForTypeNameOnly(type), str };
            this._writer.WriteLine("struct NOVTABLE {0} : {1}", args);
            using (new BlockWriter(this._writer, true))
            {
                this._writer.WriteStatement("static const Il2CppGuid IID");
                Unity.IL2CPP.ILPreProcessor.TypeResolver typeResolver = Unity.IL2CPP.ILPreProcessor.TypeResolver.For(type);
                foreach (MethodDefinition definition in type.Resolve().Methods)
                {
                    MethodReference method = typeResolver.Resolve(definition);
                    this._writer.Write(GetSignature(method, method, typeResolver, null));
                    this._writer.WriteLine(" = 0;");
                }
            }
        }

        private void WriteForwardDeclarations(TypeReference type)
        {
            Unity.IL2CPP.ILPreProcessor.TypeResolver resolver = Unity.IL2CPP.ILPreProcessor.TypeResolver.For(type);
            MarshalType marshalType = !type.Resolve().IsWindowsRuntime ? MarshalType.COM : MarshalType.WindowsRuntime;
            foreach (MethodDefinition definition in type.Resolve().Methods)
            {
                foreach (ParameterDefinition definition2 in definition.Parameters)
                {
                    MarshalDataCollector.MarshalInfoWriterFor(resolver.Resolve(definition2.ParameterType), marshalType, definition2.MarshalInfo, true, false, false, null).WriteMarshaledTypeForwardDeclaration(this._writer);
                }
                if (definition.ReturnType.MetadataType != MetadataType.Void)
                {
                    MarshalDataCollector.MarshalInfoWriterFor(resolver.Resolve(definition.ReturnType), marshalType, definition.MethodReturnType.MarshalInfo, true, false, false, null).WriteMarshaledTypeForwardDeclaration(this._writer);
                }
            }
        }
    }
}

