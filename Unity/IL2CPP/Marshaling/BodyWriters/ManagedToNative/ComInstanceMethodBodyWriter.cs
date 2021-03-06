﻿namespace Unity.IL2CPP.Marshaling.BodyWriters.ManagedToNative
{
    using Mono.Cecil;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Unity.IL2CPP;

    internal class ComInstanceMethodBodyWriter : ComMethodBodyWriter
    {
        public ComInstanceMethodBodyWriter(MethodReference method) : base(method, GetInterfaceMethod(method))
        {
        }

        private static MethodReference GetInterfaceMethod(MethodReference method)
        {
            <GetInterfaceMethod>c__AnonStorey0 storey = new <GetInterfaceMethod>c__AnonStorey0();
            TypeReference declaringType = method.DeclaringType;
            if (declaringType.IsInterface())
            {
                return method;
            }
            storey.staticInterfaces = declaringType.GetAllFactoryTypes().ToArray<TypeReference>();
            IEnumerable<TypeReference> candidateInterfaces = declaringType.GetInterfaces().Where<TypeReference>(new Func<TypeReference, bool>(storey.<>m__0));
            MethodReference overridenInterfaceMethod = method.GetOverridenInterfaceMethod(candidateInterfaces);
            if (overridenInterfaceMethod == null)
            {
                throw new InvalidOperationException($"Could not find overriden method for {method.FullName}");
            }
            return overridenInterfaceMethod;
        }

        [CompilerGenerated]
        private sealed class <GetInterfaceMethod>c__AnonStorey0
        {
            internal TypeReference[] staticInterfaces;

            internal bool <>m__0(TypeReference iface)
            {
                <GetInterfaceMethod>c__AnonStorey1 storey = new <GetInterfaceMethod>c__AnonStorey1 {
                    <>f__ref$0 = this,
                    iface = iface
                };
                return !this.staticInterfaces.Any<TypeReference>(new Func<TypeReference, bool>(storey.<>m__0));
            }

            private sealed class <GetInterfaceMethod>c__AnonStorey1
            {
                internal ComInstanceMethodBodyWriter.<GetInterfaceMethod>c__AnonStorey0 <>f__ref$0;
                internal TypeReference iface;

                internal bool <>m__0(TypeReference nonInstanceInterface) => 
                    (this.iface == nonInstanceInterface);
            }
        }
    }
}

