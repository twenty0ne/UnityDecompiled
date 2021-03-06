﻿namespace Unity.IL2CPP.Portability
{
    using System;
    using System.Reflection;

    public static class DelegatePortable
    {
        public static Delegate CreateDelegatePortable(Type type, MethodInfo methodInfo) => 
            Delegate.CreateDelegate(type, methodInfo);
    }
}

